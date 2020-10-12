using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleScript : MonoBehaviour
{
    public float maxO2 = 0.998f;
    public float minO2 = 0.2f;

    public float o2Con = 0.0f;


    public float startAlpha = 0.1f;
    public float endAlpha = 0.6f;

    public Material myMaterial;

    private Rigidbody myRigidBody;

    private float inSpeedSum;

    public void SetO2( float o2 )
    {

        o2Con = o2 > maxO2 ? maxO2 : o2;
        o2Con = o2Con < minO2 ? minO2 : o2Con;

        float koef = (o2Con - minO2) / (maxO2 - minO2);
        myRigidBody = GetComponent<Rigidbody>();
        myMaterial.color = new Color(1.0f - koef, 0.0f, koef, endAlpha - (endAlpha - startAlpha) * koef);
    }

    private void Awake()
    {
        myMaterial = GetComponent<MeshRenderer>().materials[0];
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Particle")
            inSpeedSum = myRigidBody.velocity.magnitude + collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        else
            inSpeedSum = myRigidBody.velocity.magnitude;
        
    }

    private void OnCollisionExit(Collision collision)
    {
        float velocityDelata;
        if (collision.gameObject.tag == "Particle")
            velocityDelata = Math.Abs(inSpeedSum - (myRigidBody.velocity.magnitude + collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude)) / 2.0f;
        else
            velocityDelata = Math.Abs(inSpeedSum - myRigidBody.velocity.magnitude);
        UnityEngine.Debug.Log(velocityDelata);
        myRigidBody.velocity += myRigidBody.velocity.normalized * velocityDelata;
    }
    */
}
