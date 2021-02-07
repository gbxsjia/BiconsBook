using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_MasterMind : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AbilityName = "战术思维";
        AbilityDescription = "每回合多抽一张手牌。";
        InGameManager.instance.OnTurnChanged += OnTurnChanged;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        InGameManager.instance.OnTurnChanged -= OnTurnChanged;
    }

    public void OnTurnChanged(TurnState turnState )
    {
        if (InGameManager.instance.CampTurn == OwnerCharacter.camp)
        {
            switch(turnState)
            {
                case TurnState.TurnStart:
                    InGameManager.instance.CardManagers[OwnerCharacter.camp].DrawCard();
                    break;
            }
        }
    }

}
