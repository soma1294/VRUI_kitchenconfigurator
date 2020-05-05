using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerElement : KitchenElement {

    public override void SetScale() {
        this.GetComponent<BoxCollider>().transform.localScale = new Vector3(Variables.depthsInMM[depthIndex] / 1000f, Variables.heightsInMM[heightIndex] / 1000f, Variables.depthsInMM[depthIndex] / 1000f);
        if (tempParent != null) {
            Vector3 directionToParent = this.transform.position - tempParent.position;
            
            Vector3 directionOfMove = new Vector3(0,0,0);
            if (Utils.CompareVectorDirection(directionToParent, this.transform.right, 0.2f)) {
                directionOfMove.x = 0.5f;
            }else if (Utils.CompareVectorDirection(directionToParent, -this.transform.right, 0.2f)) {
                directionOfMove.x = -0.5f;
            }

            if (Utils.CompareVectorDirection(directionToParent, this.transform.up, 0.2f)) {
                directionOfMove.y = 0.5f;
            }else if (Utils.CompareVectorDirection(directionToParent, -this.transform.up, 0.2f)) {
                directionOfMove.y = -0.5f;
            }

            if (Utils.CompareVectorDirection(directionToParent, this.transform.forward, 0.2f)) {
                directionOfMove.z = 0.5f;
            }else if (Utils.CompareVectorDirection(directionToParent, -this.transform.forward, 0.2f)) {
                directionOfMove.z = -0.5f;
            }
            directionOfMove.Scale(dimensionChange);
            this.transform.position -= directionOfMove;
            dimensionChange = Vector3.zero;
        }
    }

    public override void GenerateCorpus() {

        CombineInstance[] combine = new CombineInstance[3];

        Vector3 positivePoints = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 negativePoints = new Vector3(-0.5f, -0.5f, -0.5f);
        combine[0].mesh = Utils.GenerateCube(positivePoints, negativePoints);

        combine[1].mesh = GenerateWorkplate(Variables.depthsInMM[depthIndex], Variables.heightsInMM[heightIndex], Variables.depthsInMM[depthIndex], false);
        combine[2].mesh = GenerateBase(Variables.depthsInMM[depthIndex], Variables.heightsInMM[heightIndex], Variables.depthsInMM[depthIndex]);

        meshe.CombineMeshes(combine, false, false);
    }

    public override void UpdateMaterial() {
        //Debug.Log("MaterialChange");
        if (Variables.workPlateMaterial != null && !materials[1].Equals(Variables.workPlateMaterial)) {
            materials[1] = Variables.workPlateMaterial;
        }
        if (Variables.furnitureMaterial != null && !materials[0].Equals(Variables.furnitureMaterial)) {
            materials[0] = Variables.furnitureMaterial;
            materials[2] = Variables.furnitureMaterial;
        }
        this.GetComponent<MeshRenderer>().materials = materials;
    }
}
