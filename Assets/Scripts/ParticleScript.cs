using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public float maxO2 = 0.998f;
    public float minO2 = 0.6f;

    public float o2Con = 0.0f;


    public float startAlpha = 0.2f;
    public float endAlpha = 0.8f;

    public Material myMaterial;



    public void SetO2( float o2 )
    {
        o2Con = o2;

        float koef = (o2Con - minO2) / (maxO2 - minO2);

        myMaterial.color = new Color(1.0f - koef, 0.0f, koef, endAlpha - (endAlpha - startAlpha) * koef);
    }

    private void Awake()
    {
        myMaterial = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
