using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour {
    public float height = 2f;
    public float width = 2f;
    public float depth = 2f;

    public GameObject wallNorth; //Positive Z
    public GameObject wallSouth; //Negative Z
    public GameObject wallEast; //Positive X
    public GameObject wallWest; //Negative X

    public GameObject floor;

    public GameObject roof;

    private Prefs prefs;
    private float overlap = 0.05f;

	// Use this for initialization
	void Start() {
        prefs = new Prefs();
        prefs.Load();

		height = prefs.roomHeight;
        width = prefs.roomWidth;
        depth = prefs.roomDepth;
		
        generateRoom();

    }

	private void Update() {
		if (Variables.wallMaterial != null && !wallNorth.GetComponent<MeshRenderer>().materials[0].Equals(Variables.wallMaterial)) {
			wallNorth.GetComponent<MeshRenderer>().materials = new Material[] { Variables.wallMaterial };
			wallSouth.GetComponent<MeshRenderer>().materials = new Material[] { Variables.wallMaterial };
			wallEast.GetComponent<MeshRenderer>().materials = new Material[] { Variables.wallMaterial };
			wallWest.GetComponent<MeshRenderer>().materials = new Material[] { Variables.wallMaterial };
		}

		if (Variables.ceilingMaterial != null && !roof.GetComponent<MeshRenderer>().materials[0].Equals(Variables.ceilingMaterial)) {
			roof.GetComponent<MeshRenderer>().materials = new Material[] { Variables.ceilingMaterial };
		}

		if (Variables.floorMaterial != null && !floor.GetComponent<MeshRenderer>().materials[0].Equals(Variables.floorMaterial)) {
			floor.GetComponent<MeshRenderer>().materials = new Material[] { Variables.floorMaterial };
		}
	}


	public void generateRoom() {
        scaleAndPlaceWalls();
        scaleAndPlaceRoof();
        scaleAndPlaceFloor();

    }

    private void scaleAndPlaceWalls() {
        wallNorth.transform.localScale = new Vector3(depth + 2 * overlap, height + 2 * overlap, 2 * overlap);
        wallNorth.transform.localPosition = new Vector3(0, height / 2, width / 2 + overlap);
        if (wallNorth.GetComponent<WallBehaviour>() != null) {
            wallNorth.GetComponent<WallBehaviour>().GenerateWall(overlap);
        }
        


        wallSouth.transform.localScale = new Vector3(depth + 2 * overlap, height + 2 * overlap, 2 * overlap);
        wallSouth.transform.localPosition = new Vector3(0, height / 2, -width / 2 - overlap);
        if (wallSouth.GetComponent<WallBehaviour>() != null) {
            wallSouth.GetComponent<WallBehaviour>().GenerateWall(overlap);
        }
        


        wallEast.transform.localScale = new Vector3(width + 2 * overlap, height + 2 * overlap, 2 * overlap);
        wallEast.transform.localPosition = new Vector3(depth / 2 + overlap, height / 2, 0);
        if (wallEast.GetComponent<WallBehaviour>() != null) {
            wallEast.GetComponent<WallBehaviour>().GenerateWall(overlap);
        }
        


        wallWest.transform.localScale = new Vector3(width + 2 * overlap, height + 2 * overlap, 2 * overlap);
        wallWest.transform.localPosition = new Vector3(-depth / 2 - overlap, height / 2, 0);
        if (wallWest.GetComponent<WallBehaviour>() != null) {
            wallWest.GetComponent<WallBehaviour>().GenerateWall(overlap);
        }
	}

    private void scaleAndPlaceRoof() {
        roof.transform.localScale = new Vector3(depth + 2 * overlap, 2 * overlap, width + 2 * overlap);
        roof.transform.localPosition = new Vector3(0, height + overlap, 0);
    }

    private void scaleAndPlaceFloor() {
        floor.transform.localScale = new Vector3((depth + 2 * overlap) * 0.1f, 1, (width + 2 * overlap) * 0.1f);
        floor.transform.localPosition = new Vector3(0, 0, 0);
    }
}
