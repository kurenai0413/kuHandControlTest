using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kuGestureAction : MonoBehaviour {

    public Text DebugText;

    public float RotationSensitivity = 1.0f;

    private Vector3 manipulationPreviousPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PerformRotation();
	}

    private void PerformRotation()
    {
        if (kuGestureManager.Instance.IsNavigating)
        {
            Vector3 NPosition = kuGestureManager.Instance.NavigationPosition;
#if ShowDebug      
            DebugText.text = "Vector: " + NPosition.x + ", " + NPosition.y + ", " + NPosition.z;
#endif
            /* TODO: DEVELOPER CODING EXERCISE 2.c */

            //if (Math.Abs(NPosition.x) > Math.Abs(NPosition.y))
            //{
            //    DebugText.text += ", Rotation along Y.";
            //}
            //else
            //{
            //    DebugText.text += ", Rotation along X.";
            //}

            // 2.c: Calculate rotationFactor based on GestureManager's NavigationPosition.X 
            // and multiply by RotationSensitivity.
            // This will help control the amount of rotation.
            float rotationFactorX = RotationSensitivity * kuGestureManager.Instance.NavigationPosition.x;
            float rotationFactorY = RotationSensitivity * kuGestureManager.Instance.NavigationPosition.y;
            float rotationFactorZ = RotationSensitivity * kuGestureManager.Instance.NavigationPosition.z;

            // 2.c: transform.Rotate along the Y axis using rotationFactor.
            // rotation is along the object's local axis
            transform.Rotate(new Vector3(rotationFactorY, -rotationFactorX, -rotationFactorZ), Space.World);
        }
    }

    private void PerformManipulationStart(Vector3 position)
    {
        manipulationPreviousPosition = position;
#if ShowDebug
        DebugText.text = "PerformManipulationStart triggered.";
#endif
    }
    
    private void PerformManipulationUpdate(Vector3 position)
    {
#if ShowDebug
        DebugText.text = "PerformManipulationUpdate triggered.";
#endif

        if (kuGestureManager.Instance.IsManipulating)
        {
            /* TODO: DEVELOPER CODING EXERCISE 4.a */

            Vector3 moveVector = Vector3.zero;
            // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
            moveVector = position - manipulationPreviousPosition;
            // 4.a: Update the manipulationPreviousPosition with the current position.
            manipulationPreviousPosition = position;

            // 4.a: Increment this transform's position by the moveVector.
            transform.position += moveVector;
        }
    }
}
