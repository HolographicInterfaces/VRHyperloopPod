using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CircularVec2Tracker : ValueTracker<Vector2> {

    public Image background;
    public Image marker;

    private Vector2 xyMarkerRage;

    public void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        xyMarkerRage = new Vector2((background.rectTransform.rect.width/2) - marker.rectTransform.rect.width / 2, (background.rectTransform.rect.height/2) - marker.rectTransform.rect.height/2);
    }

    public override void ProcessInput(Vector2 value)
    {
        marker.rectTransform.anchoredPosition = new Vector2(NormalizeValue(value.x, -xyMarkerRage.x, xyMarkerRage.x, minInputValue.x, maxInputValue.x),
                                                         NormalizeValue(value.y, -xyMarkerRage.y, xyMarkerRage.y, minInputValue.y, maxInputValue.y));
    }
}
