using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Confuse : Buff_base
{
    public Buff_Confuse(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "混乱";

        BuffDescription = "所有牌的目标将变为随机";

    }
    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        InGameManager.instance.OnTurnChanged += OnTurnChanged;
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
