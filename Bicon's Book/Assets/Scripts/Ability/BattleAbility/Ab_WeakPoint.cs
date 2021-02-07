using UnityEngine;
using System.Collections;

public class Ab_WeakPoint : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        print(0);
        base.OnAbilityEquip(thisCharacter);
        InGameManager.instance.OnTurnChanged += OnTurnChange;
    }

    public override void OnAbilityUnEquip()
    {
        print(1);
        base.OnAbilityUnEquip();
        InGameManager.instance.OnTurnChanged -= OnTurnChange;
    }

    public void OnTurnChange(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn == OwnerCharacter.camp)
        {
            switch (turnState)
            {
                case TurnState.TurnStart:
                    En_WeakPoint.UseEntry(null, OwnerCharacter.camp, 1);
                     Debug.Log("OnTurnChanged Processing 1111");
                    break;
            }
        }
    }
}
