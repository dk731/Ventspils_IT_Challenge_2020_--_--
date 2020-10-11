using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuButtonsEvents : MonoBehaviour
{

    public void InfoEvent()
    {
        /*if (EditorUtility.DisplayDialog("You've Clicked Information Button!",
            " If you click \"Go!\", You will be automaticly redirected to webpage that contains all information about this project. \n\n  If you want to oppen it manualy you can copy link: \nhttps://cloudjoy29.wixsite.com/website",
            "Go!",
            "Cancel")
        )*/
        UnityEngine.Application.OpenURL("https://cloudjoy29.wixsite.com/website");
    }

    public void StartSimulationEvent()
    {
        InitialValues.o2Con = GameObject.Find("OxygenCon").GetComponent<Slider>().value;
        InitialValues.startParticleTemp = GameObject.Find("StartTemp").GetComponent<Slider>().value;
        InitialValues.startHeadTemp = GameObject.Find("BodyTemp").GetComponent<Slider>().value;
        InitialValues.headsAmount = (int)GameObject.Find("AmountOfHeads").GetComponent<Slider>().value;
        InitialValues.outsideTemp = GameObject.Find("OutsideTemp").GetComponent<Slider>().value;
        InitialValues.radiatorTemp = GameObject.Find("RadiatorTemp").GetComponent<Slider>().value;


        SceneManager.UnloadScene(0);
        SceneManager.LoadScene(1);

    }
}
