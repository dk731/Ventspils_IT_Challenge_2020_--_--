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

    public float timeShown = 5.0f;

    public Vector2 chartRange;  // x - min, y - max

    public Color defaultColor;
    public Color axisColor;

    public Vector2 chartRatio;

    public bool needZeroAxis = false;
    public float zeroAxisVal = 0.0f;
    public Color zeroAxisColor;

    private Vector2 startPoint;
    private Vector2 startPoint1;

    public int amountOfVertLines = 5;
    public float sizeOfVertLines = 20.0f;
    public Color colorOfVertLines;

    public float zoomSpeed = 1.0f;

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
        
        Color[] fillColorArray = myTexture.GetPixels();

        for (var i = 0; i < fillColorArray.Length; ++i)
        {
            fillColorArray[i] = defaultColor;
        }

        myTexture.SetPixels(fillColorArray);
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

    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        float fromAbs = from - fromMin;
        float fromMaxAbs = fromMax - fromMin;

        float normal = fromAbs / fromMaxAbs;

        float toMaxAbs = toMax - toMin;
        float toAbs = toMaxAbs * normal;

        float to = toAbs + toMin;

        return to;
    }

    void DrawAxis()
    {
        
        startPoint = new Vector2(myTransform.rect.width * chartRatio.x, myTransform.rect.height * chartRatio.y);
        startPoint1 = new Vector2(myTransform.rect.width - startPoint.x, myTransform.rect.height - startPoint.y);

        //ClearBorder();
        DrawLine(startPoint, new Vector2(startPoint.x, startPoint1.y), axisColor);
        DrawLine(new Vector2(startPoint.x, startPoint1.y), startPoint1, axisColor);

        if (needZeroAxis)
        {
            float tmpy = Remap(zeroAxisVal, chartRange.x, chartRange.y, startPoint1.y, startPoint.y);
            DrawLine(new Vector2(startPoint.x, tmpy), new Vector2(startPoint1.x, tmpy), zeroAxisColor);
        }

        float offSet = (startPoint.y - startPoint1.y) / (amountOfVertLines - 1);
        Vector2 xPosses = new Vector2(startPoint.x - sizeOfVertLines / 2, startPoint.x + sizeOfVertLines / 2);
        for (int i = 0; i < amountOfVertLines; i++)
        {
            DrawLine(new Vector2(xPosses.x, offSet * i + startPoint1.y), new Vector2(xPosses.y, offSet * i + startPoint1.y), colorOfVertLines);
        }
    }

    void ClearBorder()
    {

        Color[] fillColorArray = myTexture.GetPixels();

        for (int y = (int)startPoint1.y; y < startPoint.y; y++)
        {
            for (int x = 0; x < startPoint.x; x++)
            {
                fillColorArray[y * (int)myTransform.rect.width + x] = defaultColor;
                fillColorArray[y * (int)myTransform.rect.width + x + (int)(startPoint1.x - startPoint.x)] = defaultColor;
            }
        }

        myTexture.SetPixels(fillColorArray);
    }

    void Update()
    {
        ClearScreen();
        float curTime = Time.timeSinceLevelLoad;
        float startTime = curTime - timeShown < 0 ? 0 : curTime - timeShown;

        foreach (var item in chartLines)
        {
            if (item.Value.Count >= 1)
            {
                for (int i = 1; i < item.Value.Count; i++)
                {
                    if (item.Value[i].x >= startTime && item.Value[i].x < curTime)
                    {
                        DrawLine(new Vector2(Remap(item.Value[i - 1].x, startTime, item.Value[item.Value.Count - 1].x, startPoint.x, startPoint1.x), Remap(item.Value[i - 1].y, chartRange.x, chartRange.y, startPoint.y, startPoint1.y)),
                                 new Vector2(Remap(item.Value[i].x, startTime, item.Value[item.Value.Count - 1].x, startPoint.x, startPoint1.x), Remap(item.Value[i].y, chartRange.x, chartRange.y, startPoint.y, startPoint1.y)),
                                 item.Key
                        );
                    }
                }
            }

        }
        ClearBorder();
        DrawAxis();
        myTexture.Apply();

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (((myTransform.position.x - myTransform.rect.width / 2) < Input.mousePosition.x
                && (myTransform.position.y + myTransform.rect.height / 2) > Input.mousePosition.y)
                && (myTransform.position.x + myTransform.rect.width / 2) > Input.mousePosition.x
                && (myTransform.position.y - myTransform.rect.height / 2) < Input.mousePosition.y
                )
            {
                timeShown += zoomSpeed * Input.GetAxis("Mouse ScrollWheel");
                if (timeShown < 1)
                {
                    timeShown = 1.0f;
                }
                else if (timeShown > Time.timeSinceLevelLoad)
                {
                    timeShown = Time.timeSinceLevelLoad;
                }
            }
        }
    }
}
