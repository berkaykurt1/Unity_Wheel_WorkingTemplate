using System.Collections;
using System.Collections.Generic;
using GameDeveloper_Case.SurprisePrizes;
using GameDeveloper_Case.Whell;
using UnityEngine;
using UnityEngine.UI;

namespace GameDeveloper_Case.Level
{
    public class LevelManager : MonoBehaviour
    {
        private UIManager uIManager;
        [SerializeField] private LevelSetting levelSetting;
        [SerializeField] private RectTransform levelsContent;
        [SerializeField] private LevelControl levelPrefab;

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private List<LevelControl> levels = new List<LevelControl>();
        public List<LevelControl> Levels {get {return levels;}}


        private void Awake()
        {
            uIManager = UIManager.Instance;
            
            int newX = (100 * levelSetting.LevelCount) + (50 * (levelSetting.LevelCount -2));
            levelsContent.sizeDelta = new Vector2(levelsContent.sizeDelta.x + newX,levelsContent.sizeDelta.y);
            
            for (int i = 0; i < levelSetting.LevelCount; i++)
            {
                LevelControl newLevel = Instantiate(levelPrefab,levelsContent);
                newLevel.transform.localScale = Vector3.one;
                newLevel.name = $"ui_text_level_{i+1}";
                newLevel.LevelInitialize(i+1);
                levels.Add(newLevel);
            }

            scrollRect.horizontalNormalizedPosition = 0;

            uIManager.levelReset += LevelReset;

            uIManager.levelNext += NextLevel;


            levels[uIManager.LevelIndex-1].LevelZone(true);

            scrollRect.horizontalNormalizedPosition = levelSetting.ScrollHorizontalValue;
        }

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
            }
        }


        public void NextLevel()
        {
            if(uIManager.LevelIndex < levels.Count)
            {
                uIManager.SurprisePrizes();

                uIManager.WhellController.GetComponent<WhellController>().WhellChieldObjectsAdjust();

                Vector2 distance = levels[uIManager.LevelIndex].LevelRectTransform.anchoredPosition - levels[uIManager.LevelIndex-1].LevelRectTransform.anchoredPosition;
                
                float a =distance.x / 100;
                scrollRect.horizontalNormalizedPosition += a / 100;

                levelSetting.ScrollHorizontalValue = scrollRect.horizontalNormalizedPosition;

                for (int i = 0; i < levels.Count; i++)
                {
                    if(i == uIManager.LevelIndex)
                    {
                        levels[i].LevelZone(true);
                    }
                    else
                    {
                        levels[i].LevelZone(false);
                    }
                }
                uIManager.LevelIndex++;

                uIManager.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsSave("levelIndex",uIManager.LevelIndex);
            }
            else return;
            

        }

        public void LevelReset()
        {
            print("resetlendi");
            for (int i = 0; i < levels.Count; i++)
            {
                if(i != 0)
                {
                    levels[i].LevelZone(false);
                }                
                else
                {
                    levels[i].LevelZone(true);
                }
            }
            uIManager.LevelIndex = 0;
            uIManager.GameOver = true;
            scrollRect.horizontalNormalizedPosition = 0f;
            uIManager.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsSave("levelIndex",1);
        }


        
    }

}
