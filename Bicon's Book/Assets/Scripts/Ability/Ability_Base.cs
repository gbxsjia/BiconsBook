using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Base : MonoBehaviour
{
    public CharacterManager OwnerCharacter;
    public AbilityManager OwnerAbilityManager;
    public bool Equiped = false;
    public string AbilityName;
    public string AbilityDescription;
    public string UnblockWayDescription;
    public virtual void OnAbilityEquip(CharacterManager thisCharacter)
    {
        OwnerCharacter = thisCharacter;
        OwnerAbilityManager = thisCharacter.GetComponent<AbilityManager>();
        Equiped = true;

    }

    public virtual void OnAbilityUnEquip()
    {
        Equiped = false;
    }
}
