using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatorScript : MonoBehaviour
{

    public float temperature;

    void Start()
    {
        temperature = InitialValues.radiatorTemp;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (ButtonFunctions.radiatorRunning)
        {
            Vector3 tmp = collision.gameObject.GetComponent<Rigidbody>().velocity;
            collision.gameObject.GetComponent<Rigidbody>().velocity = tmp.normalized * (temperature - tmp.magnitude) * 0.5f;
        }
        
    }
}
