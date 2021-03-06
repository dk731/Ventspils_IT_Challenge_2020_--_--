﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    private const float TWO_PI = 6.283185f;

    public GameObject particle;

    public Vector3 particleAmount = new Vector3(25, 10, 15);

    public float startTemp = 1.0f;

    private Vector3 roomSize = new Vector3(40f, 20f, 30f);

    public float minStartO2 = 0.9f;
    private float maxStartO2 = 0.998f;

    private Vector2 prevMousePos;

    public float rotationSpeed = 1.0f;

    private List<GameObject> particlesList = new List<GameObject>();

    public ChartScript o2Chart;
    public ChartScript temperatureChart;

    public Transform rotateAround;

    public bool isVentilated;
    public float ventilationIntence;

    public Text o2ConText;
    public Text tempText;

    public GameObject headObj;

    private void Awake()
    {
        startTemp = InitialValues.startParticleTemp;
        minStartO2 = InitialValues.o2Con;
    }


    public static float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        float fromAbs = from - fromMin;
        float fromMaxAbs = fromMax - fromMin;

        float normal = fromAbs / fromMaxAbs;

        float toMaxAbs = toMax - toMin;
        float toAbs = toMaxAbs * normal;

        float to = toAbs + toMin;

        return to;
    }


    void Start()
    {
        // Charts
        o2Chart.chartLines[Color.blue] = new List<Vector2>();
        o2Chart.chartLines[Color.red] = new List<Vector2>();

        temperatureChart.chartLines[Color.red] = new List<Vector2>();
        ///


        transform.RotateAround(rotateAround.position, Vector3.up, 45);

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
                    particlesList.Add(tmp);
                }
            }
        }
        prevMousePos = Input.mousePosition;

        Vector3 firsteHeadpos = new Vector3(34, 7, -6);
        Vector3 headOffset = new Vector3(-9, 0, -6);
        int count = 0;
        for (int x = 0; x < 4; x++)
        {
            for (int z = 0; z < 4; z++)
            {

                GameObject tmp = Instantiate(headObj);

                tmp.transform.position = firsteHeadpos + new Vector3(headOffset.x * x, UnityEngine.Random.value * 2 - 1, headOffset.z * z);

                count++;
                if (count >= InitialValues.headsAmount)
                    return;
            }
        }

    }

    void Update()
    {

        if (Input.GetAxis("Horizontal") != 0)
            transform.RotateAround(rotateAround.position, Vector3.up, -Input.GetAxis("Horizontal") * rotationSpeed);

        float avgO2 = 0;
        float avgTemp = 0;

        foreach (var p in particlesList)
        {
            ParticleScript ps = p.GetComponent<ParticleScript>();

            if (isVentilated)
                ps.SetO2(ps.o2Con + (ventilationIntence * Time.deltaTime));

            avgO2 += p.GetComponent<ParticleScript>().o2Con;
            avgTemp += p.GetComponent<Rigidbody>().velocity.magnitude;

        }
        
        avgO2 /= (float)particlesList.Count;
        avgTemp /= (float)particlesList.Count;

        o2ConText.text = (avgO2 * 100f).ToString("0.00") + " %";
        tempText.text = (avgTemp * 10f).ToString("0.00") + " C°";

        o2Chart.chartLines[Color.blue].Add(new Vector2(Time.timeSinceLevelLoad, avgO2));
        o2Chart.chartLines[Color.red].Add(new Vector2(Time.timeSinceLevelLoad, 1 - avgO2));

        temperatureChart.chartLines[Color.red].Add(new Vector2(Time.timeSinceLevelLoad, avgTemp));
        prevMousePos = Input.mousePosition;
    }
}
