using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_FlashMind : Ability_Base
{
    int TurnCount;
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AbilityName = "灵光一闪";
        AbilityDescription = "每三回合多抽两张手牌";
        InGameManager.instance.OnTurnChanged += OnTurnChanged;
        InGameManager.instance.BattleStartEvent += OnBattleStart;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        InGameManager.instance.OnTurnChanged -= OnTurnChanged;
        InGameManager.instance.BattleStartEvent -= OnBattleStart;
    }

    public void OnTurnChanged(TurnState turnState )
    {
        if (InGameManager.instance.CampTurn == OwnerCharacter.camp)
        {
            switch(turnState)
            {
                case TurnState.TurnStart:
                    TurnCount += 1;
                    if(TurnCount == 3)
                    {
                        TurnCount = 0;
                        InGameManager.instance.CardManagers[OwnerCharacter.camp].DrawCard();
                        InGameManager.instance.CardManagers[OwnerCharacter.camp].DrawCard();
                    }
                    
                    break;
            }
        }
    }
    public void OnBattleStart()
    {
        TurnCount = 0;
    }
}
