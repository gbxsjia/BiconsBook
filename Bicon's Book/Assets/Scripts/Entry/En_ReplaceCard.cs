using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ReplaceCard : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, GameObject cardPrefab)
    {
        GameObject cardObj = card.gameObject; 
        
        cardObj.SetActive(false);

        useManager.GenerateNewCard(card.ownerEquipment, cardPrefab);

    }
}
