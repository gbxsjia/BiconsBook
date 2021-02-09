using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_DrawCard : MonoBehaviour
{
   public static void UseEntry( CardManager cardManager, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            cardManager.DrawCard();
            if (DrawCardEntryEvent != null)
            {
                DrawCardEntryEvent();
            }
        }
        // InGameManager.instance.CardManagers[0].
    }
    public static event System.Action DrawCardEntryEvent;
}
