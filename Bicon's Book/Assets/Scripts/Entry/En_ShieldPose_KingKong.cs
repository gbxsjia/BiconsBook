using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ShieldPose_KingKong : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager)
    {
        Buff_ShieldPose_KingKong thisbuff = new Buff_ShieldPose_KingKong(BuffType.ShieldPose_KingKong, 1, 1, StanceType.KingKong);
        BodyPart Chest = InGameManager.instance.Characters[useManager.camp].GetBodyPart(BodyPartType.Chest);
        BuffManager ThisBuffManager = InGameManager.instance.BuffManagers[useManager.camp];
        
        if(ThisBuffManager.IsBuffExistOnBodyPart(Chest,BuffType.ShieldPose_Thorn))
        {
            ThisBuffManager.RemoveBuff(ThisBuffManager.GetBuffOnBodypart(Chest, BuffType.ShieldPose_Thorn));            
        }
        
        ThisBuffManager.AddBuff(thisbuff, Chest, card.ownerEquipment.AttachedBodyPart,false);
    }
}
