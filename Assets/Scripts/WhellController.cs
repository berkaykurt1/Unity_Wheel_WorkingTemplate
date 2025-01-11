using System.Collections;
using System.Collections.Generic;
using GameDeveloper_Case.WhellPiece;
using UnityEngine;
using WhellTierEnums;

namespace GameDeveloper_Case.Whell
{
    public class WhellController : MonoBehaviour
    {
        [SerializeField] private WhellData whellData;
        public WhellData WhellData { get { return whellData; } }

        [SerializeField] private WhellTierEnum whellTier;
        
        [SerializeField] private GameObject whell;
        [SerializeField] private WheelPieceController[] whellPieces;
        
        private void Awake() 
        {
            WhellChieldObjectsAdjust();
        }

        public void WhellChieldObjectsAdjust()
        {
            whellPieces = new WheelPieceController[whell.transform.childCount];
            for (int i = 0; i < whell.transform.childCount; i++)
            {
                whellPieces[i] = whell.transform.GetChild(i).GetComponent<WheelPieceController>();
            }    
            for (int i = 0; i < whellData.Whelllevels[UIManager.Instance.LevelIndex-1].spritesName.Length; i++)
            {
                UIManager.Instance.SetSpriteFromAtlas(whellPieces[i].WhellPieceImage,whellData.Whelllevels[UIManager.Instance.LevelIndex-1].spritesName[i]);
                whellPieces[i].AwardsType = whellData.Whelllevels[UIManager.Instance.LevelIndex-1].awardsEnums[i];
                whellPieces[i].SetWhellPieceAmount(whellData.Whelllevels[0].awardsAmount[i]);
            }
        }

            
    }
}
