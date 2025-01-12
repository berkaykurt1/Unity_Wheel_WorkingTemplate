using System;
using System.Collections;
using System.Collections.Generic;
using AwardsEnums;
using UnityEngine;

namespace GameDeveloper_Case.Whell
{
    [CreateAssetMenu(fileName = "Whell Data", menuName = "Create Whell Data", order = 1)]
    public class WhellData : ScriptableObject
    {
        [SerializeField] private Sprite[] whellPieceSprites;
        public Sprite[] WhellPieceSprites { get { return whellPieceSprites;}}

        [Header("Whell Tier ")]
        [Space]
        
        [SerializeField]private  Level[] whelllevels;
        public Level[] Whelllevels { get { return whelllevels;}}
        
    }

    [Serializable]
    public class Level
    {
        public string levelName;
        //public Sprite[] sprites;
        public string[] spritesName;
        public AwardsEnum[] awardsEnums;
        public int[] awardsAmount;
        
    }
}