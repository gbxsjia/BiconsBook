using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_Base : ScriptableObject
{
    public virtual bool CheckCondition(CardManager userCardManager, BodyPart TargetBodyPart, BodyPart AllyBodyPart, TriggerType triggerType,Card_Base card, bool isTest=false)
    {
        return true;
    }

    public string AppearenceText;
    public virtual string GetAppearenceText()
    {
        return AppearenceText;
    }
}
