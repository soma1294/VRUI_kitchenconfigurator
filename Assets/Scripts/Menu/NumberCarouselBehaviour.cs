using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberCarouselBehaviour : MonoBehaviour {



    public DigitCarouselBehaviour digitTen;
    public DigitCarouselBehaviour digitOne;
    public DigitCarouselBehaviour digitTenth;
    public DigitCarouselBehaviour digitHundredth;

    public float numberValue = 0f;
	
	// Update is called once per frame
	void Update () {
        numberValue = (digitTen.value * 10 + digitOne.value + digitTenth.value / 10f + digitHundredth.value / 100f);
	}

    public void UpdateDigits(float value) {
        digitTen.value = (int)(value / 10f % 10f);
        digitOne.value = (int)(value / 1f % 10f);
        digitTenth.value = (int)(value * 10f % 10f);
        digitHundredth.value = (int)(value * 100f % 10f);
    }

    public void SetToggleGroup(ToggleGroup toggleGroup) {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++) {
            toggles[i].group = toggleGroup;
        }
    }
}
