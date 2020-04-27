using System.IO;
using UnityEngine;

public class LoadKitchenSaveFilesBehaviour : MonoBehaviour
{
    public GameObject fileButtonPrefab;
    public LoadAndSaveBehaviour loadAndSave;

    // Start is called before the first frame update
    void Start()
    {
        PopulateFileList();
    }
    
    private void PopulateFileList()
    {
        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] fileInfos = info.GetFiles("KitchenSave*.json");
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        //Set the MaxDisplayedElements of the attached ScrollPanel. 4 looks the best but if there are less objects we set it to the length of the materials array.
        GetComponent<VRUIScrollPanelBehaviour>().MaxDisplayedElements = fileInfos.Length >= 4 ? 4 : fileInfos.Length;
        for (int i = 0; i < fileInfos.Length; i++)
        {
            VRUIButtonBehaviour button = Instantiate(fileButtonPrefab, transform).GetComponent<VRUIButtonBehaviour>();
            string name = fileInfos[i].Name;
            button.name = name;
            button.transform.GetChild(0).GetChild(0).GetComponent<VRUITextcontainerBehaviour>().ChangeTextTo(name);
            button.m_onVRUIButtonDown.AddListener(delegate { loadAndSave.LoadData(name); });
        }
    }

    public void SaveNewFile()
    {
        loadAndSave.SaveData(true);
        PopulateFileList();
    }
}
