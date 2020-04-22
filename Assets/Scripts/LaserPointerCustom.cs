//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
//using Valve.VR.InteractionSystem;
//using Valve.VR;
public class LaserPointerCustom : MonoBehaviour
{
    public LayerMask collideLayerMask = -1;
    public Color color;
    public float thickness = 0.002f;
    public float maxLength = 100f;
    public GameObject holder;
    public GameObject pointer;

    //private Hand controller;
    private OVRGrabber controller;

    GameObject previousHitObject;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<OVRGrabber>();

        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
		holder.transform.localRotation = Quaternion.identity;

		pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.tag = "LaserPointer";
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, maxLength);
        pointer.transform.localPosition = new Vector3(0f, 0f, maxLength/2f);
		pointer.transform.localRotation = Quaternion.identity;
		BoxCollider collider = pointer.GetComponent<BoxCollider>();
        if (collider) {
            Object.Destroy(collider);
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
	}

    // Update is called once per frame
	void Update ()
    {
        if (controller != null) {
            pointer.SetActive(true);
            float dist = maxLength;

            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit, maxLength, collideLayerMask);

            if (bHit) {
                GameObject hitObject = hit.collider?.gameObject?.transform?.parent?.gameObject;
                if (hitObject != null) {
                     //InputModule.instance.HoverBegin(hitObject);
                }
                if(hitObject != previousHitObject) {
                    //InputModule.instance.HoverEnd(previousHitObject);
                }
                previousHitObject = hitObject;
                /*if (SteamVR_Input._default.inActions.InteractUI.GetStateDown(controller.handType)) {
                    InputModule.instance.Submit(hitObject);
                }*/
            }
            

            if (bHit) {
                dist = hit.distance + 0.01f;
            }
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);

        } else {
            pointer.SetActive(false);
        }
    }
}
