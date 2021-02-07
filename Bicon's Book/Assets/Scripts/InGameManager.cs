using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;
    public static bool IsPausing;
    public int CampTurn = 0;
    public int TurnCount = 0;

    public CharacterManager[] Characters = new CharacterManager[2];
    public CardManager[] CardManagers = new CardManager[2];
    public EquipmentManager[] EquipmentManagers = new EquipmentManager[2];
    public BuffManager[] BuffManagers = new BuffManager[2];
    public AbilityManager[] AbilityManagers = new AbilityManager[2];

    public int GridAmount = 12;
    public float GridDistance = 4;
    public float CharacterDistance = 8;
    public Vector3[] GridPositions;
    [SerializeField]
    private GameObject GridPrefab;
    [SerializeField]
    private GameObject GridParent;
    private GridInstance[] GridInstances;
    public int[] CharacterPositions = { 0, 5 };
    public int[] CharacterPositionsUpdate = { 0, 0 };

    public static int EnemyDefeat;
    public static int BossDefeat;
    public static int FinalGold;

    public bool IsBattle = false;
    private bool IsWaitingBattleEndProcess = false;
    public bool PlayerWin = true;
    public bool LostGameOver = false;
    public bool IsBoss = false;
    public int FlyAmount = 1;


    public Vector3 GridToPosition(int grid)
    {
        return GridPositions[grid];
    }
    public bool CanUseAction(CardManager userManager)
    {
        return (CampTurn == userManager.camp && turnState == TurnState.Card && IsBattle);
    }
    public int GetDistance()
    {
        return Mathf.Abs(CharacterPositions[1] - CharacterPositions[0]);
    }
    public void CharacterMoveAndUgradeGrids(int camp, int distance)
    {
        CharacterMove(camp, distance);

        UpdateGridPostions();

        OnCharacterMoveEvent();
    }
    public void OnCharacterMoveEvent()
    {
        if (OnEnemyMoving != null)
        {
            OnEnemyMoving.Invoke();
        }
    }
    public void CharacterMove(int Camp, int distance)
    {
        int MoveToPos = CharacterPositions[Camp];
        if (distance > 0)
        {
            for (int i = 1; i <= distance; i++)
            {
                MoveToPos = CharacterPositions[Camp] + i;
                if (MoveToPos < GridAmount && CharacterPositions[1 - Camp] != MoveToPos)
                {
                    continue;
                }
                else
                {
                    MoveToPos -= 1;
                    break;
                }
            }
        }
        else
        {
            for (int i = -1; i >= distance; i--)
            {
                MoveToPos = CharacterPositions[Camp] + i;
                if (MoveToPos >= 0 && CharacterPositions[1 - Camp] != MoveToPos)
                {
                    continue;
                }
                else
                {
                    MoveToPos += 1;
                    break;
                }
            }
        }
        CharacterPositions[Camp] = MoveToPos;
        if (CharacterMoveEvent != null)
        {
            CharacterMoveEvent(Camp, distance);
        }
    }
    private void Awake()
    {
        instance = this;
        GenerateGrid();
    }
    private void Start()
    {
        EnemyDefeat = 0;
        BossDefeat = 0;
        FinalGold = 0;
        StartCoroutine(GameStart());
    }
    private void Update()
    {
        if (IsWaitingBattleEndProcess && !AnimationManager.instance.isInAnimationMode)
        {
            IsWaitingBattleEndProcess = false;
            StartCoroutine(BattleEndProcess());
        }
        // Debug.Log("0: " + CharacterPositions[0]);
        // Debug.Log("1: " + CharacterPositions[1]);
    }
    public TurnState turnState = TurnState.Null;
    public event System.Action<TurnState> OnTurnChanged;
    public void ChangeState(TurnState NewState)
    {
        turnState = NewState;
        if (OnTurnChanged != null)
        {
            OnTurnChanged(NewState);
        }
        switch (NewState)
        {
            case TurnState.TurnStart:
                StartCoroutine(TurnStart());
                break;
            case TurnState.Card:
                break;
            case TurnState.TurnEnd:
                StartCoroutine(TurnEnd());
                break;
        }
    }
    public void BattleEnd(bool playerWin)
    {
        if (IsBattle)
        {
            IsBattle = false;
            PlayerWin = playerWin;
            IsWaitingBattleEndProcess = true;
            if (BattleEndEvent != null)
                BattleEndEvent();
        }
    }
    private IEnumerator BattleEndProcess()
    {
        yield return new WaitForSeconds(1);
        if (BattleEndProcessStart != null)
        {
            BattleEndProcessStart();
        }
    }
    public void ReturnToCamp()
    {
        if (ReturnToCampEvent != null)
        {
            ReturnToCampEvent();
        }
        ChangeState(TurnState.Null);
    }
    private IEnumerator TurnStart()
    {
        yield return new WaitForSeconds(0.3f);
        if (turnState == TurnState.TurnStart && IsBattle)
        {
            ChangeState(TurnState.Card);
        }
    }
    private IEnumerator TurnEnd()
    {
        if (CampTurn == 1)
        {
            TurnCount++;
        }
        CampTurn = 1 - CampTurn;
   
        yield return new WaitForSeconds(1);
        if (turnState == TurnState.TurnEnd && IsBattle)
        {
            ChangeState(TurnState.TurnStart);
        }
    }

    public event System.Action ReturnToCampEvent;
    public event System.Action BattleEndProcessStart;
    public event System.Action BattleEndEvent;
    public event System.Action BeforeBattleStartEvent;
    public event System.Action BattleStartEvent;
    public event System.Action OnEnemyMoving;
    public event System.Action<int,int> CharacterMoveEvent;
    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.1f);

        ChangeState(TurnState.Null);
    }

    public void BattleStart()
    {
        IsBattle = true;
        CampTurn = 0;
        TurnCount = 0;


        CharacterPositions[0] = 4 + CharacterPositionsUpdate[0];
        CharacterPositions[1] = 7 + CharacterPositionsUpdate[1];
        UpdateGridPostions();
        Characters[0].transform.position = GridPositions[CharacterPositions[0]];
        Characters[1].transform.position = GridPositions[CharacterPositions[1]];
        CameraManager.instance.SetCameraState(CameraPositionState.Battle);
        CameraManager.instance.SetFollowCharacters(true);
        StartCoroutine(BattleStartProcess());
    }
    public IEnumerator BattleStartProcess()
    {
        yield return new WaitForSeconds(0.1f);
        if (BeforeBattleStartEvent != null)
        {
            BeforeBattleStartEvent();
        }
        if (BattleStartEvent != null)
        {
            BattleStartEvent();
        }
        yield return new WaitForSeconds(3f);
        ChangeState(TurnState.TurnStart);
    }

    public BodyPart GetEnemyBodyPart(CardManager User, BodyPartType type)
    {
        return Characters[1 - User.camp].GetBodyPart(type);
    }
    public BodyPart GetAllyBodyPart(CardManager User, BodyPartType type)
    {
        return Characters[User.camp].GetBodyPart(type);
    }
    public GameObject GetEnemy(CardManager Requester)
    {
        return Characters[1 - Requester.camp].gameObject;
    }
    public static TargetHeightType BodyTypeToHeightIndex(BodyPartType type)
    {
        switch (type)
        {
            case BodyPartType.Chest:
                return TargetHeightType.Middle;
            case BodyPartType.Head:
                return TargetHeightType.Top;
            case BodyPartType.LeftArm:
                return TargetHeightType.Middle;
            case BodyPartType.LeftLeg:
                return TargetHeightType.Bottom;
            case BodyPartType.RightArm:
                return TargetHeightType.Middle;
            case BodyPartType.RightLeg:
                return TargetHeightType.Bottom;
            default:
                return TargetHeightType.Middle;
        }
    }

    public void GenerateGrid()
    {
        GridPositions = new Vector3[GridAmount];

        GridInstances = new GridInstance[GridAmount];
        for (int i = 0; i < GridAmount; i++)
        {
            GridInstances[i] = Instantiate(GridPrefab, GridParent.transform).GetComponent<GridInstance>();
        }
        UpdateGridPostions();
    }
    public void UpdateGridPostions()
    {
        UpdateGridPostions(CharacterPositions[0], CharacterPositions[1]);
    }
    public void UpdateGridPostions(int playerPos, int enemyPos)
    {
        for (int i = 0; i <= playerPos; i++)
        {
            GridPositions[i] = new Vector3(GridDistance * i - ((GridAmount - 1) * GridDistance + CharacterDistance) / 2, 0, 0);
            GridInstances[i].UpdateGridInstance(GridPositions[i] + Vector3.up * -7.5f, i == playerPos);
        }
        for (int i = playerPos + 1; i < enemyPos; i++)
        {
            int gridsAmount = CharacterPositions[1] - CharacterPositions[0];
            GridPositions[i] = new Vector3((i - CharacterPositions[0]) * (CharacterDistance + gridsAmount * GridDistance) / gridsAmount + GridPositions[CharacterPositions[0]].x, 0, 0);
            GridInstances[i].UpdateGridInstance(GridPositions[i] + Vector3.up * -7.5f, false);
        }
        for (int i = enemyPos; i < GridAmount; i++)
        {
            GridPositions[i] = new Vector3(GridDistance * i - ((GridAmount - 1) * GridDistance + CharacterDistance) / 2 + CharacterDistance, 0, 0);
            GridInstances[i].UpdateGridInstance(GridPositions[i] + Vector3.up * -7.5f, i == enemyPos);
        }
    }
}

public enum TurnState
{
    Null,
    TurnStart,
    Card,
    TurnEnd,
}