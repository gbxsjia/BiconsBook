using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Sacrifice : Buff_base
{
    public Buff_Sacrifice(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "献祭";
        BuffDescription = "本回合已献祭"+amount+"次";
    }

    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        InGameManager.instance.OnTurnChanged += OnTurnChange;
    }

    private void OnTurnChange(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn ==1- OwnerBuffManager.characterManager.camp)
        {
            switch (turnState)
            {
                case TurnState.TurnEnd:
                    OwnerBuffManager.RemoveBuff(this);
                    InGameManager.instance.OnTurnChanged -= OnTurnChange;
                    break;
            }
        }
    }
}
