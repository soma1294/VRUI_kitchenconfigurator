﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DrawerFurnitureData {

	//SerializationFields
	[NonSerialized]
	public DoorFurnitureData parentDoor;
	[NonSerialized]
	public CornerElementData parentCorner;
	[NonSerialized]
	public DrawerFurnitureData parentDrawer;
	public int index;
	public int parentIndex;
	public string parentType;

	//Transform
	public float[] position;
	public float[] rotation;

	//DrawerFurniture
	public int heightIndex;
	public int widthIndex;
	public int depthIndex;
	public bool onGround;
	public int[] drawers;
	public int spaceInMM;

	//Children
	[NonSerialized]
	public List<CornerElementData> cornerElements = new List<CornerElementData>();
	[NonSerialized]
	public List<DrawerFurnitureData> drawerFurnitures = new List<DrawerFurnitureData>();
	[NonSerialized]
	public List<DoorFurnitureData> doorFurnitures = new List<DoorFurnitureData>();


	public DrawerFurnitureData(GameObject drawerFurniture) {
		Transform transform = drawerFurniture.transform;
		this.position = new float[] { transform.localPosition.x, transform.localPosition.y, transform.localPosition.z };
		this.rotation = new float[] { transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z };

		DrawerFurniture drawerFurnitureComp = drawerFurniture.GetComponent<DrawerFurniture>();
		this.heightIndex = drawerFurnitureComp.heightIndex;
		this.widthIndex = drawerFurnitureComp.widthIndex;
		this.depthIndex = drawerFurnitureComp.depthIndex;
		this.onGround = drawerFurnitureComp.onGround;
		this.drawers = drawerFurnitureComp.drawers;
		this.spaceInMM = drawerFurnitureComp.spaceInMM;

		GameObject children = drawerFurniture.transform.Find("Children").gameObject;

		List<CornerElementData> cornerElementDatas = new List<CornerElementData>();
		List<DrawerFurnitureData> drawerFurnitureDatas = new List<DrawerFurnitureData>();
		List<DoorFurnitureData> doorFurnitureDatas = new List<DoorFurnitureData>();

		foreach (Transform child in children.transform) {
			if (child.gameObject.GetComponent<CornerElement>() != null) {
				cornerElementDatas.Add(new CornerElementData(child.gameObject));
			} else if (child.gameObject.GetComponent<DrawerFurniture>() != null) {
				drawerFurnitureDatas.Add(new DrawerFurnitureData(child.gameObject));
			} else if (child.gameObject.GetComponent<DoorFurniture>() != null) {
				doorFurnitureDatas.Add(new DoorFurnitureData(child.gameObject));
			}
		}

		this.cornerElements = cornerElementDatas;
		this.drawerFurnitures = drawerFurnitureDatas;
		this.doorFurnitures = doorFurnitureDatas;
	}

	public void LoadData(Transform parent = null) {
		GameObject drawerFurniturePrefab = Utils.LoadPrefabByName("DrawerThing", "FurniturePrefabs");
		GameObject drawerFurniture;
		if (parent != null) {
			drawerFurniture = GameObject.Instantiate(drawerFurniturePrefab, parent);
		} else {
			drawerFurniture = GameObject.Instantiate(drawerFurniturePrefab);
		}
		drawerFurniture.transform.localPosition = new Vector3(this.position[0], this.position[1], this.position[2]);
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(this.rotation[0], this.rotation[1], this.rotation[2]);
		drawerFurniture.transform.localRotation = rotation;

		DrawerFurniture drawerFurnitureComp = drawerFurniture.GetComponent<DrawerFurniture>();
		drawerFurnitureComp.heightIndex = this.heightIndex;
		drawerFurnitureComp.widthIndex = this.widthIndex;
		drawerFurnitureComp.depthIndex = this.depthIndex;
		drawerFurnitureComp.onGround = this.onGround;
		drawerFurnitureComp.drawers = this.drawers;
		drawerFurnitureComp.spaceInMM = this.spaceInMM;


		Transform children = drawerFurniture.transform.Find("Children");

		for (int i = 0; i < cornerElements.Count; i++) {
			cornerElements[i].LoadData(children);
		}

		for (int i = 0; i < drawerFurnitures.Count; i++) {
			drawerFurnitures[i].LoadData(children);
		}

		for (int i = 0; i < doorFurnitures.Count; i++) {
			doorFurnitures[i].LoadData(children);
		}
	}
}
