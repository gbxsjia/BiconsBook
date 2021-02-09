using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem instance;

    private CardManager playerCardManager;

    [Header("Statistics")]
    public int a_EmptyHandTurns;
    public int a_DrawCardEntryTimes;
    public int a_UseHandCards;
    public int a_FullWeightBattleTimes;
    public int a_MoveForwardTimes;
    public int a_MoveForwardByCardTimes;
    public int a_BodypartBreakTimes;
    public int a_WeakPoints;
    public int a_DestroyWeakPoints;
    public int a_UsedPunchCardCount;
    public int a_SleepTimes;
    public int a_ContinueEnterMapCount;

    public int a_SpringFinderCount;
    public bool a_SpringFinderUnblock;
    public int a_CollectorCount;
    public bool a_CollectorUnblock;
    public int a_RepairAllCount;
    public int a_BuyMedicienCount;
    public int a_FoxTradeCount;
    public int a_LearnAbilityCount;

    public static float LearnAbilityDiscount;
    public static float BuyMedicienDisCount;
    public static float RepairAllDiscound;
    public static float FoxTradeDiscound;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(LateStart());
    }
    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);
        playerCardManager = InGameManager.instance.CardManagers[0];
        InGameManager.instance.OnTurnChanged += OnTurnChanged;
        playerCardManager.UseCardEvent += OnPlayerUseCard;
        En_DrawCard.DrawCardEntryEvent += OnDrawCardEntry;
        InGameManager.instance.BattleStartEvent += OnBattleStart;
        InGameManager.instance.CharacterMoveEvent += OnCharacterMove;
        InGameManager.instance.Characters[0].BodyPartBreakEvent += PlayerBodyPartBreak;
        InGameManager.instance.BuffManagers[1].DestroyWeakPointsEvent += DestroyWeakPoints;
        InGameManager.instance.BuffManagers[1].WeakPointsEvent += WeakPoints;
        InGameManager.instance.CardManagers[0].AfterUsedCard += UsedPunchCardCount;
        UIManager.instance.MoveForwardTimesEvent += MoveForwardTimes;
        CharacterManager.PlayerInstance.SleepEvent += SleepTimes;
        UIManager.instance.EnterMapEvent += EnterMapEvent;
        CharacterManager.PlayerInstance.RepairAllEvent += OnRepairAllEqm;
    }

    public void OnLearnAbility()
    {
        a_LearnAbilityCount++;
    }

    private void OnRepairAllEqm()
    {
        a_RepairAllCount++;
    }

    private void EnterMapEvent()
    {
        if(UIManager.CurrentMapButton.thisPoint != ButtonType.Enemy)
        {
            a_ContinueEnterMapCount++;
        }
        else
        {
            a_ContinueEnterMapCount = 0;
        }
    }
    private void SleepTimes()
    {
        a_SleepTimes++;
    }
    private void MoveForwardTimes()
    {
        a_MoveForwardTimes++;
    }

    private void UsedPunchCardCount(Card_Base card)
    {
        if (card.tag == "Punch" || card.tag == "PerfectPunch")
        {
            a_UsedPunchCardCount++;
        }
       // Debug.Log(a_UsedPunchCardCount);
    }

    private void WeakPoints()
    {
        a_WeakPoints++;
        Debug.Log(a_WeakPoints);
    }

    private void DestroyWeakPoints()
    {
        a_DestroyWeakPoints++;
        Debug.Log(a_DestroyWeakPoints);
    }

    private void PlayerBodyPartBreak(BodyPart obj)
    {
        a_BodypartBreakTimes++;
    }

    private void OnCharacterMove(int camp, int distance)
    {
        if(camp==0 && distance > 0)
        {
            // a_MoveForwardTimes++;
        }
    }

    private void OnBattleStart()
    {
        float AllWeight = 0;
        for (int i = 0; i < 14; i++)
        {
            if (InGameManager.instance.EquipmentManagers[0].GetEquipment((EquipmentType)i) != null)
            {
                AllWeight += InGameManager.instance.EquipmentManagers[0].GetEquipment((EquipmentType)i).Weight;
            }
        }
        // print(AllWeight + " weig");
        if (AllWeight >= 60)
        {
            a_FullWeightBattleTimes++;
        }
    }

    private void OnPlayerUseCard(Card_Base card, TriggerType triggerType)
    {
        if (card.ownerEquipment != null)
        {
            BodyPartType bodyPartType = card.ownerEquipment.AttachedBodyPart.GetBodyPartType();
            if (bodyPartType == BodyPartType.LeftArm || bodyPartType == BodyPartType.RightArm)
            {
                a_UseHandCards++;
            }
        }
    }

    private void OnDrawCardEntry()
    {
        a_DrawCardEntryTimes++;
    }

    private void OnTurnChanged(TurnState state)
    {
        switch (state)
        {
            case TurnState.Card:
                break;
            case TurnState.TurnEnd:
                if (playerCardManager.Cards.Count == 0 && InGameManager.instance.CampTurn == 0)
                {
                    a_EmptyHandTurns++;
                }
                break;
            case TurnState.TurnStart:
                break;
            case TurnState.Null:
                break;
        }
    }
}
