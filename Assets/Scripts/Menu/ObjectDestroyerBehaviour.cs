using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;

public class ObjectDestroyerBehaviour : MonoBehaviour {

    private List<Snappable> destroyTargets = new List<Snappable>();

	
	// Update is called once per frame
	void Update () {
        if (destroyTargets.Count > 0) {
            foreach(Snappable destroyTarget in destroyTargets) {
                if (!destroyTarget.inHand) {

                    destroyTargets.Remove(destroyTarget);
                    Destroy(destroyTarget.gameObject);
                }
            }
        }
	}

    //TODO: This is a temporary fix for the deletion of furniture. Change later. 
    private void OnTriggerEnter(Collider other) {
        Snappable destroyTargetIn = other.gameObject.GetComponent<Snappable>();
        if (destroyTargetIn)
        {
            Destroy(destroyTargetIn.gameObject);
        }
        /*
        if (destroyTargetIn && destroyTargetIn.inHand && !destroyTargetIn.parentInHand) {
            destroyTargets.Add(destroyTargetIn);
			//destroyTargetIn.GetComponent<Interactable>().highlightOnHover = false;
        }
        */
    }

    private void OnTriggerExit(Collider other) {
        /*
        Snappable destroyTargetOut = other.gameObject.GetComponent<Snappable>();
        if (destroyTargetOut && destroyTargetOut.inHand && !destroyTargetOut.parentInHand) {
            destroyTargets.Remove(destroyTargetOut);
			//destroyTargetOut.GetComponent<Interactable>().highlightOnHover = true;
		}
        */
    }
}
