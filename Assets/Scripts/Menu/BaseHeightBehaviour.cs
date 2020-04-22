using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHeightBehaviour : MonoBehaviour {

    public Text value;
	
	// Update is called once per frame
	void Update () {
        value.text = (Variables.baseHeightInMM/10f).ToString("0.0") + "cm";
    }

    public void AddToBaseHeight(int amount) {
        Variables.baseHeightInMM = Mathf.Clamp(amount + Variables.baseHeightInMM, 0, 300);
        KitchenElement[] elements = FindObjectsOfType<KitchenElement>();
        for (int i = 0; i < elements.Length; i++) {
            elements[i].redraw = true;
        }
    }
}
