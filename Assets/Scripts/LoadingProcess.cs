using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValueLoading
{
    public class ValueLoadingProcess 
    {
        public object ValueLoad(string typeName,string path)
        {
            switch (typeName)
            {
                case "GameObject":
                return Resources.Load<GameObject>(path);
                case "ScriptableObject":
                return Resources.Load<ScriptableObject>(path);
                default:
                return null;
            }
        }

        
        
    }
}
