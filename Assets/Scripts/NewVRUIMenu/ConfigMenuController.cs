using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMenuController : MonoBehaviour
{
    public float xStart;
    public float yStart;
    public float zStart;
    public float yOffsetBetweenStripes;

    public bool placeOnGround = true;
    public bool isCorner = true;
    public bool isDrawer = false;
    public bool isDoor = false;

    /*
    0 = widthConfig;
    1 = bodyMaterial;
    2 = workplateConfig;
    3 = handleTypeConfig;
    4 = handleMaterialConfig;
    5 = handlePlacementConfig;
    6 = baseHeightConfig;
    7 = doorOpeningConfig;
    */

    public GameObject[] configMenuList;

    private void Start()
    {
        yStart *= transform.localScale.y;
        yOffsetBetweenStripes *= transform.localScale.y;
        ArrangeConfigMenu();
    }

    public void SwitchPreviewIsOnGround()
    {
        placeOnGround = !placeOnGround;
        ArrangeConfigMenu();
    }

    public void PreviewIsCorner()
    {
        isCorner = true;
        isDrawer = false;
        isDoor = false;
        ArrangeConfigMenu();
    }

    public void PreviewIsDrawer()
    {
        isCorner = false;
        isDrawer = true;
        isDoor = false;
        ArrangeConfigMenu();
    }

    public void PreviewIsDoor()
    {
        isCorner = false;
        isDrawer = false;
        isDoor = true;
        ArrangeConfigMenu();
    }

    private void ArrangeConfigMenu()
    {
        //Activate all neccessary config "stripes"
        if (placeOnGround)
        {
            if (isCorner && !isDoor && ! isDrawer)
            {
                configMenuList[0].SetActive(false);
                configMenuList[1].SetActive(true);
                configMenuList[2].SetActive(true);
                configMenuList[3].SetActive(false);
                configMenuList[4].SetActive(false);
                configMenuList[5].SetActive(false);
                configMenuList[6].SetActive(true);
                configMenuList[7].SetActive(false);
            }
            else if (!isCorner && isDoor && !isDrawer)
            {
                configMenuList[0].SetActive(true);
                configMenuList[1].SetActive(true);
                configMenuList[2].SetActive(true);
                configMenuList[3].SetActive(true);
                configMenuList[4].SetActive(true);
                configMenuList[5].SetActive(false);
                configMenuList[6].SetActive(true);
                configMenuList[7].SetActive(true);
            }
            else if (!isCorner && !isDoor && isDrawer)
            {
                configMenuList[0].SetActive(true);
                configMenuList[1].SetActive(true);
                configMenuList[2].SetActive(true);
                configMenuList[3].SetActive(true);
                configMenuList[4].SetActive(true);
                configMenuList[5].SetActive(true);
                configMenuList[6].SetActive(true);
                configMenuList[7].SetActive(false);
            }
            else
            {
                Debug.Log("No or multiple furniture types defined!");
            }
        }
        else
        {
            if (isCorner && !isDoor && !isDrawer)
            {
                configMenuList[0].SetActive(false);
                configMenuList[1].SetActive(true);
                configMenuList[2].SetActive(false);
                configMenuList[3].SetActive(false);
                configMenuList[4].SetActive(false);
                configMenuList[5].SetActive(false);
                configMenuList[6].SetActive(false);
                configMenuList[7].SetActive(false);
            }
            else if (!isCorner && isDoor && !isDrawer)
            {
                configMenuList[0].SetActive(true);
                configMenuList[1].SetActive(true);
                configMenuList[2].SetActive(false);
                configMenuList[3].SetActive(true);
                configMenuList[4].SetActive(true);
                configMenuList[5].SetActive(false);
                configMenuList[6].SetActive(false);
                configMenuList[7].SetActive(true);
            }
            else if (!isCorner && !isDoor && isDrawer)
            {
                configMenuList[0].SetActive(true);
                configMenuList[1].SetActive(true);
                configMenuList[2].SetActive(false);
                configMenuList[3].SetActive(true);
                configMenuList[4].SetActive(true);
                configMenuList[5].SetActive(true);
                configMenuList[6].SetActive(false);
                configMenuList[7].SetActive(false);
            }
            else
            {
                Debug.Log("No or multiple furniture types defined!");
            }
        }
        int i = 0;
        //Place the still active "stripes" at the correct y coordinates
        foreach (GameObject stripe in configMenuList)
        {
            if (stripe.activeSelf)
            {
                stripe.transform.localPosition = new Vector3(xStart, yStart + (i * yOffsetBetweenStripes), zStart);
                i++;
            }
        }
    }
}
