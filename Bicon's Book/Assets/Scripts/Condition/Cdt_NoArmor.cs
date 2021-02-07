using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Condition/NoArmor")]
public class Cdt_NoArmor : Condition_Base
{

    public override bool CheckCondition(CardManager userCardManager, BodyPart TargetBodyPart, BodyPart AllyBodyPart, TriggerType triggerType, Card_Base card, bool isTest = false)
    {
        return TargetBodyPart.ArmorCurrent <= 0;
    }
}
