using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileListBehaviour : MonoBehaviour {

	public GameObject prefab;
	public LoadAndSaveBehaviour loadAndSave;
	public bool saveEnabled;

	// Use this for initialization
	void Start () {
		Populate();
	}

	private void Populate()
	{
		DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);
		FileInfo[] fileInfos = info.GetFiles("KitchenSave*.json");
		for (int i = 0; i < fileInfos.Length; i++) {
			if (prefab.GetComponent<FileEntryBehaviour>() != null) {
				prefab.GetComponent<FileEntryBehaviour>().saveEnabled = saveEnabled;
				prefab.GetComponent<FileEntryBehaviour>().filename = fileInfos[i].Name;
				prefab.GetComponent<FileEntryBehaviour>().fileList = this;
			}
			Instantiate(prefab, transform);
		}
	}

	public void LoadFile(string filename) {
		loadAndSave.LoadData(filename);
	}

	public void SaveFile(string filename) {
		loadAndSave.OverwriteData(filename);
	}

	public void SaveNew() {
		loadAndSave.SaveData(true);
		FileEntryBehaviour[] fileEntries = gameObject.GetComponentsInChildren<FileEntryBehaviour>();
		for (int i = 0; i < fileEntries.Length; i++) {
			Destroy(fileEntries[i].gameObject);
		}
		Populate();
	}
}
