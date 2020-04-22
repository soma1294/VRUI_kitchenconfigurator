using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrabbed : MonoBehaviour
{
    private OVRGrabbable grabbable;
    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbable.isGrabbed)
        {
            grabbable.gameObject.GetComponent<Renderer>().material.color = Color.green;
        } else
        {
            grabbable.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
