using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndSaveBehaviour : MonoBehaviour {

	public void LoadData(string filename) {
		KitchenData kitchen = ReadJSON(filename);
		Variables.loadedData = kitchen;
		Variables.loadedFile = filename;
		SceneManager.LoadScene("KitchenBuilder");
	}

	public KitchenData ReadJSON(string filename) {

		string pathFilename = Path.Combine(Application.persistentDataPath, filename);

		if (File.Exists(pathFilename)) {
			string json = File.ReadAllText(pathFilename);
			return JsonUtility.FromJson<KitchenData>(json);
		} else {
			return null;
		}
	}


	public void SaveData(bool saveToFile = true) {

		VariablesData variables = new VariablesData();

		CornerElement[] cornerElements = GameObject.FindObjectsOfType<CornerElement>();
		DrawerFurniture[] drawerFurnitures = GameObject.FindObjectsOfType<DrawerFurniture>();
		DoorFurniture[] doorFurnitures = GameObject.FindObjectsOfType<DoorFurniture>();

		FurnitureData furniture = new FurnitureData(cornerElements, drawerFurnitures, doorFurnitures);

		KitchenData kitchen = new KitchenData(variables, furniture);
		Variables.loadedData = kitchen;
		Variables.loadedFile = "";
		if (saveToFile) {
			WriteJSON(kitchen);
		}
	}

	public void OverwriteData(string filename) {

		VariablesData variables = new VariablesData();

		CornerElement[] cornerElements = GameObject.FindObjectsOfType<CornerElement>();
		DrawerFurniture[] drawerFurnitures = GameObject.FindObjectsOfType<DrawerFurniture>();
		DoorFurniture[] doorFurnitures = GameObject.FindObjectsOfType<DoorFurniture>();

		FurnitureData furniture = new FurnitureData(cornerElements, drawerFurnitures, doorFurnitures);

		KitchenData kitchen = new KitchenData(variables, furniture);
		Variables.loadedData = kitchen;
		Variables.loadedFile = "";
		WriteJSON(kitchen, filename);
	}



	public void WriteJSON(KitchenData dataObject, string filename = "") {
		if (filename == "") {
			filename = "KitchenSave_" + DateTime.Now.ToString().Replace(" ", "_").Replace(":", "").Replace(".", "") + ".json";
		}
		string json = UnityEngine.JsonUtility.ToJson(dataObject, true);

		string pathFilename = System.IO.Path.Combine(Application.persistentDataPath, filename);
		File.WriteAllText(pathFilename, json);
	}

}