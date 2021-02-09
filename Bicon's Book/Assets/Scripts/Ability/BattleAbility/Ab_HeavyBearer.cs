using UnityEngine;
using System.Collections;

public class Ab_HeavyBearer : Ability_Base
{
    /*
    UnityEngine.Debug:Log(Object)
    CardManager:UpdateWeightAndStamina() (at Assets/Scripts/CardManager.cs:94)
    CardManager:Update() (at Assets/Scripts/CardManager.cs:106) alwasy show 31 for weight. 
    */
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        OwnerAbilityManager.HeavyBearer = true;
        InGameManager.instance.CardManagers[OwnerCharacter.camp].UpdateWeightAndStamina();
        UIManager.instance.UpdateWeight();
        Debug.Log("Ab_HeavyBearer on");
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        OwnerAbilityManager.HeavyBearer = false;
        InGameManager.instance.CardManagers[OwnerCharacter.camp].UpdateWeightAndStamina();
        UIManager.instance.UpdateWeight();
        Debug.Log("Ab_HeavyBearer off");
    }
}
