using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VariablesData {

	public int baseHeightInMM;

	public string workPlateMaterialName;
	public string furnitureMaterialName;

	public string floorMaterialName;
	public string wallMaterialName;
	public string ceilingMaterialName;

	public string handlesMaterialName;
	public string handlesPrefabName;

	public float[] roomMeasurements;
	public Window[] northWindows;
	public Window[] eastWindows;
	public Window[] southWindows;
	public Window[] westWindows;

	public VariablesData() {
		this.baseHeightInMM = Variables.baseHeightInMM;
		this.workPlateMaterialName = Variables.workPlateMaterial?.name;
		this.furnitureMaterialName = Variables.furnitureMaterial?.name;
		this.floorMaterialName = Variables.floorMaterial?.name;
		this.wallMaterialName = Variables.wallMaterial?.name;
		this.ceilingMaterialName = Variables.ceilingMaterial?.name;
		this.handlesMaterialName = Variables.handlesMaterial?.name;
		this.handlesPrefabName = Variables.handlesPrefab?.name;
		this.roomMeasurements = new float[] { Variables.roomMeasurements.x, Variables.roomMeasurements.y, Variables.roomMeasurements.z };
		this.northWindows = Variables.northWindows?.ToArray();
		this.eastWindows = Variables.eastWindows?.ToArray();
		this.southWindows = Variables.southWindows?.ToArray();
		this.westWindows = Variables.westWindows?.ToArray();
	}

	public void LoadData() {
		Variables.baseHeightInMM = this.baseHeightInMM;
		Variables.workPlateMaterial = Utils.LoadMaterialByName(this.workPlateMaterialName, "WorkplateMaterials");
		Variables.furnitureMaterial = Utils.LoadMaterialByName(this.furnitureMaterialName, "FurnitureMaterials");
		Variables.floorMaterial = Utils.LoadMaterialByName(this.floorMaterialName, "FloorMaterials");
		Variables.wallMaterial = Utils.LoadMaterialByName(this.wallMaterialName, "WallMaterials");
		Variables.ceilingMaterial = Utils.LoadMaterialByName(this.ceilingMaterialName, "WallMaterials");
		Variables.handlesMaterial = Utils.LoadMaterialByName(this.handlesMaterialName, "HandlesMaterials");
		Variables.handlesPrefab = Utils.LoadPrefabByName(this.handlesPrefabName, "HandlesPrefabs");
		Variables.roomMeasurements = new Vector3(this.roomMeasurements[0], this.roomMeasurements[1], this.roomMeasurements[2]);
		for (int i = 0; i < this.northWindows.Length; i++) {
			Variables.northWindows.Add(this.northWindows[i]);
		}
		for (int i = 0; i < this.southWindows.Length; i++) {
			Variables.southWindows.Add(this.southWindows[i]);
		}
		for (int i = 0; i < this.eastWindows.Length; i++) {
			Variables.eastWindows.Add(this.eastWindows[i]);
		}
		for (int i = 0; i < this.westWindows.Length; i++) {
			Variables.westWindows.Add(this.westWindows[i]);
		}

	}

}
