using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Bleed : Buff_base
{
    public Buff_Bleed(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "流血";
        BuffDescription = "每回合每层受到1点直接伤害，持续"+remains+"回合";
    }

    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        InGameManager.instance.OnTurnChanged += OnTurnChange;
    }
    public void CauseDamage()
    {
        AttachToBodyPart.TakeDirectHealthDamage(Amount, false);
    }
    private void OnTurnChange(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn == OwnerBuffManager.characterManager.camp)
        {
            switch (turnState)
            {
                case TurnState.TurnStart:
                    CauseDamage();
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
    public override void OnRemove()
    {
        base.OnRemove();
        InGameManager.instance.OnTurnChanged -= OnTurnChange;
    }
}