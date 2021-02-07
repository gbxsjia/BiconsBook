using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class CharacterManager : MonoBehaviour
{
    public int camp;

    public static CharacterManager PlayerInstance;
    public static CharacterManager EnemyInstance;
    public int characterLevel = 1;

    [HideInInspector]
    public BodyPart[] bodyParts;

    public Animator characterAnimator;
    public Animator equipmentAnimator;

    public bool isDead;
    private bool isWaitingForDeadAnimation;

    [SerializeField]
    private GameObject[] HealthBars;
    [SerializeField]
    public SpriteMeshInstance[] CharacterSpriteMeshRenders;
    [SerializeField]
    public SpriteMeshInstance[] EquipmentSpriteMeshRenders;
    [SerializeField]
    public SpriteMesh[] BodySprites;
    public SpriteMesh[] BrokenBodySprites;
    [SerializeField]
    private GameObject CharacterMeshParent;
    [SerializeField]
    private GameObject EquipmentMeshParent;

    [SerializeField]
    private List<GameObject> RightWeaponParent;
    [SerializeField]
    private List<GameObject> LeftWeaponParent;
    [SerializeField]
    private List<SpriteRenderer[]> RightWeaponRenders;
    [SerializeField]
    private List<SpriteRenderer[]> LeftWeaponRenders;

    [SerializeField]
    private SkinnedMeshRenderer[] HairRenderer;

    public SpriteRenderer[] effectRenderers;

    public bool isMoving;

    public AnimationCurve MovementCurve;

    public int m_Gold = 100;

    public event System.Action<BodyPart> BodyPartBreakEvent;
    public void SetInitialGold()
    {
        if (camp == 0)
        {
            switch (GameWorldSetting.Hardness)
            {
                case -2:
                    m_Gold = 200;
                    break;
                case -1:
                    m_Gold = 100;
                    break;
                case 0:
                    m_Gold = 50;
                    break;
                case 1:
                    m_Gold = 50;
                    break;
                case 2:
                    m_Gold = 25;
                    break;
            }
        }
    }
    public void SetInitialHealth()
    {
        if (camp == 0)
        {
            float HardPercentage = 1;

            switch (GameWorldSetting.Hardness)
            {
                case -2:
                    HardPercentage = 1.5f;
                    break;
                case -1:
                    HardPercentage = 1.25f;
                    break;
                case 0:
                    HardPercentage = 1f;
                    break;
                case 1:
                    HardPercentage = 0.75f;
                    break;
                case 2:
                    HardPercentage = 0.6f;
                    break;
            }

            foreach (BodyPart aPart in bodyParts)
            {
                switch (aPart.GetBodyPartType())
                {
                    case BodyPartType.Chest:
                        aPart.HealthMax = Mathf.RoundToInt(80 * HardPercentage);
                        aPart.HealthCurrent = aPart.HealthMax;
                        break;
                    case BodyPartType.Head:
                        aPart.HealthMax = Mathf.RoundToInt(60 * HardPercentage);
                        aPart.HealthCurrent = aPart.HealthMax;
                        break;
                    case BodyPartType.LeftArm:
                        aPart.HealthMax = Mathf.RoundToInt(50 * HardPercentage);
                        aPart.HealthCurrent = aPart.HealthMax;
                        break;
                    case BodyPartType.RightArm:
                        aPart.HealthMax = Mathf.RoundToInt(50 * HardPercentage);
                        aPart.HealthCurrent = aPart.HealthMax;
                        break;
                    case BodyPartType.LeftLeg:
                        aPart.HealthMax = Mathf.RoundToInt(50 * HardPercentage);
                        aPart.HealthCurrent = aPart.HealthMax;
                        break;
                    case BodyPartType.RightLeg:
                        aPart.HealthMax = Mathf.RoundToInt(50 * HardPercentage);
                        aPart.HealthCurrent = aPart.HealthMax;
                        break;
                }
            }
        }
    }
    public BodyPart GetRandomAliveBodyPart()
    {
        List<BodyPart> AliveBodyParts = new List<BodyPart>();
        foreach (BodyPart bp in bodyParts)
        {
            if (bp.GetIsAlive())
            { AliveBodyParts.Add(bp); }
        }

        return AliveBodyParts[Random.Range( 0, AliveBodyParts.Count)];
    }
    public BodyPart GetBodyPart(BodyPartType type)
    {
        foreach (BodyPart bp in bodyParts)
        {
            if (bp.GetBodyPartType() == type)
            {
                return bp;
            }
        }
        return null;
    }
    public void BodyPartBreak(BodyPart bodyPart)
    {
        switch (bodyPart.GetBodyPartType())
        {
            case BodyPartType.LeftArm:
                SwitchBodyPartSprite(CharacterSpriteMeshType.arm_for_l, BrokenBodySprites[0]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.arm_mid_l, BrokenBodySprites[2]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.hand_back_l, BrokenBodySprites[8]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.hand_front_l, BrokenBodySprites[10]);
                SetWeaponVisibility(false, false);
                break;
            case BodyPartType.LeftLeg:
                SwitchBodyPartSprite(CharacterSpriteMeshType.feet_front_l, BrokenBodySprites[4]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.feet_side_l, BrokenBodySprites[6]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.leg_down_l, BrokenBodySprites[12]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.leg_up_l, BrokenBodySprites[14]);
                break;
            case BodyPartType.RightArm:
                SwitchBodyPartSprite(CharacterSpriteMeshType.arm_for_r, BrokenBodySprites[1]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.arm_mid_r, BrokenBodySprites[3]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.hand_back_r, BrokenBodySprites[9]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.hand_front_r, BrokenBodySprites[11]);
                SetWeaponVisibility(true, false);
                break;
            case BodyPartType.RightLeg:
                SwitchBodyPartSprite(CharacterSpriteMeshType.feet_front_r, BrokenBodySprites[5]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.feet_side_r, BrokenBodySprites[7]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.leg_down_r, BrokenBodySprites[13]);
                SwitchBodyPartSprite(CharacterSpriteMeshType.leg_up_r, BrokenBodySprites[15]);
                break;
            case BodyPartType.Chest:
                isDead=true;
                if (!isWaitingForDeadAnimation)
                {
                    StartCoroutine(PlayDeathAnimationProcess());
                }
                break;
            case BodyPartType.Head:
                isDead = true;
                if (!isWaitingForDeadAnimation)
                {
                    StartCoroutine(PlayDeathAnimationProcess());
                }
                break;
        }
        if (BodyPartBreakEvent != null)
        {
            BodyPartBreakEvent(bodyPart);
        }
    }
    public void ArmorBreak(BodyPart bodyPart)
    {
        switch (bodyPart.GetBodyPartType())
        {
            case BodyPartType.LeftArm:
                SetEquipmentVisibility(CharacterSpriteMeshType.arm_end_l, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.arm_mid_l, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.arm_for_l, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.hand_back_l, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.hand_front_l, false);
                break;
            case BodyPartType.LeftLeg:
                SetEquipmentVisibility(CharacterSpriteMeshType.leg_down_l, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.leg_huxi_l, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.leg_huxi_l_mid, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.leg_up_l, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.feet_front_l, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.feet_side_l, false);
                break;
            case BodyPartType.RightArm:
                SetEquipmentVisibility(CharacterSpriteMeshType.arm_end_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.arm_mid_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.arm_for_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.hand_back_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.hand_front_r, false);
                break;
            case BodyPartType.RightLeg:
                SetEquipmentVisibility(CharacterSpriteMeshType.leg_down_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.leg_huxi_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.leg_huxi_r_mid, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.leg_up_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.feet_front_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.feet_side_r, false);
                break;
            case BodyPartType.Chest:
                SetEquipmentVisibility(CharacterSpriteMeshType.ass_back, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.back, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.hujing_mid, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.ass_front, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.body, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.hujing_for_l, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.ass_front_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.body_r, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.hujing_for, false);
                break;
            case BodyPartType.Head:
                SetEquipmentVisibility(CharacterSpriteMeshType.hair_back, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.hair_mid, false);
                SetEquipmentVisibility(CharacterSpriteMeshType.hair_top, false);
                break;
        }
    }
    [SerializeField] PartDropManager partDropManager;
    private IEnumerator PlayDeathAnimationProcess()
    {
        isWaitingForDeadAnimation = true;
        while (!AnimationManager.instance.IsAnimationAllDone())
        {
            yield return null;
        }
        isWaitingForDeadAnimation = false;
        partDropManager.DropWeapon();
        string animationName = AnimationManager.GetDeathAnimationName();
        characterAnimator.Play(animationName);
        equipmentAnimator.Play(animationName);//print("Play Death" + Time.deltaTime);
    }
    private void SwitchBodyPartSprite(CharacterSpriteMeshType type,SpriteMesh newSpriteMesh)
    {
        GetCharacterRenderer(type).spriteMesh = newSpriteMesh;
    }
    private void SetEquipmentVisibility(CharacterSpriteMeshType type,bool visible)
    {
        SkinnedMeshRenderer skinRenderer = GetEquipmentRenderer(type).GetComponent<SkinnedMeshRenderer>();
        if (skinRenderer != null)
        {
            skinRenderer.enabled = visible; 
        }       
    }
    private void SetWeaponVisibility(bool isRight,bool visible)
    {
        GetWeaponParent(isRight, 0).transform.parent.gameObject.SetActive(visible);
    }

    public void PlayAnimation(AnimType type, TargetHeightType height, AnimDirection direction)
    {
        string animationName = AnimationManager.GetAnimationName(type, height, direction);

        characterAnimator.Play(animationName);
        equipmentAnimator.Play(animationName);
    }



    public void DisplayMovement(int start, int end, int enemyPos)
    {
        isMoving = true;
        StartCoroutine(DisplayMovementProcess(start, end, enemyPos));
    }
    private IEnumerator DisplayMovementProcess(int start,int end, int enemyPos)
    {
        if (enemyPos > start)
        {
            InGameManager.instance.UpdateGridPostions(start, enemyPos);
        }
        else
        {
            InGameManager.instance.UpdateGridPostions(enemyPos, start);
        }
        Vector3 startPos = transform.position;
        Vector3 endPos = InGameManager.instance.GridPositions[start];

        float timer = 0.3f;
        if ((endPos - startPos).magnitude > 0.1f)
        {            
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, MovementCurve.Evaluate(1 - timer / 0.3f));
                yield return null;
            }
        }

        if (enemyPos > end)
        {
            InGameManager.instance.UpdateGridPostions(end, enemyPos);
        }
        else
        {
            InGameManager.instance.UpdateGridPostions(enemyPos, end);
        }
        startPos = transform.position;
        endPos = InGameManager.instance.GridPositions[end];

        string animationName;
        if (end > start)
        {
            animationName = AnimationManager.instance.GetMovementAnimationName(GetComponent<EquipmentManager>(), camp == 0);
            characterAnimator.Play(animationName);
            equipmentAnimator.Play(animationName);
        }
        else if (start > end)
        {
            animationName = AnimationManager.instance.GetMovementAnimationName(GetComponent<EquipmentManager>(), camp == 1);
            characterAnimator.Play(animationName);
            equipmentAnimator.Play(animationName);
        }
        timer = 0.3f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, MovementCurve.Evaluate(1 - timer / 0.3f));
            yield return null;
        }
        PlayIdle();
        isMoving = false;
    }
    public void BackToPosition()
    {
        StartCoroutine(MoveToGridProcess(InGameManager.instance.CharacterPositions[camp]));
    }
    public void MoveToGrid(int grid)
    {

        StartCoroutine(MoveToGridProcess(grid));
    }
    private IEnumerator MoveToGridProcess(int grid)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = InGameManager.instance.GridPositions[grid];

        if ((startPos - endPos).magnitude <= 0.01f)
        {
            yield break;
        }

        float timer = 0.3f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, MovementCurve.Evaluate(1 - timer / 0.5f));
            yield return null;
        }
        if (!isDead)
        {
            PlayIdle();
        }     
    }

    public void PlayIdle()
    {
        //print("Play Idle" + Time.deltaTime);
        string animationName = AnimationManager.instance.GetIdleAnimationName(GetComponent<EquipmentManager>());

        characterAnimator.Play(animationName);
        equipmentAnimator.Play(animationName);
    }

    public void RecoverAllBodyHealth(float amount,float percentage)
    {
        foreach(BodyPart a in bodyParts)
        {
            a.HealthHeal(amount + a.HealthMax * percentage);
            a.UpdateUI();
        }
    }
    public void GetBodySprites()
    {
        Component[] components = CharacterMeshParent.GetComponentsInChildren(typeof(SpriteMeshInstance), true);
        CharacterSpriteMeshRenders = new SpriteMeshInstance[44];
        for (int i = 0; i < components.Length; i++)
        {
            for (int j = 0; j < components.Length; j++)
            {
                CharacterSpriteMeshType type = (CharacterSpriteMeshType)j;
                if (type.ToString() == components[i].gameObject.name)
                {
                    CharacterSpriteMeshRenders[j] = components[i] as SpriteMeshInstance;
                    BodySprites[j] = CharacterSpriteMeshRenders[j].spriteMesh;
                }
            }
        }
    }
    public void GetAllSkinRenders()
    {
        Component[] components = CharacterMeshParent.GetComponentsInChildren(typeof(SpriteMeshInstance), true);
        CharacterSpriteMeshRenders = new SpriteMeshInstance[44];
        for (int i = 0; i < components.Length; i++) //length = 45
        {
            for (int j = 0; j < components.Length; j++)
            {
                CharacterSpriteMeshType type = (CharacterSpriteMeshType)j;
                if (type.ToString() == components[i].gameObject.name)
                {
                    CharacterSpriteMeshRenders[j] = components[i] as SpriteMeshInstance;
                    CharacterSpriteMeshRenders[j].spriteMesh = BodySprites[j];
                }
            }
        }

        components = EquipmentMeshParent.GetComponentsInChildren(typeof(SpriteMeshInstance), true);
        EquipmentSpriteMeshRenders = new SpriteMeshInstance[components.Length];
        for (int i = 0; i < components.Length; i++)
        {
            for (int j = 0; j < components.Length; j++)
            {
                CharacterSpriteMeshType type = (CharacterSpriteMeshType)j;
                if (type.ToString() == components[i].gameObject.name)
                {
                    EquipmentSpriteMeshRenders[j] = components[i] as SpriteMeshInstance;
                    EquipmentSpriteMeshRenders[j].spriteMesh = null;
                }
            }
        }
    }
    public void InitializeWeaponRender()
    {
        RightWeaponRenders = new List<SpriteRenderer[]>();
        LeftWeaponRenders = new List<SpriteRenderer[]>();
        for (int i = 0; i < RightWeaponParent.Count; i++)
        {
            RightWeaponRenders.Add(RightWeaponParent[i].GetComponentsInChildren<SpriteRenderer>(true));
        }
        for (int i = 0; i < LeftWeaponParent.Count; i++)
        {
            LeftWeaponRenders.Add(LeftWeaponParent[i].GetComponentsInChildren<SpriteRenderer>(true));
        }
    }
    public SpriteMeshInstance GetEquipmentRenderer(CharacterSpriteMeshType type)
    {
        int meshIndex = (int)type;
        return EquipmentSpriteMeshRenders[meshIndex];
    }
    public SpriteMeshInstance GetCharacterRenderer(CharacterSpriteMeshType type)
    {
        int meshIndex = (int)type;
        return CharacterSpriteMeshRenders[meshIndex];
    }
    public void OnReturnToCamp()
    {
        // Reset Mesh
        for (int i = 0; i < CharacterSpriteMeshRenders.Length; i++)
        {
            for (int j = 0; j < BodySprites.Length; j++)
            {
                if (CharacterSpriteMeshRenders[j] != null && BodySprites[i] != null)
                {
                    if (CharacterSpriteMeshRenders[j].name == BodySprites[i].name)
                    {
                        CharacterSpriteMeshRenders[i].spriteMesh = BodySprites[j];
                    }
                }
            }
        }

        for (int i = 0; i < EquipmentSpriteMeshRenders.Length; i++)
        {

            SkinnedMeshRenderer skinRenderer = EquipmentSpriteMeshRenders[i].GetComponent<SkinnedMeshRenderer>();
            if (skinRenderer != null)
            {
                skinRenderer.enabled = true;
            }
        }
        // Reset Weapon Renderer

        GetWeaponParent(true, 0).transform.parent.gameObject.SetActive(true);
        GetWeaponParent(false, 0).transform.parent.gameObject.SetActive(true);

        UIManager.instance.AllArmorHealthAmountUpdate();
        OnInventoryOpen(false);
    }
    public SpriteRenderer[] GetWeaponRenderer(bool isRightWeapon, int index)
    {
        if (isRightWeapon)
        {
            return RightWeaponRenders[index];
        }
        else
        {
            return LeftWeaponRenders[index];
        }
    }
    public GameObject GetWeaponParent(bool isRightWeapon,int index)
    {
        if (isRightWeapon)
        {
            return RightWeaponParent[index];
        }
        else
        {
            return LeftWeaponParent[index];
        }
    }
    public void SetHairVisibility(bool visible)
    {
        for (int i = 0; i < HairRenderer.Length; i++)
        {
            HairRenderer[i].enabled = visible;
        }
    }

    public void SetSortingLayers(string name)
    {
        int id = SortingLayer.NameToID(name);
        foreach (SpriteMeshInstance mesh in CharacterSpriteMeshRenders)
        {
            mesh.sortingLayerID = id;
        }
        foreach (SpriteMeshInstance mesh in EquipmentSpriteMeshRenders)
        {
            mesh.sortingLayerID = id;
        }
        for (int i = 0; i < LeftWeaponRenders.Count; i++)
        {
            foreach (SpriteRenderer render in LeftWeaponRenders[i])
            {
                render.sortingLayerID = id;
            }
        }
        for (int i = 0; i < RightWeaponRenders.Count; i++)
        {
            foreach (SpriteRenderer render in RightWeaponRenders[i])
            {
                render.sortingLayerID = id;
            }
        }
    }

    public void InnSleep()
    {
        if(PayGold(Shop.HealAllPrice))
        {
            RecoverAllBodyHealth(0, 1);
            EnemyIncubator.thisInstance.PassTime();
            UIManager.instance.UseCurtain(0.2f, 0.5f);
            UIManager.instance.AllArmorHealthAmountUpdate();
            UpGradeManager.instance.UpdateThisUi();
        }
    }

    public void Training()
    {
        if(PayGold(Shop.TrainingPrice))
        {
            UpGradeManager.instance.GainExp(1);
            UpGradeManager.instance.UpdateThisUi();
        }
    }
    public void SmithyRepairAll()
    {
        if (PayGold(Mathf.RoundToInt(Shop.RepairAllPrice * (1 - AchievementSystem.RepairAllDiscound))))
        {
            AudioManager.instance.PlayRepairSound();
            gameObject.GetComponent<EquipmentManager>().RecoverAllArmor(0, 1);
            if (RepairAllEvent != null)
            {
                RepairAllEvent();
            }
        }
        UIManager.instance.AllArmorHealthAmountUpdate();
    }
    public event System.Action RepairAllEvent;

    public void InnDinner()
    {
        if (PayGold(Shop.QuickHealPrice))
        {
            RecoverAllBodyHealth(0, 0.2f);
            UpGradeManager.instance.UpdateThisUi();
            UIManager.instance.AllArmorHealthAmountUpdate();
        }
    }

    public float m_SleepAdditional = 0;

    public event System.Action SleepEvent;
    public event System.Action AfterSleepEvent;
    public void Rest(float Amount)
    {
        if (SleepEvent != null)
        {
            SleepEvent();
        }
        RecoverAllBodyHealth(0, Amount + m_SleepAdditional);
        UpGradeManager.instance.UpdateThisUi();
        UIManager.instance.AllArmorHealthAmountUpdate();
        EnemyIncubator.thisInstance.PassTime();
        UIManager.instance.UseCurtain(0.2f, 0.5f);
        CameraManager.instance.SetCameraState(CameraPositionState.Camp);
        if (AfterSleepEvent != null)
        {
            AfterSleepEvent();
        }
    }

    public bool PayGold(int i)
    {
        if (m_Gold >= i)
        {
            AddGold(-i);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanPayGold(int i)
    {
        if (m_Gold >= i)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddGold(int amount)
    {
        m_Gold += amount;
        if (camp == 0)
        {
            UIManager.instance.UpdateGoldText();
        }     
    }
    public void ShowHealthBar(bool isVisible)
    {
        foreach (GameObject healthbar in HealthBars)
        {
            healthbar.SetActive(isVisible);
        }
    }

    Vector3 StartPos;
    private void Awake()
    {
        bodyParts = GetComponentsInChildren<BodyPart>();
        foreach (BodyPart bp in bodyParts)
        {
            bp.thisCharacter = this;
        }
        characterAnimator = CharacterMeshParent.GetComponent<Animator>();
        equipmentAnimator = EquipmentMeshParent.GetComponent<Animator>();
        InitializeWeaponRender();
        GetAllSkinRenders();
        if (camp == 0)
        {
            PlayerInstance = this;
        }
        else
        {
            EnemyInstance = this;
        }
    }

    
    private void Start()
    {

        StartPos = gameObject.transform.position;
        InGameManager.instance.Characters[camp] = this;
        InGameManager.instance.OnTurnChanged += OnTurnChanged;
        InGameManager.instance.BattleStartEvent += OnBattleStart;
        InGameManager.instance.ReturnToCampEvent += OnReturnToCamp;
        SetInitialGold();
        SetInitialHealth();          
    }
    private void OnBattleStart()
    {       
        gameObject.transform.localScale = new Vector3(1 - camp * 2, 1, 1);
        ShowHealthBar(true);
        BackToPosition();
        PlayIdle();
    }

    IEnumerator DelayPlayerIdle()
    {
        yield return new WaitForSeconds(0.5f);
        PlayIdle();
    }
    public void OnInventoryOpen(bool Open)
    {
        if (Open == true)
        {
            characterAnimator.Play("standup");
            equipmentAnimator.Play("standup");
            StartCoroutine(DelayPlayerIdle());
        }
        else
        {
            characterAnimator.Play("sitdown");
            equipmentAnimator.Play("sitdown");
        }
    }
    private void OnTurnChanged(TurnState state)
    {
        switch (state)
        {
            case TurnState.Null:
                if (camp == 0)
                {
                    gameObject.transform.localScale = new Vector3(2, 2, 2);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(-2, 2, 2);
                }
                if (camp == 0)
                {
                    characterAnimator.Play("waiting_sit");
                    equipmentAnimator.Play("waiting_sit");
                }
                else
                {
                    characterAnimator.Play("standup");
                    equipmentAnimator.Play("standup");
                }
                ShowHealthBar(false);
                transform.position = StartPos;          
                break;
        }
    }
}

//input 4[s] 5[j] 3[a] 1 2
//1 2 3 4[s] 5[j]

//for i:= 1 to 5{
//    output[input.CharacterSpriteMeshType].content = input[i].content;
//    lock[0] 
//}
//    5
//    return [1,2,3,4,5]
public enum CharacterSpriteMeshType
{
    arm_end_l,
    arm_end_r,
    arm_for_l,
    arm_for_r,
    arm_mid_l,
    arm_mid_r,
    ass_back,
    ass_front,
    ass_front_r,
    back,
    body,
    body_r,
    eyes_close,
    eyes_open,
    eyes_side,
    eyebrow,
    face_front,
    face_side,
    feet_front_l,
    feet_front_r,
    feet_side_l,
    feet_side_r,
    hair_back,
    hair_mid,
    hair_top,
    hand_back_l,
    hand_back_r,
    hand_front_l,
    hand_front_r,
    leg_down_l,
    leg_down_r,
    leg_up_l,
    leg_up_r,
    mouse,
    eyes_side2,
    eyes_side3,
    leg_huxi_l,
    leg_huxi_l_mid,
    leg_huxi_r,
    leg_huxi_r_mid,
    hujing_for,
    hujing_for_l,
    hujing_mid,
    eyebrow_mid,
    huzi_for,
    huzi_side
}
