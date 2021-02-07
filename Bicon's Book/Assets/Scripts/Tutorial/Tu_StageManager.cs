using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ForceActionInfo
{
    public bool Active;
    public int direction;
    public string CardName;
    public int bodypartCamp;
    public BodyPartType TargetType;
}
[System.Serializable]
public class HardHintInfo
{
    public bool Active;
    public string Content;
    public Vector3 MainPos;
    public bool HasPointer;
    public Vector3 PointerPos;
    public float PointerRotZ;
}
[System.Serializable]
public class SoftHintInfo
{
    public bool Active;
    public string Content;
    public float Duration;
    public int Camp;
}
[System.Serializable]
public class StageInfo
{
    public SoftHintInfo softHint;
    public HardHintInfo hardHint;
    public ForceActionInfo forceAction;
    public StageTargetType targetType;
    public float duration;
    public bool forceEndTurn;
}
public enum StageTargetType
{
    Move,
    Card,
    End,
    Time,
    Bodypart,
    ClickAnyPlace,
    TurnStart
}
public class Tu_StageManager : MonoBehaviour
{
    public StageInfo[] stageInfos;
    public int stageIndex;

    public bool isTutorialMode;

    public Tu_SoftHint SoftHintInstance;
    public Tu_HardHint HardHintInstance;

    public GameObject SoftHintPrefab;
    public GameObject HardHintPrefab;

    public AICommandGroup[] AICommandGroups;

    public CardDeckReplacer deckReplacer;
    public bool useDeckReplacer;

    public static Tu_StageManager instance;

    public string[] PlayerDrawCardNames;
    public string[] EnemyDrawCardNames;
    private int PlayerDrawCardIndex;
    private int EnemyDrawCardIndex;
    private void Awake()
    {
        instance = this;
        if(GameWorldSetting.TutorialOpen == false)
        {
            isTutorialMode = false;
            enabled = false;
        }
    }
    private void Start()
    {
        StartCoroutine(InitializeStageManager());
    }
    private IEnumerator InitializeStageManager()
    {
        yield return new WaitForSeconds(0.1f);
        isTutorialMode = true;
        UIManager.instance.TurnEndButtonEvent += Instance_TurnEndButtonEvent;
        InGameManager.instance.CardManagers[0].MoveEvent += Tu_StageManager_MoveEvent;
        InGameManager.instance.CardManagers[0].UseCardEvent += Tu_StageManager_UseCardEvent;
        InGameManager.instance.BattleStartEvent += Instance_BattleStartEvent;
        InGameManager.instance.CardManagers[0].ChangeTargetBodypartEvent += Tu_StageManager_ChangeTargetBodypartEvent;
        InGameManager.instance.OnTurnChanged += OnTurnChange;
        
        HardHintInstance = Instantiate(HardHintPrefab, UIManager.instance.transform).GetComponent<Tu_HardHint>();
        SoftHintInstance = Instantiate(SoftHintPrefab, UIManager.instance.transform).GetComponent<Tu_SoftHint>();
    }



    public bool canDoAction(StageTargetType type,BodyPart bodyPart,Card_Base card, int direction)
    {
        if (InGameManager.IsPausing)
        {
            return false;
        }
        if (!isTutorialMode)
        {
            return true;
        }  
        ForceActionInfo info = stageInfos[stageIndex].forceAction;
        if (info.Active)
        {
            if (type != stageInfos[stageIndex].targetType)
            {
                return false;
            }
            switch (type)
            {
                case StageTargetType.Bodypart:
                    return bodyPart != null && bodyPart.thisCharacter.camp == info.bodypartCamp && bodyPart.GetBodyPartType() == info.TargetType;
                case StageTargetType.Card:
                    return card != null && card.Name == info.CardName;
                case StageTargetType.Move:
                    return direction == info.direction;
                default:
                    return true;
            }
        }
        else
        {
            return true;
        }
    }


    private void Tu_StageManager_ChangeTargetBodypartEvent(BodyPart obj)
    {
        if (stageInfos[stageIndex].targetType == StageTargetType.Bodypart)
        {
            StageFinish();
        }
    }
    private void OnTurnChange(TurnState newState)
    {
        if (stageInfos.Length > stageIndex && stageInfos[stageIndex].targetType == StageTargetType.TurnStart)
        {
            if (newState == TurnState.TurnStart && InGameManager.instance.CampTurn == 0)
            {
                StageFinish();
            }
        }
    }

    private void Instance_BattleStartEvent()
    {
        StartCoroutine(StageStartProcess());        
    }

    private void Tu_StageManager_UseCardEvent(Card_Base arg1, TriggerType arg2)
    {
        if (stageInfos[stageIndex].targetType == StageTargetType.Card)
        {
            StageFinish();
        }
    }

    private void Tu_StageManager_MoveEvent(int obj)
    {
        if (stageInfos[stageIndex].targetType == StageTargetType.Move)
        {
            StageFinish();
        }
    }

    private void Instance_TurnEndButtonEvent()
    {
        if (stageInfos[stageIndex].targetType == StageTargetType.End)
        {
            StageFinish();
        }
    }
    public void StageFinish()
    {
        stageIndex++;
        if (stageIndex >= stageInfos.Length)
        {
            isTutorialMode = false;
            InGameManager.instance.Characters[1].GetComponent<AIBehavior>().SetControl(false);
            UIManager.instance.TurnEndButtonEvent -= Instance_TurnEndButtonEvent;
            InGameManager.instance.CardManagers[0].MoveEvent -= Tu_StageManager_MoveEvent;
            InGameManager.instance.CardManagers[0].UseCardEvent -= Tu_StageManager_UseCardEvent;
            InGameManager.instance.CardManagers[0].ChangeTargetBodypartEvent -= Tu_StageManager_ChangeTargetBodypartEvent;
        }
        else
        {
            StageStart();
        }
    }

    public void StageStart()
    {
        if (stageIndex < stageInfos.Length)
        {
            HardHintInstance.UpdateHardHint(stageInfos[stageIndex].hardHint);
            SoftHintInstance.UpdateSoftHint(stageInfos[stageIndex].softHint);
            if (stageInfos[stageIndex].forceEndTurn)
            {
                InGameManager.instance.ChangeState(TurnState.TurnEnd);
            }
        }
        InGameManager.instance.CardManagers[0].TargetBodyPart = InGameManager.instance.Characters[1].GetBodyPart(BodyPartType.Head);
    }

    private IEnumerator StageStartProcess()
    {
        yield return new WaitForSeconds(4);
        InGameManager.instance.Characters[1].GetComponent<AIBehavior>().SetControl(true);
        StageStart();
    }

    public void DrawCard(CardManager cm)
    {
        if (cm.camp == 0)
        {
            if (PlayerDrawCardNames.Length > PlayerDrawCardIndex)
            {
                if (PlayerDrawCardNames[PlayerDrawCardIndex].Length > 0)
                {
                    cm.DrawCard(PlayerDrawCardNames[PlayerDrawCardIndex]);
                }          
                PlayerDrawCardIndex++;
            }
            else
            {
                cm.DrawCard();
            }
        }
        if (cm.camp == 1)
        {
            if (EnemyDrawCardNames.Length > EnemyDrawCardIndex)
            {
                if (EnemyDrawCardNames[EnemyDrawCardIndex].Length > 0)
                {
                    cm.DrawCard(EnemyDrawCardNames[EnemyDrawCardIndex]);
                }
                EnemyDrawCardIndex++;
            }
            else
            {
                cm.DrawCard();
            }
        }
    }
}
