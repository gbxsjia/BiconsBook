using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Dodge : Buff_base
{
    private Equipment sourceEquipment;

    public Buff_Dodge(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "闪避";
        BuffDescription = "闪避1次攻击牌，持续一回合。";
    }


    public void OnTurnChange(TurnState turnState)
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

    public override void AmountReduce(int i)
    {
        Amount -= i;
        if (Amount <= 0)
        {
            OwnerBuffManager.RemoveBuff(this);
            InGameManager.instance.OnTurnChanged -= OnTurnChange;
        }
    }
    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        InGameManager.instance.OnTurnChanged += OnTurnChange;
    }

}
