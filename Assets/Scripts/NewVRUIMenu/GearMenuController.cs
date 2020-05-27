using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMenuController : MonoBehaviour
{
    public GameObject[] nonGearMenuTop;
    public GameObject[] nonGearMenu;
    public GameObject[] gearMenuTop;
    public GameObject[] gearMenu;

    public bool gearMenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //ArrangeGearMenu();
    }
    
    public void SwitchGearMenu()
    {
        gearMenuActive = !gearMenuActive;
        ArrangeGearMenu();
    }

    public void ArrangeGearMenu()
    {
        if (gearMenuActive)
        {
            foreach (GameObject nonGearPart in nonGearMenuTop)
            {
                nonGearPart.SetActive(false);
            }
            foreach (GameObject nonGearPart in nonGearMenu)
            {
                nonGearPart.SetActive(false);
            }
            foreach (GameObject gearPart in gearMenuTop)
            {
                gearPart.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject nonGearPart in nonGearMenuTop)
            {
                nonGearPart.SetActive(true);
            }
            foreach (GameObject gearPart in gearMenuTop)
            {
                gearPart.SetActive(false);
            }
            foreach (GameObject nonGearPart in gearMenu)
            {
                nonGearPart.SetActive(false);
            }
        }
    }
}
