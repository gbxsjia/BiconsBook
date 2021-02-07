using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Damage : MonoBehaviour
{
    public static int UseEntry(Card_Base card, CardManager useManager,BodyPart targetBodyPart, int BaseDamage, int WeaponDamage)
    {
        EM_Weapon e= card.ownerEquipment as EM_Weapon;
        int damage = 0;
        if (e != null)
        {
            damage = Mathf.RoundToInt(BaseDamage + (float)e.WeaponDamage * WeaponDamage / 100f);
        }
        else
        {
            damage = Mathf.RoundToInt(BaseDamage);
        }
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
