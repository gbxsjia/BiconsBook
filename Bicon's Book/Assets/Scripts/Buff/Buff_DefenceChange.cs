using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_DefenceChange : Buff_base
{
    BuffDurationType durationType;
    public Buff_DefenceChange(BuffType type, int remains, int amount, BuffDurationType buffDurationType) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        durationType = buffDurationType;
        BuffName = "防御";
        if (amount > 0)
        {
            BuffDescription = "受到伤害减少百分之" + amount;
        }
        else
        {
            BuffDescription = "受到伤害增加百分之" + amount;
        }
    }
    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        OwnerBuffManager.AddDefenceMultiplier(Amount);

        switch (durationType)
        {
            case BuffDurationType.OneAttack:
                buffManager.GetComponent<CardManager>().ReceiveAttackEvent += OnReceiveAttack;
                break;
            case BuffDurationType.OneFrame:
                OwnerBuffManager.RemoveBuff(this);
                break;
            case BuffDurationType.Turns:
                InGameManager.instance.OnTurnChanged += OnTurnChanged;
                break;
        }
    }

    private void OnReceiveAttack(Card_Base card, CardManager arg2, AttackFeedBack arg3)
    {
        if (card.cardType == CardType.Attack)
        {
            Remains--;
            if (Remains <= 0)
            {
                OwnerBuffManager.GetComponent<CardManager>().ReceiveAttackEvent -= OnReceiveAttack;
                OwnerBuffManager.RemoveBuff(this);
            }
        }
    }

    private void OnTurnChanged(TurnState turnState)
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
                        InGameManager.instance.OnTurnChanged -= OnTurnChanged;
                    }
                    break;
            }
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
        OwnerBuffManager.RemoveDefenceMultiplier(Amount);
        switch (durationType)
        {
            case BuffDurationType.OneAttack:
                OwnerBuffManager.GetComponent<CardManager>().ReceiveAttackEvent -= OnReceiveAttack;
                break;
            case BuffDurationType.OneFrame:
                break;
            case BuffDurationType.Turns:
                InGameManager.instance.OnTurnChanged -= OnTurnChanged;
                break;
        }
    }

}
