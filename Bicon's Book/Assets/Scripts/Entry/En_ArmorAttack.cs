using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ArmorAttack : MonoBehaviour
{
    public static int UseEntry(Card_Base card, CardManager userManager, BodyPart targetBodyPart, int BaseDamage, int ArmorDamage)
    {
        int damage = Mathf.RoundToInt(BaseDamage + card.ownerEquipment.MaxArmor * ArmorDamage / 100);
        targetBodyPart.TakeDamage(damage, card.ownerEquipment.AttachedBodyPart, out damage);
        return damage;
    }
}

