using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBetweenPlacementTypeImages : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;

    public void SwitchImage()
    {
        image1.SetActive(!image1.activeSelf);
        image2.SetActive(!image2.activeSelf);
    }
}
