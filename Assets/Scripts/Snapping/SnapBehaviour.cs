using System.Collections;
using System.Collections.Generic;
//using Valve.VR.InteractionSystem;
using UnityEngine;

public class SnapBehaviour : MonoBehaviour {

    public GameObject children;
    public GameObject orientationArrow;
	[Header("Set Position on Axis (1 is true)")]
	public Vector3Int setPosition;
    public bool offSetBase = false;

    private bool enableSnap = true;

    private Snappable snapTarget;

    private void OnTriggerEnter(Collider other) {
        if ((enableSnap && !snapTarget) || (enableSnap && snapTarget && !snapTarget.inHand)) {
            Snappable snapTargetIn = other.gameObject.GetComponent<Snappable>();
            if (snapTargetIn && snapTargetIn.inHand && !snapTargetIn.parentInHand) {
                snapTarget = snapTargetIn;
                if (orientationArrow != null) {
                    this.orientationArrow.gameObject.SetActive(false);
                }
            }
        }
    }

    private void FixedUpdate() {
        if (enableSnap && snapTarget && snapTarget.inHand && !snapTarget.parentInHand) {
            //Next line is to preserve rotation
            //snapTarget.snapRotation.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, 90 * Mathf.RoundToInt(snapTarget.transform.rotation.eulerAngles.y / 90f), this.transform.rotation.eulerAngles.z);
            snapTarget.snapRotation.eulerAngles = new Vector3(0, 90 * Mathf.RoundToInt(snapTarget.transform.rotation.eulerAngles.y / 90f), 0);

            float thisScaleX = 0;
            float thisScaleY = 0;
            float thisScaleZ = 0;

            float targetScaleX = 0;
            float targetScaleY = 0;
            float targetScaleZ = 0;

            /* if the forward Vector of the Object to snap onto is facing in the x-direction:
                * it is rotated to the right or left
                * so that the z-scale is going in the x-direction
                * x-scale is going in the z-direction
                * else if the forward Vector of the Object to snap onto is facing in the z-direction:
                * it is not rotated or facing backwards
                * so that the z-scale is going in the z-direction
                * x-scale is going in the x-direction
                */
            if (Utils.CompareFloats(this.transform.forward.x, 1f) || Utils.CompareFloats(this.transform.forward.x, -1f)) {
                thisScaleX = this.transform.lossyScale.z;
                thisScaleY = this.transform.lossyScale.y;
                thisScaleZ = this.transform.lossyScale.x;
            } else if (Utils.CompareFloats(this.transform.forward.z, 1f) || Utils.CompareFloats(this.transform.forward.z, -1f)) {
                thisScaleX = this.transform.lossyScale.x;
                thisScaleY = this.transform.lossyScale.y;
                thisScaleZ = this.transform.lossyScale.z;
            }


            /* if the forward Vector of the SnapTarget is roughly facing in the z-direction and the right Vector is rougly facing in the x-direction:
                * it is not rotated or facing backwards
                * so that the z-scale is going in the z-direction
                * x-scale is going in the x-direction
                */
            if ((Utils.CompareFloats(snapTarget.transform.forward.z, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.forward.z, -1f, 0.3f)) && (Utils.CompareFloats(snapTarget.transform.right.x, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.right.x, -1f, 0.3f))) {//forward(z)right(x)
                targetScaleX = snapTarget.transform.lossyScale.x;
                targetScaleY = snapTarget.transform.lossyScale.y;
                targetScaleZ = snapTarget.transform.lossyScale.z;
            }

            /* if the forward Vector of the SnapTarget is roughly facing in the x-direction and the right Vector is rougly facing in the z-direction:
                * it is rotated to the right or left
                * so that the z-scale is going in the x-direction
                * x-scale is going in the z-direction
                */
            if ((Utils.CompareFloats(snapTarget.transform.forward.x, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.forward.x, -1f, 0.3f)) && (Utils.CompareFloats(snapTarget.transform.right.z, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.right.z, -1f, 0.3f))) {//forward(x)right(z)
                targetScaleX = snapTarget.transform.lossyScale.z;
                targetScaleY = snapTarget.transform.lossyScale.y;
                targetScaleZ = snapTarget.transform.lossyScale.x;
            }


            /* if the forward Vector of the SnapTarget is roughly facing in the y-direction and the right Vector is rougly facing in the z-direction:
                * it is rotated up or down and to the right or left
                * so that the z-scale is going in the y-direction
                * x-scale is going in the z-direction
                */
            if ((Utils.CompareFloats(snapTarget.transform.forward.y, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.forward.y, -1f, 0.3f)) && (Utils.CompareFloats(snapTarget.transform.right.z, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.right.z, -1f, 0.3f))) {//forward(y)right(z)                                                                                                                                                                                                                                                                    */
                targetScaleX = snapTarget.transform.lossyScale.z;
                targetScaleY = snapTarget.transform.lossyScale.y;
                targetScaleZ = snapTarget.transform.lossyScale.x;
            }

            /* if the forward Vector of the SnapTarget is roughly facing in the y-direction and the right Vector is rougly facing in the x-direction:
                * it is rotated up or down
                * so that the z-scale is going in the y-direction
                * x-scale is going in the x-direction
                */
            if ((Utils.CompareFloats(snapTarget.transform.forward.y, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.forward.y, -1f, 0.3f)) && (Utils.CompareFloats(snapTarget.transform.right.x, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.right.x, -1f, 0.3f))) {//forward(y)right(x)
				targetScaleX = snapTarget.transform.lossyScale.x;
                targetScaleY = snapTarget.transform.lossyScale.y;
                targetScaleZ = snapTarget.transform.lossyScale.z;
            }

            /* if the forward Vector of the SnapTarget is roughly facing in the z-direction and the right Vector is rougly facing in the y-direction:
                * it is on its side
                * so that the z-scale is going in the z-direction
                * x-scale is going in the y-direction
                */
            if ((Utils.CompareFloats(snapTarget.transform.forward.z, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.forward.z, -1f, 0.3f)) && (Utils.CompareFloats(snapTarget.transform.right.y, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.right.y, -1f, 0.3f))) {//forward(z)right(y)
                targetScaleX = snapTarget.transform.lossyScale.x;
                targetScaleY = snapTarget.transform.lossyScale.y;
                targetScaleZ = snapTarget.transform.lossyScale.z;
            }

            /* if the forward Vector of the SnapTarget is roughly facing in the x-direction and the right Vector is rougly facing in the y-direction:
                * it is on its side and rotated to the right or left
                * so that the z-scale is going in the x-direction
                * x-scale is going in the y-direction
                */
            if ((Utils.CompareFloats(snapTarget.transform.forward.x, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.forward.x, -1f, 0.3f)) && (Utils.CompareFloats(snapTarget.transform.right.y, 1f, 0.3f) || Utils.CompareFloats(snapTarget.transform.right.y, -1f, 0.3f))) {//forward(x)right(y)
                targetScaleX = snapTarget.transform.lossyScale.z;
                targetScaleY = snapTarget.transform.lossyScale.y;
                targetScaleZ = snapTarget.transform.lossyScale.x;
            }



            float snapPositionX = 0;
            float snapPositionY = 0;
            float snapPositionZ = 0;

            Vector3 colliderDirection = (this.transform.position - this.transform.parent.position).normalized;
            //Rework
            if (Utils.CompareVectorDirection(colliderDirection, this.transform.right, 0.3f) || Utils.CompareVectorDirection(colliderDirection, -this.transform.forward, 0.3f)) {
                snapPositionY = (this.transform.position.y - thisScaleY / 2f + targetScaleY / 2f);
                if (Utils.CompareFloats(this.transform.forward.z, 1f)) {
                    snapPositionX = (this.transform.position.x - thisScaleX / 2f + targetScaleX / 2f);
                    snapPositionZ = (this.transform.position.z + thisScaleZ / 2f - targetScaleZ / 2f);
                } else if (Utils.CompareFloats(this.transform.forward.x, -1f)) {
                    snapPositionX = (this.transform.position.x - thisScaleX / 2f + targetScaleX / 2f);
                    snapPositionZ = (this.transform.position.z - thisScaleZ / 2f + targetScaleZ / 2f);
                } else if (Utils.CompareFloats(this.transform.forward.z, -1f)) {
                    snapPositionX = (this.transform.position.x + thisScaleX / 2f - targetScaleX / 2f);
                    snapPositionZ = (this.transform.position.z - thisScaleZ / 2f + targetScaleZ / 2f);
                } else if (Utils.CompareFloats(this.transform.forward.x, 1f)) {
                    snapPositionX = (this.transform.position.x + thisScaleX / 2f - targetScaleX / 2f);
                    snapPositionZ = (this.transform.position.z + thisScaleZ / 2f + targetScaleZ / 2f);
                }
            } else if (Utils.CompareVectorDirection(colliderDirection, -this.transform.right, 0.3f) || Utils.CompareVectorDirection(colliderDirection, this.transform.forward, 0.3f) || Utils.CompareVectorDirection(colliderDirection, this.transform.up, 0.3f) || Utils.CompareVectorDirection(colliderDirection, -this.transform.up, 0.3f)) {
                snapPositionY = (this.transform.position.y - thisScaleY / 2f + targetScaleY / 2f);
                if (Utils.CompareFloats(this.transform.forward.z, 1f)) {
                    snapPositionX = (this.transform.position.x + thisScaleX / 2f - targetScaleX / 2f);
                    snapPositionZ = (this.transform.position.z - thisScaleZ / 2f + targetScaleZ / 2f);
                } else if (Utils.CompareFloats(this.transform.forward.x, -1f)) {
                    snapPositionX = (this.transform.position.x + thisScaleX / 2f - targetScaleX / 2f);
                    snapPositionZ = (this.transform.position.z + thisScaleZ / 2f - targetScaleZ / 2f);
                } else if (Utils.CompareFloats(this.transform.forward.z, -1f)) {
                    snapPositionX = (this.transform.position.x - thisScaleX / 2f + targetScaleX / 2f);
                    snapPositionZ = (this.transform.position.z + thisScaleZ / 2f - targetScaleZ / 2f);
                } else if (Utils.CompareFloats(this.transform.forward.x, 1f)) {
                    snapPositionX = (this.transform.position.x - thisScaleX / 2f + targetScaleX / 2f);
                    snapPositionZ = (this.transform.position.z - thisScaleZ / 2f + targetScaleZ / 2f);
                }
            }


            if (setPosition.x == 1) {
                snapTarget.snapPosition.x = snapPositionX;
                snapTarget.snapEnabled.x = 1;
            }

            if (setPosition.y == 1) {
                snapTarget.snapPosition.y = snapPositionY;
                if (offSetBase) {
                    snapTarget.snapPosition.y += Variables.baseHeightInMM / 1000f;
                }
                snapTarget.snapEnabled.y = 1;
            } else {
                float snapLowerY = (snapTarget.transform.position.y - targetScaleY / 2f);
                int snapPositionYIndex = Mathf.Clamp(Mathf.FloorToInt(snapLowerY * 1000 / 127), 0, Variables.offFloorHeightsInMM.Length - 1);
                snapTarget.snapPosition.y = Variables.offFloorHeightsInMM[snapPositionYIndex] / 1000f + targetScaleY / 2f;
                snapTarget.snapPosition.y += Variables.baseHeightInMM / 1000f;
                snapTarget.snapEnabled.y = 1;
            }

            if (setPosition.z == 1) {
                snapTarget.snapPosition.z = snapPositionZ;
                snapTarget.snapEnabled.z = 1;
            }

            if (children != null) {
                snapTarget.snapObject = this;
            }
        }
	}

	private void OnTriggerExit(Collider other) {
        Snappable snapTargetOut = other.gameObject.GetComponent<Snappable>();
		if (snapTargetOut != null && snapTargetOut.inHand) {
            snapTargetOut.snapPosition = snapTargetOut.transform.position;
			snapTargetOut.snapRotation = snapTargetOut.transform.rotation;
			snapTargetOut.snapEnabled = new Vector3Int(0,0,0);
            snapTargetOut.snapObject = null;
            if (snapTargetOut.Equals(snapTarget)) {
                snapTarget = null;
                if (orientationArrow != null) {
                    this.orientationArrow.gameObject.SetActive(true);
                }
            }
        }

        if (!enableSnap) {
            enableSnap = true;
        }
    }

    private void OnDisable() {
        if (snapTarget != null) {
            snapTarget.snapPosition = snapTarget.transform.position;
            snapTarget.snapRotation = snapTarget.transform.rotation;
            snapTarget.snapEnabled = new Vector3Int(0, 0, 0);
            snapTarget.snapObject = null;
            snapTarget = null;
        } 
    }
}
