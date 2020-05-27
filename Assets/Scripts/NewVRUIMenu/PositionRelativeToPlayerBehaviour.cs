using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRelativeToPlayerBehaviour : MonoBehaviour
{
    public Transform player;
    private OVRGrabbable grabbable;

    private Vector3 deltaToPlayerPosition;
    private Vector3 savedPosition;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        savedPosition = transform.position;
        deltaToPlayerPosition = player.position - savedPosition;
        transform.position = savedPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (grabbable.isGrabbed)
        {
            savedPosition = transform.position;
            deltaToPlayerPosition = player.position - savedPosition;
        }
        else if (transform.position != player.position - deltaToPlayerPosition)
        {
            transform.position = player.position - deltaToPlayerPosition;
        }
    }
}
