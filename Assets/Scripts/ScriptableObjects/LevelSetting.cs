using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDeveloper_Case.Level
{
    [CreateAssetMenu(fileName = "Level Setting",menuName = "Create Level Setting",order = 2)]
    public class LevelSetting : ScriptableObject
    {
        [SerializeField] private LevelManager levelManager;
        public LevelManager LevelManager{get {return levelManager;} set { levelManager = value;} }
        [SerializeField] private int levelCount;
        public int LevelCount { get { return levelCount; } }
        [SerializeField] private float scrollHorizontalValue;
        public float ScrollHorizontalValue { get { return scrollHorizontalValue;} set { scrollHorizontalValue = value; } }
    }

}
