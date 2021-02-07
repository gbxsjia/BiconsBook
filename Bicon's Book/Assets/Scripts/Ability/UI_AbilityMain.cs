using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UI_AbilityMain : MonoBehaviour
{
    static public UI_AbilityMain thisInstance;
    [SerializeField] List<UI_AbilitySlot> AbilitySlotList;
    [SerializeField] List<UI_AbilityIcon> AbilityIconList;
    [SerializeField] List<UI_AbilityIcon> AbilityIconListInShop;
    public int m_PlayerAbilityLevel = 1;
    [SerializeField] AbilityManager m_PlayerAbilityManager;
    public Button AbilityLockButton;
    public TextMeshProUGUI AbilityLockText;

    [Header("天赋界面UI")]
    [SerializeField] Image CoreAbilityIcon;
    [SerializeField] TextMeshProUGUI CoreAbilityName;
    public List<Image> CoreAbilityBodyPartImageList;

    [SerializeField] Sprite LockedSlotSprite;
    [SerializeField] Sprite UnChangeableSlotSprite;
    [SerializeField] Sprite NormalSlotSprite;

     [SerializeField] GameObject ToolTip;
    [SerializeField] TextMeshProUGUI ToolTipText;

    
    public UI_AbilityIcon SelectAbility;
    public UI_AbilitySlot SelectAbilitySlot;

    [SerializeField] Image InfoIcon;
    [SerializeField] TextMeshProUGUI InfoAbilityName;
    [SerializeField] TextMeshProUGUI InfoAbilityDiscription;
    [SerializeField] TextMeshProUGUI InfoAbilityUnblockWay;

    [Header("天赋获得展示UI")]
    [SerializeField] GameObject m_UnblockShowObj;
    [SerializeField] Image m_UShowIcon;
    [SerializeField] TextMeshProUGUI m_UShowNameText;
    [SerializeField] TextMeshProUGUI m_UShowAbDescriptionText;//天赋效果文本
    [SerializeField] TextMeshProUGUI m_UShowUDescriptionText;//解锁方式文本
    private void Awake()
    {
         thisInstance = this;
    }

    private void AfterFlyEvent()
    {
        if(AchievementSystem.instance.a_CollectorCount>=3)
        {
            LearnAbility(AbilityType.Collector);
        }
        if (AchievementSystem.instance.a_SpringFinderCount >= 2)
        {
            LearnAbility(AbilityType.SpringFinder);
        }
        if(AchievementSystem.instance.a_ContinueEnterMapCount >=6)
        {
            LearnAbility(AbilityType.MasterExplorer);
        }
    }
    private void Start()
    {
       
        AbilitySlotList[0].Equip(AbilityIconList[0]);
        AbilitySlotList[0].m_CharacterAbilitySlot = true;
        AbilityLock();
        UpdateAbilitySlotInList();
        InGameManager.instance.ReturnToCampEvent += OnReturnToCamp;
        EnemyIncubator.thisInstance.m_AfterFlyEvent += AfterFlyEvent;
        CharacterManager.PlayerInstance.AfterSleepEvent += OnAfterSleep;
        CoreAbilityTypeUISet();

       
    }

    public void m_UnblockPerformance(UI_AbilityIcon aAbilityIcon)
    {

        m_UShowNameText.text = aAbilityIcon.thisAbility.AbilityName;
        m_UShowIcon.sprite = aAbilityIcon.GetComponent<Image>().sprite;
        m_UShowAbDescriptionText.text = aAbilityIcon.thisAbility.AbilityDescription;
        m_UShowUDescriptionText.text = aAbilityIcon.thisAbility.UnblockWayDescription;
        m_UnblockShowObj.SetActive(true);
        AudioManager.instance.PlayAbilityUnblockSound();
        m_UnblockShowObj.GetComponent<Animator>().Play("Play");
    }
    public void OnAfterSleep()
    {
        if(AchievementSystem.instance.a_SleepTimes >= 3)
        {
            LearnAbility(AbilityType.HighQualitySleep);
        }
    }
    private void OnReturnToCamp()
    {
        // Unlock Ability
        if (AchievementSystem.instance.a_EmptyHandTurns >= 15)
        {
            LearnAbility(AbilityType.SuiJiYingBian);
        }      
        if (AchievementSystem.instance.a_DrawCardEntryTimes >= 20)
        {
            LearnAbility(AbilityType.FlashMind);
        }  
        if (AchievementSystem.instance.a_UseHandCards >= 50)
        {
            LearnAbility(AbilityType.Popeye);
        }
        if (AchievementSystem.instance.a_FullWeightBattleTimes >= 5)
        {
            LearnAbility(AbilityType.HeavyBearer);
        }
        if (AchievementSystem.instance.a_MoveForwardByCardTimes >= 20)
        {
            LearnAbility(AbilityType.Reckless);
        }  
        if (AchievementSystem.instance.a_MoveForwardTimes >= 30)
        {
            LearnAbility(AbilityType.GoForward);
        }     
        if (AchievementSystem.instance.a_DestroyWeakPoints >= 20)
        {
            LearnAbility(AbilityType.Excitaion);
        }
        if (AchievementSystem.instance.a_BodypartBreakTimes >= 12)
        {
            LearnAbility(AbilityType.FireOn);
        }
        if (AchievementSystem.instance.a_WeakPoints >= 30)
        {
            LearnAbility(AbilityType.WeakPoints);
        }
        if (AchievementSystem.instance.a_UsedPunchCardCount >= 40)
        {
            LearnAbility(AbilityType.KingOfFighter);
        }
    }

    public void LearnAbility(AbilityType type)
    { 
        UI_AbilityIcon aIcon = FindAbilityWithType(type);
        if (aIcon.m_AbilityEnabled == false)
        {
            aIcon.OnAbilityLearn();
            m_UnblockPerformance(aIcon);
        }
    }

        public void ShowToolTip(string aString,Vector3 aPos)
    {
        ToolTip.SetActive(true);
        ToolTipText.text = aString;
        ToolTip.transform.position = aPos;

    }

    public void UnshowToolTip()
    {
        ToolTip.SetActive(false);
    }
    private void Update()
    {
        if (CharacterManager.PlayerInstance.m_Gold >= 500)
        {
            LearnAbility(AbilityType.PriceOfGreed);
        }

        foreach (UI_AbilitySlot a in AbilitySlotList)
        {

            if (a.m_SlotEnabled == true && a.m_Locked == false)
            {
                a.m_OutlineImage.enabled = true;

            }
            else
            {
                a.m_OutlineImage.enabled = false;
            }

            if (a.CurrentIcon == null)
            {
                continue;
            }

        }
        foreach (UI_AbilityIcon a in AbilityIconList)
        {
            if (SelectAbility == null)
            {
                break;
            }
            if (SelectAbility == a)
            {
                a.transform.parent.gameObject.GetComponent<Image>().enabled = true;
            }
            else
            {
                a.transform.parent.gameObject.GetComponent<Image>().enabled = false;
            }
        }

    }

    [SerializeField] GameObject InfoObj;
    [SerializeField] Image LockStateOutLine;
    [SerializeField] Sprite LockedOutline;
    [SerializeField] Sprite UnlockedOutline;

    public void SetInfoObjActive(bool show)
    {
        InfoObj.transform.position = SelectAbility.transform.position;
        InfoObj.SetActive(show);
    }
    public void UpdateAbilityInfo()
    {
        if (SelectAbility != null)
        {
            if (SelectAbility.m_AbilityKnown)
            {
                InfoIcon.sprite = SelectAbility.GetComponent<Image>().sprite;
                InfoAbilityName.text = SelectAbility.thisAbility.AbilityName;
                InfoAbilityDiscription.text = SelectAbility.thisAbility.AbilityDescription;
                InfoAbilityUnblockWay.text = SelectAbility.thisAbility.UnblockWayDescription;
            }
            else
            {
                InfoAbilityName.text = "未知天赋";
                InfoAbilityDiscription.text = "未知效果";
                InfoAbilityUnblockWay.text = "未知解锁方式";
            }
        }
    }

    public static bool m_AllLock = true;

    public void EquipAbilityOnLockState()
    {
        SingleAbilityLock(SelectAbilitySlot);
    }

    public GameObject EquipAbilityCheckPage;

    public void CurrentSlotEquip()
    {
        SelectAbilitySlot.Equip(SelectAbility);
    }
    public void SingleAbilityLock(UI_AbilitySlot abilitySlot)
    {

        abilitySlot.m_Locked = true;

        Ability_Base ability_Base = abilitySlot.CurrentIcon.thisAbility;
        abilitySlot.GetComponent<Image>().sprite = UnChangeableSlotSprite;
        m_PlayerAbilityManager.EquipAbility(ability_Base);
    }
    public void AbilityLock()
    {
        m_AllLock = true;
        LockStateOutLine.sprite = LockedOutline;
        AbilityLockButton.interactable = false;
        gameObject.GetComponent<Animator>().Play("Lock");
        for (int i = 0;i<m_PlayerAbilityLevel;i++)
        {
            if (AbilitySlotList[i].thisAbility != AbilityType.None)
            {

                SingleAbilityLock(AbilitySlotList[i]);

            }
        }   
        UpdateAbilitySlotInList();
    }

   public bool HasUnSaveAbility()
    {
        for (int i = 0; i < m_PlayerAbilityLevel; i++)
        {
            if (AbilitySlotList[i].thisAbility != AbilityType.None)
            {
                if(AbilitySlotList[i].m_CharacterAbilitySlot == false)
                {
                    if (AbilitySlotList[i].m_Locked == false)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public UI_AbilityIcon FindAbilityWithType(AbilityType type)
    {
        foreach (UI_AbilityIcon icon in AbilityIconList)
        {
            if (icon.thisAbilityType == type)
            {
                return icon;
            }
        }
        return null;
    }

    int UnblockPrice = 40;
    [SerializeField] TextMeshProUGUI UnblockPriceText;
    public void PriceIncrease()
    {
        UnblockPrice += 5;
        UnblockPriceText.text = UnblockPrice + "G";
    }
    public void PayUnblock()
    {
        if (CharacterManager.PlayerInstance.PayGold(UnblockPrice))
        {
            AbilityUnblock();
        }
    }
    public void AbilityUnblock()
    {
        m_AllLock = false;
        LockStateOutLine.sprite = UnlockedOutline;
        AbilityLockButton.interactable = true;
        gameObject.GetComponent<Animator>().Play("Unlock");
        for (int i = 0; i < m_PlayerAbilityLevel; i++)
        {
            if (AbilitySlotList[i].m_CharacterAbilitySlot == false)
            {
                AbilitySlotList[i].m_Locked = false;

                UI_AbilityIcon abilityIcon = AbilitySlotList[i].CurrentIcon;
                if (abilityIcon != null)
                {
                    Ability_Base ability_Base = abilityIcon.thisAbility;
                  //  ability_Base.OnAbilityUnEquip();  
                    m_PlayerAbilityManager.UnEquipAbility(ability_Base);
                    AbilitySlotList[i].GetComponent<Image>().sprite = NormalSlotSprite;
                  
                }
            }
        }
        UpdateAbilitySlotInList();
    }

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
        }
        CoreAbilityName.text = aCore.m_CoreAbilityName;
        CoreAbilityIcon.sprite = aCore.m_Icon;
    }
    public void UpdateAbilitySlotInList()
    {
        m_PlayerAbilityLevel = CharacterManager.PlayerInstance.characterLevel;
       if( m_PlayerAbilityLevel > 8)
        {
            m_PlayerAbilityLevel = 8;
        }
        for(int i = 0; i < AbilitySlotList.Count;i++)
        {
            if(i < m_PlayerAbilityLevel)
            {
                AbilitySlotList[i].m_SlotEnabled = true;
                if (AbilitySlotList[i].m_Locked == true)
                {
                    AbilitySlotList[i].GetComponent<Image>().sprite = UnChangeableSlotSprite;
                }
                else
                {
                    AbilitySlotList[i].GetComponent<Image>().sprite = NormalSlotSprite;
                }             
            }
            else
            {
                AbilitySlotList[i].m_SlotEnabled = false;
            }
        }
    }

}
