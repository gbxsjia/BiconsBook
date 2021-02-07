using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Condition/Combo")]
public class Cdt_ComboCount : Condition_Base
{
    public int minCount;
    public override bool CheckCondition(CardManager userCardManager, BodyPart TargetBodyPart, BodyPart AllyBodyPart, TriggerType triggerType, Card_Base card, bool isTest = false)
    {
        if(card == null)
        {
            return userCardManager.AttackCardUsedAmount >= minCount;
        }
        if (card.cardType == CardType.Attack)
        {
            return userCardManager.AttackCardUsedAmount >= minCount;
        }
        else
        {
            return false;
        }
    }
}
