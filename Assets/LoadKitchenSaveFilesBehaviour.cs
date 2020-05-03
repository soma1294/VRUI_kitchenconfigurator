using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadKitchenSaveFilesBehaviour : MonoBehaviour
{
    public GameObject fileButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PopulateFileList();
    }
    
    private void PopulateFileList()
    {
        //DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);
        //FileInfo[] fileInfos = info.GetFiles("KitchenSave*.json");
        if (!PlayerPrefs.HasKey("saveFileAmount"))
        {
            Debug.Log("No kitchen files found. FileList not populated.");
            return;
        }
        int saveFileAmount = PlayerPrefs.GetInt("saveFileAmount");
        string[] filenames = new string[saveFileAmount];
        for (int i = 0; i < saveFileAmount; i++)
        {
            filenames[i] = PlayerPrefs.GetString("kitchenName" + i);
        }
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        //Set the MaxDisplayedElements of the attached ScrollPanel. 4 looks the best but if there are less objects we set it to the length of the materials array.
        GetComponent<VRUIScrollPanelBehaviour>().MaxDisplayedElements = filenames.Length >= 4 ? 4 : filenames.Length;
        for (int i = 0; i < filenames.Length; i++)
        {
            VRUIButtonBehaviour button = Instantiate(fileButtonPrefab, transform).GetComponent<VRUIButtonBehaviour>();
            string name = filenames[i];
            button.name = name;
            button.transform.GetChild(0).GetChild(0).GetComponent<VRUITextcontainerBehaviour>().ChangeTextTo(name);
            button.m_onVRUIButtonDown.AddListener(delegate { LoadData("" + i); });
        }
    }

    public void SaveNewFile()
    {
        SaveData(true);
        PopulateFileList();
    }

    public void LoadData(string index)
    {
        KitchenData kitchen = ReadJSON(index);
        Variables.loadedData = kitchen;
        Variables.loadedFile = PlayerPrefs.GetString("kitchenName" + index);
        SceneManager.LoadScene("KitchenBuilder");
    }

    public void SaveData(bool saveToFile = true)
    {
        VariablesData variables = new VariablesData();

        CornerElement[] cornerElements = GameObject.FindObjectsOfType<CornerElement>();
        DrawerFurniture[] drawerFurnitures = GameObject.FindObjectsOfType<DrawerFurniture>();
        DoorFurniture[] doorFurnitures = GameObject.FindObjectsOfType<DoorFurniture>();

        FurnitureData furniture = new FurnitureData(cornerElements, drawerFurnitures, doorFurnitures);

        KitchenData kitchen = new KitchenData(variables, furniture);
        Variables.loadedData = kitchen;
        Variables.loadedFile = "";
        if (saveToFile)
        {
            WriteJSON(kitchen);
        }
    }

    public KitchenData ReadJSON(string index)
    {
        if (PlayerPrefs.HasKey("kitchenJSON" + index))
        {
            string json = PlayerPrefs.GetString("kitchenJSON" + index);
            return JsonUtility.FromJson<KitchenData>(json);
        }
        else
        {
            Debug.LogError("kitchenJSON with index: " + index + "; was not found!" );
            return null;
        }
    }

    public void WriteJSON(KitchenData dataObject, string filename = "")
    {
        int saveFileAmount;
        if (PlayerPrefs.HasKey("saveFileAmount"))
        {
            saveFileAmount = PlayerPrefs.GetInt("saveFileAmount") + 1;
        }
        else
        {
            saveFileAmount = 0;
        }
        if (filename == "")
        {
            filename = "KitchenSave_" + DateTime.Now.ToString().Replace(" ", "_").Replace(":", "").Replace(".", "") + ".json";
        }
        string json = UnityEngine.JsonUtility.ToJson(dataObject, true);

        PlayerPrefs.SetString("kitchenName" + saveFileAmount, filename);
        PlayerPrefs.SetString("kitchenJSON" + saveFileAmount, json);

        PlayerPrefs.SetInt("saveFileAmount", saveFileAmount);
    }
}
