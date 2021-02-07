using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Buff_Thorn : Buff_base
{
    public Buff_Thorn(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "荆棘";
        BuffDescription = "受到攻击时，每层对对方造成一点伤害";
    }

    GameObject effectInstance;
    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        bodyPart.OnDamageEvent += BodyPart_OnDamageEvent;
    }

    public override void OnRemove()
    {
        base.OnRemove();
        AttachToBodyPart.OnDamageEvent -= BodyPart_OnDamageEvent;
    }
    private void BodyPart_OnDamageEvent(BodyPart Attacker, BodyPart BeAttackedBody)
    {
        if(Attacker.thisCharacter.camp == OwnerBuffManager.cardManager.camp)
        {
            return;
        }

        if (Attacker.GetIsAlive())
        {
            Attacker.TakeDamage(Amount, AttachToBodyPart);
            Amount -= 2;
            if (Amount <= 0)
            {
                OwnerBuffManager.RemoveBuff(this);
                AttachToBodyPart.OnDamageEvent -= BodyPart_OnDamageEvent;
            }
        }

    }
}
