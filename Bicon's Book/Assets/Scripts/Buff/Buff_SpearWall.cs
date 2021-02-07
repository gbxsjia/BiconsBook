using UnityEngine;
using System.Collections;
using UnityEngine;


public class Buff_SpearWall : Buff_base
{
    public Buff_SpearWall(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "矛墙";
        BuffDescription = "当敌人靠近时，击退对方，并消耗一层矛墙";
    }

    public void OnEnemyMoving()
    {
        if (InGameManager.instance.CampTurn == 1 - OwnerBuffManager.characterManager.camp)
        {

            if (InGameManager.instance.GetDistance() == 1 && Amount > 0)
            {
                Equipment thisWeapon = null;
                foreach (Equipment a in SourceFromBodyPart.AttachedEquipments)
                {
                    foreach (EquipmentType b in a.SlotTypes)
                    {
                        if (b == EquipmentType.LeftWeapon)
                        {
                            thisWeapon = a;
                            break;
                        }
                    }
                    if (thisWeapon != null)
                    {
                        break;
                    }
                }
                GameObject thiscard = InGameManager.instance.CardManagers[OwnerBuffManager.characterManager.camp].GenerateNewCard(thisWeapon, OwnerBuffManager.Buff_GenerateCardList[(int)BuffGenerateCard.SpearWall]);
                InGameManager.instance.CardManagers[OwnerBuffManager.characterManager.camp].UseCard(thiscard, TriggerType.Use, true, false);
                // Debug.Log(Amount);
                Amount -= 1;
                if (Amount == 0)
                {
                    BodyPart Chest = InGameManager.instance.Characters[OwnerBuffManager.characterManager.camp].GetBodyPart(BodyPartType.Chest);
                    InGameManager.instance.BuffManagers[OwnerBuffManager.characterManager.camp].RemoveBuffByType(BuffType.SpearWall, Chest);
                    Debug.Log("SpearWall Buff had been removed!!!");
                }

            }
        }
    }


    public override void AddToManager(BuffManager buffManager,BodyPart bodyPart, BodyPart FrombodyPart)
    {
        base.AddToManager(buffManager, bodyPart, FrombodyPart);
        InGameManager.instance.OnEnemyMoving += OnEnemyMoving;
    }
}
