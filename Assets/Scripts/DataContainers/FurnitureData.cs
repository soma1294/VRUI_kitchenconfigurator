using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class FurnitureData {

	public CornerElementData[] cornerElements;
	public DrawerFurnitureData[] drawerFurnitures;
	public DoorFurnitureData[] doorFurnitures;

	[NonSerialized]
	List<CornerElementData> cornerElementDatas = new List<CornerElementData>();
	[NonSerialized]
	List<DrawerFurnitureData> drawerFurnitureDatas = new List<DrawerFurnitureData>();
	[NonSerialized]
	List<DoorFurnitureData> doorFurnitureDatas = new List<DoorFurnitureData>();

	public FurnitureData(CornerElement[] cornerElements, DrawerFurniture[] drawerFurnitures, DoorFurniture[] doorFurnitures) {


		List<CornerElementData> cornerElementDatasIntern = new List<CornerElementData>();
		List<DrawerFurnitureData> drawerFurnitureDatasIntern = new List<DrawerFurnitureData>();
		List<DoorFurnitureData> doorFurnitureDatasIntern = new List<DoorFurnitureData>();


		for(int i = 0; i < cornerElements.Length; i++) {
			if(cornerElements[i].gameObject.transform.parent == null) {
				cornerElementDatasIntern.Add(new CornerElementData(cornerElements[i].gameObject));
			}
		}

		for(int i = 0; i < drawerFurnitures.Length; i++) {
			if (drawerFurnitures[i].gameObject.transform.parent == null) {
				drawerFurnitureDatasIntern.Add(new DrawerFurnitureData(drawerFurnitures[i].gameObject));
			}
		}

		for(int i= 0; i < doorFurnitures.Length; i++) {
			if (doorFurnitures[i].gameObject.transform.parent == null) {
				doorFurnitureDatasIntern.Add(new DoorFurnitureData(doorFurnitures[i].gameObject));
			}
		}

		foreach (CornerElementData cornerData in cornerElementDatasIntern) {
			SetRecursiveParent(cornerData);
		}

		foreach (DrawerFurnitureData drawerFurniture in drawerFurnitureDatasIntern) {
			SetRecursiveParent(drawerFurniture);
		}

		foreach (DoorFurnitureData doorFurniture in doorFurnitureDatasIntern) {
			SetRecursiveParent(doorFurniture);
		}


		this.cornerElements = this.cornerElementDatas.ToArray();
		this.drawerFurnitures = this.drawerFurnitureDatas.ToArray();
		this.doorFurnitures = this.doorFurnitureDatas.ToArray();

		SetSerializinIndexes();
	}

	private void SetRecursiveParent(CornerElementData parent) {
		CornerElementData[] cornerElementDatas = parent.cornerElements.ToArray();
		DrawerFurnitureData[] drawerFurnitureDatas = parent.drawerFurnitures.ToArray();
		DoorFurnitureData[] doorFurnitureDatas = parent.doorFurnitures.ToArray();
		this.cornerElementDatas.Add(parent);

		for (int i = 0; i < cornerElementDatas.Length; i++) {
			cornerElementDatas[i].parentCorner = parent;
			SetRecursiveParent(cornerElementDatas[i]);
		}

		for (int i = 0; i < drawerFurnitureDatas.Length; i++) {
			drawerFurnitureDatas[i].parentCorner = parent;
			SetRecursiveParent(drawerFurnitureDatas[i]);
		}

		for (int i = 0; i < doorFurnitureDatas.Length; i++) {
			doorFurnitureDatas[i].parentCorner = parent;
			SetRecursiveParent(doorFurnitureDatas[i]);
		}
	}

	private void SetRecursiveParent(DrawerFurnitureData parent) {
		CornerElementData[] cornerElementDatas = parent.cornerElements.ToArray();
		DrawerFurnitureData[] drawerFurnitureDatas = parent.drawerFurnitures.ToArray();
		DoorFurnitureData[] doorFurnitureDatas = parent.doorFurnitures.ToArray();
		this.drawerFurnitureDatas.Add(parent);

		for (int i = 0; i < cornerElementDatas.Length; i++) {
			cornerElementDatas[i].parentDrawer = parent;
			SetRecursiveParent(cornerElementDatas[i]);
		}

		for (int i = 0; i < drawerFurnitureDatas.Length; i++) {
			drawerFurnitureDatas[i].parentDrawer = parent;
			SetRecursiveParent(drawerFurnitureDatas[i]);
		}

		for (int i = 0; i < doorFurnitureDatas.Length; i++) {
			doorFurnitureDatas[i].parentDrawer = parent;
			SetRecursiveParent(doorFurnitureDatas[i]);
		}
	}

	private void SetRecursiveParent(DoorFurnitureData parent) {
		CornerElementData[] cornerElementDatas = parent.cornerElements.ToArray();
		DrawerFurnitureData[] drawerFurnitureDatas = parent.drawerFurnitures.ToArray();
		DoorFurnitureData[] doorFurnitureDatas = parent.doorFurnitures.ToArray();
		this.doorFurnitureDatas.Add(parent);

		for (int i = 0; i < cornerElementDatas.Length; i++) {
			cornerElementDatas[i].parentDoor = parent;
			SetRecursiveParent(cornerElementDatas[i]);
		}

		for (int i = 0; i < drawerFurnitureDatas.Length; i++) {
			drawerFurnitureDatas[i].parentDoor = parent;
			SetRecursiveParent(drawerFurnitureDatas[i]);
		}

		for (int i = 0; i < doorFurnitureDatas.Length; i++) {
			doorFurnitureDatas[i].parentDoor = parent;
			SetRecursiveParent(doorFurnitureDatas[i]);
		}
	}
	private void SetSerializinIndexes() {
		for (int i = 0; i < cornerElements.Length; i++) {
			cornerElements[i].index = i;
		}

		for (int i = 0; i < drawerFurnitures.Length; i++) {
			drawerFurnitures[i].index = i;
		}

		for (int i = 0; i < doorFurnitures.Length; i++) {
			doorFurnitures[i].index = i;
		}

		for (int i = 0; i < cornerElements.Length; i++) {
			if (cornerElements[i].parentCorner != null) {
				cornerElements[i].parentIndex = cornerElements[i].parentCorner.index;
				cornerElements[i].parentType = "Corner";
			}else if (cornerElements[i].parentDrawer != null) {
				cornerElements[i].parentIndex = cornerElements[i].parentDrawer.index;
				cornerElements[i].parentType = "Drawer";
			}else if (cornerElements[i].parentDoor != null) {
				cornerElements[i].parentIndex = cornerElements[i].parentDoor.index;
				cornerElements[i].parentType = "Door";
			} else {
				cornerElements[i].parentIndex = -1;
				cornerElements[i].parentType = "None";
			}
		}

		for (int i = 0; i < drawerFurnitures.Length; i++) {
			if (drawerFurnitures[i].parentCorner != null) {
				drawerFurnitures[i].parentIndex = drawerFurnitures[i].parentCorner.index;
				drawerFurnitures[i].parentType = "Corner";
			} else if (drawerFurnitures[i].parentDrawer != null) {
				drawerFurnitures[i].parentIndex = drawerFurnitures[i].parentDrawer.index;
				drawerFurnitures[i].parentType = "Drawer";
			} else if (drawerFurnitures[i].parentDoor != null) {
				drawerFurnitures[i].parentIndex = drawerFurnitures[i].parentDoor.index;
				drawerFurnitures[i].parentType = "Door";
			} else {
				drawerFurnitures[i].parentIndex = -1;
				drawerFurnitures[i].parentType = "None";
			}
		}

		for (int i = 0; i < doorFurnitures.Length; i++) {
			if (doorFurnitures[i].parentCorner != null) {
				doorFurnitures[i].parentIndex = doorFurnitures[i].parentCorner.index;
				doorFurnitures[i].parentType = "Corner";
			} else if (doorFurnitures[i].parentDrawer != null) {
				doorFurnitures[i].parentIndex = doorFurnitures[i].parentDrawer.index;
				doorFurnitures[i].parentType = "Drawer";
			} else if (doorFurnitures[i].parentDoor != null) {
				doorFurnitures[i].parentIndex = doorFurnitures[i].parentDoor.index;
				doorFurnitures[i].parentType = "Door";
			} else {
				doorFurnitures[i].parentIndex = -1;
				doorFurnitures[i].parentType = "None";
			}
		}
	}
	
	private void Deserialize() {
		List<CornerElementData> cornerElementDatasIntern = new List<CornerElementData>();
		List<DrawerFurnitureData> drawerFurnitureDatasIntern = new List<DrawerFurnitureData>();
		List<DoorFurnitureData> doorFurnitureDatasIntern = new List<DoorFurnitureData>();

		for (int i = 0; i < cornerElements.Length; i++) {
			cornerElements[i].cornerElements = new List<CornerElementData>();
			cornerElements[i].drawerFurnitures = new List<DrawerFurnitureData>();
			cornerElements[i].doorFurnitures = new List<DoorFurnitureData>();

			if (cornerElements[i].parentIndex >= 0) {
				if (cornerElements[i].parentType.Equals("Corner")) {
					for (int j = 0; j < cornerElements.Length; j++) {
						if (cornerElements[j].index == cornerElements[i].parentIndex) {
							cornerElements[i].parentCorner = cornerElements[j];
							break;
						}	
					}
				}else if (cornerElements[i].parentType.Equals("Drawer")) {
					for (int j = 0; j < drawerFurnitures.Length; j++) {
						if (drawerFurnitures[j].index == cornerElements[i].parentIndex) {
							cornerElements[i].parentDrawer = drawerFurnitures[j];
							break;
						}
					}
				}else if (cornerElements[i].parentType.Equals("Door")) {
					for (int j = 0; j < doorFurnitures.Length; j++) {
						if (doorFurnitures[j].index == cornerElements[i].parentIndex) {
							cornerElements[i].parentDoor = doorFurnitures[j];
							break;
						}
					}
				}
			} else {
				cornerElementDatasIntern.Add(cornerElements[i]);
			}
		}


		for (int i = 0; i < drawerFurnitures.Length; i++) {
			drawerFurnitures[i].cornerElements = new List<CornerElementData>();
			drawerFurnitures[i].drawerFurnitures = new List<DrawerFurnitureData>();
			drawerFurnitures[i].doorFurnitures = new List<DoorFurnitureData>();

			if (drawerFurnitures[i].parentIndex >= 0) {
				if (drawerFurnitures[i].parentType.Equals("Corner")) {
					for (int j = 0; j < cornerElements.Length; j++) {
						if (cornerElements[j].index == drawerFurnitures[i].parentIndex) {
							drawerFurnitures[i].parentCorner = cornerElements[j];
							break;
						}
					}
				} else if (drawerFurnitures[i].parentType.Equals("Drawer")) {
					for (int j = 0; j < drawerFurnitures.Length; j++) {
						if (drawerFurnitures[j].index == drawerFurnitures[i].parentIndex) {
							drawerFurnitures[i].parentDrawer = drawerFurnitures[j];
							break;
						}
					}
				} else if (drawerFurnitures[i].parentType.Equals("Door")) {
					for (int j = 0; j < doorFurnitures.Length; j++) {
						if (doorFurnitures[j].index == drawerFurnitures[i].parentIndex) {
							drawerFurnitures[i].parentDoor = doorFurnitures[j];
							break;
						}
					}
				}
			} else {
				drawerFurnitureDatasIntern.Add(drawerFurnitures[i]);
			}
		}


		for (int i = 0; i < doorFurnitures.Length; i++) {
			doorFurnitures[i].cornerElements = new List<CornerElementData>();
			doorFurnitures[i].drawerFurnitures = new List<DrawerFurnitureData>();
			doorFurnitures[i].doorFurnitures = new List<DoorFurnitureData>();

			if (doorFurnitures[i].parentIndex >= 0) {
				if (doorFurnitures[i].parentType.Equals("Corner")) {
					for (int j = 0; j < cornerElements.Length; j++) {
						if (cornerElements[j].index == doorFurnitures[i].parentIndex) {
							doorFurnitures[i].parentCorner = cornerElements[j];
							break;
						}
					}
				} else if (doorFurnitures[i].parentType.Equals("Drawer")) {
					for (int j = 0; j < drawerFurnitures.Length; j++) {
						if (drawerFurnitures[j].index == doorFurnitures[i].parentIndex) {
							doorFurnitures[i].parentDrawer = drawerFurnitures[j];
							break;
						}
					}
				} else if (doorFurnitures[i].parentType.Equals("Door")) {
					for (int j = 0; j < doorFurnitures.Length; j++) {
						if (doorFurnitures[j].index == doorFurnitures[i].parentIndex) {
							doorFurnitures[i].parentDoor = doorFurnitures[j];
							break;
						}
					}
				}
			} else {
				doorFurnitureDatasIntern.Add(doorFurnitures[i]);
			}
		}

		////

		for (int i = 0; i < doorFurnitures.Length; i++) {
			doorFurnitures[i].cornerElements = new List<CornerElementData>();
			doorFurnitures[i].drawerFurnitures = new List<DrawerFurnitureData>();
			doorFurnitures[i].doorFurnitures = new List<DoorFurnitureData>();
			if (doorFurnitures[i].parentIndex >= 0) {
				if (doorFurnitures[i].parentType.Equals("Corner")) {
					doorFurnitures[i].parentCorner = cornerElements[doorFurnitures[i].parentIndex];
				} else if (doorFurnitures[i].parentType.Equals("Drawer")) {
					doorFurnitures[i].parentDrawer = drawerFurnitures[doorFurnitures[i].parentIndex];
				} else if (doorFurnitures[i].parentType.Equals("Door")) {
					doorFurnitures[i].parentDoor = doorFurnitures[doorFurnitures[i].parentIndex];
				}
			} else {
				doorFurnitureDatasIntern.Add(doorFurnitures[i]);
			}
		}


		for(int i = 0; i < cornerElements.Length; i++) {
			cornerElements[i].parentCorner?.cornerElements.Add(cornerElements[i]);
			cornerElements[i].parentDoor?.cornerElements.Add(cornerElements[i]);
			cornerElements[i].parentDrawer?.cornerElements.Add(cornerElements[i]);
		}
		for (int i = 0; i < drawerFurnitures.Length; i++) {
			drawerFurnitures[i].parentCorner?.drawerFurnitures.Add(drawerFurnitures[i]);
			drawerFurnitures[i].parentDoor?.drawerFurnitures.Add(drawerFurnitures[i]);
			drawerFurnitures[i].parentDrawer?.drawerFurnitures.Add(drawerFurnitures[i]);
		}
		for (int i = 0; i < doorFurnitures.Length; i++) {
			doorFurnitures[i].parentCorner?.doorFurnitures.Add(doorFurnitures[i]);
			doorFurnitures[i].parentDoor?.doorFurnitures.Add(doorFurnitures[i]);
			doorFurnitures[i].parentDrawer?.doorFurnitures.Add(doorFurnitures[i]);
		}

		this.cornerElementDatas = cornerElementDatasIntern;
		this.drawerFurnitureDatas = drawerFurnitureDatasIntern;
		this.doorFurnitureDatas = doorFurnitureDatasIntern;

	}
	

	public void LoadData() {
		Deserialize();
		for (int i = 0; i < cornerElementDatas.Count; i++) {
			cornerElementDatas[i].LoadData();
		}

		for (int i = 0; i < drawerFurnitureDatas.Count; i++) {
			drawerFurnitureDatas[i].LoadData();
		}

		for (int i = 0; i < doorFurnitureDatas.Count; i++) {
			doorFurnitureDatas[i].LoadData();
		}
	}

}
