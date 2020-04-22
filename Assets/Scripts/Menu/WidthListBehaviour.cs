using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class WidthListBehaviour : MonoBehaviour {
    public GameObject prefab;
    public ObjectPreviewBehaviour preview;


    void Start() {
        Populate();
    }

    public void WidthToggleChanged(Toggle toggle, int widthIndex) {
        if (toggle.isOn) {
            preview.SetWidthIndex(widthIndex);
        }
    }

    private void Populate() {

        for (int i = 0; i < Variables.widthsInMM.Length; i++) {
            if(prefab.GetComponent<WidthToggleBehaviour>() != null) {
                prefab.GetComponent<WidthToggleBehaviour>().configIndex = i;
                prefab.GetComponent<WidthToggleBehaviour>().widthList = this;
                prefab.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
            }
            Instantiate(prefab, transform);

        }

    }
}
