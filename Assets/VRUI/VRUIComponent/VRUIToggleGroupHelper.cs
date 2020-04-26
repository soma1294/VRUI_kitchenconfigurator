using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIToggleGroupHelper : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Add the Toggles that belong to the same toggle group here.")]
    private VRUIToggleBehaviour[] toggleList;

    // Start is called before the first frame update
    void Start()
    {
        foreach (VRUIToggleBehaviour toggle in toggleList)
        {
            toggle.ToggleIsStuck = false;
        }
    }

    public void ToggleInGroupWasPressed(string name)
    {
        foreach (VRUIToggleBehaviour toggle in toggleList)
        {
            if (toggle.ToggleIsStuck && toggle.name != name)
            {
                //Debug.Log("ToggleWithNameNowUnstuck = " + toggle.name);
                toggle.ToggleIsStuck = false;
            }
        }
    }

    public VRUIToggleBehaviour[] GetToggleList()
    {
        return toggleList;
    }

    public void SetToggleList(VRUIToggleBehaviour[] list)
    {
        toggleList = list;
    }

    public void AddElementAtPosition(VRUIToggleBehaviour toggle, int i)
    {
        toggleList[i] = toggle;
    }
}
