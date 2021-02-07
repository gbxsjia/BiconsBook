using UnityEngine;
using System.Collections;

public class Ab_GodMove : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        InGameManager.instance.BattleStartEvent += OnBattleStart;
        // Debug.Log("Ab_GodMove on");
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();

        InGameManager.instance.BattleStartEvent -= OnBattleStart;
        // Debug.Log("Ab_GodMove off");
    }

    public void OnBattleStart()
    {
        InGameManager.instance.CardManagers[OwnerCharacter.camp].CurrentMoveStaminaCost = 1;
        // Debug.Log("Ab_GodMove Processing");
    }
}
