using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBaseHeightBehaviour : MonoBehaviour
{
    public VRUITextcontainerBehaviour textcontainerBehaviour;
    public VRUISliderBehaviour sliderBehaviour;

    private void Start()
    {
        if (sliderBehaviour)
        {
            sliderBehaviour.MinValue = 0;
            sliderBehaviour.MaxValue = 330;
            sliderBehaviour.StartValue = 110;
        }
    }

    public void ChangeBaseHeight(float amount)
    {
        if (sliderBehaviour)
        {
            Variables.baseHeightInMM = Mathf.Clamp((int)amount, 0, 300);
            textcontainerBehaviour.ChangeTextTo(Variables.baseHeightInMM.ToString("0") + " mm");
            KitchenElement[] elements = FindObjectsOfType<KitchenElement>();
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i].redraw = true;
            }
        }
        else
        {
            Variables.baseHeightInMM = Mathf.Clamp((int)amount + Variables.baseHeightInMM, 0, 300);
            textcontainerBehaviour.ChangeTextTo(Variables.baseHeightInMM.ToString("0") + " mm");
            KitchenElement[] elements = FindObjectsOfType<KitchenElement>();
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i].redraw = true;
            }
        } 
    }
}
