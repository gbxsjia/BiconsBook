using UnityEngine;
using System.Collections;

public class Ab_Collector : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AchievementSystem.instance.a_CollectorUnblock = true;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        AchievementSystem.instance.a_CollectorUnblock = false;
    }
}