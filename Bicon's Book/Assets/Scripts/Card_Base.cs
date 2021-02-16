using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card_Base : MonoBehaviour
{
    // 卡牌说明 32个字符
    public string CardDescription;

    public bool CoreAbilityCard = false;

    public Equipment ownerEquipment;
    public RareLevel rareLevel;
    public AnimType animType;

    public AudioClip CastSound;
    public AudioClip HitSound;

    public DamagedEffectType damageType;

    public string Name;

    public int StaminaCost;
    public int StaminaInitialCost;
    public int MinDistance;
    public int MaxDistance;
    public AnimationMovementType casterMoveType;
    public AnimationMovementType opponentMoveType;

    public bool isActive = false;

    public List<EntryInfo> Entries = new List<EntryInfo>();

    public Sprite[] EffectSprites;

    public CardAnimInfo[] AnimData;

    private void Awake()
    {
        StaminaInitialCost = StaminaCost;
    }
    public void MouseDown()
    {
        UIManager.instance.MouseDownOnCard(gameObject);
    }

    public CardType cardType = CardType.Attack;

    public bool ContainsTriggerType(TriggerType triggerType)
    {
        foreach (EntryInfo info in Entries)
        {
            if (info.triggerType == triggerType)
            {
                return true;
            }
        }
        return false;
    }
    public bool ContainsEntry(EntryType type)
    {
        foreach (EntryInfo info in Entries)
        {
            if (info.type == type)
            {
                return true;
            }
        }
        return false;
    }

    public static event System.Action<float> AfterDamageEvent;
    public virtual void CardEffect(CardManager userCardManager, BodyPart TargetBodyPart, BodyPart AllyBodyPart, TriggerType triggerType)
    {
        bool TargetInRange = IsTargetInRange();
        BuffManager EnemyBuffManager = InGameManager.instance.BuffManagers[1 - userCardManager.camp];
        
        BodyPart FinalTargetBodyPart = TargetBodyPart;

        //---------------------检测闪避-------------------------
        if (cardType == CardType.Attack)
        {
            BodyPart EnemyBody = TargetBodyPart.thisCharacter.GetBodyPart(BodyPartType.Chest);
            EnemyBuffManager = InGameManager.instance.BuffManagers[TargetBodyPart.thisCharacter.camp];
            if (EnemyBuffManager.IsBuffExistOnBodyPart(EnemyBody, BuffType.Dodge))
            {
                FinalTargetBodyPart = new BodyPart();
                FinalTargetBodyPart.SetBodyPartType(BodyPartType.None);
                FinalTargetBodyPart.thisCharacter = InGameManager.instance.Characters[EnemyBody.thisCharacter.camp];
                EnemyBuffManager.GetBuffOnBodypart(EnemyBody, BuffType.Dodge).AmountReduce(1);
            }
        }
        //------------------------------------------------------


        foreach (EntryInfo info in Entries)
        {
            List<BodyPart> TargetBodyParts= new List<BodyPart>();
            switch (info.TargetMode)
            {
                case EntryInfo.EntryTargetMode.Single:
                    TargetBodyParts.Add(FinalTargetBodyPart);
                    break;
                case EntryInfo.EntryTargetMode.Hori:
                    TargetBodyParts = FinalTargetBodyPart.SameHoriBodyParts(FinalTargetBodyPart);
                    break;
                default:
                    TargetBodyParts = FinalTargetBodyPart.SameHoriBodyParts(FinalTargetBodyPart);
                    break;
            }
            TextEffectInfo effectInfo = null;
            CardEffectInfo cardEffectInfo = null;

            for (int i = 0; i < TargetBodyParts.Count; i++)
            {
                FinalTargetBodyPart = TargetBodyParts[i];

                if (triggerType == info.triggerType)
                {
                    if (info.condition != null && !info.condition.CheckCondition(userCardManager, FinalTargetBodyPart, AllyBodyPart, triggerType,this, false))
                    {
                        continue;
                    }

                    switch (info.type)
                    {
                        case EntryType.Bleed:
                            if (TargetInRange)
                            {
                                En_Bleed.UseEntry(this, userCardManager, FinalTargetBodyPart, info.i1, info.i2);
                            }
                            break;
                        case EntryType.Damage:
                            if (TargetInRange)
                            {
                                float damage = En_Damage.UseEntry(this, userCardManager, FinalTargetBodyPart, info.i1,
                                    info.i2);
                                effectInfo = new TextEffectInfo(info.type, FinalTargetBodyPart, damage);
                                if (AfterDamageEvent != null)
                                {
                                    AfterDamageEvent(damage);
                                }
                            }
                            break;
                        case EntryType.ArmorHeal:
                            En_ArmorHeal.UseEntry(AllyBodyPart, ownerEquipment, info.i1, info.i2);
                            break;
                        case EntryType.HealthHeal:
                            En_HealthHeal.UseEntry(AllyBodyPart, info.i1, info.i2);
                            break;
                        case EntryType.Overload:
                            En_Overload.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.Push:
                            if (TargetInRange)
                            {
                                En_Push.UseEntry(userCardManager, info.i1);
                            }
                            break;
                        case EntryType.RecoverLoad:
                            En_RecoverLoad.UseEntry(userCardManager, FinalTargetBodyPart, info.i1);
                            break;
                        case EntryType.StaminaRecover:
                            En_StaminaRecover.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.Stanch:
                            //    En_Stanch.UseEntry(AllyBodyPart, userCardManager,info.a);
                            break;
                        case EntryType.Butcher:

                            break;
                        case EntryType.Movement:
                            En_Movement.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.ArmorAttack:
                            if (TargetInRange)
                            {
                                float damage = En_ArmorAttack.UseEntry(this, userCardManager, FinalTargetBodyPart,
                                    info.i1, info.i2);
                                effectInfo = new TextEffectInfo(info.type, FinalTargetBodyPart, damage);
                                if (AfterDamageEvent != null)
                                {
                                    AfterDamageEvent(damage);
                                }
                            }
                            break;
                        case EntryType.QuickReact:
                            En_QuickReact.UseEntry(userCardManager, ownerEquipment.AttachedBodyPart.GetBodyPartType(),
                                info.i1);
                            break;
                        case EntryType.DrawCard:
                            En_DrawCard.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.Block:
                            En_Block.UseEntry(this, userCardManager, ownerEquipment.AttachedBodyPart, info.i1, info.i2);
                            break;
                        case EntryType.WeakPoint_Target:
                            if (TargetInRange)
                            {
                                En_WeakPoint.UseEntry(FinalTargetBodyPart, userCardManager.camp, info.i1);
                            }
                            break;
                        case EntryType.WeakPoint_Random:
                            if (TargetInRange)
                            {
                                En_WeakPoint.UseEntry(null, userCardManager.camp, info.i1);
                            }
                            break;
                        case EntryType.HealthHealAll:
                            En_SelfHealAll.UseEntry(InGameManager.instance.Characters[userCardManager.camp], info.i1,
                                info.i2);
                            break;
                        case EntryType.GenerateCard:
                            En_GenerateCard.UseEntry(this, userCardManager, info.i1, info.g1);
                            break;
                        case EntryType.Shock:
                            if (TargetInRange)
                            {
                                En_Shock.UseEntry(this, userCardManager, info.i1);
                            }
                            break;
                        case EntryType.DestroyCardOnPart:
                            if (TargetInRange)
                            {
                                List<Card_Base> cards = En_DestroyCard.UseEntry(userCardManager, FinalTargetBodyPart, info.i1);
                                if (cards != null && cards.Count > 0)
                                {
                                    foreach (Card_Base card in cards)
                                    {
                                        cardEffectInfo = new CardEffectInfo(card, CardEffectType.Destroy,
                                            1 - userCardManager.camp);
                                    }
                                }
                            }
                            break;
                        case EntryType.Charge:
                            En_Charge.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.Stable:
                            En_Stable.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.Defence_OneFrame:
                            En_DefenceChange.UseEntry(userCardManager, info.i1, info.i2, BuffDurationType.OneFrame);
                            break;
                        case EntryType.Defence_Attacks:
                            En_DefenceChange.UseEntry(userCardManager, info.i1, info.i2, BuffDurationType.OneAttack);
                            break;
                        case EntryType.Defence_Turns:
                            En_DefenceChange.UseEntry(userCardManager, info.i1, info.i2, BuffDurationType.Turns);
                            break;
                        case EntryType.Attack_Attacks:
                            En_AttackChange.UseEntry(userCardManager, info.i1, info.i2, BuffDurationType.OneAttack);
                            break;
                        case EntryType.Attack_OneFrame:
                            En_AttackChange.UseEntry(userCardManager, info.i1, info.i2, BuffDurationType.OneFrame);
                            break;
                        case EntryType.Attack_Turns:
                            En_AttackChange.UseEntry(userCardManager, info.i1, info.i2, BuffDurationType.Turns);
                            break;
                        case EntryType.Sacrifice:
                            En_Sacrifice.UseEntry(userCardManager, ownerEquipment.AttachedBodyPart, info.i1);
                            break;
                        case EntryType.BleedRefresh:
                            if (TargetInRange)
                            {
                                En_BleedRefresh.UseEntry(userCardManager);
                            }
                            break;
                        case EntryType.ComboDamage:
                            if (TargetInRange)
                            {
                                En_ComboDamage.UseEntry(this, userCardManager, FinalTargetBodyPart, info.i1, info.i2);
                            }
                            break;

                        case EntryType.ShieldPose_Kingkong:
                            En_ShieldPose_KingKong.UseEntry(this, userCardManager);
                            break;
                        case EntryType.ShieldPose_Thorn:
                            En_ShieldPose_Thorn.UseEntry(this, userCardManager);
                            break;
                        case EntryType.Thorn:
                            En_Thorn.UseEntry(this, userCardManager, AllyBodyPart, info.i1);
                            break;
                        case EntryType.HealthHealLowest:
                            En_HPHealLowest.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.BleedSeed:
                            En_BleedSeed.UseEntry(FinalTargetBodyPart, info.i1);
                            break;
                        case EntryType.AllTypeCardInHandReplace:
                            En_ReplaceCardType.UseEntry(this, userCardManager, info.g1, info.s1);
                            break;
                        case EntryType.ReplaceCard:
                            En_ReplaceCard.UseEntry(this, userCardManager, info.g1);
                            break;
                        case EntryType.ConsumeItem:
                            En_ConsumeItem.UseEntry(this, userCardManager);
                            break;
                        case EntryType.SlowRecovery:
                            En_SlowRecovery.UseEntry(info.i1, AllyBodyPart);
                            break;
                        case EntryType.Drone:
                            En_Drone.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.DroneBomber:
                            En_DroneBomber.UseEntry(userCardManager, FinalTargetBodyPart);
                            break;
                        case EntryType.SpearWall:
                            En_SpearWall.UseEntry(userCardManager, this);
                            break;
                        case EntryType.Dodge:
                            En_Dodge.UseEntry(userCardManager, info.TBT1);
                            break;
                        case EntryType.Fear:
                            En_Fear.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.BladeMind:
                            En_BladeMind.UseEntry(userCardManager, info.i1);
                            break;
                        case EntryType.ThrowRandomCard:
                            if (TargetInRange)
                            {
                                List<Card_Base> cards = En_ThrowCard.UseEntry(userCardManager, info.i1);
                                if (cards != null && cards.Count > 0)
                                {
                                    foreach (Card_Base card in cards)
                                    {
                                        cardEffectInfo = new CardEffectInfo(card, CardEffectType.Destroy,
                                            1 - userCardManager.camp);
                                    }
                                }
                            }
                            break;

                        case EntryType.Confuse:
                            En_Confuse.UseEntry(userCardManager);
                            break;

                        case EntryType.LevelDamage:
                            if (TargetInRange)
                            {
                                int damage = En_LevelDamage.UseEntry(this, userCardManager, FinalTargetBodyPart,
                                    info.i1);
                                effectInfo = new TextEffectInfo(info.type, FinalTargetBodyPart, damage);
                                if (AfterDamageEvent != null)
                                {
                                    AfterDamageEvent(damage);
                                }
                            }
                            break;

                        case EntryType.ShadowKing:
                            En_ShadowKing.UseEntry(userCardManager);
                            break;

                        case EntryType.TimeBack:
                            En_TimeBack.UseEntry(userCardManager);
                            break;

                        case EntryType.SixLink:
                            En_FiveLink.UseEntry(userCardManager);
                            break;

                        case EntryType.ThrowSelfCard:
                            
                            List<Card_Base> aCards = En_ThrowSelfCard.UseEntry(userCardManager, info.i1);
                            if (aCards != null && aCards.Count > 0)
                            {
                                foreach (Card_Base card in aCards)
                                {
                                    cardEffectInfo = new CardEffectInfo(card, CardEffectType.Destroy,
                                        userCardManager.camp);
                                }
                            }
                            break;


                        default:
                            break;
                    }



                }

                if (effectInfo != null)
                {
                    TextEffectManager.instance.AddTextEffectInfos(effectInfo);
                }
                if (cardEffectInfo != null)
                {
                    CardEffectManager.instance.AddCardEffect(cardEffectInfo);
                }



            }


        }


    }

    public virtual bool HasEnoughStamina(CardManager userCardManager)
    {
        if (userCardManager.StaminaCurrent < StaminaCost)
        {
            return false;
        }

        return true;
    }
    public virtual bool CanUseCard()
    {
        return !(ContainsTriggerType(TriggerType.AfterBeingAttacked) || ContainsTriggerType(TriggerType.ReceivingAttack) || ContainsTriggerType(TriggerType.NeverTrigger) || ContainsEntry(EntryType.Combo));
    }

    public virtual bool HasEffect(CardManager userManager,TriggerType triggerType, Card_Base TriggerCard = null)
    {
        bool hasEffect = false;
        foreach (EntryInfo info in Entries)
        {
            if(info.triggerType == TriggerType.NeverTrigger)
            {
                continue;
            }
            if (info.condition == null || info.condition.CheckCondition(userManager, userManager.TargetBodyPart, userManager.AllyBodyPart, triggerType,TriggerCard,true))
            {
                hasEffect = true;
                break;
            }
        }
        return hasEffect;
    }
    public bool IsTargetInRange()
    {
        if (MaxDistance <= 0)
        {
            return true;
        }
        float dis = InGameManager.instance.GetDistance();
        return dis >= MinDistance && dis <= MaxDistance;
    }
}

public enum CardType
{
    Attack,
    Defend,
    Buff
}
public enum TriggerType
{
    Use,
    ReceivingAttack,
    AfterBeingAttacked,
    OnDraw,
    OnAbandon,
    AfterUseCard,
    SelfTriggering,
    NeverTrigger
}
public enum EntryType
{
    Damage,
    Bleed,
    ArmorHeal,
    HealthHeal,
    Overload,
    Push,
    RecoverLoad,
    StaminaRecover,
    Stanch,
    Butcher,
    Movement,
    ArmorAttack,
    QuickReact,
    Block,
    DrawCard,
    Combo,
    WeakPoint_Target,
    WeakPoint_Random,
    HealthHealAll,
    Counsume,
    GenerateCard,
    DestroyCardOnPart,
    Shock,
    Charge,
    Stable,
    Defence_OneFrame,
    Defence_Attacks,
    Defence_Turns,
    Attack_OneFrame,
    Attack_Attacks,
    Attack_Turns,
    Sacrifice,
    BleedRefresh,
    ComboDamage,
    ShieldPose_Kingkong,
    ShieldPose_Thorn,
    Thorn,
    HealthHealLowest,
    BleedSeed,
    AllTypeCardInHandReplace,
    ReplaceCard,
    ConsumeItem,
    SlowRecovery,
    Drone,
    DroneBomber,
    Passive,
    SpearWall,
    Dodge,
    Fear,
    BladeMind,
    ThrowRandomCard,
    Confuse,
    LevelDamage,
    ShadowKing,
    TimeBack,
    SixLink,
    ThrowSelfCard,
}
