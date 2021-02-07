using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    // Events
    public event System.Action WeakPointsEvent;
    public event System.Action DestroyWeakPointsEvent;

    public Dictionary<BodyPart, List<Buff_base>> Buffs = new Dictionary<BodyPart, List<Buff_base>>();
    public CharacterManager characterManager;
    public CardManager cardManager;
    public List<Buff_base> buffsToRemove = new List<Buff_base>();

    public List<int> AttackMultipliers = new List<int>();
    public List<int> DefenceMultipliers = new List<int>();

    public List<GameObject> Buff_GenerateCardList = new List<GameObject>();

    public Buff_StanceBase StanceBuff;

    public void BattleEnd()
    {
        AttackMultipliers.Clear();
        DefenceMultipliers.Clear();
    }

    public int GetAttack()
    {
        int i = 0;
        foreach (int value in AttackMultipliers)
        {
            i += value;
        }
        return i;
    }
    public int GetDefence()
    {
        int i = 0;
        foreach (int value in DefenceMultipliers)
        {
            i += value;
        }        
        return Mathf.Min(i,95);
    }
    public void AddAttackMultiplier(int i)
    {
        AttackMultipliers.Add(i);
    }
    public void RemoveAttackMultiplier(int i)
    {
        AttackMultipliers.Remove(i);
    }
    public void AddDefenceMultiplier(int i)
    {
        DefenceMultipliers.Add(i);
    }
    public void RemoveDefenceMultiplier(int i)
    {
        DefenceMultipliers.Remove(i);
    }
    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        cardManager = GetComponent<CardManager>();
    }

    private void Start()
    {
        InGameManager.instance.BuffManagers[cardManager.camp] = this;
        InGameManager.instance.BattleEndEvent += ClearAllBuffs;
        InGameManager.instance.BattleEndEvent += BattleEnd;
    }
    private void LateUpdate()
    {
        foreach (Buff_base buff in buffsToRemove)
        {
            buff.OnRemove();
            Buffs[buff.AttachToBodyPart].Remove(buff);
            BuffUIUpdated(buff.AttachToBodyPart);
        }
        buffsToRemove.Clear();
    }

    public event System.Action<BodyPart> UpdateBuffEvent;
    public void BuffUIUpdated(BodyPart bp)
    {      
        if (UpdateBuffEvent != null)
        {
            UpdateBuffEvent(bp);
        }
        bp.UpdateBuffUI();
    }
    public Buff_base GetBuffOnBodypart(BodyPart bodyPart, BuffType type)
    {
        if (Buffs.ContainsKey(bodyPart))
        {
            foreach (Buff_base buff in Buffs[bodyPart])
            {
                if (buff.Type == type)
                {
                    return buff;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }
    public bool IsBuffExistOnBodyPart(BodyPart bodyPart, BuffType type)
    {
        if (Buffs.ContainsKey(bodyPart))
        {
            foreach (Buff_base buff in Buffs[bodyPart])
            {
                if (buff.Type == type)
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    public void ClearAllBuffs()
    {
        foreach (BodyPart bp in Buffs.Keys)
        {
            foreach (Buff_base buff in Buffs[bp])
            {
                RemoveBuff(buff);
            }
            Buffs[bp].Clear();
        }
    }
    public void AddStance(Buff_StanceBase stance)
    {
        if (StanceBuff != null && StanceBuff.thisStanceType != stance.thisStanceType)
        {
            return;
        }

        BodyPart part = characterManager.GetBodyPart(BodyPartType.Chest);
        if (!Buffs.ContainsKey(part))
        {
            Buffs.Add(part, new List<Buff_base>());
        }

        if (StanceBuff != null)
        {
            StanceBuff.OnStanceEnd();
            RemoveBuff(StanceBuff);
        }

        StanceBuff = stance;
        Buffs[part].Add(stance);
        stance.AddToManager(this, part);

        BuffUIUpdated(part);
    }

    public void AddBuff(Buff_base buff, BodyPart bodyPart, bool isStackable=false)
    {  
        if (bodyPart.GetIsAlive())
        {      
            if (!Buffs.ContainsKey(bodyPart))
            {
                Buffs.Add(bodyPart, new List<Buff_base>());
            }
            if (isStackable)
            {
                Buff_base existBuff = GetBuffOnBodypart(bodyPart, buff.Type);
                if (existBuff != null)
                {
                    existBuff.Remains = buff.Remains;
                    existBuff.Amount += buff.Amount;
                    existBuff.OnStack();

                }
                else
                {
                    Buffs[bodyPart].Add(buff);
                    buff.AddToManager(this, bodyPart);
                }
            }
            else
            {
                Buffs[bodyPart].Add(buff);
                buff.AddToManager(this, bodyPart);
            }
            BuffUIUpdated(bodyPart);
        }
    }

    public void AddBuff(Buff_base buff, BodyPart TargetbodyPart,BodyPart SourceBodyPart, bool isStackable = false,bool isReplaceable = true)
    {
    
        if (TargetbodyPart.GetIsAlive())
        {

            if (!Buffs.ContainsKey(TargetbodyPart))
            {
                Buffs.Add(TargetbodyPart, new List<Buff_base>());
            }
            if (isReplaceable)
            {
                Debug.Log("isReplaceable");

                Buff_base existBuff = GetBuffOnBodypart(TargetbodyPart, buff.Type);
                if (existBuff != null)
                {
                    RemoveBuff(existBuff);
                }
                Buffs[TargetbodyPart].Add(buff);
                buff.AddToManager(this, TargetbodyPart, SourceBodyPart);
            }
            else if (isStackable)
            {
                Debug.Log("isStackable");
                Buff_base existBuff = GetBuffOnBodypart(TargetbodyPart, buff.Type);
                if (existBuff != null)
                {
                    existBuff.Remains = buff.Remains;
                    existBuff.Amount += buff.Amount;
                    existBuff.OnStack();
                    Debug.Log(buff.Amount);
                }
                else
                {
                    Buffs[TargetbodyPart].Add(buff);
                    buff.AddToManager(this, TargetbodyPart, SourceBodyPart);
                }
            }
            else
            {
                Buffs[TargetbodyPart].Add(buff);
                buff.AddToManager(this, TargetbodyPart, SourceBodyPart);
            }
            BuffUIUpdated(TargetbodyPart);
        }
    }
    public void RemoveBuff(Buff_base buff)
    {
        buff.isRemoving = true;
        buffsToRemove.Add(buff);
    }
    public void RemoveBuffByType(BuffType buffType, BodyPart bodyPart)
    {
        for (int i = Buffs[bodyPart].Count - 1; i >= 0; i--)
        {
            if (Buffs[bodyPart][i].Type == buffType)
            {
                RemoveBuff(Buffs[bodyPart][i]);
            }
        }
        
    }
    public void AddBleedBuff(BodyPart bodyPart, int stack)
    {
        Buff_base buff = new Buff_Bleed(BuffType.Bleed, 3, stack);
        AddBuff(buff, bodyPart, true);                
    }
    public void RemoveBleedBuff(BodyPart bodyPart, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            for (int j = Buffs[bodyPart].Count - 1; j >= 0; j--)
            {
                if (Buffs[bodyPart][i].Type == BuffType.Bleed)
                {
                    Buffs[bodyPart][i].OnRemove();
                    Buffs[bodyPart].Remove(Buffs[bodyPart][i]);
                    break;
                }
            }
        }
    }
    public void AddWeakPointBuff(BodyPart bodyPart)
    {
        if (bodyPart != null)
        {
            if (!IsBuffExistOnBodyPart(bodyPart, BuffType.WeakPoint))
            {
                Buff_WeakPoint newBuff = new Buff_WeakPoint(BuffType.WeakPoint, 1, 1);
                AddBuff(newBuff, bodyPart);
            }
        }
        else
        {
            List<BodyPart> bodyParts = new List<BodyPart>();
            bodyParts.AddRange(characterManager.bodyParts);
            for (int i = bodyParts.Count - 1; i >= 0; i--)
            {
                if (IsBuffExistOnBodyPart(bodyParts[i], BuffType.WeakPoint))
                {
                    bodyParts.RemoveAt(i);
                }
            }
            if (bodyParts.Count > 0)
            {
                int i = Random.Range(0, bodyParts.Count);
                Buff_WeakPoint newBuff = new Buff_WeakPoint(BuffType.WeakPoint, 1, 1);
                AddBuff(newBuff, bodyParts[i]);
            }
        }
    }

    public void a_WeakPointsEvent()
    {
        if (WeakPointsEvent != null)
        {
            WeakPointsEvent();
        }
    }

    public void a_DestroyWeakPointsEvent()
    {
        if (DestroyWeakPointsEvent != null)
        {
            DestroyWeakPointsEvent();
        }
    }
}

public enum BuffGenerateCard
{
    VanishedBlock,
    SpearWall,
    ShadowKingAttack
}