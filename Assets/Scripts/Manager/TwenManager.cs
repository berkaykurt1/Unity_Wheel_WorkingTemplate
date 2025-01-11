using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace GameDeveloper_Case.Twen
{
    public class TwenManager : MonoBehaviour
    {
        #region  Lose Panel
        [Header("Lose Panel")]
        [SerializeField] private TextMeshProUGUI losePanelText;
        [SerializeField] private Image losePanelImage;
        [SerializeField] private Button losePanelGiveUpButton;
        [SerializeField] private Button losePanelReviveButton;

        [SerializeField] private float revealDuration = 1f;
        #endregion
        
    

        private void Start() 
        {
            losePanelImage.GetComponent<RectTransform>().DOScale(Vector3.zero,revealDuration).From();
            losePanelGiveUpButton.GetComponent<RectTransform>().DOAnchorPosY(-200f,revealDuration).From();
            losePanelReviveButton.GetComponent<RectTransform>().DOAnchorPosY(-200f,revealDuration).From();
        }
    }

}
