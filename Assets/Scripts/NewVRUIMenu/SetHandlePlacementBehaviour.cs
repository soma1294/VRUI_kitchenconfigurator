using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandlePlacementBehaviour : MonoBehaviour
{
    public DrawerFurnitureConfig drawerConfig;

    public ObjectPreviewBehaviour objectPreview;

    public void SetThisDrawerConfig(string name)
    {
        objectPreview.SetDrawerConfig(drawerConfig);
    }
}
