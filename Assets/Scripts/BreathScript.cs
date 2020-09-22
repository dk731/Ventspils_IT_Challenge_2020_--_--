using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathScript : MonoBehaviour
{

    public List<GameObject> particlesList = new List<GameObject>();
    private Dictionary<GameObject, Dictionary<string, float>> breathDict = new Dictionary<GameObject, Dictionary<string, float>>();

    public float breathInLength = 1f;
    public float breathOutLength = 1f;
    public float breathHoldDuration = 2.0f;
    public float breathsDelay = 0.3f;

    private float lastBreaath = 0.0f;
    private float breathProgress = -1.0f;

    public float breathOutSpeed = 2.0f;
    public int particlesOut = -1;

    public float o2absord = 0.1f;
    void OnTriggerEnter(Collider collision)
    {

        if (breathProgress < 0.0f && collision.gameObject.tag.Equals("Particle"))
            particlesList.Add(collision.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (breathProgress < 0.0f)
            return;

        particlesList.RemoveAll(p => p == other.gameObject);
    }

    void Update()
    {
        if (lastBreaath >= breathsDelay)
        {
            lastBreaath = 0.0f;
            breathProgress = 0.0f;

            breathDict.Clear();

            foreach (GameObject o in particlesList)
            {
                breathDict[o] = new Dictionary<string, float>() {
                    { "startTemp", o.GetComponent<Rigidbody>().velocity.magnitude },
                    { "inSpeed", (Vector3.Distance(o.transform.position, transform.position)) / breathInLength * 1.5f},
                };
                o.GetComponent<SphereCollider>().enabled = false;
            }
        }

        if (breathProgress >= 0.0f)
        {
            if (breathProgress <= breathInLength) // In
            {
                particlesList.ForEach(p => p.transform.position = Vector3.MoveTowards(p.transform.position, transform.position, breathDict[p]["inSpeed"] * Time.deltaTime));
            }
            else if (particlesList.Count > 0 && breathProgress > breathHoldDuration + breathInLength)
            {
                particlesList[0].transform.position = transform.position + new Vector3(0.0f, 0.0f, -1.0f); 
                particlesList[0].GetComponent<Rigidbody>().velocity = Vector3.left * breathOutSpeed;
                particlesList[0].GetComponent<SphereCollider>().enabled = true;
                ParticleScript tmpPs = particlesList[0].GetComponent<ParticleScript>();
                tmpPs.SetO2(tmpPs.o2Con - tmpPs.o2Con * o2absord);

                particlesList.RemoveAt(0);
            }
            else if (particlesList.Count == 0)
            {
                breathProgress = -1.0f;
                lastBreaath = 0.0f;
                particlesList.Clear();
            }

            breathProgress += Time.deltaTime;
        }


        if (breathProgress < 0.0f)
            lastBreaath += Time.deltaTime;
    }
}
