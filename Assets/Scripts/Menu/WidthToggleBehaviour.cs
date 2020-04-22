using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class WidthToggleBehaviour : MonoBehaviour {

    public int configIndex;

    public Text description;

    public WidthListBehaviour widthList; 

    private Toggle toggle;

	// Use this for initialization
	void Start () {
        PopulateData();
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            widthList.WidthToggleChanged(toggle, configIndex);
        });
    }

    public void PopulateData() {
        description.text = Variables.widthsInMM[configIndex] + "mm";
    }
}
