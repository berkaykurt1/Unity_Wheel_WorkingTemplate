using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDeveloper_Case.SurprisePrizes
{
    [CreateAssetMenu(menuName = "Create Surprise Prizes ScriptableObject",fileName = "Surprise Prizes ScriptableObject",order = 7)]
    public class SurprisePrizes_ScriptableObject : ScriptableObject
    {
        [SerializeField] private Sprite[] surprisePrizesSprites;
        public Sprite[] SurprisePrizesSprites { get { return surprisePrizesSprites;}}

        [SerializeField] private SurprisePrizes[] surprisePrizes;
        public SurprisePrizes[] SurprisePrizes { get { return surprisePrizes;}}

    }

    [Serializable]
    public class SurprisePrizes
    {
        public string name;
        public int minAmount;
        public int maxAmount; 
    }
}
