using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameDeveloper_Case.Whell;
using UnityEngine.U2D;
using GameDeveloper_Case.Award;
using GameDeveloper_Case.Zone;
using GameDeveloper_Case.SaveSystems;
using GameDeveloper_Case.Level;
using GameDeveloper_Case.SurprisePrizes;

namespace GameDeveloper_Case
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;
        public static UIManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<UIManager>();
                    
                }
                return instance;
            }
        }
        
        #region  Delegate

        public delegate void LevelReset();
        public LevelReset levelReset;

        public delegate void LevelNext();
        public LevelNext levelNext;

        #endregion

        #region  Atlas

        [Header("Sprite Atlas")]
        [SerializeField] private SpriteAtlas spriteAtlas;

        #endregion

        [Space]
        [Space]

        #region  Whell
        
        [Header("Whell")]
        [SerializeField] private WheelMovementController whellController;
        public WheelMovementController WhellController { get { return whellController; } }
        
        [SerializeField] private IndicatorControl whellIndicator;

        [SerializeField] private Button whellSpinButton;
        public Button WhellSpinButton {get { return whellSpinButton;}}

        private bool hasWhellStopTurning = false;
        public bool HasWhellStopTurning { get { return hasWhellStopTurning;} set { hasWhellStopTurning = value;}}

        

        [SerializeField] private int levelIndex = 1;
        public int LevelIndex {get {return levelIndex;}  set {levelIndex = value;} }
        


        #endregion

        [Space]
        [Space]

        #region  Awards

        [SerializeField] private RectTransform wonAwardsParent;

        Dictionary<string,AwardControl> wonAwards = new Dictionary<string,AwardControl>();

        #endregion

        [Space]
        [Space]

        #region  Zone

        [Header("Zone")]
        [SerializeField] private ZoneControl superZone;
        [SerializeField] private ZoneControl safeZone;
        
        #endregion

        [Space]

        #region  Panel

        [Header("Panel")]        
        [SerializeField] private GameObject losePanel;
        public GameObject LosePanel { get { return losePanel;}}

        [SerializeField] private GameObject settingPanel;

        #endregion

        [Space]

        #region  Buttons

        [Header("Buttons")]
        [SerializeField] private Button exitButton;

        #endregion

        [Space]
        [Space]
        
        #region  Scriptable Objects
        
        [Header("Scriptable Objects")]

        [SerializeField] private LevelSetting levelSetting;
        public LevelSetting LevelSetting { get { return levelSetting;}}

        [SerializeField] private SaveSystemsScriptableObject saveSystemsScriptableObject;
        public SaveSystemsScriptableObject SaveSystemsScriptableObject {get { return saveSystemsScriptableObject;} }
        
        [SerializeField] private WonAwards_ScriptableObject wonAwards_ScriptableObject;


        [SerializeField] private SurprisePrizes_ScriptableObject surprisePrizes_ScriptableObject;

        
        [Space]
        [Space]

        #endregion

        #region  Award Information

        [Header("Award Information")] 
        [SerializeField] private AwardInformationControl supriseAwardInformationControl;

        private Sprite wonAwardSprite;
        private int wonAwardAmount;
        
        private Sprite wonSupriseAwardSprite;
        private int wonSupriseAwardAmount;

        #endregion

        [Space]

        #region  Other Variable

        [Header("Other Variable")]
        private bool gameOver = false;
        public bool GameOver { get { return gameOver;} set { gameOver = value; } }
        
        #endregion

        private void OnEnable() 
        {
            
            whellSpinButton.onClick.AddListener(delegate{
                gameOver = false;
                StartCoroutine(whellController.WhellRotation());
                whellSpinButton.interactable = false;
                hasWhellStopTurning = false;
            });
            exitButton.onClick.AddListener(ExitButton);
        }

        private void Awake() 
        {
            if(saveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsQuery("levelIndex"))
            {
                if(wonAwards_ScriptableObject.AwardsCount.Count != 0)
                {
                    levelIndex = saveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsLoad("levelIndex",0);
                }
            }

            ControlWhellSpinActive();

            safeZone.SetZoneSprite("zone_current");
            superZone.SetZoneSprite("zone_super");

            int newY = levelSetting.LevelCount*110;
            wonAwardsParent.sizeDelta = new Vector2(300,newY);
            wonAwardsParent.anchoredPosition = new Vector2(150,-newY / 2);

            CreateWonAward();

            

            ChangeLevelWhellSprite();

            levelNext += safeZone.ZoneNumberNext;
            levelNext += superZone.ZoneNumberNext;

            
        }

        private void Start() 
        {
            if(levelIndex == 1)
            {
                GameReset();
            }
    
        }

        
        
        //you is a function for setting icon
        public void SettingButton()
        {
            settingPanel.SetActive(!settingPanel.activeSelf);
        }


        
        //This function setting sprite of a object
        public void SetSpriteFromAtlas(Image image,string spriteName)
        {
            image.sprite = spriteAtlas.GetSprite(spriteName);
        }

        //This function creates the you earn
        public void CreateAward(string wonAwardIconSpriteName,int _wonAwardAmount,bool isAward)
        {
            AwardControl newWonAwardGameObject;
            if(!WonAwardsQuery(wonAwardIconSpriteName))
            {
                newWonAwardGameObject =Instantiate(wonAwards_ScriptableObject.AwardPrefab,wonAwardsParent);
                
                newWonAwardGameObject.name  = wonAwardIconSpriteName;
                
                newWonAwardGameObject.transform.localScale = Vector3.one;
                newWonAwardGameObject.transform.localPosition = Vector3.zero;
                
                newWonAwardGameObject.AwardFeaturesIdentification(wonAwardIconSpriteName,_wonAwardAmount);
                
                

                wonAwards_ScriptableObject.Awards.Add(newWonAwardGameObject.name);
                wonAwards_ScriptableObject.AwardsCount.Add(newWonAwardGameObject.GetAwardCount());

                wonAwards.Add(wonAwardIconSpriteName,newWonAwardGameObject);

            }
            else
            {
                int awardNumber = int.Parse(wonAwards[wonAwardIconSpriteName].AwardCountText.text);
                wonAwards[wonAwardIconSpriteName].AwardCountBoosting(_wonAwardAmount);

                for (int i = 0; i < wonAwards_ScriptableObject.Awards.Count; i++)
                {
                    if(wonAwards_ScriptableObject.Awards[i] == wonAwardIconSpriteName)
                    {
                        wonAwards_ScriptableObject.AwardsCount[i] = wonAwards[wonAwardIconSpriteName].GetAwardCount();
                    }
                }
            }

            if(isAward)
            {
                wonAwardSprite = spriteAtlas.GetSprite(wonAwardIconSpriteName);
                wonAwardAmount = _wonAwardAmount;
            }
            
            if(levelIndex != 30)
            {
                AwardInformation(false);
            }
        }

        //This function creates the rewards you earn
        public void CreateWonAward()
        {
            for (int i = 0; i < wonAwards_ScriptableObject.Awards.Count; i++)
            {
                AwardControl newWonAwardGameObject =Instantiate(wonAwards_ScriptableObject.AwardPrefab,wonAwardsParent);
                
                newWonAwardGameObject.name  = wonAwards_ScriptableObject.Awards[i];
                
                newWonAwardGameObject.transform.localScale = Vector3.one;
                newWonAwardGameObject.transform.localPosition = Vector3.zero;

            
                newWonAwardGameObject.AwardFeaturesIdentification(newWonAwardGameObject.name,wonAwards_ScriptableObject.AwardsCount[i]);
                wonAwards.Add(newWonAwardGameObject.name,newWonAwardGameObject);
            }
        }

        //used to ask about the prizes you have won
        public bool WonAwardsQuery(string wonAwardIconSpriteName)
        {
            for (int i = 0; i < wonAwards_ScriptableObject.Awards.Count; i++)
            {
                if(wonAwards_ScriptableObject.Awards[i] == wonAwardIconSpriteName)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnApplicationQuit() 
        {
            if(gameOver)
            {
                wonAwards_ScriptableObject.Awards.Clear();    
            }
        }

        //allows to change the sprites of the wheel according to the level
        public void ChangeLevelWhellSprite()
        {
            string whellSpriteName = levelIndex % 30 == 0 ? "golden_whell" : levelIndex % 5 == 0 ? "bronze_whell" : "silver_whell";

            switch(whellSpriteName)
            {
                case "silver_whell":
                    SetSpriteFromAtlas(whellIndicator.IndicatorImage,"silver_indicator");
                break;
                case "bronze_whell":
                    SetSpriteFromAtlas(whellIndicator.IndicatorImage,"bronze_indicator");
                break;
                case "golden_whell":
                    SetSpriteFromAtlas(whellIndicator.IndicatorImage,"golden_indicator");
                break;
                default:
                break;

            }

            int whellSpriteIndex = levelIndex % 30 == 0 ?  2: levelIndex % 5 == 0 ? 1: 0;
            whellController.GetComponent<Image>().sprite =whellController.GetComponent<WhellController>().WhellData.WhellPieceSprites[whellSpriteIndex];
        }

        //method that allows us to move to the next level
        public void NextLevel()
        {
            SurprisePrizes();
            levelNext();
            ChangeLevelWhellSprite();
            whellController.GetComponent<WhellController>().WhellChieldObjectsAdjust();
        }

        

        public void LosePanelActive()
        {
            losePanel.SetActive(true);
        }

        //method that allows us to continue to the next level
        public void GameContuine()
        {
            levelSetting.LevelManager.GetScrollRectHorizontal();
            LosePanelPasif();
        }

        //allows us to reset the game except for gold
        public void GameReset()
        {
            safeZone.ZoneNumberReset();
            superZone.ZoneNumberReset();
            levelReset();
            LosePanelPasif();
            wonAwards_ScriptableObject.Awards.Clear();
            wonAwards_ScriptableObject.AwardsCount.Clear();
            
            for (int i = 0; i < wonAwardsParent.childCount; i++)
            {
                Destroy(wonAwardsParent.GetChild(i).gameObject);
            }
            wonAwards.Clear();
        }

       

        public void LosePanelPasif()
        {
            losePanel.SetActive(false);
        }


        //allows us to create surprise prizes
        public void SurprisePrizes()
        {
            if(levelIndex % 30 == 0)
            {
                int randomNumber = Random.Range(0, surprisePrizes_ScriptableObject.SurprisePrizesSprites.Length);

                int supriseAwardAmount = 0;

                supriseAwardAmount = Random.Range(surprisePrizes_ScriptableObject.SurprisePrizes[randomNumber].minAmount, surprisePrizes_ScriptableObject.SurprisePrizes[randomNumber].maxAmount);

                wonSupriseAwardSprite = surprisePrizes_ScriptableObject.SurprisePrizesSprites[randomNumber];
                wonSupriseAwardAmount  =supriseAwardAmount;

                CreateAward(surprisePrizes_ScriptableObject.SurprisePrizesSprites[randomNumber].name, supriseAwardAmount,false);
                AwardInformation(true);
            }

        }


        public void ExitButton()
        {
            Application.Quit();
        }

        //hangi ödülleri kazandığımızı ui olarak gösteren method
        private void AwardInformation(bool value)
        {
            supriseAwardInformationControl.AwardInformation(wonAwardSprite,wonAwardAmount,wonSupriseAwardSprite,wonSupriseAwardAmount,value);
            
        }

        public void ControlWhellSpinActive()
        {
            if(levelIndex == levelSetting.LevelCount)
            {
                whellSpinButton.interactable = false;
            }
        }

    }
}

