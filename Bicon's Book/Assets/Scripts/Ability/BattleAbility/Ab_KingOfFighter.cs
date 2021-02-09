using UnityEngine;
using System.Collections;

public class Ab_KingOfFighter : Ability_Base
{
    private int usedPunchCardCount;
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        InGameManager.instance.BattleStartEvent += OnBattleStart;
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        InGameManager.instance.BattleStartEvent -= OnBattleStart;
    }

    public void OnBattleStart()
    {
        InGameManager.instance.CardManagers[OwnerCharacter.camp].AfterUsedCard += OnAfterUsedPunchCard;
    }

    public void OnBattleEnd()
    {
        InGameManager.instance.CardManagers[OwnerCharacter.camp].AfterUsedCard -= OnAfterUsedPunchCard;
    }

    public void OnAfterUsedPunchCard(Card_Base card)
    {
        if (card.CompareTag("Punch") || card.CompareTag("PerfectPunch"))
        {
            usedPunchCardCount++;
            Debug.Log(usedPunchCardCount);
            if (usedPunchCardCount == 2)
            {
                InGameManager.instance.CardManagers[OwnerCharacter.camp].DrawPunchCard();
                usedPunchCardCount = 0;
            }
        }
    }
}
