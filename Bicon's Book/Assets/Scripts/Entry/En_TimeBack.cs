using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_TimeBack : MonoBehaviour
{
    public static void UseEntry(CardManager useManager)
    {
        for (int i = useManager.UsedCardRecordList.Count - 1; i >= 0; i--)
        {
            if (useManager.UsedCardRecordList[i] == null)
            { useManager.UsedCardRecordList.RemoveAt(i); }
        }

        for (int i = useManager.UsedCardRecordList.Count - 1; i >= 0; i--)
        { 
        
            GameObject cardObj = useManager.UsedCardRecordList[i];  
            Card_Base aCard = cardObj.GetComponent<Card_Base>();
            if (aCard.CoreAbilityCard || aCard.StaminaCost < 0)
            {
                continue;
            }

            if(aCard.StaminaCost > 0)
            {
                aCard.StaminaCost -= 1;
            }

            print(0);

            aCard.isActive = true;
            aCard.CardEffect(useManager, useManager.TargetBodyPart, useManager.AllyBodyPart, TriggerType.OnDraw);

            useManager.Cards.Add(cardObj);
            cardObj.SetActive(true);
            UIManager.instance.AddCard(cardObj, useManager.camp);
            useManager.UsedCardsDeck.Remove(cardObj);

            cardObj.GetComponent<Card_Appearance>().UpdateUI();
            break;
        }
    }
}
