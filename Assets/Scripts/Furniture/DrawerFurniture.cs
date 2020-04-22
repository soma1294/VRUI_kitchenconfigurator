using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerFurniture : KitchenFurniture {

    public int[] drawers;

    public int spaceInMM = 10;

    public override Mesh GenerateFace(int width, int height, int depth) {

        float drawerThickness = (float)Variables.wallThicknessInMM / depth;
        float spaceThickness = (float)spaceInMM / height;
        float handleOffset = (float)30/ height;

        CombineInstance[] combine = new CombineInstance[drawers.Length];

        Vector3[] handlePositions = new Vector3[drawers.Length];

        int drawersPos = 0;

        for (int i = 0; i < drawers.Length; i++) {

            float drawerHeight = drawers[i] / 6f;

            float drawerOffset = drawersPos / 6f;

            Vector3 positivePoints = new Vector3(0.5f - spaceThickness, 0.5f - drawerOffset - spaceThickness, -0.5f);
            Vector3 negativePoints = new Vector3(-0.5f + spaceThickness, 0.5f - drawerOffset - drawerHeight + spaceThickness, -0.5f - drawerThickness);

            combine[i].mesh = GenerateDrawer(positivePoints, negativePoints);
            drawersPos += drawers[i];

            handlePositions[i] = new Vector3(0, positivePoints.y - handleOffset, negativePoints.z);
        }

        PlaceHandles(handlePositions);

        Mesh drawerMesh = new Mesh();
        drawerMesh.CombineMeshes(combine, true, false);
        return drawerMesh;
    }

    public override void UpdateMaterial() {
        Debug.Log("MaterialChange");
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

    private Mesh GenerateDrawer(Vector3 positivePoints, Vector3 negativePoints) {

        return Utils.GenerateCube(positivePoints, negativePoints);
    }
}
