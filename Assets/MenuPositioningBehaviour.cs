using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPositioningBehaviour : MonoBehaviour
{
    public bool rotateMenu;
    public bool positionMenu;
    public Transform menuPositionerSphere;
    private Vector3 sphereStartPosition;
    private Vector3 sphereDeltaPosition;

    public Transform menuRotaterSphere;
    private Vector3 sphereStartEulerRotation;
    private Vector3 sphereDeltaEulerRotation;

    private Vector3 startPosition;
    private Vector3 startEulerRotation;

    private Vector3 roundedEulerRotation;

    // Start is called before the first frame update
    void Start()
    {
        sphereStartPosition = menuPositionerSphere.position;
        sphereDeltaPosition = Vector3.zero;
        sphereStartEulerRotation = menuRotaterSphere.eulerAngles;
        sphereDeltaEulerRotation = Vector3.zero;

        startPosition = transform.position;
        startEulerRotation = transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        if (positionMenu)
        {
            //Position
            sphereDeltaPosition = menuPositionerSphere.position - sphereStartPosition;
            transform.position = startPosition + sphereDeltaPosition;
        }
        if (rotateMenu)
        {
            //Rotation
            sphereDeltaEulerRotation = menuRotaterSphere.eulerAngles - sphereStartEulerRotation;
            transform.eulerAngles = RoundVector3To(startEulerRotation - sphereDeltaEulerRotation, 15f);
        }
    }

    private Vector3 RoundVector3To(Vector3 vector, float amount)
    {
        float x = Mathf.Round(vector.x / amount);
        float y = Mathf.Round(vector.y / amount);
        float z = 0;

        x *= amount;
        y *= amount;

        return new Vector3(x, y, z);
    }
}
