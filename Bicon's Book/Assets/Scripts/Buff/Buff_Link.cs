using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Link : Buff_base
{
    public Buff_Link(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "链接";
        BuffDescription = "五连鞭";
    }

    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        Card_Base.AfterDamageEvent += Damage;
    }

    private void Damage(float damage)
    {
        AttachToBodyPart.TakeDamage(damage,AttachToBodyPart);
       
        OwnerBuffManager.RemoveBuff(this); 
        Card_Base.AfterDamageEvent -= Damage;
    }
}
