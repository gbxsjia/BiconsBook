using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_BleedRefresh : MonoBehaviour
{
    public static void UseEntry(CardManager useManager)
    {
        BuffManager buffManager = InGameManager.instance.BuffManagers[1 - useManager.camp];

        List<Buff_Bleed> bleedBuffs = new List<Buff_Bleed>();
        foreach (BodyPart bp in buffManager.Buffs.Keys)
        {           
            foreach (Buff_base buff in buffManager.Buffs[bp])
            {
                Buff_Bleed bleed = buff as Buff_Bleed;
                if (bleed != null)
                {
                    bleedBuffs.Add(bleed);
                }
            }
        }
        foreach (Buff_Bleed bleed in bleedBuffs)
        {
            bleed.Remains = 3;
            bleed.CauseDamage();
        }
    }
}
