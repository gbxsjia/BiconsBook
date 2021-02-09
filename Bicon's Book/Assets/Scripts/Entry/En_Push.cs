using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Push : MonoBehaviour
{
    public static void UseEntry(CardManager cardManager,int distance)
    {
        BuffManager buffManager = InGameManager.instance.BuffManagers[1 - cardManager.camp];
        CharacterManager characterManager = InGameManager.instance.Characters[1 - cardManager.camp];
        if (buffManager.IsBuffExistOnBodyPart(characterManager.GetBodyPart(BodyPartType.Chest), BuffType.Stable))
        {
            return;
        }

        if (cardManager.camp == 1)
        {
            InGameManager.instance.CharacterMoveAndUgradeGrids(0, -distance);
        }
        else if(cardManager.camp == 0)
        {
            InGameManager.instance.CharacterMoveAndUgradeGrids(1, distance);
        }
    }        

}
