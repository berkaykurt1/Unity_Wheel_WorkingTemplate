using System.Collections;
using System.Collections.Generic;
using GameDeveloper_Case.AtlasNameEnums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GameDeveloper_Case.Award
{
    public class AwardControl : MonoBehaviour
    {
        [SerializeField] private Image awardIcon;
        public Image AwardIcon { get { return awardIcon; } }

        [SerializeField] private TextMeshProUGUI awardCountText;
        public TextMeshProUGUI AwardCountText { get { return awardCountText;}}

        [SerializeField] private int awardCount = 0;
        public int AwardCount { get { return awardCount;} set { awardCount = value; } }

        [SerializeField] private AtlasNameEnum awardAtlasName;
        public AtlasNameEnum AwardAtlasName { get { return awardAtlasName; }  set { awardAtlasName = value;}}

        //defines the ui etc. of the prizes
        public void AwardFeaturesIdentification(string awardIconSpriteName,int _awardCount)
        {
            UIManager.Instance.SetSpriteFromAtlas(awardIcon,awardIconSpriteName);
            awardCountText.text = _awardCount.ToString();
            awardCount = _awardCount;
        }

        //increases the amounts of rewards
        public void AwardCountBoosting(int _awardCount)
        {
            awardCount += _awardCount;
            awardCountText.text = awardCount.ToString();
        }  

        //withdrawing the amounts of the prizes
        public int GetAwardCount()
        {
            return awardCount;
        }
        
    }

}
