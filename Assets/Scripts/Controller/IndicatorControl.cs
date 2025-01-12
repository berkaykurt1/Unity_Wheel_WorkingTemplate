using System.Collections;
using System.Collections.Generic;
using GameDeveloper_Case.WhellPiece;
using UnityEngine;
using UnityEngine.UI;

namespace GameDeveloper_Case
{
    public class IndicatorControl : MonoBehaviour
    {
        [SerializeField] private Image indicatorImage;
        public Image IndicatorImage { get { return indicatorImage; } }
        

        private void OnTriggerStay2D(Collider2D other) 
        {
            if(UIManager.Instance.HasWhellStopTurning)
            {
                if(other.GetComponent<WheelPieceController>() != null)
                {
                    other.GetComponent<WheelPieceController>().WhatAwards();
                }
                else
                {
                    UIManager.Instance.NextLevel();
                }
                UIManager.Instance.HasWhellStopTurning = false;   
            }
        }
    }
}
