using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KitchenElement : MonoBehaviour {

    public int heightIndex = 5;
    public int widthIndex = 4;
    public int depthIndex = 1;
    public int offFloorHeightIndex = 0;

    public bool redraw = true;
    public bool onGround = true;

    public Vector3 dimensionChange = Vector3.zero;

    public Transform tempParent;

    public Mesh meshe;

    public Material[] materials;

    private int previousBaseHeight;

    public GameObject handlesPrefab;

    // Use this for initialization
    void Start() {
        meshe = new Mesh();
        this.GetComponent<MeshFilter>().mesh = meshe;
        SetScale();
        GenerateCorpus();
        previousBaseHeight = Variables.baseHeightInMM;
        materials = this.GetComponent<MeshRenderer>().materials;
    }

    // Update is called once per frame
    void Update() {

		if(materials == null) {
			materials = this.GetComponent<MeshRenderer>().materials;
		}
        //The depth is determined by wether the object is on the ground or not
        depthIndex = onGround ? 1 : 0;

        if (Variables.handlesPrefab != null && !handlesPrefab.Equals(Variables.handlesPrefab)) {
            handlesPrefab = Variables.handlesPrefab;
            redraw = true;
            Debug.Log("HandlesChanged");
        }
            
        if ((Variables.workPlateMaterial != null && !materials[1].Equals(Variables.workPlateMaterial)) || (Variables.furnitureMaterial != null && !materials[0].Equals(Variables.furnitureMaterial))) {
            UpdateMaterial();
        }
        if (redraw) {
            tempParent = this.transform.parent;
            this.transform.parent = null;
            SetScale();
            GenerateCorpus();
            //Debug.Log(gameObject.name + ": " + (tempParent == null) + " " + (tempParent != null) + " " + (tempParent.parent.gameObject.name) + " " + onGround + " " + (previousBaseHeight != Variables.baseHeightInMM));
            if ((tempParent == null || (tempParent != null && tempParent.parent.gameObject.GetComponent<KitchenElement>() == null)) 
                && onGround && previousBaseHeight != Variables.baseHeightInMM) {
                float changeAmount = (Variables.baseHeightInMM - previousBaseHeight) / 1000f;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + changeAmount, this.transform.position.z);
                previousBaseHeight = Variables.baseHeightInMM;
            }
            this.transform.parent = tempParent;
            tempParent = null;

        /*
            KitchenElement[] children = this.GetComponentsInChildren<KitchenElement>();
            for (int i = 0; i < children.Length; i++) {
                children[i].redraw = true;
            }
        */
            redraw = false;
        }
    }

    public abstract void SetScale();

    public abstract void GenerateCorpus();

    public abstract void UpdateMaterial();

    public Mesh GenerateWorkplate(int width, int height, int depth, bool overhang = true) {
        Mesh meshWP = new Mesh();
        
        if (onGround) {


            float wpThicknessPercentX = (float)Variables.workPlateOverhangInMM / width;
            float wpThicknessPercentY = (float)Variables.workPlateThicknessInMM / height;
            float wpThicknessPercentZ = (float)Variables.workPlateOverhangInMM / depth;


            Vector3 positivePoints = new Vector3(0.5f, 0.5f + wpThicknessPercentY , 0.5f);
            Vector3 negativePoints = new Vector3(-0.5f, 0.5f, -0.5f);
            if (overhang) {
                if (!CheckForNeighbour(this.transform.right)) {
                    positivePoints.x += wpThicknessPercentX;
                }
                if (!CheckForNeighbour(-this.transform.right)) {
                    negativePoints.x -= wpThicknessPercentX;
                }
                if (!CheckForNeighbour(this.transform.forward)) {
                    positivePoints.z += wpThicknessPercentZ;
                }
                if (!CheckForNeighbour(-this.transform.forward)) {
                    negativePoints.z -= wpThicknessPercentZ;
                }
            }
            
            meshWP = Utils.GenerateCube(positivePoints, negativePoints);

        } else {
            Vector3[] vertices = new Vector3[1];

            vertices[0] = new Vector3(0, 0, 0);

            meshWP.vertices = vertices;

            int[] tri = new int[3];
            
            tri[0] = 0;
            tri[1] = 0;
            tri[2] = 0;

            meshWP.triangles = tri;

            meshWP.RecalculateNormals();
        }
        return meshWP;
    }

    public Mesh GenerateBase(int width, int height, int depth) {
        Mesh meshWP = new Mesh();
        Vector3[] vertices;
        int[] tri;
        if (onGround) {

            float baseHeightPercent = (float)Variables.baseHeightInMM / height;
            float baseInsetPercentX = (float)Variables.baseInsetInMM / width;
            float baseInsetPercentZ = (float)Variables.baseInsetInMM / depth;


            Vector3 positivePoints = new Vector3(0.5f, -0.5f, 0.5f);
            Vector3 negativePoints = new Vector3(-0.5f, -0.5f-baseHeightPercent, -0.5f);

            if (!CheckForNeighbour(this.transform.right)) {
                positivePoints.x -= baseInsetPercentX;
            }
            if (!CheckForNeighbour(-this.transform.right)) {
                negativePoints.x += baseInsetPercentX;
            }
            if (!CheckForNeighbour(this.transform.forward)) {
                positivePoints.z -= baseInsetPercentZ;
            }
            if (!CheckForNeighbour(-this.transform.forward)) {
                negativePoints.z += baseInsetPercentZ;
            }

            meshWP = Utils.GenerateCube(positivePoints, negativePoints);

            
        } else {
            vertices = new Vector3[1];

            vertices[0] = new Vector3(0, 0, 0);

            meshWP.vertices = vertices;

            tri = new int[3];
            //wpRear
            tri[0] = 0;
            tri[1] = 0;
            tri[2] = 0;

            meshWP.triangles = tri;

            meshWP.RecalculateNormals();
        }
        return meshWP;
    }

    public bool CheckForNeighbour(Vector3 checkDirection) {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, checkDirection, out hit, 0.5f, layerMask)) {
            return true;
        }
        return false;
    }

}
