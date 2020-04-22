using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerToggleBehaviour : MonoBehaviour {

    public DrawerFurnitureConfig config;

    public Text description;
    public Image preview;

    public DrawerListBehaviour drawerList;

    private Toggle toggle;

    // Use this for initialization
    void Start () {
        if(config != null) {
            PopulateData();
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate {
                drawerList.DrawerToggleChanged(toggle, config);
            });
        }
    }

    public void PopulateData() {
        description.text = config.description;
        preview.sprite = config.preview;
    }

}
