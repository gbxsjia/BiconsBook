using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Drone : MonoBehaviour
{
    public static void UseEntry(CardManager userCardManager, int stack)
    {
        BuffManager buffManager = InGameManager.instance.BuffManagers[userCardManager.camp];
        CharacterManager characterManager = InGameManager.instance.Characters[userCardManager.camp];
        Buff_Drone thisbuff = new Buff_Drone(BuffType.Drone, 0, stack);
        buffManager.AddBuff(thisbuff, characterManager.GetBodyPart(BodyPartType.Chest), true);
    }


}
