using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ShadowKing : MonoBehaviour
{
    public static void UseEntry(CardManager useManager)
    {
        BodyPart ThisChest = InGameManager.instance.Characters[useManager.camp].GetBodyPart(BodyPartType.Chest);
       Buff_ShadowKing buff = new Buff_ShadowKing(BuffType.ShadowKing,1,1, InGameManager.instance.Characters[useManager.camp].characterLevel);
        BuffManager ThisBuffManager = InGameManager.instance.BuffManagers[useManager.camp];
        ThisBuffManager.AddBuff(buff, ThisChest);
    }
}
