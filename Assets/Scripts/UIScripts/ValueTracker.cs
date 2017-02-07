using UnityEngine;
using System.Collections;

public abstract class ValueTracker<T> : MonoBehaviour {

    public T minInputValue;
    public T maxInputValue;

    public T testValue;

    public abstract void Initialize();
    public abstract void ProcessInput(T value);


    void OnValidate()
    {
        Initialize();
        ProcessInput(testValue);
    }


    public float NormalizeValue(float value, float toMin, float toMax, float fromMin, float fromMax)
    {
        float normalizedValue = Mathf.InverseLerp(fromMin, fromMax, value);
        float scaleValue = toMax - toMin;
        normalizedValue *= scaleValue;
        normalizedValue += toMin;
        return normalizedValue;
    }
}
