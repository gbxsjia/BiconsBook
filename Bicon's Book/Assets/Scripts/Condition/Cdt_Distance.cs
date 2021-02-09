using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition/Distance")]
public class Cdt_Distance : Condition_Base
{
    public DistanceCondition type;
    public int Distance;
    public override bool CheckCondition(CardManager userCardManager, BodyPart TargetBodyPart, BodyPart AllyBodyPart, TriggerType triggerType, Card_Base card, bool isTest = false)
    {
        int dis = InGameManager.instance.GetDistance();
        switch (type)
        {
            case DistanceCondition.LessOrEqual:
                return dis <= Distance;
            case DistanceCondition.Equal:
                return dis == Distance;
            case DistanceCondition.GreaterOrEqual:
                return dis >= Distance;
            default:
                return false;
        }
    }
}

public enum DistanceCondition
{
    LessOrEqual,
    Equal,
    GreaterOrEqual
}