using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDeveloper_Case.Level
{
    [CreateAssetMenu(fileName = "Level Setting",menuName = "Create Level Setting",order = 2)]
    public class LevelSetting : ScriptableObject
    {
        [SerializeField] private int levelCount;
        public int LevelCount { get { return levelCount; } }
        [SerializeField] private List<LevelControl> levels = new List<LevelControl>();
        public List<LevelControl> Levels { get { return levels; } }
        [SerializeField] private float scrollHorizontalValue;
        public float ScrollHorizontalValue { get { return scrollHorizontalValue;} set { scrollHorizontalValue = value; } }
    }

}
