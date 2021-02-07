using UnityEngine;
using System.Collections;

public class Ab_Student : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
      //  UpGradeManager.instance.m_ExpMultipler = 1.25f;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
      //  UpGradeManager.instance.m_ExpMultipler = 1f;
    }
}