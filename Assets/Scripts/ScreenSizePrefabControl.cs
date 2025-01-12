using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameDeveloper_Case.Settings
{
    public class ScreenSizePrefabControl : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI screnSizeValueText;
        [SerializeField] private Button screnSizeChooseButton;
        [SerializeField] private ScreenSizeEnum screenSizeEnum;

        private void OnEnable() 
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<TextMeshProUGUI>() != null)
                {
                    screnSizeValueText = transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                }
                else if(transform.GetChild(i).GetComponent<Button>() != null)  
                {
                    screnSizeChooseButton = transform.GetChild(i).GetComponent<Button>();
                }
            }    


            SetScreenSizeName();
            screnSizeChooseButton.onClick.AddListener(SetResolution);
        }

        //the screen resoultion is adjusting
        void SetResolution()
        {
            int height = 1080; // Sabit yükseklik
            int width = 0;

            switch (screenSizeEnum)
            {
                case ScreenSizeEnum.Aspect20_9:
                    width = Mathf.RoundToInt(height * (20f / 9f));
                    break;

                case ScreenSizeEnum.Aspect16_9:
                    width = Mathf.RoundToInt(height * (16f / 9f));
                    break;

                case ScreenSizeEnum.Aspect4_3:
                    width = Mathf.RoundToInt(height * (4f / 3f));
                    break;
            }

            Screen.SetResolution(width, height, FullScreenMode.Windowed);
        }

        //Setting the name of the screen resolution object
        private void SetScreenSizeName()
        {
            switch (screenSizeEnum)
            {
                case ScreenSizeEnum.Aspect20_9:
                    screnSizeValueText.text = "20 : 9";
                    break;

                case ScreenSizeEnum.Aspect16_9:
                    screnSizeValueText.text = "16 : 9";
                    break;

                case ScreenSizeEnum.Aspect4_3:
                    screnSizeValueText.text = "4 : 9";
                    break;

                default:
                    break;
            }
        }
        
    }

}
