using UnityEngine;
using System.Collections;

public class Ab_Excitation : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        InGameManager.instance.CardManagers[OwnerCharacter.camp].AbilityUpdateEvent += AbilityUpdateEvent;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        InGameManager.instance.CardManagers[OwnerCharacter.camp].AbilityUpdateEvent -= AbilityUpdateEvent;
    }

    public void AbilityUpdateEvent()
    {
        if (OwnerAbilityManager.WeakPointRemoved)
        {
            InGameManager.instance.CardManagers[OwnerCharacter.camp].StaminaCurrent += 1;
            OwnerAbilityManager.WeakPointRemoved = false;
            Debug.Log("Ab_Excitation Processing");
        }
    }
}