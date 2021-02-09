using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Fear : Buff_base
{
    public Buff_Fear(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "畏惧";
        BuffDescription = "每回合开始后退1格。";
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
                    if (OwnerBuffManager.cardManager.camp == 0)
                    {
                        OwnerBuffManager.cardManager.ForceMove(-1);
                    }
                    else
                    {
                        OwnerBuffManager.cardManager.ForceMove(1);
                    }
                    Remains -= 1;
                    if (Remains <= 0)
                    {
                        OnRemove();
                        InGameManager.instance.OnTurnChanged -= OnTurnChange;
                        OwnerBuffManager.RemoveBuff(this);
                    }             
                    break;
            }
        }
    }
}