using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ReplaceCardType : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, GameObject cardPrefab, string ReplaceTag)
    {
        int GenerateAmount = 0;
       for(int i = useManager.Cards.Count-1;i>=0;i--)
        {
            if(useManager.Cards[i].CompareTag(ReplaceTag))
            {
                GenerateAmount += 1;
                useManager.Cards[i].SetActive(false);
                Destroy(useManager.Cards[i],5);
                useManager.Cards.RemoveAt(i);
            }
        }

        for (int i = 0; i < GenerateAmount; i++)
        {
            useManager.GenerateNewCard(card.ownerEquipment, cardPrefab);
        }
    }
}
