using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class MaterialListBehaviour: MonoBehaviour {
    public GameObject prefab;
    public MaterialType materialType;

    void Start() {
        Populate();
    }

    public void MaterialToggleChanged(Toggle toggle, MaterialConfig materialConfig) {
        if (toggle.isOn) {
            switch (materialType) {
                case MaterialType.Workplate:
                    Variables.workPlateMaterial = materialConfig.material;
                    break;
                case MaterialType.Furniture:
                    Variables.furnitureMaterial = materialConfig.material;
                    break;
                case MaterialType.Floor:
                    Variables.floorMaterial = materialConfig.material;
                    break;
                case MaterialType.Wall:
                    Variables.wallMaterial = materialConfig.material;
                    break;
                case MaterialType.Ceiling:
                    Variables.ceilingMaterial = materialConfig.material;
                    break;
                case MaterialType.Handles:
                    Variables.handlesMaterial = materialConfig.material;
                    break;
            }
        }
    }

    private void Populate() {

        MaterialConfig[] configs = Resources.LoadAll<MaterialConfig>("MaterialConfigs/"+materialType.ToString()+"MaterialConfigs");

        for (int i = 0; i < configs.Length; i++) {
            if(prefab.GetComponent<MaterialToggleBehaviour>() != null) {
                prefab.GetComponent<MaterialToggleBehaviour>().config = configs[i];
                prefab.GetComponent<MaterialToggleBehaviour>().materialList = this;
                prefab.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
            }
            Instantiate(prefab, transform);

        }

    }


}
