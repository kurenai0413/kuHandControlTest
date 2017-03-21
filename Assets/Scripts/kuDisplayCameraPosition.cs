using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kuDisplayCameraPosition : MonoBehaviour {

    public Text CameraPositionText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 CameraPosition = GetComponent<Camera>().transform.position;
        Vector3 CameraUp = GetComponent<Camera>().transform.up;
        Vector3 CameraRight = GetComponent<Camera>().transform.right;

        CameraPositionText.text = "Camera Position: " + CameraPosition.x + ",  "
                                + CameraPosition.y + ", " + CameraPosition.z
                                + "\nCamera Up: " + CameraUp.x + ", " + CameraUp.y + ", " + CameraUp.z
                                + "\nCamera Right: " + CameraRight.x + ", " + CameraRight.y + ", " + CameraRight.z;

    }
}
