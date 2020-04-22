using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialToggleBehaviour : MonoBehaviour {

    public MaterialConfig config;

    public MeshRenderer preview;

    public MaterialListBehaviour materialList;

    private Toggle toggle;

    // Use this for initialization
    void Start () {
        if(config != null) {
            PopulateData();
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate {
                materialList.MaterialToggleChanged(toggle, config);
            });
        }
    }

    public void PopulateData() {
		preview.materials = new Material[] { config.material };
    }

}
