using System.Collections;
using System.Collections.Generic;
using GameDeveloper_Case.Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameDeveloper_Case.Zone
{
    public class ZoneControl : MonoBehaviour
    {
        [SerializeField] private Image zoneImage;
        [SerializeField] private TextMeshProUGUI zoneNumberText;
        [SerializeField] private TextMeshProUGUI zoneNameText;
        [SerializeField] private ZoneEnum type;

        [SerializeField] private int zoneNumber = 0;

        //adjusting zone ui values
        public void SetZoneSprite(string spriteName)
        {
            zoneNameText.text = type == ZoneEnum.Safe ? "Safe Zone" : "Super Zone";
            UIManager.Instance.SetSpriteFromAtlas(zoneImage,spriteName);
            

            if(SaveZoneNumberQuery())
            {
                ZoneNumberLoad();
            }
            else
            {
                if (UIManager.Instance.LevelIndex == 1)
                {
                    switch (type)
                    {
                        case ZoneEnum.Safe:
                            zoneNumber = 5;
                            zoneNumberText.text = zoneNumber.ToString();
                            break;
                        case ZoneEnum.Super:
                            zoneNumber = 30;
                            zoneNumberText.text = zoneNumber.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }

            ZoneNumberSave();
        }

    
        //allows to advance zone values
        public void ZoneNumberNext()
        {
            switch (type)
            {
                case ZoneEnum.Safe:

                    if (UIManager.Instance.LevelIndex % 5 == 0 )
                    {
                        zoneNumberText.text = UIManager.Instance.LevelIndex != UIManager.Instance.LevelSetting.LevelCount ? (UIManager.Instance.LevelIndex + 5).ToString() : UIManager.Instance.LevelSetting.LevelCount.ToString();
                        zoneNumber = int.Parse(zoneNumberText.text);

                    }
                    else
                    {
                        zoneNumberText.text = zoneNumber.ToString();
                    }

                    break;
                case ZoneEnum.Super:
                    if(UIManager.Instance.LevelIndex % 30 == 0)
                    {
                        zoneNumberText.text = UIManager.Instance.LevelIndex != UIManager.Instance.LevelSetting.LevelCount ? (UIManager.Instance.LevelIndex + 30).ToString() : UIManager.Instance.LevelSetting.LevelCount.ToString();
                        zoneNumber = int.Parse(zoneNumberText.text);

                    }

                break;
                default:
                break;
            }
            ZoneNumberSave();
        }

        //resetting zone ui values
        public void ZoneNumberReset()
        {
            switch (type)
            {
                case ZoneEnum.Safe:
                    zoneNumber = 5;
                    zoneNumberText.text = zoneNumber.ToString();
                break;
                case ZoneEnum.Super:
                    zoneNumber = 30;
                    zoneNumberText.text = zoneNumber.ToString();
                break;
                default:
                break;
            }

            ZoneNumberSave();
        }

        //saving zone ui values
        private void ZoneNumberSave()
        {
            switch(type)
            {
                case ZoneEnum.Safe:
                    UIManager.Instance.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsSave("safeZoneNumber",zoneNumber);
                break;
                case ZoneEnum.Super:
                    UIManager.Instance.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsSave("superZoneNumber",zoneNumber);
                break;
                default:
                break;
            }
        }

        //loading zone ui values
        private void ZoneNumberLoad()
        {
            switch(type)
            {
                case ZoneEnum.Safe:
                    zoneNumber = UIManager.Instance.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsLoad("safeZoneNumber",0);
                    zoneNumberText.text  = zoneNumber.ToString();
                break;
                case ZoneEnum.Super:
                    zoneNumber = UIManager.Instance.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsLoad("superZoneNumber",0);
                    zoneNumberText.text  = zoneNumber.ToString();
                break;
                default:
                break;

            }
        }

        //zone questions values
        private bool SaveZoneNumberQuery()
        {
            return UIManager.Instance.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsQuery("safeZoneNumber") && UIManager.Instance.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsQuery("superZoneNumber");
        }

    }

}
