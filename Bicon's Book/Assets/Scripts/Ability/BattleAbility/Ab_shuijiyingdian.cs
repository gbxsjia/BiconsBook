using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_shuijiyingdian : Ability_Base
{
    private int DrawCardCount = 0;
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AbilityName = "随机应变";
        AbilityDescription = "打光手牌后，抽一张牌，每回合最多触发4次";
        InGameManager.instance.CardManagers[OwnerCharacter.camp].AbilityUpdateEvent += AbilityUpdateEvent;
        InGameManager.instance.OnTurnChanged += OnTurnChanged;
        Debug.Log("Ab_on");
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        InGameManager.instance.CardManagers[OwnerCharacter.camp].AbilityUpdateEvent -= AbilityUpdateEvent;
        InGameManager.instance.OnTurnChanged -= OnTurnChanged; 

    }

    public void OnTurnChanged(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn == OwnerCharacter.camp)
        {
            switch(turnState)
            {
                case TurnState.TurnStart:
                    DrawCardCount = 0;
                    break;

            }
        }
    }

    public void AbilityUpdateEvent()
    {
        Debug.Log(InGameManager.instance.CardManagers[OwnerCharacter.camp].Cards.Count);
        if (InGameManager.instance.CampTurn == OwnerCharacter.camp)
        {
            if (InGameManager.instance.CardManagers[OwnerCharacter.camp].Cards.Count == 0 && DrawCardCount < 4)
            {
                InGameManager.instance.CardManagers[OwnerCharacter.camp].DrawCard();
                DrawCardCount++;
                Debug.Log(DrawCardCount);
            }
        }
    }
}
