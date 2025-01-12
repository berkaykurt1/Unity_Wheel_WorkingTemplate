using System;
using System.Collections;
using System.Collections.Generic;
using AwardsEnums;
using GameDeveloper_Case.Economy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameDeveloper_Case.WhellPiece
{
    public class WheelPieceController : MonoBehaviour
    {
        [SerializeField] private AwardsEnum awardsType;
        public AwardsEnum AwardsType {get {return awardsType;} set {awardsType = value;} }
        
        [SerializeField] private Image whellPieceImage;
        public Image WhellPieceImage {get {return whellPieceImage;}}

        [SerializeField] private TextMeshProUGUI whellPieceAmountText;
        public TextMeshProUGUI WhellPieceAmountText {get {return whellPieceAmountText;}}

        [SerializeField] private int whellPieceAmount;
        public int WhellPieceAmount {get {return whellPieceAmount;}}

        private void Awake() 
        {

        }

        //method for determining which prize has been won
        public void WhatAwards()
        {
            switch (awardsType)
            {
                case AwardsEnum.Glasses_Easter:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("glasses",whellPieceAmount,true);
                break;
                case AwardsEnum.Cap_Easter:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("cap",whellPieceAmount,true);
                break;
                case AwardsEnum.Cash:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("cash",whellPieceAmount,true);
                break;
                case AwardsEnum.Big_Nolight:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("big_nolight",whellPieceAmount,true);
                break;
                case AwardsEnum.Bronze_Nolight:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("bronze_nolight",whellPieceAmount,true);
                break;
                case AwardsEnum.Gold_Nolight:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("gold_nolight",whellPieceAmount,true);
                break;
                case AwardsEnum.Silver_Nolight:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("silver_nolight",whellPieceAmount,true);
                break;
                case AwardsEnum.Small_Nolight:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("small_nolight",whellPieceAmount,true);
                break;
                case AwardsEnum.Standart_Nolight:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("standart_nolight",whellPieceAmount,true);
                break;
                case AwardsEnum.Super_Nolight:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("super_nolight",whellPieceAmount,true);
                break;
                case AwardsEnum.Gold:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("gold",whellPieceAmount,true);
                    EconomyManager.Instance.AddGold(whellPieceAmount);
                break;
                case AwardsEnum.Pumpkin:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("pumpkin",whellPieceAmount,true);
                break;
                case AwardsEnum.Easter_Time:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("mle_easter",whellPieceAmount,true);
                break;
                case AwardsEnum.Summer_Vice:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("mle_summer",whellPieceAmount,true);
                break;
                case AwardsEnum.Grenade_M26:
                    UIManager.Instance.LosePanelActive();
                break;
                case AwardsEnum.Grenade_M67:
                    UIManager.Instance.LosePanelActive();
                break;
                case AwardsEnum.Neurostim:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("neurostim",whellPieceAmount,true);
                break;
                case AwardsEnum.Regenerator:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("regenerator",whellPieceAmount,true);
                break;
                case AwardsEnum.Molotov:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("molotov",whellPieceAmount,true);
                break;
                case AwardsEnum.Tier1_Shotgun:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("mini_shotgun",whellPieceAmount,true);
                break;
                case AwardsEnum.Tier2_Mle:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("mle",whellPieceAmount,true);
                break;
                case AwardsEnum.Tier2_Rifle:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("rifle",whellPieceAmount,true);
                break;
                case AwardsEnum.Tier3_Shotgun:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("shotgun",whellPieceAmount,true);
                break;
                case AwardsEnum.Tier3_Smg:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("smg",whellPieceAmount,true);
                break;
                case AwardsEnum.Tier3_Sniper:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("sniper",whellPieceAmount,true);
                break;
                case AwardsEnum.Armor_Points:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("Armor_Points",whellPieceAmount,true);
                break;
                case AwardsEnum.Knife_Points:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("Knife_Points",whellPieceAmount,true);
                break;
                case AwardsEnum.Pistol_Points:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("Pistol_Points",whellPieceAmount,true);
                break;
                case AwardsEnum.Rifle_Points:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("Rifle_Points",whellPieceAmount,true);
                break;
                case AwardsEnum.Sniper_Points:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("Sniper_Points",whellPieceAmount,true);
                break;
                case AwardsEnum.Submachine_Points:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("Submachine_Points",whellPieceAmount,true);
                break;
                case AwardsEnum.Vest_Points:
                    UIManager.Instance.NextLevel();
                    UIManager.Instance.CreateAward("Vest_Points",whellPieceAmount,true);
                break;
                default:
                    UIManager.Instance.NextLevel();
                break;
            }
            //UIManager.Instance.SurprisePrizes();
        }
        
        //determines the amount of the prize he wins
        public void SetWhellPieceAmount(int _whellPieceAmount)
        {
            whellPieceAmount = _whellPieceAmount;
            whellPieceAmountText.text = whellPieceAmount.ToString();
        }

    }

}
