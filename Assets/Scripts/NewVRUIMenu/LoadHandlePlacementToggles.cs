using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadHandlePlacementToggles : MonoBehaviour
{
    public GameObject togglePrefab;
    public ObjectPreviewBehaviour objectPreview;

    private VRUIToggleGroupHelper toggleGroupHelper;

    // Start is called before the first frame update
    void Start()
    {
        //toggleGroupHelper = GetComponent<VRUIToggleGroupHelper>();
        //Populate();
    }

    private void Update()
    {
        toggleGroupHelper = GetComponent<VRUIToggleGroupHelper>();
        Populate();
        enabled = false;
    }

    private void Populate()
    {
        //Load the specified configs from the resource folder
        DrawerFurnitureConfig[] configs = Resources.LoadAll<DrawerFurnitureConfig>("FurnitureData/DrawerConfigs");
        //Set the MaxDisplayedElements of the attached ScrollPanel. 4 looks the best but if there are less objects we set it to the length of the materials array.
        GetComponent<VRUIScrollPanelBehaviour>().MaxDisplayedElements = configs.Length >= 4 ? 4 : configs.Length;
        //Set the length of the ToggleGroup to the length of the materials array. We need the same amount of toggles as we have materials.
        toggleGroupHelper.SetToggleList(new VRUIToggleBehaviour[configs.Length]);
        if (transform.childCount != configs.Length)
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
                for (int i = 0; i < configs.Length; i++)
                {
                    GameObject toggle = Instantiate(togglePrefab, transform);
                    imageToggle = toggle.GetComponent<VRUIToggleBehaviour>();
                    imageToggle.PhysicalToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = configs[i].preview;
                    imageToggle.PhysicalToggle.transform.GetChild(1).GetComponent<VRUITextcontainerBehaviour>().ChangeTextTo(configs[i].description);
                    string toggleName = configs[i].description;
                    imageToggle.name = toggleName;
                    imageToggle.GetComponent<SetHandlePlacementBehaviour>().drawerConfig = configs[i];
                    imageToggle.GetComponent<SetHandlePlacementBehaviour>().objectPreview = objectPreview;
                    imageToggle.m_onVRUIToggleDown.AddListener(imageToggle.GetComponent<SetHandlePlacementBehaviour>().SetThisDrawerConfig);
                    imageToggle.m_onVRUIToggleDown.AddListener(delegate { toggleGroupHelper.ToggleInGroupWasPressed(toggleName); });
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
}
