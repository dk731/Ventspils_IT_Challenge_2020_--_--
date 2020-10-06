using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    private bool runnign = true;
    private Image pauseButton;

    public Sprite pauseSprite;
    public Sprite startSprite;

    public List<GameObject> paramList;

    private bool paramActive = true;


    void Start()
    {
        pauseButton = GameObject.Find("PauseButton").GetComponent<Image>();
    }

    public void startStopEvent()
    {
        if (runnign)
        {
            Time.timeScale = 0;
            pauseButton.sprite = startSprite;
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.sprite = pauseSprite;
        }
        runnign = !runnign;
    }

    public static void infoEvent()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void parametrsEvent()
    {
        
        if (!paramActive)
            GameObject.Find("CameraFollower").GetComponent<CameraFollower>().selectedObj.SetActive(false);
        paramList.ForEach(l => l.SetActive(paramActive));

        if (paramActive)
            GameObject.Find("CameraFollower").GetComponent<CameraFollower>().selectedObj.SetActive(true);

        paramActive = !paramActive;
        
    }
}
