using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_ShadowKing : Buff_base
{
    public Buff_ShadowKing(BuffType type, int remains, int amount,int Level) : base(type,remains,amount)
    { 
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "西贝王的暗影";
        BuffDescription = "每当你使用一张攻击牌，对对方造成"+ Level * 6 + "点伤害。";
    }
    public override void AddToManager(BuffManager buffManager, BodyPart TargetBodyPart)
    {
        base.AddToManager(buffManager, TargetBodyPart);
        InGameManager.instance.OnTurnChanged += OnTurnChange;
        OwnerBuffManager.cardManager.AfterUsedCard += ShadowAttack;
    }

    void ShadowAttack(Card_Base card)
    {
        if (card.cardType == CardType.Attack)
        {
            Equipment thisWeapon = null;
            GameObject aCard = OwnerBuffManager.cardManager.GenerateNewCard(thisWeapon, OwnerBuffManager.Buff_GenerateCardList[(int)BuffGenerateCard.ShadowKingAttack]);
            OwnerBuffManager.cardManager.UseCard(aCard);
        }
    }

    private void OnTurnChange(TurnState turnState)
    {

        if (InGameManager.instance.CampTurn == OwnerBuffManager.characterManager.camp)
        {

            switch (turnState)
            {
                case TurnState.TurnEnd:
      
                    OwnerBuffManager.cardManager.AfterUsedCard -= ShadowAttack;
                    InGameManager.instance.OnTurnChanged -= OnTurnChange;
                    OwnerBuffManager.RemoveBuff(this);

                    break;
            }
        }


    }
}
