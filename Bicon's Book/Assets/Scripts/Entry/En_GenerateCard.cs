using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_GenerateCard : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, int amount, GameObject cardPrefab)
    {


        for (int i = 0; i < amount; i++)
        {
            useManager.GenerateNewCard(card.ownerEquipment, cardPrefab);
        }
    }
}
