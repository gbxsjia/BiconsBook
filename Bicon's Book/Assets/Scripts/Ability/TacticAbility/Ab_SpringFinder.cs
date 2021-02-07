using UnityEngine;
using System.Collections;

public class Ab_SpringFinder : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AchievementSystem.instance.a_SpringFinderUnblock = true;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        AchievementSystem.instance.a_SpringFinderUnblock = false;
    }
}