using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatorScript : MonoBehaviour
{

    public float heatMult;

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().velocity *= heatMult;
    }
}
