using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPreviewBehaviour : MonoBehaviour {

    public GameObject drawerObject;
    public GameObject cornerObject;
    public GameObject doorObject;


    private GameObject objectInPreview;

    // Use this for initialization
    void Start () {
        objectInPreview = Instantiate(cornerObject, transform);
	}

    public void ChangeObjectToDrawers(bool change) {
        if (change) {
			bool onGround = objectInPreview.GetComponent<KitchenElement>().onGround;
			Destroy(objectInPreview);
            objectInPreview = Instantiate(drawerObject, transform);
			objectInPreview.GetComponent<KitchenElement>().onGround = onGround;
			objectInPreview.GetComponent<KitchenElement>().redraw = true;
        }
    }

    public void ChangeObjectToDoor(bool change) {
        if (change) {
			bool onGround = objectInPreview.GetComponent<KitchenElement>().onGround;
			Destroy(objectInPreview);
            objectInPreview = Instantiate(doorObject, transform);
			objectInPreview.GetComponent<KitchenElement>().onGround = onGround;
			objectInPreview.GetComponent<KitchenElement>().redraw = true;
        }
    }

    public void ChangeObjectToCorner(bool change) {
        if (change) {
			bool onGround = objectInPreview.GetComponent<KitchenElement>().onGround;
			Destroy(objectInPreview);
            objectInPreview = Instantiate(cornerObject, transform);
			objectInPreview.GetComponent<KitchenElement>().onGround = onGround;
			objectInPreview.GetComponent<KitchenElement>().redraw = true;
        }
    }

    public void SetObjectInPreviewOnGround(bool onGround) {
        objectInPreview.GetComponent<KitchenElement>().onGround = onGround;
        objectInPreview.GetComponent<KitchenElement>().redraw = true;
    }

    public void SetWidthIndex(int widthIndex) {
        objectInPreview.GetComponent<KitchenElement>().widthIndex = widthIndex;
        objectInPreview.GetComponent<KitchenElement>().redraw = true;
    }

    public void SetDrawerConfig(DrawerFurnitureConfig drawerConfig) {
        if (objectInPreview.GetComponent<DrawerFurniture>() != null) {
            objectInPreview.GetComponent<DrawerFurniture>().drawers = drawerConfig.drawerConfig;
            objectInPreview.GetComponent<DrawerFurniture>().redraw = true;
        }
    }

	public void SetDoorHandle(bool handleLeft){
		if (objectInPreview.GetComponent<DoorFurniture>() != null) {
			objectInPreview.GetComponent<DoorFurniture>().handleLeft = handleLeft;
			objectInPreview.GetComponent<DoorFurniture>().redraw = true;
		}
	}

	private void OnTriggerExit(Collider other) {
        KitchenElement objectExiting = other.gameObject.GetComponent<KitchenElement>();
        if (objectExiting != null && other.gameObject.Equals(objectInPreview)) {
            //When an object exits the preview a new object with the same properties is created
            objectExiting.redraw = false;
            objectInPreview = Instantiate(other.gameObject, transform);
            if (objectInPreview.GetComponent<KitchenElement>().onGround) {
                objectInPreview.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + Variables.baseHeightInMM / 1000f, this.transform.position.z);
            } else {
                objectInPreview.transform.position = this.transform.position;
            }
            
            objectInPreview.transform.rotation = this.transform.rotation;
        }
    }
}
