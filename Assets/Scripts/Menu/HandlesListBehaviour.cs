using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class HandlesListBehaviour : MonoBehaviour {
    public GameObject prefab;

    void Start() {
        Populate();
    }

    public void HandlesToggleChanged(Toggle toggle, HandlesConfig handlesConfig) {
        if (toggle.isOn) {
            Variables.handlesPrefab = handlesConfig.handlesPrefab;
        }
    }

    private void Populate() {

        HandlesConfig[] configs = Resources.LoadAll<HandlesConfig>("FurnitureData/HandlesConfigs");

        for (int i = 0; i < configs.Length; i++) {
            if (prefab.GetComponent<HandlesToggleBehaviour>() != null) {
                prefab.GetComponent<HandlesToggleBehaviour>().config = configs[i];
                prefab.GetComponent<HandlesToggleBehaviour>().handlesList = this;
                prefab.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
            }
            Instantiate(prefab, transform);

        }

    }
}
