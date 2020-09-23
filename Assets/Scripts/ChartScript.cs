using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartScript : MonoBehaviour
{
    private RectTransform myTransform;
    private Texture2D myTexture;

    public Dictionary<Color, List<Vector2>> chartLines = new Dictionary<Color, List<Vector2>>();

    public float timeShown = 2.0f;

    public Vector2 chartRange;

    public Color defaultColor;
    public Color axisColor;

    public Vector2 chartRatio;

    public bool needZeroAxis = false;
    public Color zeroAxisColor;

    public void DrawLine(Vector2 p1, Vector2 p2, Color col)
    {
        Vector2 t = p1;
        float frac = 1 / Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
        float ctr = 0;

        while ((int)t.x != (int)p2.x || (int)t.y != (int)p2.y)
        {
            t = Vector2.Lerp(p1, p2, ctr);
            ctr += frac;
            myTexture.SetPixel((int)t.x, (int)t.y, col);
        }
    }

    void ClearScreen()
    {
        for (int y = 0; y < myTransform.rect.height; y++)
        {
            for (int x = 0; x < myTransform.rect.width; x++)
            {
                myTexture.SetPixel(x, y, defaultColor);
            }
        }
    }

    void Start()
    {
        myTransform = GetComponent<RectTransform>();
        myTexture = new Texture2D((int)myTransform.rect.width, (int)myTransform.rect.height, TextureFormat.ARGB32, false);
        GetComponent<RawImage>().material.mainTexture = myTexture;

        ClearScreen();
        myTexture.Apply();

        gameObject.GetComponent<RawImage>().texture = myTexture;
    }

    void DrawAxis()
    {
        Vector2 startPoint = new Vector2(myTransform.rect.width * chartRatio.x, myTransform.rect.height * chartRatio.y);
        Vector2 startPoint1 = new Vector2(startPoint.x, myTransform.rect.height - startPoint.y);

        DrawLine(startPoint, startPoint1, axisColor);
        DrawLine(startPoint1, new Vector2(myTransform.rect.width - startPoint1.x, startPoint1.y), axisColor);

        if (needZeroAxis)
        {
            float yy = 
        }


    }

    void Update()
    {
        ClearScreen();
        DrawAxis();
        float startTime = Time.time - timeShown < 0 ? 0 : Time.time - timeShown;

        foreach (var item in chartLines)
        {
            
        }
        myTexture.Apply();
    }
}
