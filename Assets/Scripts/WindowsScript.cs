using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsScript : MonoBehaviour
{
    public float outsideTemp;
    public float outsideOxygen;

    public float heatExchange;

    public float oxygenExchange;

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody tmpr = collision.gameObject.GetComponent<Rigidbody>();
        ParticleScript tmpp = collision.gameObject.GetComponent<ParticleScript>();
        float tempDelta = outsideTemp - tmpr.velocity.magnitude;
        float oxygendelta = outsideOxygen - tmpp.o2Con;

        tmpr.velocity += tmpr.velocity.normalized * (tempDelta * heatExchange);
        Debug.Log(tmpp.o2Con + "   " + oxygendelta * oxygenExchange);
        tmpp.SetO2(tmpp.o2Con + oxygendelta * oxygenExchange);
        
    }
}
