using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_BladeMind : MonoBehaviour
{
    public static void UseEntry(CardManager useManager, int stack)
    {
        BodyPart ThisChest = InGameManager.instance.Characters[useManager.camp].GetBodyPart(BodyPartType.Chest);
        Buff_BladeMind buff = new Buff_BladeMind(BuffType.BladeMind, 1, stack);
        BuffManager ThisBuffManager = InGameManager.instance.BuffManagers[useManager.camp];
        ThisBuffManager.AddBuff(buff, ThisChest, true);
    }
}
