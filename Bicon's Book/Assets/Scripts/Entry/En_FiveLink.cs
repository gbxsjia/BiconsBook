using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_FiveLink : MonoBehaviour
{
    public static void UseEntry(CardManager useManager)
    {
       
        BuffManager ThisBuffManager = InGameManager.instance.BuffManagers[1 - useManager.camp];
        BodyPart[] bodyParts = InGameManager.instance.Characters[1 - useManager.camp].bodyParts;
        for (int i = 0; i < bodyParts.Length; i++)
        {
            Buff_Link buff = new Buff_Link(BuffType.Link, 1, 1);
            ThisBuffManager.AddBuff(buff, bodyParts[i]);
        }
       
    }
}
