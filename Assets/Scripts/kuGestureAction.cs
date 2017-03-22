using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA;
using UnityEngine.VR.WSA.Persistence;

namespace kuHoloLensUtility
{
    public class kuGestureAction : MonoBehaviour
    {
        public Text DebugText;

        public float RotationSensitivity    = 0.5f;

        public float TranslationSensitivity = 0.3f;

        public string ObjAnchorName;

        private Vector3 manipulationPreviousPosition;

        WorldAnchorStore anchorStore;

        // Use this for initialization
        void Start()
        {
            WorldAnchorStore.GetAsync(AnchorStoreReady);
            AttachAnchor();
        }

        void AnchorStoreReady(WorldAnchorStore store)
        {
            anchorStore = store;

            string[] ids = anchorStore.GetAllIds();

            for (int index = 0; index < ids.Length; index++)
            {
                Debug.Log(ids[index]);
                if (ids[index] == ObjAnchorName)
                {
                    //text.text = ObjectAnchorStoreName + "founded.";

                    WorldAnchor wa = anchorStore.Load(ids[index], gameObject);
                    break;
                }
            }
        }

            // Update is called once per frame
        void Update()
        {
           
        }

        private void RemoveAnchor()
        {
            WorldAnchor anchor = gameObject.GetComponent<WorldAnchor>();
            if (anchor != null)
            {
                DestroyImmediate(anchor);
            }

            string[] ids = anchorStore.GetAllIds();
            for (int index = 0; index < ids.Length; index++)
            {
                if (ids[index] == ObjAnchorName)
                {
                    bool deleted = anchorStore.Delete(ids[index]);
                    break;
                }
            }

#if (ShowDebug)
            DebugText.text = "Anchor removed.";
#endif
        }

        private void AttachAnchor()
        {
            WorldAnchor attachingAnchor = gameObject.AddComponent<WorldAnchor>();
            if (attachingAnchor.isLocated)
            {
                bool saved = anchorStore.Save(ObjAnchorName, attachingAnchor);
            }
            else
            {
                attachingAnchor.OnTrackingChanged += AttachingAnchor_OnTrackingChanged;
            }

#if (ShowDebug)
            DebugText.text = "Anchor Attached.";
#endif
        }

        private void PerformRotation(Vector3 position)
        {
            if (kuGestureManager.Instance.IsNavigating)
            {
                Vector3 NPosition = position;
#if ShowDebug
                //DebugText.text = "Vector: " + NPosition.x + ", " + NPosition.y + ", " + NPosition.z;
#endif
                /* TODO: DEVELOPER CODING EXERCISE 2.c */

                // 2.c: Calculate rotationFactor based on GestureManager's NavigationPosition.X 
                // and multiply by RotationSensitivity.
                // This will help control the amount of rotation.
                float rotationFactorX = RotationSensitivity * kuGestureManager.Instance.NavigationPosition.x;
                float rotationFactorY = RotationSensitivity * kuGestureManager.Instance.NavigationPosition.y;
                float rotationFactorZ = RotationSensitivity * kuGestureManager.Instance.NavigationPosition.z;

                // 2.c: transform.Rotate along the Y axis using rotationFactor.
                // rotation is along the object's local axis
                RemoveAnchor();
                transform.Rotate(new Vector3(rotationFactorY, -rotationFactorX, -rotationFactorZ), Space.World);
                AttachAnchor();
            }
        }

        private void PerformManipulationStart(Vector3 position)
        {
            manipulationPreviousPosition = position;
#if ShowDebug
            //DebugText.text = "PerformManipulationStart triggered.";
#endif
        }

        private void PerformManipulationUpdate(Vector3 position)
        {
#if ShowDebug
            //DebugText.text = "PerformManipulationUpdate triggered. ("
            //               + position.x + ", " + position.y + ", " + position.z + ")";
#endif
            
            /* TODO: DEVELOPER CODING EXERCISE 4.a */

            Vector3 RelativeVector = Vector3.zero;
            // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
            RelativeVector = position - manipulationPreviousPosition;
            // 4.a: Update the manipulationPreviousPosition with the current position.
            manipulationPreviousPosition = position;

            Vector3 MoveVector = Vector3.zero;

            if (RelativeVector.x != 0)
            {
                MoveVector = RelativeVector.x * Camera.main.transform.right;
            }
            if (RelativeVector.y != 0)
            {
                MoveVector = RelativeVector.y * Camera.main.transform.up;
            }
            if (RelativeVector.z != 0)
            {
                MoveVector = RelativeVector.z * Camera.main.transform.forward;
            }

            // 4.a: Increment this transform's position by the moveVector.
            RemoveAnchor();
            transform.position += TranslationSensitivity * MoveVector;
            AttachAnchor();
        }

        private void AttachingAnchor_OnTrackingChanged(WorldAnchor self, bool located)
        {
            if (located)
            {
                bool saved = anchorStore.Save(ObjAnchorName, self);
                self.OnTrackingChanged -= AttachingAnchor_OnTrackingChanged;
            }
        }
    }
}
