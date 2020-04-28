﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIOVRHandTrackingGestureController : VRUIGestureController
{
    private OVRHand ovrHand;

    private bool handTracking;

    public bool deactivateThumbColliderOnPointing;
    public Collider thumbCollider;

    public bool deactivateGrabColliderOnPointing;
    public Collider grabCollider;

    public float minPinchStrength;
    public float maxPinchStrengthForPointing;
    public Collider[] collidersToDeactivateOnPinch;

    // Start is called before the first frame update
    void Start()
    {
        handTracking = GetComponent<OVRCustomSkeleton>();
        ovrHand = GetComponent<OVRHand>();
    }

    // Update is called once per frame
    void Update()
    {
        //Handtracking gesture recognition
        if (handTracking)
        {
            OVRHand.TrackingConfidence indexFingerPointingConfidence = ovrHand.GetFingerConfidence(OVRHand.HandFinger.Index);
            float pinchStrength = ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
            //Pinch Gesture
            if (pinchStrength >= minPinchStrength)
            {
                VRUIGesture = VRUIGesture.Pinch;
                if (deactivateThumbColliderOnPointing)
                {
                    thumbCollider.enabled = true;
                }
                if (deactivateGrabColliderOnPointing)
                {
                    grabCollider.enabled = true;
                }
                if (collidersToDeactivateOnPinch.Length > 0)
                {
                    foreach (Collider collider in collidersToDeactivateOnPinch)
                    {
                        collider.enabled = false;
                    }
                }
            }
            //Pointing Gesture
            else if (indexFingerPointingConfidence == OVRHand.TrackingConfidence.High && pinchStrength < maxPinchStrengthForPointing)
            {
                VRUIGesture = VRUIGesture.IndexPointing;
                if (deactivateThumbColliderOnPointing)
                {
                    thumbCollider.enabled = false;
                }
                if (deactivateGrabColliderOnPointing)
                {
                    grabCollider.enabled = false;
                }
                if (collidersToDeactivateOnPinch.Length > 0)
                {
                    foreach (Collider collider in collidersToDeactivateOnPinch)
                    {
                        collider.enabled = true;
                    }
                }
            }
            else
            {
                VRUIGesture = VRUIGesture.None;
                if (deactivateThumbColliderOnPointing)
                {
                    thumbCollider.enabled = true;
                }
                if (deactivateGrabColliderOnPointing)
                {
                    grabCollider.enabled = true;
                }
                if (collidersToDeactivateOnPinch.Length > 0)
                {
                    foreach (Collider collider in collidersToDeactivateOnPinch)
                    {
                        collider.enabled = true;
                    }
                }
            }
        }
        //Controller gesture recognition
        else
        {
            //Left hand
            if (Hand == HandSide.left)
            {
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= minPinchStrength)
                {
                    VRUIGesture = VRUIGesture.Pinch;
                }
                else if (!OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger))
                {
                    VRUIGesture = VRUIGesture.IndexPointing;
                }
                else
                {
                    VRUIGesture = VRUIGesture.None;
                }
            }
            //Right hand
            else
            {
                if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= minPinchStrength)
                {
                    VRUIGesture = VRUIGesture.Pinch;
                }
                else if (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))
                {
                    VRUIGesture = VRUIGesture.IndexPointing;
                }
                else
                {
                    VRUIGesture = VRUIGesture.None;
                }
            }
        }
    }
}