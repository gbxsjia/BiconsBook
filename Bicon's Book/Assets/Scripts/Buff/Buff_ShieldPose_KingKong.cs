using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_ShieldPose_KingKong : Buff_StanceBase
{
    public Buff_ShieldPose_KingKong(BuffType type, int remains, int amount, StanceType stanceType) : base(type, remains, amount, stanceType)
    {
        Type = type;
        thisStanceType = stanceType;
        Remains = remains;
        Amount = amount;
        BuffName = "举姿：金刚";
        BuffDescription = "每回合开始获得一张格挡";
    }
    public override void AddToManager(BuffManager buffManager, BodyPart TargetBodyPart, BodyPart SourceBodyPart)
    {
        base.AddToManager(buffManager, TargetBodyPart,SourceBodyPart);
        InGameManager.instance.OnTurnChanged += OnTurnChange;
    }

    public override void OnStanceEnd()
    {
        base.OnStanceEnd();
        InGameManager.instance.OnTurnChanged -= OnTurnChange;
    }    

    void CreateBlock()
    {
        Equipment thisWeapon = null;
        foreach(Equipment a in SourceFromBodyPart.AttachedEquipments)
        {
            foreach(EquipmentType b in a.SlotTypes )
            {
                if(b == EquipmentType.LeftWeapon)
                {
                    thisWeapon = a;
                    break;
                }
            }
            if(thisWeapon != null)
            {
                break;
            }
        }
      
        InGameManager.instance.CardManagers[OwnerBuffManager.characterManager.camp].GenerateNewCard(thisWeapon, OwnerBuffManager.Buff_GenerateCardList[(int)BuffGenerateCard.VanishedBlock]);
    }
    private void OnTurnChange(TurnState turnState)
    {
        if (SourceFromBodyPart.GetIsAlive() == true)
        {
            if (InGameManager.instance.CampTurn == OwnerBuffManager.characterManager.camp)
            {
                switch (turnState)
                {
                    case TurnState.TurnStart:
                        CreateBlock();
                        break;
                }
            }

        }
        else
        { 
            InGameManager.instance.OnTurnChanged -= OnTurnChange;
            OwnerBuffManager.RemoveBuff(this);           
        }

    }
}
