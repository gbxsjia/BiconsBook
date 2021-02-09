using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_DefenceChange : MonoBehaviour
{
    public static void UseEntry(CardManager userCardManager, int turn, int amount, BuffDurationType durationType)
    {
        BuffManager buffManager = InGameManager.instance.BuffManagers[userCardManager.camp];
        CharacterManager characterManager = InGameManager.instance.Characters[userCardManager.camp];
        Buff_DefenceChange buff = new Buff_DefenceChange(BuffType.DefenceChange,turn,amount, durationType);
        buffManager.AddBuff(buff, characterManager.GetBodyPart(BodyPartType.Chest));

    }
}
