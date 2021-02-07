using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_StanceBase : Buff_base
{
    public Buff_StanceBase(BuffType type, int remains, int amount, StanceType stanceType) : base(type, remains, amount)
    {
        Type = BuffType.Stance;
        thisStanceType = stanceType;
        Remains = remains;
        Amount = amount;
        BuffName = "XX姿态";
        BuffDescription = "此处填描述";
    }

    public StanceType thisStanceType;
    private bool StanceEnd = true;
    public virtual void OnStanceEnd()
    {
        StanceEnd = false;
    }

    public override void OnRemove()
    {
        base.OnRemove();
        if (StanceEnd)
        {
            OnStanceEnd();
        }
    }
}
public enum StanceType
{
    none,    
    KingKong,
    Thron
}
