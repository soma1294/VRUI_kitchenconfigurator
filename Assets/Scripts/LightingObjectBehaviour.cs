using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingObjectBehaviour : MonoBehaviour {

    public LightProbeGroup lightProbe;

    public ReflectionProbe groundReflectionProbe;
	public ReflectionProbe workplateReflectionProbe;

	public ReflectionProbe wallPosZReflectionProbe;
    public ReflectionProbe wallNegZReflectionProbe;
    public ReflectionProbe wallPosXReflectionProbe;
    public ReflectionProbe wallNegXReflectionProbe;

    public Transform cameraToFollow;

    private Prefs prefs;

    private float depth;
    private float height;
    private float width;

    // Use this for initialization
    void Start () {
        prefs = new Prefs();
        prefs.Load();

        depth = prefs.roomDepth;
        height = prefs.roomHeight;
        width = prefs.roomWidth;

        //PlaceLightProbes();
        PlaceReflectionProbesOnce();
    }

	void PlaceReflectionProbesOnce() {

		Vector3 roomMiddle = new Vector3(depth, height/2f, width);

		groundReflectionProbe.transform.position = new Vector3(0, -roomMiddle.y, 0);
		groundReflectionProbe.size = new Vector3(1, height, 1);

		workplateReflectionProbe.transform.position = new Vector3(0, 1.32f, 0);
		workplateReflectionProbe.size = new Vector3(depth, height * .9f, width);

		wallPosZReflectionProbe.transform.position = new Vector3(0, roomMiddle.y, (roomMiddle.z + 0.02f));
		wallPosZReflectionProbe.size = new Vector3(depth, height * .9f, width);

		wallNegZReflectionProbe.transform.position = new Vector3(0, roomMiddle.y, -(roomMiddle.z + 0.02f));
		wallNegZReflectionProbe.size = new Vector3(depth, height * .9f, width);

		wallPosXReflectionProbe.transform.position = new Vector3((roomMiddle.x + 0.02f), roomMiddle.y, 0);
		wallPosXReflectionProbe.size = new Vector3(depth, height * .9f, width);

		wallNegXReflectionProbe.transform.position = new Vector3(-(roomMiddle.x + 0.02f), roomMiddle.y, 0);
		wallNegXReflectionProbe.size = new Vector3(depth, height * .9f, width);

	}


	#if (UNITY_EDITOR)
	void PlaceLightProbes() {
        Vector3[] probePositions = new Vector3[1331];

        float xOffset = depth / 9.0f;
        float yOffset = height / 8.0f;
        float zOffset = width / 9.0f;

        for (int x = 0; x < 11; x++) {
            for (int y = 0; y < 11; y++) {
                for (int z = 0; z < 11; z++) {
                    int index = x * 121 + y * 11 + z;
                    float xPos = x - 5;
                    float yPos = y - 1;
                    float zPos = z - 5;
                    probePositions[index].Set(xPos * xOffset, yPos * yOffset, zPos * zOffset);
                }
            }
        }

        lightProbe.probePositions = probePositions;
    }
	

    private void Update() {
		if (cameraToFollow != null) {
			PlaceReflectionProbe();
		}
    }

    void PlaceReflectionProbe() {

		//Vector3 cameraPosition = new Vector3(Mathf.Round(cameraToFollow.position.x * 100f) / 100f, Mathf.Round(cameraToFollow.position.y * 100f) / 100f, Mathf.Round(cameraToFollow.position.z * 100f) / 100f);
		Vector3 cameraPosition = new Vector3(cameraToFollow.position.x, cameraToFollow.position.y, cameraToFollow.position.z);

        groundReflectionProbe.transform.position = new Vector3(cameraPosition.x, -cameraPosition.y, cameraPosition.z);
        groundReflectionProbe.size = new Vector3(1, height, 1);
        groundReflectionProbe.center = new Vector3(-cameraPosition.x, (-height / 2 + cameraPosition.y), -cameraPosition.z);
        groundReflectionProbe.RenderProbe();

		workplateReflectionProbe.transform.position = new Vector3(cameraPosition.x, 1.32f, cameraPosition.z);
		workplateReflectionProbe.size = new Vector3(depth, height * .9f, width);
		workplateReflectionProbe.center = new Vector3(-cameraPosition.x, ((height / 2f)-1.3f), -cameraPosition.z);
		workplateReflectionProbe.RenderProbe();



		wallPosZReflectionProbe.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, (width + cameraPosition.z + 0.02f));
        wallPosZReflectionProbe.size = new Vector3(depth, height * .9f, width);
        wallPosZReflectionProbe.center = new Vector3(-cameraPosition.x, -cameraPosition.y + height / 2f, -cameraPosition.z);
        wallPosZReflectionProbe.RenderProbe();

        wallNegZReflectionProbe.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -(width + cameraPosition.z + 0.02f));
        wallNegZReflectionProbe.size = new Vector3(depth, height * .9f, width);
        wallNegZReflectionProbe.center = new Vector3(-cameraPosition.x, -cameraPosition.y + height / 2f, cameraPosition.z);
        wallNegZReflectionProbe.RenderProbe();

        wallPosXReflectionProbe.transform.position = new Vector3((depth + cameraPosition.x + 0.02f), cameraPosition.y, cameraPosition.z);
        wallPosXReflectionProbe.size = new Vector3(depth, height * .9f, width);
        wallPosXReflectionProbe.center = new Vector3(-cameraPosition.x, -cameraPosition.y + height / 2f, -cameraPosition.z);
        wallPosXReflectionProbe.RenderProbe();

        wallNegXReflectionProbe.transform.position = new Vector3(-(depth + cameraPosition.x + 0.02f), cameraPosition.y, cameraPosition.z);
        wallNegXReflectionProbe.size = new Vector3(depth, height * .9f, width);
        wallNegXReflectionProbe.center = new Vector3(cameraPosition.x, -cameraPosition.y + height / 2f, -cameraPosition.z);
        wallNegXReflectionProbe.RenderProbe();
    }
	#endif
}
