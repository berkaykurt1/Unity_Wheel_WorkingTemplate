using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameDeveloper_Case.LosePanel
{
    public class LosePanelController : MonoBehaviour
    {
        [SerializeField] private Button giveUpButton;
        [SerializeField] private Button reviveButton;
        [SerializeField] private Image background;
        [SerializeField] private Image icon;
        [SerializeField] private Sprite deathSprite;

        private void OnEnable() 
        {
            giveUpButton = GameObject.FindWithTag("loseGiveUpButton").GetComponent<Button>();
            giveUpButton.onClick.AddListener(GiveUpButtonFunction);

            reviveButton = GameObject.FindWithTag("loseReviveButton").GetComponent<Button>();
            reviveButton.onClick.AddListener(ReviveButtonFunction);
            
        }

        private void Awake() 
        {

        }


        public void GiveUpButtonFunction()
        {
            UIManager.Instance.GameReset();
        }

        public void ReviveButtonFunction()
        {
            UIManager.Instance.GameContuine();
        }

    }

}
