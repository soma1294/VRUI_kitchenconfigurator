using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMaterialToggles : MonoBehaviour
{
    public GameObject togglePrefab;
    public MaterialType materialType;
    [HideInInspector]
    public bool populated = false;

    private VRUIToggleGroupHelper toggleGroupHelper;
    private Material materialToSet;

    // Start is called before the first frame update
    void Start()
    {
        //toggleGroupHelper = GetComponent<VRUIToggleGroupHelper>();
        //Populate();
    }

    // Update is called once per frame
    void Update()
    {
        toggleGroupHelper = GetComponent<VRUIToggleGroupHelper>();
        Populate();
        enabled = false;
    }

    private void Populate()
    {
        //Load the specified materials from the resource folder
        Material[] materials = Resources.LoadAll<Material>("Materials/" + materialType.ToString() + "Materials/");
        //Set the MaxDisplayedElements of the attached ScrollPanel. 4 looks the best but if there are less objects we set it to the length of the materials array.
        GetComponent<VRUIScrollPanelBehaviour>().MaxDisplayedElements = materials.Length >= 4 ? 4 : materials.Length;
        //Set the length of the ToggleGroup to the length of the materials array. We need the same amount of toggles as we have materials.
        toggleGroupHelper.SetToggleList(new VRUIToggleBehaviour[materials.Length]);
        if (transform.childCount != materials.Length)
        {
            //Make sure the ScrollPanel has no children by destroying them
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child);
                }
            }
            //Make sure the prefab has a VRUIToggleBehaviour
            VRUIToggleBehaviour materialToggle = togglePrefab.GetComponent<VRUIToggleBehaviour>();
            if (materialToggle)
            {
                //For every toggle we set multiple variables and make sure they have the correct materials
                for (int i = 0; i < materials.Length; i++)
                {
                    GameObject toggle = Instantiate(togglePrefab, transform);
                    materialToggle = toggle.GetComponent<VRUIToggleBehaviour>();
                    materialToggle.BaseMaterial = materials[i];
                    materialToggle.ActiveMaterial = materials[i];
                    materialToggle.name = materials[i].name + "MaterialToggle";
                    materialToggle.m_onVRUIToggleDown.AddListener(SetToggleMaterialInVariables);
                    materialToggle.m_onVRUIToggleDown.AddListener( delegate { toggleGroupHelper.ToggleInGroupWasPressed(materialToggle.name); });
                    toggleGroupHelper.AddElementAtPosition(materialToggle, i);
                }
                populated = true;
            }
            else
            {
                Debug.LogError("No VRUIToggleBehaviour found. Cannot populate Material ScrollPanel: Name = " + name);
            }
        }
        else
        {
            Debug.LogError("MaterialScrollPanel already has children!");
        }
    }

    public void SetToggleMaterialInVariables(string name)
    {
        materialToSet = GameObject.Find(name).GetComponent<VRUIToggleBehaviour>().PhysicalToggle.GetComponent<MeshRenderer>().material;
        switch (materialType)
        {
            case MaterialType.Workplate:
                Variables.workPlateMaterial = materialToSet;
                break;
            case MaterialType.Furniture:
                Variables.furnitureMaterial = materialToSet;
                break;
            case MaterialType.Floor:
                Variables.floorMaterial = materialToSet;
                break;
            case MaterialType.Wall:
                Variables.wallMaterial = materialToSet;
                break;
            case MaterialType.Ceiling:
                Variables.ceilingMaterial = materialToSet;
                break;
            case MaterialType.Handles:
                Variables.handlesMaterial = materialToSet;
                break;
        }
    }
}
