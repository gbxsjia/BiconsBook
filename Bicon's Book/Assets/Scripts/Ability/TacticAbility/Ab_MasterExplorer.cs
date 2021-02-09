using UnityEngine;
using System.Collections;

public class Ab_MasterExplorer : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        EnemyIncubator.thisInstance.AddtionalMapPoint = 1;
        EnemyIncubator.thisInstance.AddtionalEventPoint = 1;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        EnemyIncubator.thisInstance.AddtionalMapPoint = 0;
        EnemyIncubator.thisInstance.AddtionalEventPoint = 0;
    }
}