using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameDeveloper_Case.SurprisePrizes
{
    public class AwardInformationControl : MonoBehaviour
    {
        [SerializeField] private Image awardIcon;
        [SerializeField] private TextMeshProUGUI awardAmountText;
        [SerializeField] private Image supriseAwardIcon;
        [SerializeField] private TextMeshProUGUI supriseAwardAmountText;
        

        public void AwardInformation(Sprite awardSprite,int awardAmount,Sprite supriseSprite,int supriseAmount,bool supriseAwardInformationActive)
        {
            awardIcon.gameObject.SetActive(true);
            awardAmountText.gameObject.SetActive(true);

            if(supriseAwardInformationActive)
            {
                supriseAwardIcon.gameObject.SetActive(true);
                supriseAwardAmountText.gameObject.SetActive(true);
            }
            awardIcon.sprite = awardSprite;
            awardAmountText.text = awardAmount.ToString();
            supriseAwardIcon.sprite = supriseSprite;
            supriseAwardAmountText.text = supriseAmount.ToString();

            StartCoroutine(Deneme());
        }

        private IEnumerator Deneme()
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
