using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFurniture : KitchenFurniture {

    public int[] drawers;

    public int spaceInMM = 10;

    public bool handleLeft = true;

    public override Mesh GenerateFace(int width, int height, int depth) {

        float doorThickness = (float)Variables.wallThicknessInMM / depth;
        float spaceThickness = (float)spaceInMM / height;
        float handleOffset = (float)30 / height;
        float handleOffsetSide = (float)100 / width;

        Vector3 positivePoints = new Vector3(0.5f - spaceThickness, 0.5f - spaceThickness, -0.5f);
        Vector3 negativePoints = new Vector3(-0.5f + spaceThickness, -0.5f + spaceThickness, -0.5f - doorThickness);

        Vector3 handlePosition;

        if (onGround) {
            handlePosition = new Vector3(0, positivePoints.y - handleOffset, negativePoints.z);
        } else {
            handlePosition = new Vector3(0, negativePoints.y + handleOffset, negativePoints.z);
        }

        if (handleLeft) {
            handlePosition.x = negativePoints.x + handleOffsetSide;
        } else {
            handlePosition.x = positivePoints.x - handleOffsetSide;
        }

        PlaceHandles(new Vector3[]{handlePosition});

        Mesh doorMesh = new Mesh();
        doorMesh = GenerateDoor(positivePoints, negativePoints);
        return doorMesh;
    }

    public override void UpdateMaterial() {
        if (Variables.workPlateMaterial != null && !materials[1].Equals(Variables.workPlateMaterial)) {
            materials[1] = Variables.workPlateMaterial;
        }
        if (Variables.furnitureMaterial != null && !materials[0].Equals(Variables.furnitureMaterial)) {
            materials[0] = Variables.furnitureMaterial;
            materials[2] = Variables.furnitureMaterial;
            materials[3] = Variables.furnitureMaterial;
        }
        this.GetComponent<MeshRenderer>().materials = materials;
    }

    private Mesh GenerateDoor(Vector3 positivePoints, Vector3 negativePoints) {

        return Utils.GenerateCube(positivePoints, negativePoints);
    }
}
