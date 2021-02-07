using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_BleedSeed : Buff_base
{
    public Buff_BleedSeed(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "血种";
        BuffDescription = "每回合随机部位受到层数*1%的直接伤害来回复对方血量最少的部位";
    }

    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        InGameManager.instance.OnTurnChanged += OnTurnChange;
    }
    public void AbsorbDamage()
    {
        BodyPart thisBodyPart = OwnerBuffManager.characterManager.bodyParts[Random.Range(0, 6)];
        thisBodyPart.TakeDirectHealthDamage(thisBodyPart.HealthCurrent * Amount / 100, false);
        CharacterManager EnemyCharacter = InGameManager.instance.Characters[Mathf.Abs(OwnerBuffManager.characterManager.camp - 1)];     
        


        BodyPart LowestBodyPart = null;
        int LowestHealth = 99999;
        foreach (BodyPart a in EnemyCharacter.bodyParts)
        {
            if (a.HealthCurrent > 0 && a.HealthCurrent != a.HealthMax && a.HealthCurrent < LowestHealth)
            {
                LowestBodyPart = a;
                LowestHealth = a.HealthCurrent;
            }
        }

        if (LowestBodyPart != null)
        {
            LowestBodyPart.HealthHeal(thisBodyPart.HealthCurrent * Amount / 100);
        }
    }

    private void OnTurnChange(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn == OwnerBuffManager.characterManager.camp)
        {
            switch (turnState)
            {
                case TurnState.TurnStart:
                    AbsorbDamage();
                    break;
            }
        }
    }
    public override void OnRemove()
    {
        base.OnRemove();
        InGameManager.instance.OnTurnChanged -= OnTurnChange;
    }
}