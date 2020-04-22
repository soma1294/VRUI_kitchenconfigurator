using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DoorFurnitureData {

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
	public int spaceInMM;
	public bool handleLeft;

	//Children
	[NonSerialized]
	public List<CornerElementData> cornerElements = new List<CornerElementData>();
	[NonSerialized]
	public List<DrawerFurnitureData> drawerFurnitures = new List<DrawerFurnitureData>();
	[NonSerialized]
	public List<DoorFurnitureData> doorFurnitures = new List<DoorFurnitureData>();


	public DoorFurnitureData(GameObject doorFurniture) {
		Transform transform = doorFurniture.transform;
		this.position = new float[] { transform.localPosition.x, transform.localPosition.y, transform.localPosition.z };
		this.rotation = new float[] { transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z };

		DoorFurniture doorFurnitureComp = doorFurniture.GetComponent<DoorFurniture>();
		this.heightIndex = doorFurnitureComp.heightIndex;
		this.widthIndex = doorFurnitureComp.widthIndex;
		this.depthIndex = doorFurnitureComp.depthIndex;
		this.onGround = doorFurnitureComp.onGround;
		this.spaceInMM = doorFurnitureComp.spaceInMM;
		this.handleLeft = doorFurnitureComp.handleLeft;

		GameObject children = doorFurniture.transform.Find("Children").gameObject;

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
		GameObject doorFurniturePrefab = Utils.LoadPrefabByName("DoorThing", "FurniturePrefabs");
		GameObject doorFurniture;
		if (parent != null) {
			doorFurniture = GameObject.Instantiate(doorFurniturePrefab, parent);
		} else {
			doorFurniture = GameObject.Instantiate(doorFurniturePrefab);
		}
		doorFurniture.transform.localPosition = new Vector3(this.position[0], this.position[1], this.position[2]);
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(this.rotation[0], this.rotation[1], this.rotation[2]);
		doorFurniture.transform.localRotation = rotation;

		DoorFurniture doorFurnitureComp = doorFurniture.GetComponent<DoorFurniture>();
		doorFurnitureComp.heightIndex = this.heightIndex;
		doorFurnitureComp.widthIndex = this.widthIndex;
		doorFurnitureComp.depthIndex = this.depthIndex;
		doorFurnitureComp.onGround = this.onGround;
		doorFurnitureComp.handleLeft = this.handleLeft;
		doorFurnitureComp.spaceInMM = this.spaceInMM;


		Transform children = doorFurniture.transform.Find("Children");

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
