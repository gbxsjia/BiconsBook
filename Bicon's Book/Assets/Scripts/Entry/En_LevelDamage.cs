using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_LevelDamage : MonoBehaviour
{
    public static int UseEntry(Card_Base card, CardManager useManager,BodyPart targetBodyPart, int BaseDamage)
    {
        int damage = 0;

            damage = Mathf.RoundToInt(BaseDamage * InGameManager.instance.Characters[useManager.camp].characterLevel);
        
        if (card.ownerEquipment != null)
        {
            targetBodyPart.TakeDamage(damage, card.ownerEquipment.AttachedBodyPart, out damage);
        }
        else
        {
              targetBodyPart.TakeDamage(damage, null, out damage);
        }
        return damage;
    }
}
