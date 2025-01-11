using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDeveloper_Case.SaveSystems
{
    [Serializable]
    public class PlayerPrefsSaveSystem 
    {
        

        public void PlayerPrefsSave(string key,string value)
        {
            PlayerPrefs.SetString(key,value);
        }
        
        public void PlayerPrefsSave(string key,float value)
        {
            PlayerPrefs.SetFloat(key,value);
        }
        
        public void PlayerPrefsSave(string key,int value)
        {
            PlayerPrefs.SetInt(key,value);
        }



        public string PlayerPrefsLoad(string key,string type)
        {
            return PlayerPrefs.GetString(key);
        }

        public float PlayerPrefsLoad(string key,float type)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public int PlayerPrefsLoad(string key,int type)
        {
            return PlayerPrefs.GetInt(key);
        }


        public bool PlayerPrefsQuery(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        
    }

}
