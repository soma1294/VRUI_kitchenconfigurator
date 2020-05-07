using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportActivationBehaviour : MonoBehaviour
{
    private bool leftButtonActive = false;
    private bool rightButtonActive = false;

    public TeleportingVRUI teleporting;

    public OVRHand leftHand;
    public OVRHand rightHand;
    private GameObject leftHandObject;
    private GameObject rightHandObject;

    private void Start()
    {
        leftHandObject = leftHand.gameObject;
        rightHandObject = rightHand.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHandObject.activeSelf && rightHandObject.activeSelf)
        {
            if (leftHand.IsTracked && rightHand.IsTracked)
            {
                if (leftButtonActive && rightButtonActive && !BothHandsPinnching())
                {
                    teleporting.VirtualButtonIsPressed();
                }
                if (!leftButtonActive || !rightButtonActive)
                {
                    teleporting.CancelTeleport();
                }
                if (BothHandsPinnching())
                {
                    teleporting.VirtualButtonIsUp();
                }
            }
        }
    }

    private bool BothHandsPinnching()
    {
        return leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
    }

    public void ActivateLeftButton()
    {
        leftButtonActive = true;
    }

    public void ActivateRightButton() 
    {
        rightButtonActive = true;
    }

    public void DeactivateLeftButton()
    {
        leftButtonActive = false;
    }

    public void DeactivateRightButton()
    {
        rightButtonActive = false;
    }
}
