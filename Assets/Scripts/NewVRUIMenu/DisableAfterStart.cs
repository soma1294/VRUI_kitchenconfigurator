using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterStart : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(false);
        enabled = false;
    }
}
