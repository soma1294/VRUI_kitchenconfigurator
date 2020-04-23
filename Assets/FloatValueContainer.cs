using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatValueContainer : MonoBehaviour
{
    public float value = 0;
    public float minValue;
    public float maxValue;

    [System.Serializable]
    public class FloatValueContainerOnValueChanged : UnityEvent<float> { }
    public FloatValueContainerOnValueChanged OnValueChanged = new FloatValueContainerOnValueChanged();

    public VRUITextcontainerBehaviour textcontainer;

    private void Start()
    {
        if(!textcontainer)
            textcontainer = GetComponent<VRUITextcontainerBehaviour>();
        OnValueChanged.Invoke(value);
        if (textcontainer)
            textcontainer.ChangeTextTo(value.ToString("00.00 m"));
    }

    public void AddAmount(float amount)
    {
        if (value + amount >= maxValue)
            value = maxValue;
        else
            value += amount;
        if(textcontainer)
            textcontainer.ChangeTextTo(value.ToString("00.00 m"));
        OnValueChanged.Invoke(value);
    }

    public void SubtractAmount(float amount)
    {
        if (value - amount <= minValue)
            value = minValue;
        else
            value -= amount;
        if(textcontainer)
            textcontainer.ChangeTextTo(value.ToString("00.00 m"));
        OnValueChanged.Invoke(value);
    }

    public float Value
    {
        get { return value; }
        set
        {
            this.value = value;
            if(textcontainer)
                textcontainer.ChangeTextTo(value.ToString("00.00 m"));
            OnValueChanged.Invoke(value);
        }
    }
}
