using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Grabber : MonoBehaviour
{
    [SerializeField] private string tagName = "Grabbable";
    [SerializeField] private OVRInput.Controller controller;
    private bool isGrabbing = false;
    private bool isTriggerPressed = false;
    private Rigidbody otherRigidbody = null;
    private Vector3 velocity = Vector3.zero;
    private Vector3 previousPos = Vector3.zero;
    void Update()
    {
        isTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger,
        controller);
        if (!isTriggerPressed && isGrabbing)
            Release();
    }
    private void FixedUpdate()
    {
        if (!isGrabbing) return;
        velocity = (this.transform.position - previousPos) / Time.deltaTime;
        previousPos = this.transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if (isGrabbing) return;
        if (other.tag.Equals(tagName) && isTriggerPressed)
            Grab(other);
    }
    private void Grab(Collider other)
    {
        otherRigidbody = other.GetComponent<Rigidbody>();
        otherRigidbody.isKinematic = true;
        other.transform.parent = this.transform;
        other.transform.localPosition = Vector3.zero;
        isGrabbing = true;
    }
    private void Release()
    {
        otherRigidbody.transform.parent = null;
        otherRigidbody.isKinematic = false;
        otherRigidbody.velocity = velocity;
        otherRigidbody = null;
        isGrabbing = false;
    }
}
