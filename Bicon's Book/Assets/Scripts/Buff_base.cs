using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_base
{
    public Buff_base(BuffType type,int remains,int amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
    }
    public BuffType Type;
    public int Remains;
    public int Amount;
    public BuffManager OwnerBuffManager;
    public BodyPart AttachToBodyPart;
    public BodyPart SourceFromBodyPart;

    public string BuffName;
    public string BuffDescription;

    public bool isRemoving;

    public virtual void AddToManager(BuffManager buffManager,BodyPart bodyPart)
    {
        OwnerBuffManager = buffManager;
        AttachToBodyPart = bodyPart;      
    }

    public virtual void AddToManager(BuffManager buffManager, BodyPart TargetBodyPart, BodyPart SourceBodyPart)
    {
        OwnerBuffManager = buffManager;
        AttachToBodyPart = TargetBodyPart;
        SourceFromBodyPart = SourceBodyPart;
    }

    public virtual void OnRemove()
    {

    }

    public virtual void AmountReduce(int i)
    {
        Amount -= i;
        if(Amount <= 0)
        {
            OwnerBuffManager.RemoveBuff(this);
        }
        OwnerBuffManager.BuffUIUpdated(AttachToBodyPart);
    }
    public virtual void OnStack()
    {

    }

    public virtual string GetName()
    {
        return BuffName;
    }
    public virtual string GetDescription()
    {
        return BuffDescription;
    }

}

public enum BuffDurationType
{
    OneFrame,
    OneAttack,
    Turns
}
public enum BuffType
{
    None,
    Bleed,
    SpearWall,
    Prepare,
    Dodge,
    Overload,
    Shock,
    Pressed,
    Block,
    WeakPoint,
    AttackChange,
    DefenceChange,
    Stable,
    Sacrifice,
    BrokenLeg,
    ShieldPose_KingKong,
    ShieldPose_Thorn,
    Stance,
    Thorn,
    BleedSeed,
    SlowRecovery,
    Drone,
    Fear,
    BladeMind,
    Confuse,
    ShadowKing,
    Link
}