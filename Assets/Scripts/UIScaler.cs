using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
    private Vector2 originalSize = new Vector2(2540, 1440);
    private RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localScale = new Vector3(Screen.width / originalSize.x, Screen.height / originalSize.y, 1);
    }
}
