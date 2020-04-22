using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;

[Serializable]
public class CornerElementData {

	///SerializationFields
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

	//CornerElement
	public int heightIndex;
	public int widthIndex;
	public int depthIndex;
	public bool onGround;

	//Children
	[NonSerialized]
	public List<CornerElementData> cornerElements = new List<CornerElementData>();
	[NonSerialized]
	public List<DrawerFurnitureData> drawerFurnitures = new List<DrawerFurnitureData>();
	[NonSerialized]
	public List<DoorFurnitureData> doorFurnitures = new List<DoorFurnitureData>();

	public CornerElementData(GameObject cornerElement) {
		Transform transform = cornerElement.transform;
		this.position = new float[] { transform.localPosition.x, transform.localPosition.y, transform.localPosition.z };
		this.rotation = new float[] { transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z };

		CornerElement cornerElementComp = cornerElement.GetComponent<CornerElement>();
		this.heightIndex = cornerElementComp.heightIndex;
		this.widthIndex = cornerElementComp.widthIndex;
		this.depthIndex = cornerElementComp.depthIndex;
		this.onGround = cornerElementComp.onGround;

		GameObject children = cornerElement.transform.Find("Children").gameObject;
		
		List<CornerElementData> cornerElementDatas = new List<CornerElementData>();
		List<DrawerFurnitureData> drawerFurnitureDatas = new List<DrawerFurnitureData>();
		List<DoorFurnitureData> doorFurnitureDatas = new List<DoorFurnitureData>();
		
		foreach (Transform child in children.transform) {
			if (!child.gameObject.Equals(cornerElement)) {
				if (child.gameObject.GetComponent<CornerElement>() != null) {
					cornerElementDatas.Add(new CornerElementData(child.gameObject));
				} else if (child.gameObject.GetComponent<DrawerFurniture>() != null) {
					drawerFurnitureDatas.Add(new DrawerFurnitureData(child.gameObject));
				} else if (child.gameObject.GetComponent<DoorFurniture>() != null) {
					doorFurnitureDatas.Add(new DoorFurnitureData(child.gameObject));
				}
			}
		}
		
		this.cornerElements = cornerElementDatas;
		this.drawerFurnitures = drawerFurnitureDatas;
		this.doorFurnitures = doorFurnitureDatas;
		
	}
	

	public void LoadData(Transform parent = null) {
		GameObject cornerElementPrefab = Utils.LoadPrefabByName("CornerThing", "FurniturePrefabs");
		GameObject cornerElement;
		if(parent != null) {
			cornerElement = GameObject.Instantiate(cornerElementPrefab, parent);
		} else {
			cornerElement = GameObject.Instantiate(cornerElementPrefab);
		}

		cornerElement.transform.localPosition = new Vector3(this.position[0], this.position[1], this.position[2]);
		
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(this.rotation[0], this.rotation[1], this.rotation[2]);
		cornerElement.transform.localRotation = rotation;

		CornerElement cornerElementComp = cornerElement.GetComponent<CornerElement>();
		cornerElementComp.heightIndex = this.heightIndex;
		cornerElementComp.widthIndex = this.widthIndex;
		cornerElementComp.depthIndex = this.depthIndex;
		cornerElementComp.onGround = this.onGround;

		Transform children = cornerElement.transform.Find("Children");

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
