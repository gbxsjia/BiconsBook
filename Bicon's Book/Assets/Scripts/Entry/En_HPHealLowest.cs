using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_HPHealLowest : MonoBehaviour
{
    public static void UseEntry(CardManager cardManager,int b)
    {
        BodyPart ThisBodyPart = null;
        int LowestHealth = 99999;
        foreach(BodyPart a in InGameManager.instance.Characters[cardManager.camp].bodyParts )
        {
            if(a.HealthCurrent > 0 && a.HealthCurrent != a.HealthMax &&a.HealthCurrent < LowestHealth)
            {
                ThisBodyPart = a;
                LowestHealth = a.HealthCurrent;
            }
        }
        if (ThisBodyPart != null)
        {
            ThisBodyPart.HealthHeal(ThisBodyPart.HealthMax * b / 100);
        }
    }
}
