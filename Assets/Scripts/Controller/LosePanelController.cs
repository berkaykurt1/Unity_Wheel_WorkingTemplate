using System.Collections;
using System.Collections.Generic;
using GameDeveloper_Case.Economy;
using GameDeveloper_Case.Twen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GameDeveloper_Case.LosePanel
{
    public class LosePanelController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI loseText;
        [SerializeField] private RectTransform loseCard;
        [SerializeField] private Button giveUpButton;
        [SerializeField] private Button reviveButton;
        private void OnEnable() 
        {
            giveUpButton.onClick.AddListener(GiveUpButtonFunction);

            reviveButton.onClick.AddListener(ReviveButtonFunction);
            
            if(EconomyManager.Instance.CurrentGold == 0)
            {
                reviveButton.interactable = false;
            }
            LosePanelGameObjectReset();
            TwenManager.Instance.LosePanelDoTween(loseText.GetComponent<RectTransform>(),reviveButton.GetComponent<RectTransform>(),giveUpButton.GetComponent<RectTransform>(),loseCard);
        }

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.A)) 
            {
                loseCard.localScale = Vector2.zero;
            }  
            if(Input.GetKeyDown(KeyCode.D)) 
            {
                loseCard.localScale = Vector2.one;
            }  
        }

        //you is a function for givebutton from object of the lose panel
        public void GiveUpButtonFunction()
        {
            UIManager.Instance.GameReset();
        }

        //you is a function for revivebutton from object of the lose panel
        public void ReviveButtonFunction()
        {
            if(EconomyManager.Instance.CurrentGold >= 250)
            {
                EconomyManager.Instance.SpendGold(250);
                UIManager.Instance.GameContuine();
            }
            else
            {
                UIManager.Instance.GameReset();
            }

        }

        //lose resets the position etc. of panel objects
        public void LosePanelGameObjectReset()
        {
            loseCard.localScale = Vector2.zero;
            loseCard.localRotation = Quaternion.identity;

            RectTransform giveUpTransform = giveUpButton.GetComponent<RectTransform>();
            giveUpTransform.anchoredPosition = new Vector2(giveUpTransform.anchoredPosition.x,-400); 
            
            RectTransform reviveTransform = reviveButton.GetComponent<RectTransform>();
            reviveTransform.anchoredPosition = new Vector2(reviveTransform.anchoredPosition.x,-400); 
        }

        
    }

}
