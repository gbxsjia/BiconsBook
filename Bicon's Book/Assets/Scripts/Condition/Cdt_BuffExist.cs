using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition/BuffExist")]
public class Cdt_BuffExist : Condition_Base
{
    public BuffType Type;
    public int Stack;
    public bool ConsumeAfterCheck=false;

    public override bool CheckCondition(CardManager userCardManager, BodyPart TargetBodyPart, BodyPart AllyBodyPart, TriggerType triggerType,Card_Base card, bool isTest = false)
    {
        BuffManager buffManager = InGameManager.instance.BuffManagers[1-userCardManager.camp];
        Buff_base buff = buffManager.GetBuffOnBodypart(TargetBodyPart, Type);
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
