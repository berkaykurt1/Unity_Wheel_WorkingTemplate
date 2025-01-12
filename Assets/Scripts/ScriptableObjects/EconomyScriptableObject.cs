using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDeveloper_Case.Economy
{
    [CreateAssetMenu(fileName = "Economy Scriptable Object", menuName = "Create Economy Scriptable Object",order = 8)]
    public class EconomyScriptableObject : ScriptableObject
    {
        [SerializeField] private int gold;
        public int Gold { get { return gold; } set { gold = value; } }


    }

}
