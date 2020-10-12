using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsScript : MonoBehaviour
{
    public float outsideTemp;
    public float outsideOxygen;

    public float heatExchange;

    public float oxygenExchange;

    void Start()
    {
        outsideTemp = InitialValues.outsideTemp;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (ButtonFunctions.windowRunning)
        {
            Rigidbody tmpr = collision.gameObject.GetComponent<Rigidbody>();
            ParticleScript tmpp = collision.gameObject.GetComponent<ParticleScript>();
            float tempDelta = outsideTemp - tmpr.velocity.magnitude;
            float oxygendelta = outsideOxygen - tmpp.o2Con;

            tmpr.velocity += tmpr.velocity.normalized * (tempDelta * heatExchange);
            tmpp.SetO2(tmpp.o2Con + oxygendelta * oxygenExchange);
        }
        
        
    }
}
