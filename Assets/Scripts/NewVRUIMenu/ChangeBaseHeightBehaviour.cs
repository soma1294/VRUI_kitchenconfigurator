using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBaseHeightBehaviour : MonoBehaviour
{
    public VRUITextcontainerBehaviour textcontainerBehaviour;

    public void ChangeBaseHeight(float amount)
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
