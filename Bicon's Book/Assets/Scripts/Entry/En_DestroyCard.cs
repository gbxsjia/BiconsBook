using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_DestroyCard : MonoBehaviour
{
    public static List<Card_Base> UseEntry(CardManager useManager, BodyPart targetBodyPart, int amount)
    {
        List<Card_Base> cards = new List<Card_Base>();
        for (int i = 0; i < amount; i++)
        {
            Card_Base card= InGameManager.instance.CardManagers[1 - useManager.camp].ThrowBodypartCard(targetBodyPart);
            if (card != null)
            {
                cards.Add(card);
            }
        }
        return cards;
    }
}
