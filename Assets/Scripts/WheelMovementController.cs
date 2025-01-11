using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Whell_Interfaces;

namespace GameDeveloper_Case.Whell
{
    public class WheelMovementController : MonoBehaviour,IWheelRotation
    {
        [SerializeField] private WheelMovementSettings spinMovementSettings;
        public WheelMovementSettings SpinMovementSettings { get { return spinMovementSettings; } }


        public IEnumerator WhellRotation()
        {
            float time = 0f;
            while(time <= spinMovementSettings.SpinRotationTime)
            {
                UIManager.Instance.HasWhellStopTurning = false;
                transform.Rotate(Vector3.back * spinMovementSettings.SpinRotationSpeed * Time.deltaTime);
                
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).transform.rotation = Quaternion.identity;
                }

                
                yield return null;
                time += Time.deltaTime;
            }
            
            
            UIManager.Instance.HasWhellStopTurning = true;

            UIManager.Instance.Ui_Spin_Generic_Button.interactable = true;

        }
    }


}
