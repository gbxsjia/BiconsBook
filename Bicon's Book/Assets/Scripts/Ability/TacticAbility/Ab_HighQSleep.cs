using UnityEngine;
using System.Collections;

public class Ab_HighQSleep : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        CharacterManager.PlayerInstance.m_SleepAdditional = 0.25f;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        CharacterManager.PlayerInstance.m_SleepAdditional = 0f;
    }
}