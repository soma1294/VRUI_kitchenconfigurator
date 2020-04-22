using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class DrawerListBehaviour: MonoBehaviour {
    public GameObject prefab;
    public ObjectPreviewBehaviour preview;

    void Start() {
        Populate();
    }

    public void DrawerToggleChanged(Toggle toggle, DrawerFurnitureConfig drawerConfig) {
        if (toggle.isOn) {
            preview.SetDrawerConfig(drawerConfig);
        }
    }

    private void Populate() {
        DrawerFurnitureConfig[] configs = Resources.LoadAll<DrawerFurnitureConfig>("FurnitureData/DrawerConfigs");

        for (int i = 0; i < configs.Length; i++) {
            if(prefab.GetComponent<DrawerToggleBehaviour>() != null) {
                prefab.GetComponent<DrawerToggleBehaviour>().config = configs[i];
                prefab.GetComponent<DrawerToggleBehaviour>().drawerList = this;
                prefab.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
            }
            Instantiate(prefab, transform);

        }

    }


}
