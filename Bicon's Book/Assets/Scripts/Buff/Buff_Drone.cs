using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Drone : Buff_base
{
    public Buff_Drone(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "浮游炮";
        BuffDescription = "每回合对敌方随机部位施加层数的直接伤害";
    }

    public void DroneDamage()
    {
        //BodyPart enemyBodyPart = InGameManager.instance.Characters[Mathf.Abs(OwnerBuffManager.characterManager.camp - 1)].bodyParts[Random.Range(0, 6)];
        BodyPart enemyBodyPart = InGameManager.instance.Characters[1-OwnerBuffManager.characterManager.camp].bodyParts[Random.Range(0, 6)];

        while (enemyBodyPart.GetIsAlive() == false)
        {
            enemyBodyPart = OwnerBuffManager.characterManager.bodyParts[Random.Range(0, 6)];
        }
        enemyBodyPart.TakeDamage(Amount, enemyBodyPart);
    }

    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        InGameManager.instance.OnTurnChanged += OnTurnChange;
    }

    public void OnTurnChange(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn == OwnerBuffManager.characterManager.camp)
        {
            switch (turnState)
            {
                case TurnState.TurnStart:
                    DroneDamage();
                    break;
            }
        }
    }
}
