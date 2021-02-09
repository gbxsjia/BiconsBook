using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ThrowSelfCard : MonoBehaviour
{
    public static List<Card_Base> UseEntry(CardManager useManager, int amount)
    {
        List<Card_Base> cards = new List<Card_Base>();
        for (int i = 0; i < amount; i++)
        {
            Card_Base card= InGameManager.instance.CardManagers[useManager.camp].DestroyRandomCard();
            if (card != null)
            {
                cards.Add(card);
            }
        }
        return cards;
    }
}
