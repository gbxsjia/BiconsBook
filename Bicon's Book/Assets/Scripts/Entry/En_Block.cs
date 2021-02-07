using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Block : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, BodyPart allyBodyPart,int armorAmount, int armorPercent)
    {
        CardManager enemyCardManager = InGameManager.instance.CardManagers[1 - useManager.camp];
        enemyCardManager.attackFeedBack.newBodyPart = allyBodyPart;
        enemyCardManager.attackFeedBack.isDefended = true;
        enemyCardManager.attackFeedBack.defendCard = card;
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
        allyBodyPart.AddTempArmor(armorAmount + card.ownerEquipment.MaxArmor * armorPercent / 100);

      //  useManager.AddToUsedCards(card.gameObject);
    }
}
