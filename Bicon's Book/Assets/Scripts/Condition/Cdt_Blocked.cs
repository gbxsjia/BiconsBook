using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition/Blocked")]
public class Cdt_Blocked : Condition_Base
{
    public override bool CheckCondition(CardManager userCardManager, BodyPart TargetBodyPart, BodyPart AllyBodyPart, TriggerType triggerType, Card_Base card, bool isTest = false)
    {
        return userCardManager.attackFeedBack.isDefended;
    }
}
