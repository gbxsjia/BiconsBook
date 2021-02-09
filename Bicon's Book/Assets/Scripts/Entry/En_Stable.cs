using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Stable : MonoBehaviour
{
    public static void UseEntry(CardManager userCardManager, int turn)
    {
        BuffManager buffManager = InGameManager.instance.BuffManagers[userCardManager.camp];
        CharacterManager characterManager = InGameManager.instance.Characters[userCardManager.camp];
        Buff_Stable buff = new Buff_Stable(BuffType.Stable, turn, 1);
        buffManager.AddBuff(buff, characterManager.GetBodyPart(BodyPartType.Chest));
    }
}
