using UnityEngine;
using System.Collections;

public class Ab_Exercise : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        // InGameManager.instance.BattleStartEvent += OnBattleStart;
        InGameManager.instance.CardManagers[OwnerCharacter.camp].StaminaInitialMax += 1;
        InGameManager.instance.CardManagers[OwnerCharacter.camp].UpdateWeightAndStamina();
        UIManager.instance.UpdateWeight();

        Debug.Log("Ab_Exercise on");
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        InGameManager.instance.CardManagers[OwnerCharacter.camp].StaminaInitialMax -= 1;
        InGameManager.instance.CardManagers[OwnerCharacter.camp].UpdateWeightAndStamina();
        // InGameManager.instance.BattleStartEvent -= OnBattleStart;
        UIManager.instance.UpdateWeight();
        Debug.Log("Ab_Exercise off");
    }
}
