using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenConfigBehaviour : MonoBehaviour {

	public LoadAndSaveBehaviour loadAndSave;
	private Prefs prefs;
	// Use this for initialization
	void Awake () {
		prefs = new Prefs();
		prefs.Load();
		if (!Variables.loadedFile.Equals("")) {
			Variables.loadedData.variables.LoadData();

			prefs.roomHeight = Variables.roomMeasurements.y;
			prefs.roomWidth = Variables.roomMeasurements.z;
			prefs.roomDepth = Variables.roomMeasurements.x;
			prefs.Save();
		}

		if (Utils.CompareFloats(prefs.roomHeight, Variables.loadedData.variables.roomMeasurements[1]) && Utils.CompareFloats(prefs.roomWidth, Variables.loadedData.variables.roomMeasurements[2]) && Utils.CompareFloats(prefs.roomDepth, Variables.loadedData.variables.roomMeasurements[0])) {
			Variables.loadedData.furniture.LoadData();
		}
	}

	public void startRoomConfig(){
		Variables.roomMeasurements = new Vector3(prefs.roomDepth, prefs.roomHeight, prefs.roomWidth);
		prefs.Save();
		loadAndSave.SaveData(false);
		SceneManager.LoadScene("RoomConfiguration");
	}
}
