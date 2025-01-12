using System.Collections;
using System.Collections.Generic;
using GameDeveloper_Case.Economy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameDeveloper_Case.SurprisePrizes
{
    public class AwardInformationControl : MonoBehaviour
    {
        [SerializeField] private Image awardIcon;
        [SerializeField] private TextMeshProUGUI awardAmountText;
        private string awardSpriteName;
        [SerializeField] private Image supriseAwardIcon;
        [SerializeField] private TextMeshProUGUI supriseAwardAmountText;
        private string supriseAwardName;
        
        //method that displays the information of the rewards as ui
        public void AwardInformation(Sprite awardSprite,int awardAmount,Sprite supriseAwardSprite,int supriseAwardAmount,bool supriseAwardInformationActive)
        {
            

            if(supriseAwardInformationActive)
            {
                supriseAwardIcon.gameObject.SetActive(true);
                supriseAwardAmountText.gameObject.SetActive(true);
                supriseAwardIcon.sprite = supriseAwardSprite;
                supriseAwardAmountText.text = supriseAwardAmount.ToString();
            }
            else
            {
                awardIcon.gameObject.SetActive(true);
                awardAmountText.gameObject.SetActive(true);
            }
            

            awardIcon.sprite = awardSprite;
            awardAmountText.text = awardAmount.ToString();

           

            StartCoroutine(CloseAwardInformationActive(supriseAwardInformationActive));

        }

        //method that allows ui to close awards information
        private IEnumerator CloseAwardInformationActive(bool value)
        {
            yield return new WaitForSeconds(1);
            
            if(supriseAwardIcon.gameObject.activeSelf && supriseAwardAmountText.gameObject.activeSelf)
            {
                supriseAwardAmountText.text = "";

                supriseAwardIcon.gameObject.SetActive(false);
                supriseAwardAmountText.gameObject.SetActive(false);
            }



            
            
            awardAmountText.text = "";
            awardIcon.gameObject.SetActive(false);
            awardAmountText.gameObject.SetActive(false);
        }

        

    }

}
