using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    private GameObject PlayerCardHolder;
    [SerializeField]
    private GameObject EnemyCardHolder;
    [SerializeField]
    private Text StaminaText;

    [SerializeField] TextMeshProUGUI FinalScoreText;
    [SerializeField] TextMeshProUGUI FinalGoldScoreText;
    [SerializeField] TextMeshProUGUI EnemyDefeatScoreText;
    [SerializeField] TextMeshProUGUI BossDefeatScoreText;
    [SerializeField] TextMeshProUGUI LostFinalScoreText;
    [SerializeField] TextMeshProUGUI LostFinalGoldScoreText;
    [SerializeField] TextMeshProUGUI LostEnemyDefeatScoreText;
    [SerializeField] TextMeshProUGUI LostBossDefeatScoreText;


    public Camera MainCam;

    public CardManager playerCardManager;

    public GameObject SelectCard=null;
    public GameObject MouseStayCard = null;

    public GameObject BattleUI;
    [SerializeField]
    private GameObject EquipmentUI;
    [SerializeField]
    private GameObject UpgradeUI;
    [SerializeField]
    private GameObject CampUI;
    public GameObject m_DayUI;
    public GameObject m_ChaseUI;
    [SerializeField] GameObject m_SleepUI;
    [SerializeField] GameObject m_GoldUI;
    [SerializeField] GameObject m_RestUI;
    [SerializeField] GameObject m_LeftButtonUI;
    [SerializeField] GameObject m_InnUI;
    [SerializeField] GameObject m_TrainingHallUI;
    [SerializeField] GameObject m_BlackSmithUI;
    public GameObject m_MapInfoUI;
    [SerializeField]
    private GameObject DistanceParent;
    [SerializeField]
    private TextMeshProUGUI DistanceText;


    public static bool MapInfoOpen = false;
    public static EnemySelectButton CurrentMapButton;
    public UI_BattleReward BattleRewardInstance;

    public UI_InventorySlot[] InventorySlots;
    public UI_ShopSlot[] EqmShopSlots;
    public UI_ShopSlot[] InnShopSlots;
    public UI_EquipmentSlot[] EquipmentSlots;
    [SerializeField]
    private Button MoveLeftButton;
    [SerializeField]
    private Button MoveRightButton;

    public List<Image> CoreAbilityBodyPartBgImageList;
    public List<Image> CoreAbilityBodyPartImageList;


    [SerializeField] Image BlackCurtain;

    [SerializeField] TextMeshProUGUI DayText;
    [SerializeField] TextMeshProUGUI TimeText;
    [SerializeField] TextMeshProUGUI GoldText;
    [SerializeField] TextMeshProUGUI GoldTextInEqm;

    [SerializeField] Image TimeCurtain;
    public GameObject EquipmentStatusPanel;
    [SerializeField] UI_WarningText warningTextInstance;
    [SerializeField] TextMeshProUGUI EnemyCardAmountText;
    [SerializeField] TextMeshProUGUI EnemyStaminaAmountText;

    public Scrollbar InventoryScrollbar;
    static public bool InInventoryScrollArea = false;
    public Scrollbar ShopScrollbar;
    static public bool InShopScrollArea = false;

    private bool isInCardArea;
    public void InitializeEquipmentSlots()
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            EquipmentSlots[i].type = (EquipmentType)i;
        }
    }
    [SerializeField] TextMeshProUGUI DifficultyText;
    [SerializeField] TextMeshProUGUI LostDifficultyText;

    [SerializeField] TextMeshProUGUI OverAllHighestScore;
    [SerializeField] TextMeshProUGUI LostOverAllHighestScore;
    public void UpdateFinalScore(bool Win)
    {
        switch (GameWorldSetting.Hardness)
        {
            case -2:
                DifficultyText.text = "祥和";
                LostDifficultyText.text="祥和";
                break;
            case -1:
                DifficultyText.text = "平静";
                LostDifficultyText.text = "平静";
                break;
            case 0:
                DifficultyText.text = "普通";
                LostDifficultyText.text = "普通";
                break;
            case 1:
                DifficultyText.text = "磨难";
                LostDifficultyText.text = "磨难";
                break;
            case 2:
                DifficultyText.text = "英雄";
                LostDifficultyText.text = "英雄";
                break;
        }

        EnemyDefeatScoreText.text = InGameManager.EnemyDefeat +"";
        BossDefeatScoreText.text = InGameManager.BossDefeat + "";
        LostEnemyDefeatScoreText.text = InGameManager.EnemyDefeat + "";
        LostBossDefeatScoreText.text = InGameManager.BossDefeat + "";
        int FinalScore = GameWorldSetting.instance.FinalScoreCaculate(Win);
        FinalScoreText.text = FinalScore + "";
        FinalGoldScoreText.text = InGameManager.FinalGold + "";

        LostFinalScoreText.text = FinalScore + "";
        LostFinalGoldScoreText.text = InGameManager.FinalGold + "";

        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (FinalScore > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", FinalScore);
            }
        }
        else 
        {
            PlayerPrefs.SetInt("HighScore", FinalScore); 
        }

        LostOverAllHighestScore.text = PlayerPrefs.GetInt("HighScore") + "";
        OverAllHighestScore.text = PlayerPrefs.GetInt("HighScore") + "";
    }
    public void UpdateEnemyStatusInfo()
    {
        EnemyCardAmountText.text = InGameManager.instance.CardManagers[1].Cards.Count.ToString();
        EnemyStaminaAmountText.text = InGameManager.instance.CardManagers[1].StaminaCurrent.ToString();
    }
    private void Awake()
    {
        instance = this;
        MainCam = Camera.main;
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlots[i].index = i;
        }
        UpdateDayTimeUI();
        InGameManager.instance.BattleStartEvent += ChaseUIShrink;
        InGameManager.instance.ReturnToCampEvent += ChaseUIExpand;
        ChaserScrollTransparentChange(0.8f);
    }

    [SerializeField] Scrollbar ChaseScrollBar;
    public void ChaserScrollTransparentChange(float Ratio)
    {
        Image[] AllDisImage = ChaseScrollBar.GetComponentsInChildren<Image>();
        foreach(Image a in AllDisImage)
        {
            a.color = new Color(1f, 1f, 1f, Ratio);
        }
    }
    public void ChaseUIExpand()
    {
        m_DayUI.GetComponent<Animator>().Play("Expand");
        m_ChaseUI.GetComponent<Animator>().Play("Expand");
    }
    public void ChaseUIShrink()
    {
        m_DayUI.GetComponent<Animator>().Play("Shrink");
        m_ChaseUI.GetComponent<Animator>().Play("Shrink");
    }

    public enum TimeType
    {
        清晨,
        正午,
        黄昏,
        夜晚
    }

   public TimeType timeType = TimeType.清晨;

    public void UpdateGoldText()
    {
        GoldText.text = "" + InGameManager.instance.Characters[0].m_Gold;
        GoldTextInEqm.text = "" + InGameManager.instance.Characters[0].m_Gold;
    }

    [SerializeField] GameObject GridParentObj;

    [SerializeField] GameObject ChaserDistanceObj;
    [SerializeField] GameObject SecondaryChaserDistanceObj;
    [SerializeField] GameObject SecondaryChaserDangerObj;
    [SerializeField] GameObject PlayerDistanceObj;

    [SerializeField] GameObject PointDisIconPrefab;
    [SerializeField] Sprite PassedPointDisIconSprite;

    [SerializeField] GameObject PlayerDisIconPrefab;
    [SerializeField] GameObject ChaserDisIconPrefab;
    [SerializeField] GameObject BossDisIconPrefab;
    [SerializeField] GameObject SBossDisIconPrefab;
    public void InitialDistanceUI()
    {
        for(int i = 0;i<EnemyIncubator.thisInstance.BossMapId[EnemyIncubator.thisInstance.BossMapId.Count - 1]+1;i++)
        {
            GameObject aPoint = Instantiate(PointDisIconPrefab);
            aPoint.transform.SetParent(GridParentObj.transform);
            aPoint.transform.localScale = new Vector3(1, 1, 1);
        }

        PlayerDistanceObj = Instantiate(PlayerDisIconPrefab);
        PlayerDistanceObj.transform.SetParent(GridParentObj.transform.GetChild(0));
        PlayerDistanceObj.transform.localPosition = new Vector3(-100, 0, 0);
        ChaserDistanceObj = Instantiate(ChaserDisIconPrefab);
        foreach(int i in EnemyIncubator.thisInstance.BossMapId)
        {
            GameObject aBoss;
            if (i != EnemyIncubator.thisInstance.BossMapId[EnemyIncubator.thisInstance.BossMapId.Count - 1])
            {
               aBoss = Instantiate(SBossDisIconPrefab);
            }
            else
            {
                 aBoss = Instantiate(BossDisIconPrefab);
            }
            aBoss.transform.SetParent(GridParentObj.transform.GetChild(i));
            aBoss.transform.localPosition = new Vector3(0, 0, 0);
            aBoss.transform.localScale = new Vector3(1, 1, 1);
            aBoss.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
        }
    }
    public void UpdateDistanceUIInfo()
    {
        if (EnemyIncubator.CurrentMapID >= 0)
        {
            Transform CurrentPointT = GridParentObj.transform.GetChild(EnemyIncubator.CurrentMapID);
           CurrentPointT .GetComponent<Image>().sprite = PassedPointDisIconSprite;
            PlayerDistanceObj.transform.SetParent(CurrentPointT);
        }
        if (EnemyIncubator.EnemyMapID >= 0 && EnemyIncubator.EnemyMapID < 33)
        {
            ChaserDistanceObj.transform.SetParent(GridParentObj.transform.GetChild(EnemyIncubator.EnemyMapID).transform);
        }

        PlayerDistanceObj.transform.localScale = new Vector3(1, 1, 1);
        ChaserDistanceObj.transform.localScale = new Vector3(1, 1, 1);
        PlayerDistanceObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = EnemyIncubator.CurrentMapID + "";
        ChaserDistanceObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = EnemyIncubator.EnemyMapID + "";
        SecondaryChaserDistanceObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = EnemyIncubator.EnemyMapID + "";
    }

    [Header("ArmorHealthUI")]

    [SerializeField] TextMeshProUGUI HeadArmorAmountText;
    [SerializeField] TextMeshProUGUI BodyArmorAmountText;
    [SerializeField] TextMeshProUGUI LeftArmArmorAmountText;
    [SerializeField] TextMeshProUGUI RightArmArmorAmountText;
    [SerializeField] TextMeshProUGUI LeftLegArmorAmountText;
    [SerializeField] TextMeshProUGUI RightLegArmorAmountText;

    [SerializeField] TextMeshProUGUI HeadHealthAmountText;
    [SerializeField] TextMeshProUGUI BodyHealthAmountText;
    [SerializeField] TextMeshProUGUI LeftArmHealthAmountText;
    [SerializeField] TextMeshProUGUI RightArmHealthAmountText;
    [SerializeField] TextMeshProUGUI LeftLegHealthAmountText;
    [SerializeField] TextMeshProUGUI RightLegHealthAmountText;

    [SerializeField] Image HeadArmorAmountImage;
    [SerializeField] Image BodyArmorAmountImage;
    [SerializeField] Image LeftArmArmorAmountImage;
    [SerializeField] Image RightArmArmorAmountImage;
    [SerializeField] Image LeftLegArmorAmountImage;
    [SerializeField] Image RightLegArmorAmountImage;
               
    [SerializeField] Image HeadHealthAmountImage;
    [SerializeField] Image BodyHealthAmountImage;
    [SerializeField] Image LeftArmHealthAmountImage;
    [SerializeField] Image RightArmHealthAmountImage;
    [SerializeField] Image LeftLegHealthAmountImage;
    [SerializeField] Image RightLegHealthAmountImage;


    [Header("DayTime")]
    [SerializeField] Image TimeBackground;
    [SerializeField] Image TimeForeground;
    [SerializeField]  Sprite MorningSprite;
    [SerializeField]  Sprite NoonSprite;
    [SerializeField]  Sprite SunsetSprite;
    [SerializeField] Sprite NightSprite;
    [SerializeField] Sprite MorningSprite2;
    [SerializeField] Sprite NoonSprite2;
    [SerializeField] Sprite SunsetSprite2;
    [SerializeField] Sprite NightSprite2;
    public void UpdateDayTimeUI()
    {
        DayText.text = "第" + EnemyIncubator.Day + "天";
        switch(EnemyIncubator.DayTime)
        {
            case (0):
                {
                    TimeBackground.sprite = MorningSprite;
                    TimeForeground.sprite = MorningSprite2;
                    TimeText.text = "清晨";
                    timeType = TimeType.清晨;
                }break;
            case (1):
                {
                    TimeBackground.sprite = NoonSprite;
                    TimeForeground.sprite = NoonSprite2;
                    TimeText.text = "正午";
                    timeType = TimeType.正午;
                }
                break;
            case (2):
                {
                    TimeBackground.sprite = SunsetSprite;
                    TimeForeground.sprite = SunsetSprite2;
                    TimeText.text = "黄昏";
                    timeType = TimeType.黄昏;
                }
                break;
            case (3):
                {
                    TimeBackground.sprite = NightSprite;
                    TimeForeground.sprite = NightSprite2;
                    TimeText.text = "夜晚";
                    timeType = TimeType.夜晚;
                }
                break;
        }

        if (DayTimeChangeEvent != null)
        {
            DayTimeChangeEvent(EnemyIncubator.DayTime);
        }
    }
    public event System.Action<int> DayTimeChangeEvent;

    public event System.Action EnterMapEvent;
    public void EnterMapButtonPressed()
    {
        m_MapInfoUI.GetComponent<Animator>().Play("Close");
        CurrentMapButton.BigMapPoint();
        if (EnterMapEvent != null)
        {
            EnterMapEvent();
        }
    }
    Coroutine curtainProcess;
    public void UseCurtain(float wait,float MiddleWait)
    {
        if (curtainProcess != null)
        {
            StopCoroutine(curtainProcess);
        }
        curtainProcess = StartCoroutine(CurtainProcess(wait,MiddleWait));
    }
    IEnumerator CurtainProcess(float wait, float Duration)
    {
        BlackCurtain.gameObject.SetActive(true);
        yield return new WaitForSeconds(wait);
        float BlackAlpha = 0;
        while (BlackAlpha <= 0.95f)
        {
            BlackAlpha += Time.deltaTime * 1.5f;
            BlackCurtain.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, BlackAlpha));
            yield return new WaitForFixedUpdate();
        }
        BlackCurtain.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(Duration);
        while (BlackAlpha > 0.05)
        {
            BlackAlpha -= Time.deltaTime * 1.5f;
            BlackCurtain.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, BlackAlpha));
            yield return new WaitForFixedUpdate();
        }
        BlackCurtain.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(wait);
        BlackCurtain.gameObject.SetActive(false);
    }
    private void Start()
    {
        InGameManager.instance.BattleEndProcessStart += OnBattleEndPorcessStart;
        InGameManager.instance.BattleStartEvent += Instance_BattleStartEvent;
        StartCoroutine(CheckHoldingCardCondition());
        InitialDistanceUI();
        UpdateDistanceUIInfo();
        InGameManager.instance.BattleStartEvent += CoreAbilityTypeUISet;
        UI_EqmStatusPanel.m_Instance.SetSelectOutlinePos(InventorySlots[0]);
    }

    public void AllArmorHealthAmountUpdate()
    {
        CharacterManager Player = CharacterManager.PlayerInstance;
        if (Player != null)
        {
            HeadArmorAmountText.text = Player.GetBodyPart(BodyPartType.Head).ArmorCurrent + " / " + Player.GetBodyPart(BodyPartType.Head).ArmorMax;
            BodyArmorAmountText.text = Player.GetBodyPart(BodyPartType.Chest).ArmorCurrent + " / " + Player.GetBodyPart(BodyPartType.Chest).ArmorMax;
            LeftArmArmorAmountText.text = Player.GetBodyPart(BodyPartType.LeftArm).ArmorCurrent + " / " + Player.GetBodyPart(BodyPartType.LeftArm).ArmorMax;
            RightArmArmorAmountText.text = Player.GetBodyPart(BodyPartType.RightArm).ArmorCurrent + " / " + Player.GetBodyPart(BodyPartType.RightArm).ArmorMax;
            LeftLegArmorAmountText.text = Player.GetBodyPart(BodyPartType.LeftLeg).ArmorCurrent + " / " + Player.GetBodyPart(BodyPartType.LeftLeg).ArmorMax;
            RightLegArmorAmountText.text = Player.GetBodyPart(BodyPartType.RightLeg).ArmorCurrent + " / " + Player.GetBodyPart(BodyPartType.RightLeg).ArmorMax;

            HeadHealthAmountText.text = Player.GetBodyPart(BodyPartType.Head).HealthCurrent + " / " + Player.GetBodyPart(BodyPartType.Head).HealthMax;
            BodyHealthAmountText.text = Player.GetBodyPart(BodyPartType.Chest).HealthCurrent + " / " + Player.GetBodyPart(BodyPartType.Chest).HealthMax;
            LeftArmHealthAmountText.text = Player.GetBodyPart(BodyPartType.LeftArm).HealthCurrent + " / " + Player.GetBodyPart(BodyPartType.LeftArm).HealthMax;
            RightArmHealthAmountText.text = Player.GetBodyPart(BodyPartType.RightArm).HealthCurrent + " / " + Player.GetBodyPart(BodyPartType.RightArm).HealthMax;
            LeftLegHealthAmountText.text = Player.GetBodyPart(BodyPartType.LeftLeg).HealthCurrent + " / " + Player.GetBodyPart(BodyPartType.LeftLeg).HealthMax;
            RightLegHealthAmountText.text = Player.GetBodyPart(BodyPartType.RightLeg).HealthCurrent + " / " + Player.GetBodyPart(BodyPartType.RightLeg).HealthMax;

            HeadArmorAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.Head).ArmorCurrent / Player.GetBodyPart(BodyPartType.Head).ArmorMax;
            BodyArmorAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.Chest).ArmorCurrent  / Player.GetBodyPart(BodyPartType.Chest).ArmorMax;
            LeftArmArmorAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.LeftArm).ArmorCurrent  /  Player.GetBodyPart(BodyPartType.LeftArm).ArmorMax;
            RightArmArmorAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.RightArm).ArmorCurrent / Player.GetBodyPart(BodyPartType.RightArm).ArmorMax;
            LeftLegArmorAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.LeftLeg).ArmorCurrent /  Player.GetBodyPart(BodyPartType.LeftLeg).ArmorMax;
            RightLegArmorAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.RightLeg).ArmorCurrent  /  Player.GetBodyPart(BodyPartType.RightLeg).ArmorMax;

            HeadHealthAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.Head).HealthCurrent /  Player.GetBodyPart(BodyPartType.Head).HealthMax;
            BodyHealthAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.Chest).HealthCurrent  / Player.GetBodyPart(BodyPartType.Chest).HealthMax;
            LeftArmHealthAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.LeftArm).HealthCurrent  /  Player.GetBodyPart(BodyPartType.LeftArm).HealthMax;
            RightArmHealthAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.RightArm).HealthCurrent  /  Player.GetBodyPart(BodyPartType.RightArm).HealthMax;
            LeftLegHealthAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.LeftLeg).HealthCurrent  /  Player.GetBodyPart(BodyPartType.LeftLeg).HealthMax;
            RightLegHealthAmountImage.fillAmount = (float)Player.GetBodyPart(BodyPartType.RightLeg).HealthCurrent  /  Player.GetBodyPart(BodyPartType.RightLeg).HealthMax;
        }
    }

    private void Instance_BattleStartEvent()
    {
        EquipmentUI.SetActive(false);
        BattleUI.SetActive(true);
        CampUI.SetActive(false);
        DistanceParent.SetActive(true);
    }

    private void OnTimeChange()
    {
        switch(timeType)
        {
            case (TimeType.清晨):
                {
                    TimeCurtain.color = Vector4.Lerp(TimeCurtain.color, new Color(1, 1, 213f / 255f, 0.15f),0.06f);
                }
                break;
            case (TimeType.正午):
                {
                    TimeCurtain.color =Vector4.Lerp(TimeCurtain.color,  new Color(1, 1, 1, 0f),0.06f);
                }                    
                break;               
            case (TimeType.黄昏):    
                {                     
                    TimeCurtain.color =Vector4.Lerp(TimeCurtain.color, new Color(193f / 255f, 143f / 255f, 89f / 255f, 0.2f),0.06f);
                }                    
                break;               
            case (TimeType.夜晚):    
                {                    
                    TimeCurtain.color =Vector4.Lerp(TimeCurtain.color, new Color(8f / 255f, 10f / 255f, 49f / 255f, 0.4f),0.06f);
                }
                break;
        }

    }
    [SerializeField] GameObject EscMenu;

    bool MouseInDistanceUIArea()
    {
        if(Input.mousePosition.x > Screen.width / 4 && Input.mousePosition.x < Screen.width / 4 * 3 && Input.mousePosition.y > Screen.height / 10 * 9)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChaserUIUpdate(float i)
    {
        if (MouseInDistanceUIArea())
        {
            ChaserScrollTransparentChange(1f);
            m_ChaseUI.transform.localScale = Vector3.Lerp(m_ChaseUI.transform.localScale, Vector3.one * 0.85f, 0.1f);
        }
        else
        {
            ChaserScrollTransparentChange(0f);
            m_ChaseUI.transform.localScale = Vector3.Lerp(m_ChaseUI.transform.localScale, Vector3.one * 0.8f, 0.1f);
            if (i < 0)
            {
                if (EnemyIncubator.CurrentMapID > EnemyIncubator.thisInstance.BossMapId[EnemyIncubator.thisInstance.BossMapId.Count - 1] - 6)
                {
                    ChaseScrollBar.value = Mathf.Lerp(ChaseScrollBar.value, 1, 0.1f);

                }
                else if (EnemyIncubator.CurrentMapID < 7)
                {
                    ChaseScrollBar.value = Mathf.Lerp(ChaseScrollBar.value, 0, 0.1f);
                }
                else
                {
                    ChaseScrollBar.value = Mathf.Lerp(ChaseScrollBar.value, (EnemyIncubator.CurrentMapID - 6f) / (EnemyIncubator.thisInstance.BossMapId[EnemyIncubator.thisInstance.BossMapId.Count - 1] - 12f), 0.1f);

                }
            }
            else
            {
                ChaseScrollBar.value = Mathf.Lerp(ChaseScrollBar.value, i, 0.1f);
            }

        }

        if (EnemyIncubator.Day * 4 + EnemyIncubator.DayTime + 1 > 10)
        {
            SecondaryChaserDangerObj.SetActive(false);
            if (Mathf.Abs((EnemyIncubator.EnemyMapID - 6f) / 20f - ChaseScrollBar.value) > 7f / 20f)
            {
                SecondaryChaserDistanceObj.SetActive(true);
            }
            else
            {
                SecondaryChaserDistanceObj.SetActive(false);
            }
        }
        else
        {
            if(MouseInDistanceUIArea())
            {
                SecondaryChaserDangerObj.transform.GetChild(0).gameObject.SetActive(true);
                SecondaryChaserDangerObj.transform.GetChild(1).gameObject.SetActive(false);
                SecondaryChaserDangerObj.transform.GetChild(2).gameObject.SetActive(false);
                SecondaryChaserDangerObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "审判官正在被牵制（" + Mathf.Abs(EnemyIncubator.EnemyMapID) + "）";
            }
            else
            {
                SecondaryChaserDangerObj.transform.GetChild(0).gameObject.SetActive(false);
                SecondaryChaserDangerObj.transform.GetChild(1).gameObject.SetActive(true);
                SecondaryChaserDangerObj.transform.GetChild(2).gameObject.SetActive(true);
                SecondaryChaserDangerObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =Mathf.Abs(EnemyIncubator.EnemyMapID).ToString();
            }
            SecondaryChaserDangerObj.SetActive(true);
            SecondaryChaserDistanceObj.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel")==0.1f)
        {
            if(InShopScrollArea)
            {
                ShopScrollbar.value += 1f;

                if (ShopScrollbar.value > 1)
                {
                    ShopScrollbar.value = 1;
                }
            }

            if(InInventoryScrollArea)
            {
                InventoryScrollbar.value += 0.33f;
                if (InventoryScrollbar.value > 1)
                {
                    InventoryScrollbar.value = 1;
                }
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") == -0.1f)
        {
            if (InShopScrollArea)
            { 
                ShopScrollbar.value -= 1f;
                if(ShopScrollbar.value < 0)
                {
                    ShopScrollbar.value = 0;
                }
            }

            if (InInventoryScrollArea)
            {
                InventoryScrollbar.value -= 0.33f;
                if (InventoryScrollbar.value < 0)
                {
                    InventoryScrollbar.value = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && EnemyIncubator.thisInstance.InCameraMotion == false)
        {

            EnemyIncubator.thisInstance.SetInCameraMotion(true);
            EscMenu.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscMenu.active)
            {
                EnemyIncubator.thisInstance.SetInCameraMotion(false);
                EscMenu.SetActive(false);
            }

        }

        if (EnemyIncubator.CurrentMapID >= 0)
        {
            PlayerDistanceObj.transform.localPosition = Vector3.Lerp(PlayerDistanceObj.transform.localPosition, Vector3.zero, 0.05f);
            ChaserDistanceObj.transform.localPosition = Vector3.Lerp(PlayerDistanceObj.transform.localPosition, Vector3.zero, 0.05f);
        }

        if (EnemyIncubator.thisInstance.InCameraMotion == false)
        {
            ChaserUIUpdate(-1f);
        }

        PlayerMousePosition();
        ArrangeCards();
        MouseUp();
        StaminaText.text =playerCardManager.StaminaCurrent.ToString();
        OnTimeChange();
        MoveLeftButton.interactable = InGameManager.instance.CardManagers[0].CanMove(-1);
        MoveRightButton.interactable = InGameManager.instance.CardManagers[0].CanMove(1);

        UpdateEnemyStatusInfo();
        DistanceText.text = InGameManager.instance.GetDistance().ToString();
        DistanceParent.transform.position = (InGameManager.instance.Characters[0].transform.position + InGameManager.instance.Characters[1].transform.position) / 2;
    }
    public void PlayerTurnEndButton()
    {
        if (Tu_StageManager.instance == null || !Tu_StageManager.instance.canDoAction(StageTargetType.End, null, null, -1))
        {
            return;
        }

        if (InGameManager.instance.CampTurn==0 && InGameManager.instance.turnState==TurnState.Card && InGameManager.instance.IsBattle)
        {
            InGameManager.instance.ChangeState(TurnState.TurnEnd);
            if (TurnEndButtonEvent != null)
            {
                TurnEndButtonEvent();
            }
        }
    }
    public event System.Action TurnEndButtonEvent;
    public void GoldUIActive(bool Active)
    {
        m_GoldUI.SetActive(Active);
    }

   public bool InTown = false;

    public void GoToTown()
    {
        if (InTown == false)
        {
            AudioManager.instance.LowBGM();
            AudioManager.instance.PlayInVillage();
            CameraManager.instance.SetCameraState(CameraPositionState.Town);
            InTown = true;
        }
        else
        {
            AudioManager.instance.MediumBGM();
            AudioManager.instance.PlayWind();
            EnemyIncubator.thisInstance.FlyMap(1);
            InTown = false;
        }
    }
    public void BattleStart()
    {
        InGameManager.instance.BattleStart();
    }

    public GameObject GameWinUI;
    [SerializeField] GameObject GameLoseUI;

    public void GameOver()
    {
        GameLoseUI.SetActive(true);
    }
    public void Button_ReturnToCamp()
    {
        if (EnemyIncubator.thisInstance.OnFinalBossFight() == false)
        {
            EnemyIncubator.thisInstance.InCameraMotion = false;
            BattleRewardInstance.ReturnToCamp();
            InGameManager.instance.ReturnToCamp();
            EquipmentUI.SetActive(true);
            BattleUI.SetActive(false);
            DistanceParent.SetActive(false);
            CampUI.SetActive(true);
        }
        else
        {
            GameWinUI.SetActive(true);
            UpdateFinalScore(true);
        }
    }
    System.Action EndGameEvent;
    public void BackToMainMenu()
    {
        if (EndGameEvent != null)
        {
            EndGameEvent();
        }
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    private void OnBattleEndPorcessStart()
    {
        StartCoroutine(BattleRewardInstance.BattleEndStartProcess(InGameManager.instance.PlayerWin));
    }
    private void PlayerMousePosition()
    {
        bool newState = Input.mousePosition.y <= ReleaseThreshold && Input.mousePosition.y >= 80 ;
        if (newState != isInCardArea)
        {
            isInCardArea = newState;
        }
    }
    public void UpdateCardSibling()
    {
        for (int i = 0; i < playerCardManager.Cards.Count; i++)
        {
            GameObject card = playerCardManager.Cards[i]; if (card != MouseStayCard)
            {
                if (card != null)
                {
                    card.transform.SetAsFirstSibling();
                }
            }
        }
        if (MouseStayCard != null)
        {
            MouseStayCard.transform.SetAsLastSibling();
        }
    }
    public void AddCard(GameObject card, int Camp)
    {
        if (Camp == 0)
        {
            card.transform.SetParent(PlayerCardHolder.transform);
        }
        else
        {
            card.transform.SetParent(EnemyCardHolder.transform);
            card.transform.localPosition = new Vector3(0, 0, 0);
        }
        card.transform.SetAsFirstSibling();
        card.transform.localScale = Vector3.one;
    }
    
    public void MouseDownOnCard(GameObject card)
    {
        if (SelectCard == null && InGameManager.instance.CampTurn==0 && InGameManager.instance.turnState==TurnState.Card && card.GetComponent<Card_Base>().isActive)
        {
            SelectCard = card;
        }
    }
    public void MouseUp()
    {
        if (!Input.GetMouseButton(0) && InGameManager.instance.CampTurn == 0 && InGameManager.instance.turnState == TurnState.Card)
        {
            if (SelectCard != null)
            {
                if (SelectCard.transform.position.y >= ReleaseThreshold)
                {
                    playerCardManager.UseCard(SelectCard);
                }
                SelectCard = null;
            }           
        }
    }
    public event System.Action MoveForwardTimesEvent;
    public void ButtonMove(bool isLeft)
    {
        if (isLeft)
        {
            InGameManager.instance.CardManagers[0].Move(-1);
        }
        else
        {
            InGameManager.instance.CardManagers[0].Move(1);
            if (MoveForwardTimesEvent != null)
            {
                MoveForwardTimesEvent();
            }
        }
    }

    [SerializeField]
    private float ReleaseThreshold = 50;
    [SerializeField]
    private float yM = 0.3f;
    [SerializeField]
    private float YOffset = -9.5f;
    [SerializeField]
    private float NewCardAngle;
    [SerializeField]
    private float NewyM;
    [SerializeField]
    Image WeightImage;
    [SerializeField]
    TextMeshProUGUI CurrentWeightText;
    [SerializeField]
    TextMeshProUGUI MaxStaminaText;

    public void UpdateWeight()
    {
        CardManager PlayerCardM = CharacterManager.PlayerInstance.GetComponent<CardManager>();
        CurrentWeightText.text = PlayerCardM.AllWeight.ToString();
        MaxStaminaText.text = PlayerCardM.StaminaMax.ToString();
        if (PlayerCardM.AllWeight <= 80)
        {
            WeightImage.fillAmount = PlayerCardM.AllWeight / 80f;
        }
        else
        {
            WeightImage.fillAmount = 1;
        }
    }
    public void ArrangeCards()
    {        
        if (playerCardManager.Cards != null)
        {
            int count = playerCardManager.Cards.Count;

            for (int i = 0; i < count; i++)
            {
                float PosIndex = i - (count - 1f) / 2f;               
                
                Vector3 position;                               
                GameObject card = playerCardManager.Cards[i];

                if (card != SelectCard)
                {
                    float angle;
                    if (isInCardArea)
                    {
                        int shrinkAmount = count - 4;
                        if(shrinkAmount > 0)
                        {
                            angle = PosIndex * NewCardAngle *  1f / (1f + shrinkAmount * 0.16f);
                        }
                        else
                        {
                            angle = PosIndex * NewCardAngle;
                        }
                        position = new Vector3(0, NewyM * Screen.height / 1080, 0);
                    }
                    else
                    {
                        int shrinkAmount = count - 2;
                        if (shrinkAmount > 1)
                        {
                            angle = PosIndex * NewCardAngle * 1f / (1f + shrinkAmount * 0.16f);
                        }
                        else
                        {
                            angle = PosIndex * NewCardAngle;
                        }
                        position = new Vector3(0, yM * Screen.height / 1080, 0);
                    }
           
                    position = Quaternion.Euler(0, 0, angle) * position;
                    position += PlayerCardHolder.transform.position + new Vector3(0, YOffset * Screen.height / 1080, 0);

                    card.transform.position = Vector3.Lerp(card.transform.position, position, 0.15f);
                    Quaternion q = Quaternion.Euler(0, 0, angle);
                    card.transform.rotation = Quaternion.Slerp(card.transform.rotation, q, 0.15f);
                }
                else
                {
                    position = Input.mousePosition;
                    position.z = 0;
                    card.transform.position = Vector3.Lerp(card.transform.position, position, 0.2f);
                    card.transform.rotation = Quaternion.Slerp(card.transform.rotation, Quaternion.identity, 0.15f);
                }
            }
        }        
    }
    private IEnumerator CheckHoldingCardCondition()
    {
        while (true)
        {
            if (SelectCard != null)
            {
                Card_Base card = SelectCard.GetComponent<Card_Base>();
                if (!card.IsTargetInRange() && card.MaxDistance > 0)
                {
                    ShowWarningText("目标在射程之外!", 0.1f);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
 
    }
    bool InReadyScene = false;
    bool InventoryOpen = false;
    bool UpgradeOpen = false;
    public bool InEqmShop = false;
    public bool InTrainingHall = false;
    public bool InInn = false;

    public Button BlackSmithSwitchButton;
    public Button InnSwitchButton;
    public Button TrainingHallSwitchButton;
    public GameObject TownSwitchButtons;

    public void ExpendTownSwitchPanel()
    {
        TownSwitchButtons.GetComponent<Animator>().Play("Open");
    }

    public void ShrinkTownSwitchPanel()
    {
        TownSwitchButtons.GetComponent<Animator>().Play("Close");
    }
    public void OutShop(bool IsInShop)
    {
        Shop.instance.CancelTrade();
        EnemyIncubator.thisInstance.InCameraMotion = false;
        InInn = IsInShop;
        InEqmShop = IsInShop;
        SetTownPlaceNull();
    }

    public enum TownPlace
    {
        Null,
        BlackSmith,
        Inn,
        TrainingHall
    }

   public  TownPlace CurrentPlace=TownPlace.Null;

    public void SetTownPlaceNull()
    {
        CurrentPlace = TownPlace.Null;
    }

    public void SetTownPlace(TownPlace townPlace)
    {
        CurrentPlace = townPlace;
    }
    public void CloseCurrentPlacePanel(TownPlace TargetPlace)
    {
        switch(CurrentPlace)
        {
            case (TownPlace.BlackSmith):
                { 
                    InEqmShop = false;
                    m_BlackSmithUI.GetComponent<Animator>().Play("Close");
                    if (TargetPlace == TownPlace.TrainingHall)
                    {
                        CloseInventoryInInn();
                        OpenUpgradeInInn();
                    }
                
                }break;
            case (TownPlace.Inn):
                {  
                    InInn = false;
                    m_InnUI.GetComponent<Animator>().Play("Close");
                    if (TargetPlace == TownPlace.TrainingHall)
                    {
                        CloseInventoryInInn();
                        OpenUpgradeInInn();
                    }
                
                }
                break;
            case (TownPlace.TrainingHall):
                {
                    InTrainingHall = false;
                    m_TrainingHallUI.GetComponent<Animator>().Play("Close");
                    OpenInventoryInShop();
                    CloseUpgradeInInn();

                }
                break;

        }
        CurrentPlace = TargetPlace;
    }
    public void UpdateAllEquipmentValue()
    {
        
        foreach(UI_EquipmentSlot a in EquipmentSlots)
        {
            if (a.thisItem != null)
            {
                a.thisItem.UpdateItemValue(1.5f);
            }
        }
        foreach (UI_InventorySlot a in InventorySlots)
        {
            if (a.thisItem != null)
            {
                a.thisItem.UpdateItemValue(1.5f);
            }
        }
        foreach (UI_ShopSlot a in EqmShopSlots)
        {
            if (a.thisItem != null)
            {
                a.thisItem.UpdateItemValue(3);
            }
        }

        foreach (UI_ShopSlot a in InnShopSlots)
        {
            if (a.thisItem != null)
            {
                a.thisItem.UpdateItemValue(3);
            }
        }
    }

    public void OpenBlackSmithPanel()
    {
        m_BlackSmithUI.GetComponent<Animator>().Play("Open");
        Shop.instance.CancelTrade();
        InEqmShop = true;
        InInn = false;
        InTrainingHall = false;
        CloseCurrentPlacePanel(TownPlace.BlackSmith);
        UpdateAllEquipmentValue();
        
    }
    public void OpenTrainingPanel()
    {
        InEqmShop = false;
        InInn = false;
        InTrainingHall = true;
        m_TrainingHallUI.GetComponent<Animator>().Play("Open");
        CloseCurrentPlacePanel(TownPlace.TrainingHall);
    }
    public void OpenInnPanel()
    {
        Shop.instance.CancelTrade();
        InEqmShop = false;
        InInn = true;
        InTrainingHall = false;
        m_InnUI.GetComponent<Animator>().Play("Open");
        CloseCurrentPlacePanel(TownPlace.Inn);
    }
    public void OpenInventoryInShop()
    {
        EquipmentUI.GetComponentInChildren<Animator>().Play("ExpendInShop");
    }
    public void OpenInventory()
    {
        EquipmentUI.GetComponentInChildren<Animator>().Play("EquipmentPannel_Open");
    }   
    public void CloseInventoryInInn()
    {
        EquipmentUI.GetComponentInChildren<Animator>().Play("CloseInShop");
    }
    public void OpenUpgradeInInn()
    {
        UpgradeUI.GetComponentInChildren<Animator>().Play("OpenInShop");
    }

    public void CloseUpgradeInInn()
    {
        UpgradeUI.GetComponentInChildren<Animator>().Play("CloseInShop");
    }



    public void ShowWarningText(string content,float duration)
    {
        warningTextInstance.ShowText(content, duration);
    }


    [SerializeField] GameObject AbilityUnsavedHintObj;

    public void CoreAbilityTypeUISet()
    {
        CoreAbility_Base aCore = InGameManager.instance.AbilityManagers[0].m_CoreAbility;
        if (aCore != null)
        {
            for (int i = 0; i < CoreAbilityBodyPartImageList.Count; i++)
            {
                if (i < aCore.m_AbilityComboNeedList.Count)
                {
                    CoreAbilityBodyPartImageList[i].sprite = ArtResourceManager.instance.GetAbilityBodypartIcon(aCore.m_AbilityComboNeedList[i].m_BodyPartType);
                }
            }
            for (int i = 0; i < CoreAbilityBodyPartBgImageList.Count; i++)
            {
                if (i < aCore.m_AbilityComboNeedList.Count)
                {
                    CoreAbilityBodyPartBgImageList[i].sprite = ArtResourceManager.instance.GetAbilityBodypartIcon(aCore.m_AbilityComboNeedList[i].m_BodyPartType);
                }
            }
        }
    }

    public void CoreAbilityTriggered(int i )
    {
        CoreAbilityBodyPartImageList[i].enabled = true;
        CoreAbilityBodyPartImageList[i].gameObject.GetComponent<Animator>().Play("Trigger");
    }
    public void CoreAbilityUIBattleUpdate()
    { 
        CoreAbility_Base aCore = InGameManager.instance.AbilityManagers[0].m_CoreAbility;
        for (int i = 0; i < aCore.m_AbilityComboNeedList.Count; i++)
        {
            if (aCore.m_AbilityComboNeedList[i].m_Triggered == false)
            {
                CoreAbilityBodyPartImageList[i].enabled = false;
            }
        }
       
    }
    public void CloseUIPanel(bool force)
    {
        if(UpgradeOpen)
        {
            if (force == false)
            {
                if (UI_AbilityMain.thisInstance.HasUnSaveAbility())
                {
                    AbilityUnsavedHintObj.SetActive(true);
                    return;
                }
            }
        }

        GoldUIActive(true);

        m_LeftButtonUI.GetComponent<Animator>().Play("Close");       
        if (InventoryOpen == true)
        { 
            InventoryOpen = false;
            EquipmentUI.GetComponentInChildren<Animator>().Play("EquipmentPannel_Close");
        }
        if(UpgradeOpen == true)
        {      
            UpgradeOpen = false;
            UpgradeUI.GetComponentInChildren<Animator>().Play("Close");
        } 
        CharacterManager.PlayerInstance.OnInventoryOpen(false);
        CameraManager.instance.SetCameraState(CameraPositionState.Camp);
      
    }
    public void OnCampBuildingClicked(string type)
    {
        switch (type)
        {
            case "Sleep":
                CameraManager.instance.SetCameraState(CameraPositionState.Sleep);
                if (timeType == TimeType.夜晚)
                {
                    m_SleepUI.SetActive(true);
                }
                else
                {
                    m_RestUI.SetActive(true);
                }
                break;
            case "Equipment":
                if (InventoryOpen == false)
                {
                    GoldUIActive(false);
                    m_LeftButtonUI.GetComponent<Animator>().Play("Open");
                   
                    EquipmentUI.GetComponentInChildren<Animator>().Play("EquipmentPannel_Open");
                    CameraManager.instance.SetCameraState(CameraPositionState.Equipment);
                    InventoryOpen = true;


                    if (UpgradeOpen)
                    {
                        UpgradeUI.GetComponentInChildren<Animator>().Play("Close");
                        UpgradeOpen = false;
                    }
                    else
                    { 
                        CharacterManager.PlayerInstance.OnInventoryOpen(true);
                    }
                }
                //else
                //{
                //    GoldUIActive(true);
                //    m_LeftButtonUI.GetComponent<Animator>().Play("Close");
                //    CharacterManager.PlayerInstance.OnInventoryOpen(false);
                //    EquipmentUI.GetComponentInChildren<Animator>().Play("EquipmentPannel_Close");
                //    CameraManager.instance.SetCameraState(CameraPositionState.Camp);
                //    InventoryOpen = false;
                //}
                break;
            case "Upgrade":
                if (UpgradeOpen == false)
                {
                    GoldUIActive(false);
                    m_LeftButtonUI.GetComponent<Animator>().Play("Open");
                    UpgradeUI.GetComponentInChildren<Animator>().Play("Open");
                    
                    CameraManager.instance.SetCameraState(CameraPositionState.Equipment);
                    UpgradeOpen = true;

                    if (InventoryOpen == true)
                    {
                        EquipmentUI.GetComponentInChildren<Animator>().Play("EquipmentPannel_Close");
                        InventoryOpen = false;
                    }
                    else
                    {
                        CharacterManager.PlayerInstance.OnInventoryOpen(true);
                    }
                }
                //else
                //{
                //    GoldUIActive(true);
                //    m_LeftButtonUI.GetComponent<Animator>().Play("Close");
                //    UpgradeUI.GetComponentInChildren<Animator>().Play("Close");
                //    CharacterManager.PlayerInstance.OnInventoryOpen(false);
                //    CameraManager.instance.SetCameraState(CameraPositionState.Camp);
                //    UpgradeOpen = false;
                //}
                break;
            case "Map":
                if (InReadyScene == false)
                {
                    print(1);
                    CameraManager.instance.SetCameraState(CameraPositionState.Map);
                    if(GameWorldSetting.TutorialOpen == true)
                    {
                        print(0);
                        Tu_BigWorld.instance.StartTutorial(1);
                    }
                    InReadyScene = true;
                }
                else
                {
                    CameraManager.instance.SetCameraState(CameraPositionState.Camp);
                    InReadyScene = false;
                }
                break;
        }
    }
    public void OnCampBuildingClicked(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Sleep:
                CameraManager.instance.SetCameraState(CameraPositionState.Sleep);
                if (timeType == TimeType.夜晚)
                {
                    m_SleepUI.SetActive(true);
                }
                else
                {
                    m_RestUI.SetActive(true);
                }
                break;
            case ButtonType.Equipment:
                if (InventoryOpen == false)
                {
                    GoldUIActive(false);
                    m_LeftButtonUI.GetComponent<Animator>().Play("Open");
                    CharacterManager.PlayerInstance.OnInventoryOpen(true);
                    EquipmentUI.GetComponentInChildren<Animator>().Play("EquipmentPannel_Open");
                    CameraManager.instance.SetCameraState(CameraPositionState.Equipment);
                    InventoryOpen = true;
                    AudioManager.instance.PlayBagSound();
                     
                    if(UpgradeOpen) 
                    {
                        GoldUIActive(true);
                        UpgradeUI.GetComponentInChildren<Animator>().Play("Close");
                        UpgradeOpen = false;
                    }
                }
                break;
            case ButtonType.Upgrade:
                if (UpgradeOpen == false)
                {
                    GoldUIActive(false);
                    m_LeftButtonUI.GetComponent<Animator>().Play("Open");
                    CharacterManager.PlayerInstance.OnInventoryOpen(true);
                    UpgradeUI.GetComponentInChildren<Animator>().Play("Open");
                    CameraManager.instance.SetCameraState(CameraPositionState.Equipment);
                    UpgradeOpen = true;
                    AudioManager.instance.PlayBookSound();
                                
                    if(InventoryOpen == true)
                    {
                        EquipmentUI.GetComponentInChildren<Animator>().Play("EquipmentPannel_Close");
                        InventoryOpen = false;
                    }
                }
                else
                {
                    GoldUIActive(true);
                    UpgradeUI.GetComponentInChildren<Animator>().Play("Close");
                    CameraManager.instance.SetCameraState(CameraPositionState.Camp);
                    UpgradeOpen = false;
                }
                break;
            case ButtonType.Map:
                if (InReadyScene == false)
                {
                    if (GameWorldSetting.TutorialOpen == true)
                    {
                        Tu_BigWorld.instance.StartTutorial(1);
                    }
                    CameraManager.instance.SetCameraState(CameraPositionState.Map);
                    InReadyScene = true;
                }
                else
                {
                    CameraManager.instance.SetCameraState(CameraPositionState.Camp);
                    InReadyScene = false;
                }
                break;
        }
    }
}
