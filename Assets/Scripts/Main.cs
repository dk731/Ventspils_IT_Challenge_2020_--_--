using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Main : MonoBehaviour
{
    private const float TWO_PI = 6.283185f;

    public GameObject particle;

    public Vector3 particleAmount = new Vector3(25, 10, 15);

    public float startTemp = 1.0f;

    private Vector3 roomSize = new Vector3(40f, 20f, 30f);

    public float minStartO2 = 0.9f;
    private float maxStartO2 = 0.998f;


    void Start()
    {
        Vector3 spacing = new Vector3();
        spacing.x = roomSize.x / particleAmount.x;
        spacing.y = roomSize.y / particleAmount.y;
        spacing.z = roomSize.z / -particleAmount.z;

        for (int z = 1; z < particleAmount.z; z++)
        {
            for (int y = 1; y < particleAmount.y; y++)
            {
                for (int x = 1; x < particleAmount.x; x++)
                {
                    GameObject tmp = Instantiate(particle);
                    tmp.GetComponent<ParticleScript>().SetO2(minStartO2 + UnityEngine.Random.value * (maxStartO2 - minStartO2));

                    tmp.transform.position = new Vector3(x * spacing.x, y * spacing.y, z * spacing.z);

                    Vector3 randRotation = new Vector3(UnityEngine.Random.value * TWO_PI, UnityEngine.Random.value * TWO_PI, 0);
                    float tmpang = (float)Math.Cos(randRotation.y);
                    tmp.GetComponent<Rigidbody>().velocity = new Vector3(tmpang * (float)Math.Cos(randRotation.x), tmpang * (float)Math.Sin(randRotation.x), (float)Math.Sin(randRotation.y)) * startTemp;
                }
            }
        }
    }

    void Update()
    {
       
    }
}
