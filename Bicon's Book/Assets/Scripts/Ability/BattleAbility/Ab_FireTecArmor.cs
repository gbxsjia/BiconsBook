using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_FireTecArmor : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AbilityName = "拜火技术";
        AbilityDescription = "每次战斗结束后，低于50%的护甲会恢复至50%";
        foreach(BodyPart a in OwnerCharacter.bodyParts)
        {
            a.AutoRepairAmount = 0.5f;
        }
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        foreach (BodyPart a in OwnerCharacter.bodyParts)
        {
            a.AutoRepairAmount = 0;
        }
    }


}
