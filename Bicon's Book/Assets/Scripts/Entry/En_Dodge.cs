using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Dodge : MonoBehaviour
{
    public static void UseEntry(CardManager useManager,BuffType buffType)
    {        
        BodyPart ThisChest = InGameManager.instance.Characters[useManager.camp].GetBodyPart(BodyPartType.Chest);

        Buff_base aBuff = InGameManager.instance.BuffManagers[useManager.camp].GetBuffOnBodypart(ThisChest, buffType);
        int aStackB = 0;
        if(aBuff != null)
        {
            aStackB = 1;
            aBuff.AmountReduce(1);
        }


        Buff_Dodge buff_Dodge = new Buff_Dodge(BuffType.Dodge, 1,1+aStackB);
        ThisChest.thisCharacter.GetComponent<BuffManager>().AddBuff(buff_Dodge,ThisChest,true );
    }
}
