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
using GameDeveloper_Case.ScriptableObjects;
using GameDeveloper_Case.SpriteAtlas_SO;
using GameDeveloper_Case.AtlasNameEnums;
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


        [SerializeField] private SpriteAtlas spriteAtlas;


        [Space]
        [Space]

        [SerializeField] private WheelMovementController whellController;
        public WheelMovementController WhellController { get { return whellController; } }
        
        [SerializeField] private IndicatorControl whellIndicator;

        [SerializeField] private Button ui_spin_generic_button;
        public Button Ui_Spin_Generic_Button {get { return ui_spin_generic_button;}}

        private bool hasWhellStopTurning = false;
        public bool HasWhellStopTurning { get { return hasWhellStopTurning;} set { hasWhellStopTurning = value;}}

        

        [SerializeField] private int levelIndex = 1;
        public int LevelIndex {get {return levelIndex;}  set {levelIndex = value;} }
        



        [Space]
        [Space]

        #region  Awards

        [SerializeField] private Transform wonAwardsParent;

        Dictionary<string,AwardControl> wonAwards = new Dictionary<string,AwardControl>();

        #endregion

        [Space]
        [Space]
        [SerializeField] private ZoneControl superZone;
        [SerializeField] private ZoneControl safeZone;

        private bool gameOver = false;
        public bool GameOver { get { return gameOver;} set { gameOver = value; } }
        
        [SerializeField] private GameObject losePanel;

        [SerializeField] private Button exitButton;
    
        #region  Scriptable Objects
        
        [SerializeField] private LevelSetting levelSetting;
        public LevelSetting LevelSetting { get { return levelSetting;}}

        [SerializeField] private SaveSystemsScriptableObject saveSystemsScriptableObject;
        public SaveSystemsScriptableObject SaveSystemsScriptableObject {get { return saveSystemsScriptableObject;} }
        
        [SerializeField] private WonAwards_ScriptableObject wonAwards_ScriptableObject;

        [SerializeField] private ScriptableObjects_SO scriptableObjects_SO;
        public ScriptableObjects_SO ScriptableObjects_SO {get {return scriptableObjects_SO;}}   

        [SerializeField] private SpriteAtlas_ScriptableObject spriteAtlas_SO;
        
        [SerializeField] private SurprisePrizes_ScriptableObject surprisePrizes_ScriptableObject;

        public LevelSetting levelSetting_SO;

        #endregion

        #region  Award Information

        [SerializeField] private AwardInformationControl supriseAwardInformationControl;

        private Sprite wonAwardSprite;
        private int wonAwardAmount;
        
        private Sprite wonSupriseAwardSprite;
        private int wonSupriseAwardAmount;

        #endregion

        private void OnValidate() 
        {
            ui_spin_generic_button = GameObject.FindWithTag("ui_spin_generic_button").GetComponent<Button>();

            

            ui_spin_generic_button.onClick.AddListener(delegate{
                gameOver = false;
                StartCoroutine(whellController.WhellRotation());
                ui_spin_generic_button.interactable = false;
                hasWhellStopTurning = false;
            });

            exitButton.onClick.AddListener(ExitButton);

        }

        private void Awake() 
        {
            safeZone.SetZoneSprite("zone_current");
            superZone.SetZoneSprite("zone_super");


            CreateWonAward();

            if(saveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsQuery("levelIndex"))
            {
                if(wonAwards_ScriptableObject.AwardsCount.Count != 0)
                {
                    levelIndex = saveSystemsScriptableObject.PlayerPrefsSaveSystem.PlayerPrefsLoad("levelIndex",0);
                }
            }

            ChangeLevelWhellSprite();

            levelNext += safeZone.ZoneNumberNext;
            levelNext += superZone.ZoneNumberNext;

            print("level index : " + levelIndex);
        }

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                //TakeScreenshot(20,9);
                
                print("level index : " + levelIndex);
                levelIndex = 29;
                NextLevel();

            }

            
            

        }


        void TakeScreenshot(int widthRatio, int heightRatio)
        {
            int targetWidth = Screen.currentResolution.width;
            int targetHeight;

                    targetHeight = Mathf.RoundToInt(targetWidth / 20f * 9f);  // 20:9 oranı

            Screen.SetResolution(targetWidth, targetHeight, true);  // Fullscreen
        }

        public void SetSpriteFromAtlas(Image image,string spriteName)
        {
            image.sprite = spriteAtlas.GetSprite(spriteName);
        }

        public void CreateAward(string wonAwardIconSpriteName,int _wonAwardAmount)
        {
            if(!WonAwardsQuery(wonAwardIconSpriteName))
            {
                AwardControl newWonAwardGameObject =Instantiate(wonAwards_ScriptableObject.AwardPrefab,wonAwardsParent);
                
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

            wonAwardSprite = spriteAtlas.GetSprite(wonAwardIconSpriteName);
            wonAwardAmount = _wonAwardAmount;


            if(levelIndex != 30)
            {
                AwardInformation(false);
            }
        }

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

        public void NextLevel()
        {
            levelNext();
            ChangeLevelWhellSprite();
        }

        

        public void LosePanelActive()
        {
            losePanel.SetActive(true);
        }

        public void GameContuine()
        {
            levelNext();
            LosePanelPasif();
        }

        public void GameReset()
        {
            safeZone.ZoneNumberReset();
            superZone.ZoneNumberReset();
            levelReset();
            LosePanelPasif();
        }
        public void LosePanelPasif()
        {
            losePanel.SetActive(false);
        }


        public void SurprisePrizes()
        {
            if(levelIndex % 30 == 0)
            {
                AwardInformation(true);
                int randomNumber = Random.Range(0, surprisePrizes_ScriptableObject.SurprisePrizesSprites.Length);

                int supriseAwardAmount = 0;

                supriseAwardAmount = Random.Range(surprisePrizes_ScriptableObject.SurprisePrizes[randomNumber].minAmount, surprisePrizes_ScriptableObject.SurprisePrizes[randomNumber].maxAmount);

                wonSupriseAwardSprite = surprisePrizes_ScriptableObject.SurprisePrizesSprites[randomNumber];
                wonSupriseAwardAmount  =supriseAwardAmount;

                CreateAward(surprisePrizes_ScriptableObject.SurprisePrizesSprites[randomNumber].name, supriseAwardAmount);
            }

        }


        public void ExitButton()
        {
            Application.Quit();
        }

        private void AwardInformation(bool value)
        {
            supriseAwardInformationControl.AwardInformation(wonAwardSprite,wonAwardAmount,wonSupriseAwardSprite,wonSupriseAwardAmount,value);
        }
    }
}

