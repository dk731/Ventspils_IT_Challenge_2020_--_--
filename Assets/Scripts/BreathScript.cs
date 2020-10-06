using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathScript : MonoBehaviour
{

    public List<GameObject> particlesList = new List<GameObject>();
    private Dictionary<GameObject, Dictionary<string, float>> breathDict = new Dictionary<GameObject, Dictionary<string, float>>();

    private List<GameObject> unCollidables = new List<GameObject>();

    public float breathInLength = 1f;
    public float breathOutLength = 1f;
    public float breathHoldDuration = 2.0f;
    public float breathsDelay = 0.3f;

    private float lastBreaath = -3.0f;
    private float breathProgress = -1.0f;

    public int particlesOut = -1;

    public float o2absord = 0.1f;

    public float delayBetweenOutParticles = 0.1f;

    private float timeBetweenParticlesOut = 10.0f;

    public float bodyTemp;

    public float tempExchange;

    private bool firstTime = true;
    private int index;

    public string cycleName = "";

    public float collisionActiveDistance;

    private Vector3 relativePos;

    private void Start()
    {
        relativePos = (transform.GetChild(0).position - transform.position).normalized;
    }

    void OnTriggerEnter(Collider collision)
    {

        if (breathProgress < 0.0f && collision.gameObject.tag.Equals("Particle"))
            particlesList.Add(collision.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (breathProgress >= 0.0f)
            return;

        particlesList.RemoveAll(p => p == other.gameObject);
    }

    void Update()
    {
        unCollidables.RemoveAll(p => {
            bool check = (p.transform.position - transform.position).magnitude > collisionActiveDistance;
            if (check)
                Physics.IgnoreCollision(GetComponent<SphereCollider>(), p.GetComponent<SphereCollider>(), false);
            return check;
            }
        );
       
        

        if (lastBreaath >= breathsDelay)
        {
            if (particlesList.Count == 0)
                return;

            lastBreaath = 0.0f;
            breathProgress = 0.0f;

            breathDict.Clear();

            foreach (GameObject o in particlesList)
            {
                breathDict[o] = new Dictionary<string, float>() {
                    { "startTemp", o.GetComponent<Rigidbody>().velocity.magnitude },
                    { "inSpeed", (Vector3.Distance(o.transform.position, transform.position)) / breathInLength },
                };
                o.GetComponent<SphereCollider>().enabled = false;
                o.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            firstTime = true;
            index = 0;
            timeBetweenParticlesOut = 10.0f;
        }

        if (breathProgress >= 0.0f)
        {
            if (breathProgress <= breathInLength) // In
            {
                particlesList.ForEach(p => p.transform.position = Vector3.MoveTowards(p.transform.position, transform.position, breathDict[p]["inSpeed"] * Time.deltaTime));
            }
            else if (particlesList.Count > index && breathProgress > breathHoldDuration + breathInLength)
            {
                if (firstTime)
                {
                    firstTime = false;
                    particlesList.ForEach(p => p.SetActive(false));
                }
                    
                if (timeBetweenParticlesOut >= delayBetweenOutParticles)
                {
                    Physics.IgnoreCollision(GetComponent<SphereCollider>(), particlesList[index].GetComponent<SphereCollider>(), true);
                    particlesList[index].SetActive(true);
                    particlesList[index].GetComponent<Rigidbody>().velocity = relativePos * (breathDict[particlesList[index]]["startTemp"] - (breathDict[particlesList[index]]["startTemp"] - bodyTemp) * tempExchange);
                    //Debug.Log((bodyTemp + (breathDict[particlesList[index]]["startTemp"] - bodyTemp) * tempExchange) + "    " + breathDict[particlesList[index]]["startTemp"]);
                    particlesList[index].GetComponent<SphereCollider>().enabled = true;
                    ParticleScript tmpPs = particlesList[index].GetComponent<ParticleScript>();
                    tmpPs.SetO2(tmpPs.o2Con - tmpPs.o2Con * o2absord);
                    timeBetweenParticlesOut = 0;
                    index++;
                    
                }
                else
                {
                    timeBetweenParticlesOut += Time.deltaTime;
                }
                
            }
            else if (particlesList.Count == index)
            {
                unCollidables = new List<GameObject>(particlesList);
                particlesList.Clear();
                breathProgress = -1000.0f;
                lastBreaath = 0.0f;
            }
           
            breathProgress += Time.deltaTime;
           
        }


        if (breathProgress < 0.0f)
            lastBreaath += Time.deltaTime;
    }
}
