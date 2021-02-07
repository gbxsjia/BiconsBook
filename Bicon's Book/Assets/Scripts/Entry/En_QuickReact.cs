using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_QuickReact : MonoBehaviour
{
   public static void UseEntry(CardManager cardManager,BodyPartType bodyPartType, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (bodyPartType == BodyPartType.LeftArm)
            {
                cardManager.DrawCard(bodyPartType, EquipmentType.LeftWeapon);
            }
            if (bodyPartType == BodyPartType.RightArm)
            {
                cardManager.DrawCard(bodyPartType, EquipmentType.RightWeapon);
            }

        }
    }

}
