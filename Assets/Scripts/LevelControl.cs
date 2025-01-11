using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameDeveloper_Case.Level
{
    public class LevelControl : MonoBehaviour
    {
        [SerializeField] private GameObject ui_card_zone_map_frame;

        private RectTransform levelRectTransform;
        public RectTransform LevelRectTransform { get { return levelRectTransform; } }

        [SerializeField] private TextMeshProUGUI ui_text_level_number;

        private void Awake() 
        {
            levelRectTransform = GetComponent<RectTransform>();    
        }

        public void LevelInitialize(int levelNumber)
        {
            ui_text_level_number.text = levelNumber.ToString(); 
        }

        public void LevelZone(bool isLevelZone)
        {
            ui_card_zone_map_frame.SetActive(isLevelZone);
        }
    }
}
