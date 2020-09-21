using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Main : MonoBehaviour
{
    private const float TWO_PI = 6.283185f;

    public GameObject o2;
    public GameObject co2;
    public GameObject n2;


    public Vector3 particleAmount = new Vector3(25, 10, 15);

    public float nitrogenCon = 0.7808f;
    public float oxygenCon = 0.2094f;

    public float startTemp = 1;

    private Vector3 roomSize = new Vector3(50f, 20f, 30f);

   

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
                    float randVal = UnityEngine.Random.value;
                    GameObject tmp;
                    if (randVal <= nitrogenCon)
                    {
                        tmp = Instantiate(n2);

                    }
                    else if (randVal <= nitrogenCon + oxygenCon)
                    {
                        tmp = Instantiate(o2);

                    }
                    else
                    {
                        tmp = Instantiate(co2);

                    }
                    tmp.transform.position = new Vector3(x * spacing.x, y * spacing.y, z * spacing.z);

                    Vector3 randRotation = new Vector3(UnityEngine.Random.value * TWO_PI, UnityEngine.Random.value * TWO_PI, 0);

                    tmp.GetComponent<Rigidbody>().velocity = new Vector3((float)Math.Cos(randRotation.x), (float)Math.Sin(randRotation.x), (float)Math.Cos(randRotation.y)) * startTemp;
                }
            }
        }
    }

    void Update()
    {

    }
}
