using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Shock : Buff_base
{
    public Buff_Shock(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "震撼";
        BuffDescription = "下回合抽牌数量减少1点";
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

                    OwnerBuffManager.RemoveBuff(this);
                    InGameManager.instance.OnTurnChanged -= OnTurnChange;
                    break;
            }
        }
    }
}
