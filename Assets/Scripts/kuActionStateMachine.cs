using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ActionType { Rotate, Move };


namespace kuHoloLensUtility
{
    public class kuActionStateMachine : Singleton<kuActionStateMachine>
    {
        [HideInInspector]
        public ActionType BtnMenuStatus;      // false: move, true: rotate
                                              // 這邊塞初值會無效......

        public Text IndicatorText;

        private void Awake()
        {
            BtnMenuStatus = ActionType.Move;
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (BtnMenuStatus == ActionType.Move)
            {
                IndicatorText.text = "Translation.";
            }
            else
            {
                IndicatorText.text = "Rotation.";
            }
        }

        public void TransitToRotate()
        {
            BtnMenuStatus = ActionType.Rotate;
        }

        public void TransitToMove()
        {
            BtnMenuStatus = ActionType.Move;
        }
    }
}