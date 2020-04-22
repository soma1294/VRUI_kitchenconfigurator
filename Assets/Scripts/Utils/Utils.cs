using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    static public bool CompareVectorDirection(Vector3 vec1, Vector3 vec2, float tolerance = 0.01f) {
        return Vector3.Dot(vec1.normalized, vec2.normalized) > 1f - tolerance && Vector3.Dot(vec1.normalized, vec2.normalized) <= 1f;
    }

    static public bool CompareFloats(float f1, float f2, float tolerance = 0.01f) {
        return Mathf.Abs(f1 - f2) < tolerance;
    }

    static public Vector3 getAdjustedScaleFromAngle(Transform target, Vector3 originalScale, float tolerance = 0.01f) {
        //Set target scale dependent on orientation
        Vector3 adjustedScale = Vector3.zero;

        if ((CompareFloats(target.forward.z, 1f, tolerance) || CompareFloats(target.forward.z, -1f, tolerance)) && (CompareFloats(target.right.x, 1f, tolerance) || CompareFloats(target.right.x, -1f, tolerance))) {
            //forward(z)right(x)
            adjustedScale.x = originalScale.x;
            adjustedScale.y = originalScale.y;
            adjustedScale.z = originalScale.z;
        }

        if ((CompareFloats(target.forward.x, 1f, tolerance) || CompareFloats(target.forward.x, -1f, tolerance)) && (CompareFloats(target.right.z, 1f, tolerance) || CompareFloats(target.right.z, -1f, tolerance))) {
            //forward(x)right(z)
            adjustedScale.x = originalScale.z;
            adjustedScale.y = originalScale.y;
            adjustedScale.z = originalScale.x;
        }

        if ((CompareFloats(target.forward.y, 1f, tolerance) || CompareFloats(target.forward.y, -1f, tolerance)) && (CompareFloats(target.right.z, 1f, tolerance) || CompareFloats(target.right.z, -1f, tolerance))) {
            //forward(y)right(z)
            adjustedScale.x = originalScale.y;
            adjustedScale.y = originalScale.z;
            adjustedScale.z = originalScale.x;
        }

        if ((CompareFloats(target.forward.y, 1f, tolerance) || CompareFloats(target.forward.y, -1f, tolerance)) && (CompareFloats(target.right.x, 1f, tolerance) || CompareFloats(target.right.x, -1f, tolerance))) {
            //forward(y)right(x)
            adjustedScale.x = originalScale.x;
            adjustedScale.y = originalScale.z;
            adjustedScale.z = originalScale.y;
        }

        if ((CompareFloats(target.forward.z, 1f, tolerance) || CompareFloats(target.forward.z, -1f, tolerance)) && (CompareFloats(target.right.y, 1f, tolerance) || CompareFloats(target.right.y, -1f, tolerance))) {
            //forward(z)right(y)
            adjustedScale.x = originalScale.y;
            adjustedScale.y = originalScale.x;
            adjustedScale.z = originalScale.z;
        }

        if ((CompareFloats(target.forward.x, 1f, tolerance) || CompareFloats(target.forward.x, -1f, tolerance)) && (CompareFloats(target.right.y, 1f, tolerance) || CompareFloats(target.right.y, -1f, tolerance))) {
            //forward(x)right(y)
            adjustedScale.x = originalScale.z;
            adjustedScale.y = originalScale.x;
            adjustedScale.z = originalScale.y;
        }

        return adjustedScale;
    }

    static public Mesh GenerateCube(Vector3 positivePoints, Vector3 negativePoints) {

        Mesh cubeMesh = new Mesh();
        Vector3[] vertices = new Vector3[24];
        Vector2[] uvs = new Vector2[24];
        int[] tri = new int[36];

        //Front
        vertices[0] = new Vector3(negativePoints.x, negativePoints.y, negativePoints.z);
        vertices[1] = new Vector3(positivePoints.x, negativePoints.y, negativePoints.z);
        vertices[2] = new Vector3(positivePoints.x, positivePoints.y, negativePoints.z);
        vertices[3] = new Vector3(negativePoints.x, positivePoints.y, negativePoints.z);

        tri[0] = 1;
        tri[1] = 0;
        tri[2] = 3;

        tri[3] = 3;
        tri[4] = 2;
        tri[5] = 1;

        uvs[0] = new Vector2(vertices[0].y, vertices[0].x);
        uvs[1] = new Vector2(vertices[1].y, vertices[1].x);
        uvs[2] = new Vector2(vertices[2].y, vertices[2].x);
        uvs[3] = new Vector2(vertices[3].y, vertices[3].x);

        //Right

        vertices[4] = new Vector3(positivePoints.x, negativePoints.y, positivePoints.z);
        vertices[5] = new Vector3(positivePoints.x, positivePoints.y, positivePoints.z);
        vertices[6] = new Vector3(positivePoints.x, negativePoints.y, negativePoints.z);
        vertices[7] = new Vector3(positivePoints.x, positivePoints.y, negativePoints.z);

        tri[6] = 4;
        tri[7] = 6;
        tri[8] = 7;

        tri[9] = 7;
        tri[10] = 5;
        tri[11] = 4;

        uvs[4] = new Vector2(vertices[4].y, vertices[4].z);
        uvs[5] = new Vector2(vertices[5].y, vertices[5].z);
        uvs[6] = new Vector2(vertices[6].y, vertices[6].z);
        uvs[7] = new Vector2(vertices[7].y, vertices[7].z);


        //Rear
        vertices[8] = new Vector3(negativePoints.x, negativePoints.y, positivePoints.z);
        vertices[9] = new Vector3(negativePoints.x, positivePoints.y, positivePoints.z);
        vertices[10] = new Vector3(positivePoints.x, negativePoints.y, positivePoints.z);
        vertices[11] = new Vector3(positivePoints.x, positivePoints.y, positivePoints.z);

        tri[12] = 8;
        tri[13] = 10;
        tri[14] = 11;

        tri[15] = 11;
        tri[16] = 9;
        tri[17] = 8;

        uvs[8] = new Vector2(vertices[8].y, vertices[8].x);
        uvs[9] = new Vector2(vertices[9].y, vertices[9].x);
        uvs[10] = new Vector2(vertices[10].y, vertices[10].x);
        uvs[11] = new Vector2(vertices[11].y, vertices[11].x);


        //Left
        vertices[12] = new Vector3(negativePoints.x, negativePoints.y, negativePoints.z);
        vertices[13] = new Vector3(negativePoints.x, positivePoints.y, negativePoints.z);
        vertices[14] = new Vector3(negativePoints.x, negativePoints.y, positivePoints.z);
        vertices[15] = new Vector3(negativePoints.x, positivePoints.y, positivePoints.z);

        tri[18] = 12;
        tri[19] = 14;
        tri[20] = 15;

        tri[21] = 15;
        tri[22] = 13;
        tri[23] = 12;

        uvs[12] = new Vector2(vertices[12].y, vertices[12].z);
        uvs[13] = new Vector2(vertices[13].y, vertices[13].z);
        uvs[14] = new Vector2(vertices[14].y, vertices[14].z);
        uvs[15] = new Vector2(vertices[15].y, vertices[15].z);

        //Top
        vertices[16] = new Vector3(positivePoints.x, positivePoints.y, negativePoints.z);
        vertices[17] = new Vector3(negativePoints.x, positivePoints.y, negativePoints.z);
        vertices[18] = new Vector3(positivePoints.x, positivePoints.y, positivePoints.z);
        vertices[19] = new Vector3(negativePoints.x, positivePoints.y, positivePoints.z);

        tri[24] = 16;
        tri[25] = 17;
        tri[26] = 19;

        tri[27] = 19;
        tri[28] = 18;
        tri[29] = 16;

        uvs[16] = new Vector2(vertices[16].x, vertices[16].z);
        uvs[17] = new Vector2(vertices[17].x, vertices[17].z);
        uvs[18] = new Vector2(vertices[18].x, vertices[18].z);
        uvs[19] = new Vector2(vertices[19].x, vertices[19].z);

        //Bottom
        vertices[20] = new Vector3(negativePoints.x, negativePoints.y, negativePoints.z);
        vertices[21] = new Vector3(positivePoints.x, negativePoints.y, negativePoints.z);
        vertices[22] = new Vector3(negativePoints.x, negativePoints.y, positivePoints.z);
        vertices[23] = new Vector3(positivePoints.x, negativePoints.y, positivePoints.z);

        tri[30] = 23;
        tri[31] = 22;
        tri[32] = 20;

        tri[33] = 20;
        tri[34] = 21;
        tri[35] = 23;

        uvs[20] = new Vector2(vertices[20].x, vertices[20].z);
        uvs[21] = new Vector2(vertices[21].x, vertices[21].z);
        uvs[22] = new Vector2(vertices[22].x, vertices[22].z);
        uvs[23] = new Vector2(vertices[23].x, vertices[23].z);


        cubeMesh.vertices = vertices;
        cubeMesh.triangles = tri;
        cubeMesh.uv = uvs;

        cubeMesh.RecalculateNormals();

        return cubeMesh;
    }

	public static Material LoadMaterialByName(string matName, string folder) {
		Material mat = Resources.Load<Material>("Materials/" + folder + "/" + matName);
		if (mat == null) {
			Debug.LogError("Can't find material: " + matName);
			return null;
		} else
			return mat;
	}

	public static GameObject LoadPrefabByName(string prefabName, string folder) {
		GameObject prefab = Resources.Load<GameObject>("Prefabs/" + folder + "/" + prefabName);
		if (prefab == null) {
			Debug.LogError("Can't find prefab: " + prefabName);
			return null;
		} else
			return prefab;
	}
}
