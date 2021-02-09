using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_SlowRecovery : Buff_base
{
    public Buff_SlowRecovery(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "缓慢恢复";
        
        BuffDescription = "每回合回复肢体"+ 3 * amount + " + "+8 * (0.5f + amount*0.5f)  +"%生命";
    }

    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);        
        InGameManager.instance.OnTurnChanged += OnTurnChange;
    }
    public void HealthHeal()
    {     
        AttachToBodyPart.HealthHeal(3*Amount+0.08f*(0.5f+0.5f*Amount)*AttachToBodyPart.HealthMax);
    }

    private void OnTurnChange(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn == OwnerBuffManager.characterManager.camp)
        {
            switch (turnState)
            {
                case TurnState.TurnEnd:
                    HealthHeal();
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