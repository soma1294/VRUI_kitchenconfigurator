using Parabox.CSG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour {
    public CompassDirections wallIndex = CompassDirections.NORTH;
    public Window[] windows;

    public GameObject windowers;


    public void GenerateWall(float overlap) {
        switch (wallIndex) {
            case CompassDirections.NORTH:
                windows = Variables.northWindows?.ToArray();
                break;
            case CompassDirections.EAST:
                windows = Variables.eastWindows?.ToArray();
                break;
            case CompassDirections.SOUTH:
                windows = Variables.southWindows?.ToArray();
                break;
            case CompassDirections.WEST:
                windows = Variables.westWindows?.ToArray();
                break;
        }
        if (windows?.Length > 0) {
            Mesh meshe = new Mesh();

            GameObject wall = new GameObject();
            wall.AddComponent<MeshFilter>();
            wall.AddComponent<MeshRenderer>();
            wall.GetComponent<MeshFilter>().mesh = this.GetComponent<MeshFilter>().mesh;

        
            GameObject windowersMesh = new GameObject();
            windowersMesh.AddComponent<MeshFilter>();
            windowersMesh.AddComponent<MeshRenderer>();
            windowersMesh.GetComponent<MeshFilter>().mesh = GenerateWindows(overlap);

            meshe = CSG.Subtract(wall, windowersMesh);

            this.GetComponent<MeshFilter>().mesh = meshe;
            
            windowers.GetComponent<MeshFilter>().mesh = windowersMesh.GetComponent<MeshFilter>().mesh;
            windowers.transform.localScale = new Vector3(1,1,0.1f);
            Destroy(wall);
            Destroy(windowersMesh);
        }
    }

    private Mesh GenerateWindows(float overlap) {
        overlap += 0.0001f;
        Mesh mesh = new Mesh();

        CombineInstance[] combine = new CombineInstance[windows.Length];

        Vector3 wallDimensions = this.transform.lossyScale * 1000;
        for (int i = 0; i < windows.Length; i++) {

            float windowOffsetX = (windows[i].windowRightOffsetinMM + overlap * 1000) / wallDimensions.x;
            float windowOffsetY = (windows[i].windowBottomOffsetinMM + overlap * 1000) / wallDimensions.y;


            float windowWidthPercent = windows[i].windowWidthInMM / wallDimensions.x;
            float windowHeightPercent = windows[i].windowHeightInMM / wallDimensions.y;

            float innerPointPosX = 0.5f - windowOffsetX;
            float innerPointNegX = innerPointPosX - windowWidthPercent;
            float innerPointNegY = -0.5f + windowOffsetY;
            float innerPointPosY = innerPointNegY + windowHeightPercent;

            Vector3 positivePoints = new Vector3(innerPointPosX, innerPointPosY, 0.6f);
            Vector3 negativePoints = new Vector3(innerPointNegX, innerPointNegY, -0.6f);

            combine[i].mesh = Utils.GenerateCube(positivePoints, negativePoints);
        }

        mesh.CombineMeshes(combine, true, false);
        return mesh;

    }
}
