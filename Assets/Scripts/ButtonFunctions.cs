using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    private bool runnign = true;
    private bool ventilationRunning = false;
    public static bool windowRunning = true;
    public static bool radiatorRunning = true;

    public Image pauseButton;
    public Image ventilationButton;
    public Image widnowButton;
    public Image radiatorButton;

    public Sprite pauseSprite;
    public Sprite startSprite;

    public Sprite startVentilation;
    public Sprite stopVentilation;

    public Sprite onSprite;
    public Sprite offSprite;

    public GameObject menuPanel;

    public Slider timeScaleSlider;

    public Slider ventilationSlider;

    public Main mainScript;

    public Material ventilationIndicator;

    public Material windowMatertial;
    public Material radiatorMaterial;

    public void startStopEvent()
    {
        if (runnign)
        {
            Time.timeScale = 0;
            timeScaleSlider.value = 0;
            timeScaleSlider.interactable = false;
            pauseButton.sprite = startSprite;
        }
        else
        {
            Time.timeScale = 1;
            timeScaleSlider.value = 1;
            timeScaleSlider.interactable = true;
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
        if (menuPanel.activeSelf)
        {
            GameObject.Find("CameraFollower").GetComponent<CameraFollower>().selectedObj.SetActive(false);
            menuPanel.SetActive(false);
        }
        else
        {
            menuPanel.SetActive(true);
            GameObject.Find("CameraFollower").GetComponent<CameraFollower>().selectedObj.SetActive(true);
        }
        
    }

    public void onTimeScaleChange()
    {
        Time.timeScale = timeScaleSlider.value;
    }

    public void onHomeClick()
    {
        SceneManager.UnloadScene(1);
        SceneManager.LoadScene(0);
    }

    public void onVentelationClick()
    {

        ventilationRunning = !ventilationRunning;
        ventilationSlider.interactable = ventilationRunning;
        
        ventilationButton.sprite = ventilationRunning ? stopVentilation : startVentilation;
        ventilationIndicator.color = ventilationRunning ? new Color(0.0f, 1.0f, 0.0f, 0.5f) : new Color(1.0f, 0.0f, 0.0f, 0.5f);

        mainScript.isVentilated = ventilationRunning;
        mainScript.ventilationIntence = ventilationSlider.value;
        
    }

    public void ventilationSliderOnChange()
    {
        mainScript.ventilationIntence = ventilationSlider.value;
    }

    public void widowBtnClick()
    {
        windowRunning = !windowRunning;
        widnowButton.sprite = windowRunning ? offSprite : onSprite;

        windowMatertial.color = windowRunning ? new Color(windowMatertial.color.r, windowMatertial.color.g, windowMatertial.color.b, 0.2f) : new Color(windowMatertial.color.r, windowMatertial.color.g, windowMatertial.color.b, 0.0f);
    }

    public void radiatorBtnClick()
    {
        radiatorRunning = !radiatorRunning;
        radiatorButton.sprite = radiatorRunning ? offSprite : onSprite;

        radiatorMaterial.color = radiatorRunning ? new Color(radiatorMaterial.color.r, radiatorMaterial.color.g, radiatorMaterial.color.b, 0.2f) : new Color(radiatorMaterial.color.r, radiatorMaterial.color.g, radiatorMaterial.color.b, 0.0f);
    }
}
