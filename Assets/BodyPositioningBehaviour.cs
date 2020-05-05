using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPositioningBehaviour : MonoBehaviour
{
    public Transform head;
    public float bodyOffset;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(head.position.x, head.position.y + bodyOffset, head.position.z); 
    }
}
