using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity;
using UnityEngine.UI;

public class kuGestureManager : Singleton<kuGestureManager> {

    public Text DebugText;

    public GameObject TargetObject;

    public GestureRecognizer NavigationRecognizer { get; private set; }

    public GestureRecognizer ManipulationRecognizer { get; private set; }

    public GestureRecognizer ActiveRecognizer { get; private set; }
    // will be used if we have multiple GestureRecognizer

    public bool IsNavigating { get; private set; }

    public Vector3 NavigationPosition { get; private set; }

    // Use this for initialization
    void Start () {
	
	}

    private void Awake()
    {
        /* TODO: DEVELOPER CODING EXERCISE 2.b */

        // 2.b: Instantiate the NavigationRecognizer.
        NavigationRecognizer = new GestureRecognizer();

        // 2.b: Add GestureSettings to the NavigationRecognizer's RecognizableGestures.
        NavigationRecognizer.SetRecognizableGestures(GestureSettings.Tap | 
                                                     GestureSettings.NavigationRailsX |
                                                     GestureSettings.NavigationRailsY |
                                                     GestureSettings.NavigationRailsZ);

        // Assign event table
        // 2.b: Register for the TappedEvent with the NavigationRecognizer_TappedEvent function.
        NavigationRecognizer.TappedEvent += NavigationRecognizer_TappedEvent;
        // 2.b: Register for the NavigationStartedEvent with the NavigationRecognizer_NavigationStartedEvent function.
        NavigationRecognizer.NavigationStartedEvent += NavigationRecognizer_NavigationStartedEvent;
        // 2.b: Register for the NavigationUpdatedEvent with the NavigationRecognizer_NavigationUpdatedEvent function.
        NavigationRecognizer.NavigationUpdatedEvent += NavigationRecognizer_NavigationUpdatedEvent;
        // 2.b: Register for the NavigationCompletedEvent with the NavigationRecognizer_NavigationCompletedEvent function. 
        NavigationRecognizer.NavigationCompletedEvent += NavigationRecognizer_NavigationCompletedEvent;
        // 2.b: Register for the NavigationCanceledEvent with the NavigationRecognizer_NavigationCanceledEvent function. 
        NavigationRecognizer.NavigationCanceledEvent += NavigationRecognizer_NavigationCanceledEvent;

        NavigationRecognizer.StartCapturingGestures();

        DebugText.text = "Start Capturing Gestures.";
    }

    private void OnDestroy()
    {
        // 2.b: Unregister the Tapped and Navigation events on the NavigationRecognizer.
        NavigationRecognizer.TappedEvent -= NavigationRecognizer_TappedEvent;

        NavigationRecognizer.NavigationStartedEvent -= NavigationRecognizer_NavigationStartedEvent;
        NavigationRecognizer.NavigationUpdatedEvent -= NavigationRecognizer_NavigationUpdatedEvent;
        NavigationRecognizer.NavigationCompletedEvent -= NavigationRecognizer_NavigationCompletedEvent;
        NavigationRecognizer.NavigationCanceledEvent -= NavigationRecognizer_NavigationCanceledEvent;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void NavigationRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
    {
        // Do something at the TargetObject
        //TargetObject
        DebugText.text = "Tap.";
    }

    private void NavigationRecognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        // Is entered to Navigation state

        // 2.b: Set IsNavigating to be true.
        IsNavigating = true;

        // 2.b: Set NavigationPosition to be relativePosition.
        NavigationPosition = relativePosition;

        DebugText.text = "Navigation Start.";
    }

    private void NavigationRecognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = true; 

        NavigationPosition = relativePosition;
    }

    private void NavigationRecognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = false;
    }

    private void NavigationRecognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        // 2.b: Set IsNavigating to be false.
        IsNavigating = false;
    }
}
