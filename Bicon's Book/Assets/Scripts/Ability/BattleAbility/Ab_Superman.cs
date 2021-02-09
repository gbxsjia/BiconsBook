using UnityEngine;
using System.Collections;

public class Ab_Superman : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AbilityName = "强壮身躯";
        AbilityDescription = "全身增加12血量上限";

        UpGradeManager.instance.UpgradeAllPart(12);



    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();

        UpGradeManager.instance.UpgradeAllPart(-12);


    }
}
