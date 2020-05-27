using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAdderBehaviour : MonoBehaviour
{
    public WindowConfigMenuBehaviour[] configMenuList;

    //north == 0, east == 1, south == 2, west == 3
    public CompassDirections direction;

    public void AddWindowInConfigMenu()
    {
        foreach (WindowConfigMenuBehaviour menu in configMenuList)
        {
            if (menu.wallIndex == direction)
            {
                menu.AddWindow();
            }
        }
    }

    public void ChangeWindowDirectionToNorth()
    {
        direction = CompassDirections.NORTH;
    }

    public void ChangeWindowDirectionToWest()
    {
        direction = CompassDirections.WEST;
    }

    public void ChangeWindowDirectionToEast()
    {
        direction = CompassDirections.EAST;
    }

    public void ChangeWindowDirectionToSouth()
    {
        direction = CompassDirections.SOUTH;
    }
}
