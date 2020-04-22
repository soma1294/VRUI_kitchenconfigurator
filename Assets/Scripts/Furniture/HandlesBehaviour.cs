using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlesBehaviour : MonoBehaviour {

    public Material[] materials;

    // Use this for initialization
    void Start () {
        materials = this.GetComponent<MeshRenderer>().materials;
    }
	
	// Update is called once per frame
	void Update () {
        if (Variables.handlesMaterial != null && !materials[0].Equals(Variables.handlesMaterial)) {
            materials[0] = Variables.handlesMaterial;
            this.GetComponent<MeshRenderer>().materials = materials;
        }
    }
}
