using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlesToggleBehaviour : MonoBehaviour {

    public HandlesConfig config;

    public Text description;
    public Transform preview;

    public HandlesListBehaviour handlesList;

    private Toggle toggle;

    // Use this for initialization
    void Start () {
        if(config != null) {
            PopulateData();
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate {
                handlesList.HandlesToggleChanged(toggle, config);
            });
        }
    }

    public void PopulateData() {
        description.text = config.description;
        Instantiate(config.handlesPrefab, preview);
    }

}
