using UnityEngine;
using System.Collections;

public class Ab_PriceOfGreed : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        OwnerAbilityManager.ExtraReward = true;
        InGameManager.instance.CardManagers[OwnerCharacter.camp].StaminaInitialMax -= 1;
        InGameManager.instance.CardManagers[OwnerCharacter.camp].UpdateWeightAndStamina();
        UIManager.instance.UpdateWeight();
        Debug.Log("Ab_PriceOfGreed on");
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        OwnerAbilityManager.ExtraReward = false;
        InGameManager.instance.CardManagers[OwnerCharacter.camp].StaminaInitialMax += 1;
        InGameManager.instance.CardManagers[OwnerCharacter.camp].UpdateWeightAndStamina();
        UIManager.instance.UpdateWeight();
        Debug.Log("Ab_PriceOfGreed off");
    }
}

