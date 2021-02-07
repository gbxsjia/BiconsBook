using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_BladeMind : Buff_base
{
    public Buff_BladeMind(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "青石元气";

        BuffDescription = "每层提升5%伤害和5%伤害减免，最高10层";

    }
    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        OnStack();
    }

    public override void OnStack()
    {
        if (Amount < 10)
        {

            OwnerBuffManager.AddDefenceMultiplier(5);
            OwnerBuffManager.AddAttackMultiplier(5);

            base.OnStack();

        }
    }

    public override void AmountReduce(int i)
    {
        for (int x = 0; x < i; x++)
        {
            OwnerBuffManager.RemoveDefenceMultiplier(5);
            OwnerBuffManager.RemoveAttackMultiplier(5);
        }
        base.AmountReduce(i);

    }
}
