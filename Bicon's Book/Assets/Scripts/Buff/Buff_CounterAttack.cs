using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_CounterAttack : Buff_base
{
    private Equipment sourceEquipment;

    public Buff_CounterAttack(BuffType type, int remains, int amount,Equipment fromEquipment) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        sourceEquipment = fromEquipment;
    }

}
