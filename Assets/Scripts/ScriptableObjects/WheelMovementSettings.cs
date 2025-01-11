using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDeveloper_Case.Whell
{
    [CreateAssetMenu(fileName = "Spin Movement Settings",menuName = "Create Spin Movement Settings",order = 0)]
    public class WheelMovementSettings : ScriptableObject
    {
        [Header("Rotation")]
        [SerializeField] private float spinRotationSpeed;
        public float SpinRotationSpeed {get {return spinRotationSpeed;}} 

        [SerializeField] private float spinRotationTime;
        public float SpinRotationTime {get {return spinRotationTime;}}

        
       
    }
    
}
