using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivationController : MonoBehaviour
{
    public GameObject objectToControl;

    public void ActivateObject()
    {
        objectToControl.SetActive(true);
    }

    public void DeactivateObject()
    {
        objectToControl.SetActive(false);
    }
}
