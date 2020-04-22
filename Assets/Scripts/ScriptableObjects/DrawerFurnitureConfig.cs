using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DrawerFurnitureConfig", menuName = "Furniture/Drawer")]
public class DrawerFurnitureConfig : ScriptableObject {
    public Sprite preview;

    public string description;

    public int[] drawerConfig;
}
