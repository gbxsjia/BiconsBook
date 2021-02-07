using UnityEngine;
using System.Collections;

public class Ab_RecklessFight : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AbilityName = "鲁莽作战";
        AbilityDescription = "战斗初始位置+1";
        InGameManager.instance.CharacterPositionsUpdate[OwnerCharacter.camp] = InGameManager.instance.CardManagers[OwnerCharacter.camp].RealDirection(1, OwnerCharacter.camp);
        Debug.Log("鲁莽作战 on");
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        InGameManager.instance.CharacterPositionsUpdate[OwnerCharacter.camp] = 0;
    }
}