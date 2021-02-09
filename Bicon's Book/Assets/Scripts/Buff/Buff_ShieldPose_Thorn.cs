using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_ShieldPose_Thorn : Buff_StanceBase
{
    public Buff_ShieldPose_Thorn(BuffType type, int remains, int amount, StanceType stanceType) : base(type, remains, amount, stanceType)
    {
        Type = type;
        thisStanceType = stanceType;
        Remains = remains;
        Amount = amount;
        BuffName = "举姿：荆棘";
        BuffDescription = "每回合在指定部位添加6层荆棘，每次触发减少2层";
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


    private void OnTurnChange(TurnState turnState)
    {

        if (SourceFromBodyPart.GetIsAlive() == true)
        {
            if (InGameManager.instance.CampTurn == OwnerBuffManager.characterManager.camp)
            {
                switch (turnState)
                {
                    case TurnState.TurnStart:
                        Buff_Thorn buff = new Buff_Thorn(BuffType.Thorn, 1, 6);
                        OwnerBuffManager.AddBuff(buff, OwnerBuffManager.cardManager.AllyBodyPart, true);
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
