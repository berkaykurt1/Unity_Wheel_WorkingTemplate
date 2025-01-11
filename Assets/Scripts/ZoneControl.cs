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

    

        public void ZoneNumberNext()
        {
            switch (type)
            {
                case ZoneEnum.Safe:

                    if (UIManager.Instance.LevelIndex % 5 == 0 )
                    {
                        print("safe index 5'den büyük" + UIManager.Instance.LevelIndex);
                        zoneNumberText.text = (UIManager.Instance.LevelIndex + 5).ToString();
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
                        if(UIManager.Instance.LevelIndex == UIManager.Instance.LevelSetting.LevelCount)
                        {
                            zoneNumberText.text = UIManager.Instance.LevelIndex.ToString();
                        }
                        else
                        {
                            zoneNumberText.text = (UIManager.Instance.LevelIndex + 30).ToString();
                            zoneNumber = int.Parse(zoneNumberText.text);

                        }
                    }

                break;
                default:
                break;
            }
            ZoneNumberSave();
        }

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

        private bool SaveZoneNumberQuery()
        {
            return UIManager.Instance.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsQuery("safeZoneNumber") && UIManager.Instance.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsQuery("superZoneNumber");
        }

    }

}
