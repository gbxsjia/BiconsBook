using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_AddStance : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, Buff_StanceBase stance)
    {
        EM_Weapon e = card.ownerEquipment as EM_Weapon;
        useManager.GetComponent<BuffManager>().AddStance(stance);
    }
}
