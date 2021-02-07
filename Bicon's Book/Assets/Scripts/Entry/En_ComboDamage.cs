using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ComboDamage : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, BodyPart targetBodyPart, int BaseDamage, int WeaponDamage)
    {
        EM_Weapon e = card.ownerEquipment as EM_Weapon;
        targetBodyPart.TakeDamage((BaseDamage + e.WeaponDamage * WeaponDamage / 100)* useManager.AttackCardUsedAmount, card.ownerEquipment.AttachedBodyPart);
    }
}
