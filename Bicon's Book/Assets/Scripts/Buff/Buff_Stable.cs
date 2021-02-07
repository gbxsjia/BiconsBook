using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Stable : Buff_base
{
    public Buff_Stable(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "稳固";
        BuffDescription = "不受强制位移的影响";
    }

    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        InGameManager.instance.OnTurnChanged += OnTurnChange;
    }

    private void OnTurnChange(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn == OwnerBuffManager.characterManager.camp)
        {
            switch (turnState)
            {
                case TurnState.TurnStart:

                    Remains -= 1;
                    if (Remains <= 0)
                    {
                        OwnerBuffManager.RemoveBuff(this);
                        InGameManager.instance.OnTurnChanged -= OnTurnChange;
                    }

                    break;
            }
        }
    }
}
