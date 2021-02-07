using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_SmithDiscount : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AchievementSystem.RepairAllDiscound += 0.25f;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        AchievementSystem.RepairAllDiscound -= 0.25f;
    }
}
