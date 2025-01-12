using System.Collections;
using System.Collections.Generic;
using GameDeveloper_Case;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameDeveloper_Case.Economy
{
    public class EconomyManager : MonoBehaviour
    {
        [SerializeField] private EconomyScriptableObject economyScriptableObject;
        public static EconomyManager Instance { get; private set; }
        [SerializeField] private Image gold;
        [SerializeField] private TextMeshProUGUI goldAmountText;
        
        [SerializeField]private int currentGold;
        public int CurrentGold { get { return currentGold; } }

        private void Awake()
        {
            UIManager.Instance.SetSpriteFromAtlas(gold, "gold");

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Oyun sahneleri değişse bile yok olmasın
            }
            else
            {
                Destroy(gameObject);
            }

            LoadEconomyValue();
            WriteGoldText();

        }

        //gold attracts its value
        public int GetGold()
        {
            return currentGold;
        }

        //gold increases its value
        public void AddGold(int amount)
        {
            currentGold += amount;
            WriteGoldText();

        }

        private bool isSpend = false;
        public bool IsSpend { get { return isSpend;} set { isSpend = value;}}

        //gold depreciates in value
        public void SpendGold(int amount)
        {
            if(!isSpend)
            {
                if (currentGold >= amount && currentGold > 0)
                {
                    print("amount : " + amount);
                    currentGold -= amount;
                    goldAmountText.text = currentGold.ToString();
                    WriteGoldText();
                }
                isSpend = true;
            }
            else
            {
                isSpend = false;
            }
           
            
        }

        //prints the gold value to its text
        public void WriteGoldText()
        {
            goldAmountText.text = currentGold.ToString();
            SaveEconomyValue();
        }
        
        //gold records its value
        public void SaveEconomyValue()
        {
            economyScriptableObject.Gold = currentGold;
        }

        //uploading the value of gold
        public void LoadEconomyValue()
        {
            currentGold = economyScriptableObject.Gold;
        }


        //gold resets its value
        public void EconomyValueReset()
        {
            currentGold = 0;
            economyScriptableObject.Gold = currentGold;
            goldAmountText.text = currentGold.ToString();
        }

    }
}

