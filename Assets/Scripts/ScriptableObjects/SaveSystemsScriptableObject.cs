using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDeveloper_Case.SaveSystems
{
    [CreateAssetMenu(menuName = "Save Systems Scriptable Object",fileName = "Create Save Systems Scriptable Object",order = 4)]
    public class SaveSystemsScriptableObject : ScriptableObject
    {
        private PlayerPrefsSaveSystem playerPrefsSaveSystem = new PlayerPrefsSaveSystem();
        public PlayerPrefsSaveSystem PlayerPrefsSaveSystem { get { return playerPrefsSaveSystem;}}
        
    }

}
