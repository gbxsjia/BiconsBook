using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Thorn : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, BodyPart targetBodyPart, int stack)
    {
        EM_Weapon e = card.ownerEquipment as EM_Weapon;
        Buff_Thorn buff = new Buff_Thorn(BuffType.Thorn, 1, stack+ e.WeaponSpecial);
        BuffManager ThisBuffManager = InGameManager.instance.BuffManagers[useManager.camp];
        ThisBuffManager.AddBuff(buff, targetBodyPart, true);
    }
}
