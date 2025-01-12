using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using GameDeveloper_Case.LosePanel;

namespace GameDeveloper_Case.Twen
{
    public class TwenManager : MonoBehaviour
    {
        private static TwenManager instance;
        public static TwenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TwenManager>();
                }
                return instance;
            }
        }

        
        [SerializeField] private float revealDuration = 1f;
        
        
        //allows animation of lose panel objects
        public void LosePanelDoTween(RectTransform losePanelText,RectTransform losePanelReviveButton,RectTransform losePanelGiveUpButton,RectTransform losePanelCard)
        {
            losePanelText.DOAnchorPosY(200,1).From().OnComplete(delegate
            {
                losePanelText.GetComponent<TextMeshProUGUI>().DOFade(1,revealDuration).OnComplete(delegate
                {
                    losePanelReviveButton.DOAnchorPosY(0, revealDuration);
                    losePanelGiveUpButton.DOAnchorPosY(0, revealDuration).OnComplete(delegate
                    {
                        losePanelCard.DOScale(1,revealDuration);
                        losePanelCard.DORotate(new Vector3(0,0,-180),revealDuration).SetEase(Ease.InOutCubic).OnComplete(()=>losePanelCard.DORotate(Vector3.zero,revealDuration));

                    });
                });
            });
            
           
        }

        
    }

}
