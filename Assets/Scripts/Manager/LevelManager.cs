using System.Collections;
using System.Collections.Generic;
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


        private void OnEnable()
        {
            uIManager = UIManager.Instance;
            uIManager.LevelSetting.LevelManager = this;
            
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

            GetScrollRectHorizontal();

        }


        //method that allows us to move to the next level
        public void NextLevel()
        {
            if(uIManager.LevelIndex < levels.Count)
            {
        
                uIManager.WhellController.GetComponent<WhellController>().WhellChieldObjectsAdjust();
                
                Vector2 distance = levels[uIManager.LevelIndex].LevelRectTransform.anchoredPosition - levels[uIManager.LevelIndex -1].LevelRectTransform.anchoredPosition;
                
                float value =distance.x / 100;
                SetScrollRectHorizontal(value);

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

            uIManager.ControlWhellSpinActive();

        }

        //allows us to reset the level
        public void LevelReset()
        {
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
            uIManager.LevelSetting.ScrollHorizontalValue = 0;
            uIManager.LevelIndex = 1;
            uIManager.GameOver = true;
            scrollRect.horizontalNormalizedPosition = 0f;
            uIManager.SaveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsSave("levelIndex",1);
        }

        //scroll rect allows movement on the x axis
        public void SetScrollRectHorizontal(float value)
        {
            scrollRect.horizontalNormalizedPosition += value / 100;

            levelSetting.ScrollHorizontalValue = scrollRect.horizontalNormalizedPosition;
        }


        //scroll rect pulls the value on the x-axis
        public void GetScrollRectHorizontal()
        {
            scrollRect.horizontalNormalizedPosition = levelSetting.ScrollHorizontalValue;
        }
        
    }

}
