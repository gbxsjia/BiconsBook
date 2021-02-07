using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_BrokenLeg : Buff_base
{
    public Buff_BrokenLeg(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        if (amount == -1)
        {
            BuffName = "断腿";
        }
        else
        {
            BuffName = "绊腿";
        }
        BuffDescription = "移动的体力消耗增加1点";
    }
    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        if(Amount>0)
        {
            InGameManager.instance.OnTurnChanged += OnTurnChanged;
        }
    }

    private void OnTurnChanged(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn == OwnerBuffManager.characterManager.camp)
        {
            switch (turnState)
            {
                case TurnState.TurnEnd:

                    OwnerBuffManager.RemoveBuff(this);
                    InGameManager.instance.OnTurnChanged -= OnTurnChanged;

                    break;
            }
        }
    }
}
