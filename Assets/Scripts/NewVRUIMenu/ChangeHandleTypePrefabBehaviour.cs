using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHandleTypePrefabBehaviour : MonoBehaviour
{
    public GameObject handlePrefab;

    public void SetThisHandleType()
    {
        if (handlePrefab)
            Variables.handlesPrefab = handlePrefab;
        else
            Debug.Log("No HandlePrefab set for = " + name);
    }
}
