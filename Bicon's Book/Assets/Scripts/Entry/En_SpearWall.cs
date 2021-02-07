using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_SpearWall : MonoBehaviour
{
    public static void UseEntry(CardManager cardManager, Card_Base card)
    {
        //  Debug.Log("Buff had been added to buff manager!");
        Buff_SpearWall buff_SpearWall = new Buff_SpearWall(BuffType.SpearWall, 0, 1);
        BodyPart Chest = InGameManager.instance.Characters[cardManager.camp].GetBodyPart(BodyPartType.Chest);
        if (InGameManager.instance.GetDistance() > 1)
        {
            InGameManager.instance.BuffManagers[cardManager.camp].AddBuff(buff_SpearWall, Chest, card.ownerEquipment.AttachedBodyPart, true, false);
        }
    }
}
