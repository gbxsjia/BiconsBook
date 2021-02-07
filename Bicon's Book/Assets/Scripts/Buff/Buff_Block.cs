using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Block : Buff_base
{
    public Buff_Block(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
    }

  
}
