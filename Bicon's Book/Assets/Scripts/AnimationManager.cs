using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;
    public static string GetAnimationName(AnimType type, TargetHeightType height, AnimDirection direction)
    {
        switch (type)
        {
            default:
                return "";
            case AnimType.Punch:
                switch (height)
                {
                    default: return "";
                    case TargetHeightType.Top:
                        switch (direction)
                        {
                            default: return "punch_left_top";
                            case AnimDirection.Left:
                                return "punch_left_top";
                            case AnimDirection.Right:
                                return "punch_right_top";
                        }
                    case TargetHeightType.Middle:
                        switch (direction)
                        {
                            default: return "punch_left_mid";
                            case AnimDirection.Left:
                                return "punch_left_mid";
                            case AnimDirection.Right:
                                return "punch_right_mid";
                        }
                    case TargetHeightType.Bottom:
                        switch (direction)
                        {
                            default: return "punch_left_bot";
                            case AnimDirection.Left:
                                return "punch_left_bot";
                            case AnimDirection.Right:
                                return "punch_right_bot";
                        }
                }
            case AnimType.Damaged:
                switch (height)
                {
                    default: return "";
                    case TargetHeightType.Top:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Center:
                                return "damaged_head";
                        }
                    case TargetHeightType.Middle:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "damaged_left_arm";
                            case AnimDirection.Right:
                                return "damaged_right_arm";
                            case AnimDirection.Center:
                                return "damaged_chest";
                        }
                    case TargetHeightType.Bottom:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "damaged_left_leg";
                            case AnimDirection.Right:
                                return "damaged_right_leg";
                        }
                }
            case AnimType.Kick:
                switch (height)
                {
                    default: return "";
                    case TargetHeightType.Top:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "kick_left_top";
                            case AnimDirection.Right:
                                return "kick_right_top";
                        }
                    case TargetHeightType.Middle:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "kick_left_mid";
                            case AnimDirection.Right:
                                return "kick_right_mid";
                        }
                    case TargetHeightType.Bottom:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "kick_left_bot";
                            case AnimDirection.Right:
                                return "kick_right_bot";
                        }
                }
            case AnimType.FlyKick:
                switch (height)
                {
                    default: return "";
                    case TargetHeightType.Top:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "flykick_left_top";
                            case AnimDirection.Right:
                                return "flykick_right_top";
                        }
                    case TargetHeightType.Middle:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "flykick_left_mid";
                            case AnimDirection.Right:
                                return "flykick_right_mid";
                        }
                    case TargetHeightType.Bottom:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "flykick_left_bot";
                            case AnimDirection.Right:
                                return "flykick_right_bot";
                        }
                }
            case AnimType.Cleaver_Slash:
                switch (direction)
                {
                    case AnimDirection.Left:
                        return "cleaver_slash_left";
                    case AnimDirection.Right:
                        return "cleaver_slash_right";
                    default: return "";
                }
            case AnimType.Cleaver_TiaoPi:
                switch (direction)
                {
                    case AnimDirection.Left:
                        return "cleaver_tiaopi_left";
                    case AnimDirection.Right:
                        return "cleaver_tiaopi_right";
                    default: return "";
                }
            case AnimType.Buff_Stand:
                return "buff_stand";
            case AnimType.SpearPoke:
                switch (direction)
                {
                    default: return "";
                    case AnimDirection.Left:
                        return "ciji_left_mid";
                    case AnimDirection.Right:
                        return "ciji_right_mid";
                }
            case AnimType.Block_Dual:
                switch (height)
                {
                    default: return "";
                    case TargetHeightType.Top:
                        return "gedang_shuangchi_top";
                    case TargetHeightType.Middle:
                        return "gedang_shuangchi_mid";
                    case TargetHeightType.Bottom:
                        return "gedang_shuangchi_bot";

                }
            case AnimType.Block_BareHand:
                switch (height)
                {
                    default: return "";
                    case TargetHeightType.Top:
                        return "gedang_wuwuqi_top";
                    case TargetHeightType.Middle:
                        return "gedang_wuwuqi_mid";
                    case TargetHeightType.Bottom:
                        return "gedang_wuwuqi_bot";

                }
            case AnimType.Block_SingleHand:
                switch (height)
                {
                    default: return "";
                    case TargetHeightType.Top:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "gedang_left_top";
                            case AnimDirection.Right:
                                return "gedang_right_top";
                        }
                    case TargetHeightType.Middle:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "gedang_left_mid";
                            case AnimDirection.Right:
                                return "gedang_right_mid";
                        }
                    case TargetHeightType.Bottom:
                        switch (direction)
                        {
                            default: return "";
                            case AnimDirection.Left:
                                return "gedang_left_bot";
                            case AnimDirection.Right:
                                return "gedang_right_bot";
                        }
                }
            case AnimType.Buff_Weapon:
                return "buff_weapon";
            case AnimType.Dagger_Poke:
                switch (direction)
                {              
                    case AnimDirection.Left:
                        return "bishou_left_mid";
                    case AnimDirection.Right:
                        return "bishou_right_mid";
                    default: return "";
                }
            case AnimType.Hammer_Slash:
                switch (direction)
                {
                    default: return "";
                    case AnimDirection.Left:
                        return "lunchui_left_mid";
                    case AnimDirection.Right:
                        return "lunchui_right_mid";
                }
            case AnimType.BodyCheck:
                return "zhuangji_mid";
            case AnimType.Sword_Poke:
                switch (direction)
                {
                    case AnimDirection.Left:
                        return "sword_left_mid";
                    case AnimDirection.Right:
                        return "sword_right_mid";
                    default: return "";
                }
            case AnimType.DunJi:
                switch (direction)
                {
                    case AnimDirection.Left:
                        return "dunji_left_mid";
                    case AnimDirection.Right:
                        return "dunji_right_mid";
                    default: return "";
                }
            case AnimType.MagicCast:
                return "mofa_vfx";
            case AnimType.MoveForward:
                return "idle_dual_walkcard";
            case AnimType.MoveBackward:
                return "idle_dual_walkbackcard";
            case AnimType.HeadAttack:
                return "headattack";
            case AnimType.twoHandHammer_Slash:
                return "";
            case AnimType.XBShadowTrigger:
                return "xibeiwangdeanyingchufa";
            case AnimType.XBShadowAttack:
                return "xibeiwangdeanying";
        }

    }
    public static string GetDeathAnimationName()
    {
        return "Dead2";
    }
    public AnimationCurve MovementCurve;
    private float AnimationDuration = 1;
    private bool isAnimionPlaying = false;
    public bool isInAnimationMode = false;
    [SerializeField]
    private string[] SortingLayerNames;
    [SerializeField]
    private GameObject[] EffectPrefab;
    [SerializeField]
    private GameObject CardDisplayPrefab;
    [SerializeField]
    private GameObject AnimationCurtain;

    [SerializeField]
    Vector3[] CardShowPositions;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
     
    }
    public bool IsAnimationAllDone()
    {
        return AnimationQueue.Count == 0 && !isInAnimationMode;
    }


    private void Update()
    {
        AnimationLoop();
    }

    private List<AnimationInfo> AnimationQueue = new List<AnimationInfo>();

    public void AddAnimationToEnd(AnimationInfo info)
    {
        AnimationQueue.Add(info);
    }
    public void InsertAnimationAtPlace(AnimationInfo info, int place)
    {
        AnimationQueue.Insert(place, info);
    }

    private void AnimationLoop()
    {

        if (AnimationQueue.Count > 0 && !isAnimionPlaying)
        {
            StartCoroutine(PlayAnimation(AnimationQueue[0]));
            isAnimionPlaying = true;
        }
    }

    private void EnterAnimationMode()
    {
        isInAnimationMode = true;

        CameraManager.instance.SetFollowCharacters(false);

        AnimationCurtain.SetActive(true);
    }

    private void ExitAnimationMode()
    {
        isInAnimationMode = false;

        InGameManager.instance.Characters[0].PlayIdle();
        InGameManager.instance.Characters[1].PlayIdle();

        InGameManager.instance.Characters[0].SetSortingLayers(SortingLayerNames[2]);
        InGameManager.instance.Characters[1].SetSortingLayers(SortingLayerNames[1]);

        CameraManager.instance.SetFollowCharacters(true);

        // Camera Effect Reset
        CameraManager.instance.CameraReset();

        AnimationCurtain.SetActive(false);
    }

    [SerializeField] Animator AbilityComboDisplay;
    private IEnumerator PlayAnimation(AnimationInfo info)
    {
        AnimationQueue.RemoveAt(0);
        if(info.isSpecialPerformance)
        {
            if (isInAnimationMode)
            {
                ExitAnimationMode();
                InGameManager.instance.Characters[0].BackToPosition();
                InGameManager.instance.Characters[1].BackToPosition();
            }
            AbilityComboDisplay.Play("Trigger");
            yield return new WaitForSeconds(1.5f);
            isAnimionPlaying = false;
            isInAnimationMode = false;

            yield break;
        }
        if (info.isMovement)
        {
            if (isInAnimationMode)
            {
                ExitAnimationMode();
            }

            InGameManager.instance.Characters[info.CasterCamp].DisplayMovement(info.CasterStart, info.CasterEnd, info.OpponentPos);
            InGameManager.instance.Characters[1 - info.CasterCamp].MoveToGrid(info.OpponentPos);
            while (InGameManager.instance.Characters[info.CasterCamp].isMoving)
            {
                yield return null;
            }
            isAnimionPlaying = false;
            isInAnimationMode = false;
       
            yield break;
        }
        
        CharacterManager casterCharacter = InGameManager.instance.Characters[info.CasterCamp];
        CharacterManager opponentCharacter = InGameManager.instance.Characters[info.OpponentCamp];

        Transform casterT = InGameManager.instance.Characters[info.CasterCamp].transform;
        Transform opponentT = InGameManager.instance.Characters[info.OpponentCamp].transform;

        if (!isInAnimationMode)
        {
            EnterAnimationMode();
        }

        // PlayAnimation

        casterCharacter.PlayAnimation(info.CasterAnimType, info.CasterHeightType, info.CasterAnimDirection);
        casterCharacter.SetSortingLayers(SortingLayerNames[6]);


        // Spawn Text Effect
        if (info.TextEffectInfos != null)
        {
            foreach (TextEffectInfo TEI in info.TextEffectInfos)
            {
                TextEffectManager.instance.SpawnTextEffect(TEI);
            }
        }
        if (info.CardEffectInfos != null)
        {
            foreach (CardEffectInfo CEI in info.CardEffectInfos)
            {
                CardEffectManager.instance.SpawnCardEffect(CEI);
            }
        }      
        if (info.HasTarget)
        {
            AudioManager.instance.PlayAttackSound(true,info.CastSound,info.HitSound);
            opponentCharacter.PlayAnimation(info.OpponentAnimType, info.OpponentHeightType, info.OpponentAnimDirection);
            opponentCharacter.SetSortingLayers(SortingLayerNames[5]);

            CameraManager.instance.CameraShake(0.2f);
            // Generate Damage Effect
            GenerateDamagedEffect(info.CasterCard, info.TargetBodypart);
        }
        else
        {
            AudioManager.instance.PlayAttackSound(false, info.CastSound, info.HitSound);
            opponentCharacter.PlayIdle();

            opponentCharacter.SetSortingLayers(SortingLayerNames[2 - opponentCharacter.camp]);
            opponentT.position = InGameManager.instance.GridPositions[InGameManager.instance.CharacterPositions[opponentCharacter.camp]];
        }

        // Show The Card used

        GameObject CasterDisplayCard = Instantiate(CardDisplayPrefab, UIManager.instance.transform);
        Card_Appearance casterCardAppearance = CasterDisplayCard.GetComponent<Card_Appearance>();
        casterCardAppearance.card = info.CasterCard;

        GameObject opponentDisplayCard = null;
        if (info.OpponentCard != null)
        {          
            opponentDisplayCard = Instantiate(CardDisplayPrefab, UIManager.instance.transform);
            Card_Appearance opponentCardAppearance = opponentDisplayCard.GetComponent<Card_Appearance>();
            opponentCardAppearance.card = info.OpponentCard;
            CasterDisplayCard.transform.localPosition = CardShowPositions[info.CasterCamp];
            opponentDisplayCard.transform.localPosition = CardShowPositions[info.OpponentCamp];
        }
        else
        {
            CasterDisplayCard.transform.localPosition = CardShowPositions[2];
        }

        // Set Start Position
        Vector3 casterStartPos = casterT.position;
        Vector3 casterDestiny = casterT.position;
        Vector3 casterPos = casterT.position;
        Vector3 opponentStartPos = opponentT.position;
        Vector3 opponentDestiny = opponentT.position;
        Vector3 opponentPos = opponentT.position;
        // o o o o o o o
        // 2 1 0 6 3 4 5

        switch (info.CasterMoveType)
        {
            case AnimationMovementType.None:
                casterStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[8];
                casterDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[8];
                break;
            case AnimationMovementType.BigBackward:
                casterStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4];
                casterDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4 + 3];
                break;
            case AnimationMovementType.BigForward:
                casterStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4 + 3];
                casterDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4];
                break;
            case AnimationMovementType.MediumBackward:
                casterStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4];
                casterDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4 + 2];
                break;
            case AnimationMovementType.MediumForward:
                casterStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4 + 2];
                casterDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4];
                break;
            case AnimationMovementType.SmallBackward:
                casterStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4];
                casterDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4 + 1];
                break;
            case AnimationMovementType.SmallForward:
                casterStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4 + 1];
                casterDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp * 4];
                break;
        }

        if (info.HasTarget)
        {
            switch (info.OpponentMoveType)
            {
                case AnimationMovementType.None:
                    opponentStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[8];
                    opponentDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[8];
                    break;
                case AnimationMovementType.BigBackward:
                    opponentStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4];
                    opponentDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4 + 3];
                    break;
                case AnimationMovementType.BigForward:
                    opponentStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4 + 3];
                    opponentDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4];
                    break;
                case AnimationMovementType.MediumBackward:
                    opponentStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4];
                    opponentDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4 + 2];
                    break;
                case AnimationMovementType.MediumForward:
                    opponentStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4 + 2];
                    opponentDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4];
                    break;
                case AnimationMovementType.SmallBackward:
                    opponentStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4];
                    opponentDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4 + 1];
                    break;
                case AnimationMovementType.SmallForward:
                    opponentStartPos = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4 + 1];
                    opponentDestiny = CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[opponentCharacter.camp * 4];
                    break;
            }
        }
        // Camera Effect

        CameraManager.instance.TurnZAngle(AnimationDuration);

        // Override Effect

        // Move The Characters
        float timer = AnimationDuration;
        while (timer >= 0)
        {
            float ZValue = (CameraManager.instance.transform.position + CameraManager.instance.ChacaterAnimPositions[casterCharacter.camp]).z;
            casterPos = Vector3.Lerp(casterStartPos, casterDestiny, MovementCurve.Evaluate(1 - (timer / AnimationDuration)));
            casterPos.z = ZValue;
            casterT.position = casterPos;

            if (info.HasTarget)
            {
                opponentPos = Vector3.Lerp(opponentStartPos, opponentDestiny, MovementCurve.Evaluate(1 - (timer / AnimationDuration)));
                opponentPos.z = ZValue;
                opponentT.position = opponentPos;
            }

            timer -= Time.deltaTime;
            yield return null;
            for (int i = 0; i < 3; i++)
            {
                if (info.CasterCard.EffectSprites.Length > i && info.CasterCard.EffectSprites[i] != null)
                {
                    casterCharacter.effectRenderers[i].sprite = info.CasterCard.EffectSprites[i];

                }
            }
    
        }

        isAnimionPlaying = false;
        Destroy(CasterDisplayCard);
        if (opponentDisplayCard != null)
        {
            Destroy(opponentDisplayCard);
        }
        // Exit Playing Mode
        if (AnimationQueue.Count == 0)
        {
            ExitAnimationMode();
            InGameManager.instance.Characters[0].BackToPosition();
            InGameManager.instance.Characters[1].BackToPosition();
        }
    }
    // two hand WeaponType++ 
    public string GetIdleAnimationName(EquipmentManager equipmentManager)
    {
        if (equipmentManager)
        {
            HoldingWeaponType type = equipmentManager.GetHoldingWeaponType();
            switch (type)
            {
                case HoldingWeaponType.BareHand:
                    return "idle_hand";
                case HoldingWeaponType.Single:
                    if (equipmentManager.GetWeapon(true) != null && !equipmentManager.GetWeapon(true).isDefaultEquipment)
                    {
                        return "Idle_onehand_left";
                    }
                    else
                    {
                        return "Idle_onehand_right";
                    }
                case HoldingWeaponType.Dual:
                    return "idle_dual";
                default: return "";
            }
        }
        return "idle_hand";
    }
    public string GetMovementAnimationName(EquipmentManager equipmentManager, bool isForward)
    {
        HoldingWeaponType type = equipmentManager.GetHoldingWeaponType();

        switch (type)
        {
            case HoldingWeaponType.BareHand:
                if (isForward)
                {
                    return "idle_hand_walk";
                }
                else
                {
                    return "idle_hand_walkback";
                }
            case HoldingWeaponType.Single:
                if (equipmentManager.GetWeapon(true) != null)
                {
                    if (isForward)
                    {
                        return "Idle_onehand_left_walk";
                    }
                    else
                    {
                        return "Idle_onehand_left_walkback";
                    }
                }
                else
                {
                    if (isForward)
                    {
                        return "Idle_onehand_right_walk";
                    }
                    else
                    {
                        return "Idle_onehand_right_walkback";
                    }
                }
            case HoldingWeaponType.Dual:
                if (isForward)
                {
                    return "idle_dual_walk";
                }
                else
                {
                    return "idle_dual_walkback";
                }
            default: return "";
        }
    }

    public GameObject GetDamagedEffect(DamagedEffectType type, bool isArmor)
    {
        int i = (int)type;
        i *= 2;
        if (isArmor)
        {           
            i++;
        }   
        return EffectPrefab[i];
    }
    private void GenerateDamagedEffect(Card_Base card, BodyPart bodyPart)
    {
        if (card.damageType != DamagedEffectType.None)
        {
            if (bodyPart.GetArmor() > 0)
            {
                GameObject prefab = GetDamagedEffect(card.damageType,true);
                if (prefab != null)
                {
                    GameObject g = Instantiate(prefab, bodyPart.transform);
                    Destroy(g, 1);
                    g.transform.localPosition = Vector3.zero;
                    g.transform.rotation = Quaternion.identity;
                }
            }
            else
            {
                GameObject prefab = GetDamagedEffect(card.damageType,false);
                if (prefab != null)
                {
                    GameObject g = Instantiate(prefab, bodyPart.transform);
                    Destroy(g, 1);
                    g.transform.localPosition = Vector3.zero;
                    g.transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}
public enum AnimType
{
    Damaged,
    Punch,
    Kick,
    FlyKick,
    Cleaver_Slash,
    Cleaver_TiaoPi,
    Buff_Stand,
    SpearPoke,
    Block_SingleHand,
    Block_Dual,
    Buff_Weapon,
    Block_BareHand,
    Dagger_Poke,
    Hammer_Slash,
    BodyCheck,
    Sword_Poke,
    DunJi,
    MagicCast,
    MoveForward,
    MoveBackward,
    HeadAttack,
    twoHandHammer_Slash,
    XBShadowTrigger,
    XBShadowAttack
}
public enum TargetHeightType
{
    Top,
    Middle,
    Bottom
}
public enum AnimDirection
{
    Center,
    Left,
    Right
}
public enum DamagedEffectType
{
    None=0,
    Pierce=1,
    Slash=2,
    Dull=3,
}