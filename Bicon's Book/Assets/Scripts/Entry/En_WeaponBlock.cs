using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_WeaponBlock : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, BodyPart allyBodyPart, int armorPercent)
    {
        EM_Weapon weapon = card.ownerEquipment as EM_Weapon;
        CardManager enemyCardManager = InGameManager.instance.CardManagers[1 - useManager.camp];
        enemyCardManager.attackFeedBack.newBodyPart = allyBodyPart;

        //Change Animation
        EquipmentManager equipmentManager = InGameManager.instance.EquipmentManagers[useManager.camp];
        switch (equipmentManager.GetHoldingWeaponType())
        {
            case HoldingWeaponType.BareHand:
                enemyCardManager.attackFeedBack.newAnimType = AnimType.Block_BareHand;
                break;
            case HoldingWeaponType.Single:
                enemyCardManager.attackFeedBack.newAnimType = AnimType.Block_SingleHand;
                break;
            case HoldingWeaponType.Dual:
                enemyCardManager.attackFeedBack.newAnimType = AnimType.Block_Dual;
                break;
        }

        //Add temp armor
        allyBodyPart.AddTempArmor(weapon.WeaponDamage * armorPercent / 100);

        useManager.AddToUsedCards(card.gameObject);
    }
}
