using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadImageToggles : MonoBehaviour
{
    public GameObject togglePrefab;
    //public MaterialType materialType;

    private VRUIToggleGroupHelper toggleGroupHelper;

    // Start is called before the first frame update
    void Start()
    {
        toggleGroupHelper = GetComponent<VRUIToggleGroupHelper>();
        Populate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Populate()
    {
        //Load the specified materials from the resource folder
        Sprite[] sprites = Resources.LoadAll<Sprite>("FurnitureData/DrawerConfigs/ConfigImages");
        //Set the MaxDisplayedElements of the attached ScrollPanel. 4 looks the best but if there are less objects we set it to the length of the materials array.
        GetComponent<VRUIScrollPanelBehaviour>().MaxDisplayedElements = sprites.Length >= 4 ? 4 : sprites.Length;
        //Set the length of the ToggleGroup to the length of the materials array. We need the same amount of toggles as we have materials.
        toggleGroupHelper.SetToggleList(new VRUIToggleBehaviour[sprites.Length]);
        if (transform.childCount != sprites.Length)
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
            VRUIToggleBehaviour imageToggle = togglePrefab.GetComponent<VRUIToggleBehaviour>();
            if (imageToggle)
            {
                //For every toggle we set multiple variables and make sure they have the correct materials
                for (int i = 0; i < sprites.Length; i++)
                {
                    GameObject toggle = Instantiate(togglePrefab, transform);
                    imageToggle = toggle.GetComponent<VRUIToggleBehaviour>();
                    imageToggle.PhysicalToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprites[i];
                    imageToggle.name = sprites[i].name + "ImageToggle";
                    imageToggle.onVRUIToggleDown.AddListener(SetToggleImageInVariables);
                    toggleGroupHelper.AddElementAtPosition(imageToggle, i);
                }
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

    public void SetToggleImageInVariables(string name)
    {
        //Sprite materialToSet = GetComponent<VRUIToggleBehaviour>().PhysicalToggle.GetComponent<MeshRenderer>().material;
        
    }
}
