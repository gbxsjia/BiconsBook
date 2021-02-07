using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Condition/BuffExistSelf")]
public class Cdt_BuffExistSelf : Condition_Base
{
    public BuffType Type;
    public bool isSelfBodyPart;
    public BodyPartType partType;
    public int Stack;
    public bool ConsumeAfterCheck = false;

    public override bool CheckCondition(CardManager userCardManager, BodyPart TargetBodyPart, BodyPart AllyBodyPart, TriggerType triggerType, Card_Base card, bool isTest = false)
    {
        BuffManager buffManager = InGameManager.instance.BuffManagers[userCardManager.camp];
        CharacterManager characterManager = InGameManager.instance.Characters[userCardManager.camp];
        BodyPart targetBodypart = AllyBodyPart;
        if (!isSelfBodyPart)
        {
            targetBodypart = characterManager.GetBodyPart(partType);
        }

        Buff_base buff = buffManager.GetBuffOnBodypart(targetBodypart, Type);
        bool result = false;
        if (buff != null)
        {
            result = buff.Amount >= Stack;

            if (ConsumeAfterCheck && !isTest)
            {
                buff.Amount -= Stack;
                if (buff.Amount <= 0)
                {
                    buffManager.RemoveBuff(buff);
                }
                buffManager.BuffUIUpdated(TargetBodyPart);
            }
            return result;
        }
        else
        {
            return result;
        }
    }
}
