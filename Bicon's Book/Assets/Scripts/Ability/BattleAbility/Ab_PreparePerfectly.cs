using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_PreparePerfectly : Ability_Base
{
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AbilityName = "战前准备";
        AbilityDescription = "开局第一回合额外抽取三张牌";
        InGameManager.instance.BattleStartEvent += OnBattleStart;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        InGameManager.instance.BattleStartEvent -= OnBattleStart;
    }

    public void OnBattleStart()
    {
        for (int i = 0; i < 3; i++)
        {
            InGameManager.instance.CardManagers[OwnerCharacter.camp].DrawCard();
        }
    }

}



