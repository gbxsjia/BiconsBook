using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ConsumeItem : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager)
    {

        card.gameObject.SetActive(false);
        useManager.Cards.Remove(card.gameObject);

        ItemInstance instance = UIManager.instance.EquipmentSlots[card.ownerEquipment.AttachedSlotIndex].thisItem;

        InGameManager.instance.EquipmentManagers[useManager.camp].Equip(null, (EquipmentType)card.ownerEquipment.AttachedSlotIndex);

        Destroy(instance.gameObject);
        Destroy(card.ownerEquipment);
    }
}
