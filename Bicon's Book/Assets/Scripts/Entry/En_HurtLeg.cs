using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_HurtLeg : MonoBehaviour
{
    public static void UseEntry(CardManager useManager, int stack,BuffType buffType)
    {        
        BodyPart ThisChest = InGameManager.instance.Characters[useManager.camp].GetBodyPart(BodyPartType.Chest);
        BodyPart TargetChest = InGameManager.instance.Characters[1-useManager.camp].GetBodyPart(BodyPartType.Chest);
        Buff_base aBuff = InGameManager.instance.BuffManagers[useManager.camp].GetBuffOnBodypart(ThisChest, buffType);
        int finalAddStack = stack;
        if(aBuff != null)
        {
            finalAddStack += 1;
        }
        if (finalAddStack > 0)
        {
            Buff_BrokenLeg buff = new Buff_BrokenLeg(BuffType.BrokenLeg, 1, finalAddStack);
            BuffManager ThisBuffManager = InGameManager.instance.BuffManagers[1-useManager.camp];
            ThisBuffManager.AddBuff(buff, TargetChest, true);
        }
    }
}
