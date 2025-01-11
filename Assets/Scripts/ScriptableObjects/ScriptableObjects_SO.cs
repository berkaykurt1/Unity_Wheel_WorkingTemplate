using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDeveloper_Case.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Scriptable Objects",fileName = "Scriptable Objects",order = 5)]
    public class ScriptableObjects_SO : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();
        public List<ScriptableObject> ScriptableObjects { get { return scriptableObjects; } set { scriptableObjects = value; } }
    }

}
