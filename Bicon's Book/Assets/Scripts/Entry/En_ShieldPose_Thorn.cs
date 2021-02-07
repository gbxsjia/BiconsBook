using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ShieldPose_Thorn : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager)
    {
        Buff_ShieldPose_Thorn thisbuff = new Buff_ShieldPose_Thorn(BuffType.ShieldPose_Thorn, 1, 1, StanceType.Thron);
        BodyPart Chest = InGameManager.instance.Characters[useManager.camp].GetBodyPart(BodyPartType.Chest);
        BuffManager ThisBuffManager = InGameManager.instance.BuffManagers[useManager.camp];
        
        if(ThisBuffManager.IsBuffExistOnBodyPart(Chest,BuffType.ShieldPose_KingKong))
        {
            ThisBuffManager.RemoveBuff(ThisBuffManager.GetBuffOnBodypart(Chest, BuffType.ShieldPose_KingKong));            
        }
        
        ThisBuffManager.AddBuff(thisbuff, Chest, card.ownerEquipment.AttachedBodyPart,false);
    }
}
