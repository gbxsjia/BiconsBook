using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Butcher : MonoBehaviour
{
    public static void UseEntry(Card_Base card, BodyPart bodyPart, float BaseDamage,float WeaponDamage, int damagePercent)
    {

            EM_Weapon e = card.ownerEquipment as EM_Weapon;
            float damage = Mathf.RoundToInt(BaseDamage + e.WeaponDamage * WeaponDamage / 100);
        
        if (bodyPart.GetArmor() <= 0)
        {
            bodyPart.TakeDamage(damage,card.ownerEquipment.AttachedBodyPart);
        }       
    }
}
