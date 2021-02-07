using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Bleed : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, BodyPart targetBodyPart, int stack, int WeaponSpecialPercent)
    {
        EM_Weapon e = card.ownerEquipment as EM_Weapon;
        if (e != null)
        {
            targetBodyPart.thisCharacter.GetComponent<BuffManager>().AddBleedBuff(targetBodyPart, stack + e.WeaponSpecial * WeaponSpecialPercent / 100);
        }
        else
        {
            targetBodyPart.thisCharacter.GetComponent<BuffManager>().AddBleedBuff(targetBodyPart, stack);
        }
    }
}
