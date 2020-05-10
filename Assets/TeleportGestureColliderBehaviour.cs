using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGestureColliderBehaviour : MonoBehaviour
{
    public bool leftCollider;

    public TeleportActivationBehaviour teleportActivation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Left Hand") && leftCollider)
            teleportActivation.ActivateLeftButton();
        else if (other.CompareTag("Right Hand") && !leftCollider)
            teleportActivation.ActivateRightButton();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Left Hand") && leftCollider)
            teleportActivation.ActivateLeftButton();
        else if (other.CompareTag("Right Hand") && !leftCollider)
            teleportActivation.ActivateRightButton();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Left Hand") && leftCollider)
            teleportActivation.DeactivateLeftButton();
        else if (other.CompareTag("Right Hand") && !leftCollider)
            teleportActivation.DeactivateRightButton();
    }
}
