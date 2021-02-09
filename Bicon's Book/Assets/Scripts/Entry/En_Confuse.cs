using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Confuse : MonoBehaviour
{
    public static void UseEntry(CardManager useManager)
    {
        BodyPart ThisChest = InGameManager.instance.Characters[1 - useManager.camp].GetBodyPart(BodyPartType.Chest);
        Buff_Confuse buff = new Buff_Confuse(BuffType.Confuse, 1, 1);
        BuffManager ThisBuffManager = InGameManager.instance.BuffManagers[1-useManager.camp];
        ThisBuffManager.AddBuff(buff, ThisChest, true);

    }
}
