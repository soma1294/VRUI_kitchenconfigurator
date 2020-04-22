using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KitchenFurniture : KitchenElement{

    public GameObject handlesContainer;

    public override void SetScale() {
        this.GetComponent<BoxCollider>().transform.localScale = new Vector3(Variables.widthsInMM[widthIndex] / 1000f, Variables.heightsInMM[heightIndex] / 1000f, Variables.depthsInMM[depthIndex] / 1000f);
        if (tempParent != null) {
            Vector3 directionToParent = this.transform.position - tempParent.position;

            Vector3 directionOfMove = new Vector3(0, 0, 0);
            if (Utils.CompareVectorDirection(directionToParent, this.transform.right, 0.3f)) {
                directionOfMove.x = 0.5f;
            } else if (Utils.CompareVectorDirection(directionToParent, -this.transform.right, 0.3f)) {
                directionOfMove.x = -0.5f;
            }
            directionOfMove.y = 0.5f;
            if (Utils.CompareVectorDirection(directionToParent, this.transform.up, 0.3f)) {
                directionOfMove.y = 0.5f;
            } else if (Utils.CompareVectorDirection(directionToParent, -this.transform.up, 0.3f)) {
                directionOfMove.y = -0.5f;
            }

            if (Utils.CompareVectorDirection(directionToParent, this.transform.forward, 0.3f)) {
                directionOfMove.z = 0.5f;
            } else if (Utils.CompareVectorDirection(directionToParent, -this.transform.forward, 0.3f)) {
                directionOfMove.z = -0.5f;
            }
            directionOfMove.Scale(dimensionChange);
            this.transform.position -= directionOfMove;
        }
        dimensionChange = Vector3.zero;
    }

    public override void GenerateCorpus() {

        
        float wallThicknessPercentX = (float)Variables.wallThicknessInMM / Variables.widthsInMM[widthIndex];
        float wallThicknessPercentY = (float)Variables.wallThicknessInMM / Variables.heightsInMM[heightIndex];
        float wallThicknessPercentZ = (float)Variables.wallThicknessInMM / Variables.depthsInMM[depthIndex];

        //InnerCorpusPositions
        float innerPointPosX = 0.5f - wallThicknessPercentX;
        float innerPointNegX = -0.5f + wallThicknessPercentX;
        float innerPointPosY = 0.5f - wallThicknessPercentY;
        float innerPointNegY = -0.5f + wallThicknessPercentY;
        float innerPointPosZ = 0.5f - wallThicknessPercentZ;
        float innerPointNegZ = -0.5f + wallThicknessPercentZ;

        CombineInstance[] combine = new CombineInstance[4];
        
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[56];
        Vector2[] uvs = new Vector2[56];
        int[] tri = new int[84];

        //Rear
        vertices[0] = new Vector3(-0.5f, -0.5f, 0.5f);
        vertices[1] = new Vector3(0.5f, -0.5f, 0.5f);
        vertices[2] = new Vector3(0.5f, 0.5f, 0.5f);
        vertices[3] = new Vector3(-0.5f, 0.5f, 0.5f);

        tri[0] = 0;
        tri[1] = 1;
        tri[2] = 2;
        
        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 0;

        uvs[0] = new Vector2(vertices[0].y, vertices[0].x);
        uvs[1] = new Vector2(vertices[1].y, vertices[1].x);
        uvs[2] = new Vector2(vertices[2].y, vertices[2].x);
        uvs[3] = new Vector2(vertices[3].y, vertices[3].x);

        //Right
        vertices[4] = new Vector3(0.5f, -0.5f, -0.5f);
        vertices[5] = new Vector3(0.5f, 0.5f, -0.5f);
        vertices[6] = new Vector3(0.5f, -0.5f, 0.5f);
        vertices[7] = new Vector3(0.5f, 0.5f, 0.5f);

        tri[6] = 6;
        tri[7] = 4;
        tri[8] = 5;
        
        tri[9] = 5;
        tri[10] = 7;
        tri[11] = 6;

        uvs[4] = new Vector2(vertices[4].y, vertices[4].z);
        uvs[5] = new Vector2(vertices[5].y, vertices[5].z);
        uvs[6] = new Vector2(vertices[6].y, vertices[6].z);
        uvs[7] = new Vector2(vertices[7].y, vertices[7].z);

        //Left
        vertices[8] = new Vector3(-0.5f, -0.5f, -0.5f);
        vertices[9] = new Vector3(-0.5f, 0.5f, -0.5f);
        vertices[10] = new Vector3(-0.5f, -0.5f, 0.5f);
        vertices[11] = new Vector3(-0.5f, 0.5f, 0.5f);

        tri[12] = 8;
        tri[13] = 10;
        tri[14] = 11;

        tri[15] = 11;
        tri[16] = 9;
        tri[17] = 8;

        uvs[8] = new Vector2(vertices[8].y, vertices[8].z);
        uvs[9] = new Vector2(vertices[9].y, vertices[9].z);
        uvs[10] = new Vector2(vertices[10].y, vertices[10].z);
        uvs[11] = new Vector2(vertices[11].y, vertices[11].z);

        //InnerRear
        vertices[12] = new Vector3(innerPointNegX, innerPointNegY, innerPointPosZ);
        vertices[13] = new Vector3(innerPointPosX, innerPointNegY, innerPointPosZ);
        vertices[14] = new Vector3(innerPointPosX, innerPointPosY, innerPointPosZ);
        vertices[15] = new Vector3(innerPointNegX, innerPointPosY, innerPointPosZ);

        tri[18] = 14;
        tri[19] = 13;
        tri[20] = 12;

        tri[21] = 12;
        tri[22] = 15;
        tri[23] = 14;

        uvs[12] = new Vector2(vertices[12].y, vertices[12].x);
        uvs[13] = new Vector2(vertices[13].y, vertices[13].x);
        uvs[14] = new Vector2(vertices[14].y, vertices[14].x);
        uvs[15] = new Vector2(vertices[15].y, vertices[15].x);

        //InnerRight
        vertices[16] = new Vector3(innerPointPosX, innerPointNegY, -0.5f);
        vertices[17] = new Vector3(innerPointPosX, innerPointPosY, -0.5f);
        vertices[18] = new Vector3(innerPointPosX, innerPointNegY, innerPointPosZ);
        vertices[19] = new Vector3(innerPointPosX, innerPointPosY, innerPointPosZ);

        tri[24] = 17;
        tri[25] = 16;
        tri[26] = 18;

        tri[27] = 18;
        tri[28] = 19;
        tri[29] = 17;

        uvs[16] = new Vector2(vertices[16].y, vertices[16].z);
        uvs[17] = new Vector2(vertices[17].y, vertices[17].z);
        uvs[18] = new Vector2(vertices[18].y, vertices[18].z);
        uvs[19] = new Vector2(vertices[19].y, vertices[19].z);

        //InnerLeft
        vertices[20] = new Vector3(innerPointNegX, innerPointNegY, -0.5f);
        vertices[21] = new Vector3(innerPointNegX, innerPointPosY, -0.5f);
        vertices[22] = new Vector3(innerPointNegX, innerPointNegY, innerPointPosZ);
        vertices[23] = new Vector3(innerPointNegX, innerPointPosY, innerPointPosZ);

        tri[30] = 23;
        tri[31] = 22;
        tri[32] = 20;
    
        tri[33] = 20;
        tri[34] = 21;
        tri[35] = 23;

        uvs[20] = new Vector2(vertices[20].y, vertices[20].z);
        uvs[21] = new Vector2(vertices[21].y, vertices[21].z);
        uvs[22] = new Vector2(vertices[22].y, vertices[22].z);
        uvs[23] = new Vector2(vertices[23].y, vertices[23].z);

        //InnerTop
        vertices[24] = new Vector3(innerPointPosX, innerPointPosY, -0.5f);
        vertices[25] = new Vector3(innerPointNegX, innerPointPosY, -0.5f);
        vertices[26] = new Vector3(innerPointPosX, innerPointPosY, innerPointPosZ);
        vertices[27] = new Vector3(innerPointNegX, innerPointPosY, innerPointPosZ);

        tri[36] = 27;
        tri[37] = 25;
        tri[38] = 24;

        tri[39] = 24;
        tri[40] = 26;
        tri[41] = 27;

        uvs[24] = new Vector2(vertices[24].x, vertices[24].z);
        uvs[25] = new Vector2(vertices[25].x, vertices[25].z);
        uvs[26] = new Vector2(vertices[26].x, vertices[26].z);
        uvs[27] = new Vector2(vertices[27].x, vertices[27].z);

        //InnerBottom
        vertices[28] = new Vector3(innerPointNegX, innerPointNegY, innerPointPosZ);
        vertices[29] = new Vector3(innerPointPosX, innerPointNegY, innerPointPosZ);
        vertices[30] = new Vector3(innerPointNegX, innerPointNegY, -0.5f);
        vertices[31] = new Vector3(innerPointPosX, innerPointNegY, -0.5f);

        tri[42] = 30;
        tri[43] = 28;
        tri[44] = 29;

        tri[45] = 29;
        tri[46] = 31;
        tri[47] = 30;

        uvs[28] = new Vector2(vertices[28].x, vertices[28].z);
        uvs[29] = new Vector2(vertices[29].x, vertices[29].z);
        uvs[30] = new Vector2(vertices[30].x, vertices[30].z);
        uvs[31] = new Vector2(vertices[31].x, vertices[31].z);

        //FrontRight
        vertices[32] = new Vector3(0.5f, -0.5f, -0.5f);
        vertices[33] = new Vector3(0.5f, 0.5f, -0.5f);
        vertices[34] = new Vector3(innerPointPosX, innerPointNegY, -0.5f);
        vertices[35] = new Vector3(innerPointPosX, innerPointPosY, -0.5f);

        tri[48] = 32;
        tri[49] = 34;
        tri[50] = 35;

        tri[51] = 35;
        tri[52] = 33;
        tri[53] = 32;

        uvs[32] = new Vector2(vertices[32].y, vertices[32].x);
        uvs[33] = new Vector2(vertices[33].y, vertices[33].x);
        uvs[34] = new Vector2(vertices[34].y, vertices[34].x);
        uvs[35] = new Vector2(vertices[35].y, vertices[35].x);

        //FrontTop
        vertices[36] = new Vector3(0.5f, 0.5f, -0.5f);
        vertices[37] = new Vector3(-0.5f, 0.5f, -0.5f);
        vertices[38] = new Vector3(innerPointPosX, innerPointPosY, -0.5f);
        vertices[39] = new Vector3(innerPointNegX, innerPointPosY, -0.5f);

        tri[54] = 38;
        tri[55] = 39;
        tri[56] = 37;

        tri[57] = 37;
        tri[58] = 36;
        tri[59] = 38;

        uvs[36] = new Vector2(vertices[36].y, vertices[36].x);
        uvs[37] = new Vector2(vertices[37].y, vertices[37].x);
        uvs[38] = new Vector2(vertices[38].y, vertices[38].x);
        uvs[39] = new Vector2(vertices[39].y, vertices[39].x);

        //FrontBottom
        vertices[40] = new Vector3(-0.5f, -0.5f, -0.5f);
        vertices[41] = new Vector3(0.5f, -0.5f, -0.5f);
        vertices[42] = new Vector3(innerPointNegX, innerPointNegY, -0.5f);
        vertices[43] = new Vector3(innerPointPosX, innerPointNegY, -0.5f);

        tri[60] = 41;
        tri[61] = 40;
        tri[62] = 42;

        tri[63] = 42;
        tri[64] = 43;
        tri[65] = 41;

        uvs[40] = new Vector2(vertices[40].y, vertices[40].x);
        uvs[41] = new Vector2(vertices[41].y, vertices[41].x);
        uvs[42] = new Vector2(vertices[42].y, vertices[42].x);
        uvs[43] = new Vector2(vertices[43].y, vertices[43].x);

        //FrontLeft
        vertices[44] = new Vector3(-0.5f, -0.5f, -0.5f);
        vertices[45] = new Vector3(-0.5f, 0.5f, -0.5f);
        vertices[46] = new Vector3(innerPointNegX, innerPointNegY, -0.5f);
        vertices[47] = new Vector3(innerPointNegX, innerPointPosY, -0.5f);

        tri[66] = 46;
        tri[67] = 44;
        tri[68] = 45;

        tri[69] = 45;
        tri[70] = 47;
        tri[71] = 46;

        uvs[44] = new Vector2(vertices[44].y, vertices[44].x);
        uvs[45] = new Vector2(vertices[45].y, vertices[45].x);
        uvs[46] = new Vector2(vertices[46].y, vertices[46].x);
        uvs[47] = new Vector2(vertices[47].y, vertices[47].x);

        //Top
        vertices[48] = new Vector3(0.5f, 0.5f, -0.5f);
        vertices[49] = new Vector3(-0.5f, 0.5f, -0.5f);
        vertices[50] = new Vector3(0.5f, 0.5f, 0.5f);
        vertices[51] = new Vector3(-0.5f, 0.5f, 0.5f);

        tri[72] = 48;
        tri[73] = 49;
        tri[74] = 51;

        tri[75] = 51;
        tri[76] = 50;
        tri[77] = 48;

        uvs[48] = new Vector2(vertices[48].x, vertices[48].z);
        uvs[49] = new Vector2(vertices[49].x, vertices[49].z);
        uvs[50] = new Vector2(vertices[50].x, vertices[50].z);
        uvs[51] = new Vector2(vertices[51].x, vertices[51].z);

        //Bottom
        vertices[52] = new Vector3(-0.5f, -0.5f, -0.5f);
        vertices[53] = new Vector3(0.5f, -0.5f, -0.5f);
        vertices[54] = new Vector3(-0.5f, -0.5f, 0.5f);
        vertices[55] = new Vector3(0.5f, -0.5f, 0.5f);

        tri[78] = 55;
        tri[79] = 54;
        tri[80] = 52;

        tri[81] = 52;
        tri[82] = 53;
        tri[83] = 55;

        uvs[52] = new Vector2(vertices[52].x, vertices[52].z);
        uvs[53] = new Vector2(vertices[53].x, vertices[53].z);
        uvs[54] = new Vector2(vertices[54].x, vertices[54].z);
        uvs[55] = new Vector2(vertices[55].x, vertices[55].z);


        mesh.vertices = vertices;
        mesh.triangles = tri;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        combine[0].mesh = mesh;

        combine[1].mesh = GenerateWorkplate(Variables.widthsInMM[widthIndex], Variables.heightsInMM[heightIndex], Variables.depthsInMM[depthIndex]);
        combine[2].mesh = GenerateBase(Variables.widthsInMM[widthIndex], Variables.heightsInMM[heightIndex], Variables.depthsInMM[depthIndex]);
        combine[3].mesh = GenerateFace(Variables.widthsInMM[widthIndex], Variables.heightsInMM[heightIndex], Variables.depthsInMM[depthIndex]);


        meshe.CombineMeshes(combine, false, false);
    }

    public abstract Mesh GenerateFace(int width, int height, int depth);

    public void PlaceHandles(Vector3[] positions) {
        foreach(Transform handle in handlesContainer.transform) {
            Destroy(handle.gameObject);
        }
        GameObject currentHandle;
        for (int i = 0; i < positions.Length; i++) {
            currentHandle = Instantiate(handlesPrefab, handlesContainer.transform);
            currentHandle.transform.localPosition = positions[i];
            currentHandle.transform.localScale = new Vector3(1.0f / transform.localScale.x, 1.0f / transform.localScale.y, 1.0f / transform.localScale.z);
        }
    }
}
