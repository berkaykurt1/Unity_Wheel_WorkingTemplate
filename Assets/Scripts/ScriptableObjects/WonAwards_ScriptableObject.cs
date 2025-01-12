using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDeveloper_Case.Award
{
    [CreateAssetMenu(fileName = "Wond Awards ScriptableObject", menuName = "Create Won Awards ScriptableObject", order = 3)]
    public class WonAwards_ScriptableObject : ScriptableObject
    {
        [SerializeField] private List<string> awards = new List<string>();
        public List<string> Awards { get { return awards; } }
        
        [SerializeField] private List<int> awardsCount = new List<int>();
        public List<int> AwardsCount { get {return awardsCount; } }

        [SerializeField] private AwardControl awardPrefab;
        public AwardControl AwardPrefab { get {return awardPrefab;} }

        


    }

}
