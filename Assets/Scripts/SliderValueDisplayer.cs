using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplayer : MonoBehaviour
{
    public Text myDisplayText;
    private Slider mySlider;
    public string textAfterValue;
    public float valueMult;

    // Start is called before the first frame update
    private void Awake()
    {
        mySlider = GetComponent<Slider>();

        mySlider.onValueChanged.AddListener(ValueChange);
        myDisplayText.text = (mySlider.value * valueMult).ToString("0.00") + textAfterValue;
    }

    void ValueChange(float value)
    {
        myDisplayText.text = (mySlider.value * valueMult).ToString("0.00") + textAfterValue;
    }

}
