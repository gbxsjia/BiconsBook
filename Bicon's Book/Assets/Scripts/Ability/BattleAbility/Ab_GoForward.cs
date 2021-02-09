using UnityEngine;
using System.Collections;

public class Ab_GoForward : Ability_Base
{
    CardManager thisCardManager;
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        thisCardManager = InGameManager.instance.CardManagers[OwnerAbilityManager.thisCharacter.camp];
        thisCardManager.ForwardMoveStaminaOffset -= 1;
        thisCardManager.BackwardMoveStaminaOffset += 1;
    }

    public override void OnAbilityUnEquip()
    {
        thisCardManager.ForwardMoveStaminaOffset += 1;
        thisCardManager.BackwardMoveStaminaOffset -= 1;
        base.OnAbilityUnEquip();
    }
}
