using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class kuCursorFeedback : MonoBehaviour {

    [Tooltip("Drag a prefab object to display when a hand is detected.")]
    public GameObject HandDetectedAsset;
    private GameObject handDetectedGameObject;

    [Tooltip("Drag a prefab object to display when a scroll enabled Interactible is detected.")]
    public GameObject ScrollDetectedAsset;
    private GameObject scrollDetectedGameObject;

    //[Tooltip("Drag a prefab object to display when a pathing enabled Interactible is detected.")]
    //public GameObject PathingDetectedAsset;
    //private GameObject pathingDetectedGameObject;

    [Tooltip("Drag a prefab object to parent the feedback assets.")]
    public GameObject FeedbackParent;

    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        if (HandDetectedAsset != null)
        {
            handDetectedGameObject = InstantiatePrefab(HandDetectedAsset);
        }

        if (ScrollDetectedAsset != null)
        {
            scrollDetectedGameObject = InstantiatePrefab(ScrollDetectedAsset);
        }
    }

    // Update is called once per frame
    void Update () {
        UpdateHandDetectedState();
        UpdateScrollDetectedState();
    }

    private GameObject InstantiatePrefab(GameObject inputPrefab)
    {
        GameObject instantiatedPrefab = null;

        if (inputPrefab != null && FeedbackParent != null)
        {
            instantiatedPrefab = GameObject.Instantiate(inputPrefab);
            // Assign parent to be the FeedbackParent
            // so that feedback assets move and rotate with this parent.
            instantiatedPrefab.transform.parent = FeedbackParent.transform;

            // Set starting state of gameobject to be inactive.
            instantiatedPrefab.gameObject.SetActive(false);
        }

        return instantiatedPrefab;
    }

    private void UpdateHandDetectedState()
    {
        if (handDetectedGameObject == null || CursorManager.Instance == null)
        {
            return;
        }

        handDetectedGameObject.SetActive(HandsManager.Instance.HandDetected);
    }

    private void UpdateScrollDetectedState()
    {
        if (scrollDetectedGameObject == null)
        {
            return;
        }


    }
}
