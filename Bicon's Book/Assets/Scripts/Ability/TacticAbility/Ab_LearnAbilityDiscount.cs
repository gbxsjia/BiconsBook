using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_LearnAbilityDiscount : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AchievementSystem.LearnAbilityDiscount += 0.25f;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        AchievementSystem.LearnAbilityDiscount -= 0.25f;
    }
}
