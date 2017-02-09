using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ArcFloatTracker : ValueTracker<float>
{
    public string title;
    public string units;

    public Text titleText;
    public Text valueText;
    public Image background;
    public Image fill;

    public override void Initialize()
    {
        titleText.text = title;
    }

    public override void ProcessInput(float value)
    {
        //fill.fillAmount = NormalizeValue(value, 0, 1, minInputValue, maxInputValue);
        valueText.text = value + units;
    }
}
