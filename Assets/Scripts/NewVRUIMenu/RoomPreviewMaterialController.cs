using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPreviewMaterialController : MonoBehaviour
{
    public MeshRenderer floorRenderer;
    public MeshRenderer wallRenderer;

    private Material currentFloorMaterial;
    private Material currentWallMaterial;

    private Material floorMaterial;
    private Material wallMaterial;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        floorMaterial = Variables.floorMaterial;
        wallMaterial = Variables.wallMaterial;

        currentFloorMaterial = floorRenderer.material;
        currentWallMaterial = wallRenderer.material;
        if (floorMaterial && currentFloorMaterial != floorMaterial)
        {
            floorRenderer.material = floorMaterial;
        }
        if (wallMaterial && currentWallMaterial != wallMaterial)
        {
            wallRenderer.material = wallMaterial;
        }
    }
}
