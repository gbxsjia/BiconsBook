using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_AttackChange : Buff_base
{
    BuffDurationType durationType;
    public Buff_AttackChange(BuffType type, int remains, int amount, BuffDurationType buffDurationType) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        durationType = buffDurationType;
        BuffName = "伤害";
        if (amount > 0)
        {
            BuffDescription = "伤害增加百分之" + amount;
        }
        else
        {
            BuffDescription = "伤害降低百分之" + amount;
        }
  
    }
    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        OwnerBuffManager.AddAttackMultiplier(Amount);

        switch (durationType)
        {
            case BuffDurationType.OneAttack:
                buffManager.GetComponent<CardManager>().AfterBeingAttackedEvent += OnUseCard; 
                break;
            case BuffDurationType.OneFrame:
                OwnerBuffManager.RemoveBuff(this);
                break;
            case BuffDurationType.Turns:
                InGameManager.instance.OnTurnChanged += OnTurnChanged;
                break;
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

    private void OnUseCard(Card_Base card, BodyPart bodyPart)
    {
        if (card.cardType == CardType.Attack)
        {
            Remains--;
            if (Remains <= 0)
            {
                OwnerBuffManager.GetComponent<CardManager>().AfterBeingAttackedEvent -= OnUseCard;
                OwnerBuffManager.RemoveBuff(this);
            }
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
        OwnerBuffManager.RemoveAttackMultiplier(Amount);
        switch (durationType)
        {
            case BuffDurationType.OneAttack:
                OwnerBuffManager.GetComponent<CardManager>().AfterBeingAttackedEvent -= OnUseCard;
                break;
            case BuffDurationType.OneFrame:
                break;
            case BuffDurationType.Turns:
                InGameManager.instance.OnTurnChanged -= OnTurnChanged;
                break;
        }
    }

}
