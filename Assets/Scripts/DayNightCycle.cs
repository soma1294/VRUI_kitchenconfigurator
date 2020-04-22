using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

	private Transform sunTransform;
	float rotationSpeed = 5f;

	// Use this for initialization
	void Start () {
		sunTransform = GetComponent<Transform>();
		sunTransform.transform.position = Vector3.zero;
		sunTransform.transform.localRotation = Quaternion.Euler(0, 0, 0);//AngleAxis(90f, Vector3.right);
	}
	
	// Update is called once per frame
	void Update () {
		//sunTransform.transform.rotation = Quaternion.AngleAxis(sunTransform.transform.rotation.x,Vector3.right) ;
		//float newRotation = sunTransform.transform.localRotation.eulerAngles.x + rotationSpeed * Time.deltaTime;
		//Debug.Log(newRotation);
		sunTransform.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
		//sunTransform.transform.localRotation = Quaternion.Euler(newRotation, 0, 0);//AngleAxis(newRotation, Vector3.right);


	}
}
