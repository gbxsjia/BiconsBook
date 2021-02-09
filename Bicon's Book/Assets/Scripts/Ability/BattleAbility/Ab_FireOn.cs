using UnityEngine;
using System.Collections;

public class Ab_FireOn : Ability_Base
{
    private int DrawCardCount;
    private float CurrentOverallHealth;
    private BodyPart[] bodyParts;

    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        CurrentOverallHealth = getOverallCurrentHealth();
        // Debug.Log("Ab_FireOn on");
        OwnerCharacter.GetComponent<CardManager>().AfterBeingAttackedEvent += AfterBeingAttackedEvent;
        InGameManager.instance.OnTurnChanged += OnTurnChanged;

        // InGameManager.instance.CardManagers[OwnerCharacter.camp].AfterBeingAttackedEvent += AfterBeingAttackedEvent;

    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        print(0);
        // InGameManager.instance.CardManagers[OwnerCharacter.camp].AfterBeingAttackedEvent -= AfterBeingAttackedEvent;
        OwnerCharacter.GetComponent<CardManager>().AfterBeingAttackedEvent -= AfterBeingAttackedEvent;
        InGameManager.instance.OnTurnChanged -= OnTurnChanged;


    }

    public void AfterBeingAttackedEvent(Card_Base card, BodyPart bodyPart)
    {
        if (DrawCardCount < 3)
        {
            InGameManager.instance.CardManagers[OwnerCharacter.camp].DrawCard();
            DrawCardCount++;
            Debug.Log("Draw Card Counts:" + DrawCardCount);
        }
    }

    public void OnTurnChanged(TurnState turnState)
    {
        if (InGameManager.instance.CampTurn == OwnerCharacter.camp)
        {
            switch (turnState)
            {
                case TurnState.TurnEnd:
                    DrawCardCount = 0;
                    // Debug.Log("OnTurnChanged Processing");
                    break;
            }
        }
    }

    public void OnBattleStart()
    {
        InGameManager.instance.CardManagers[1 - OwnerCharacter.camp].AbilityUpdateEvent += AbilityUpdateEvent;

    }

    public void OnBattleEnd()
    {
        InGameManager.instance.CardManagers[1 - OwnerCharacter.camp].AbilityUpdateEvent -= AbilityUpdateEvent;
        InGameManager.instance.OnTurnChanged -= OnTurnChanged;
    }



    public void AbilityUpdateEvent()
    {
        // Debug.Log("Most current health: " + getOverallCurrentHealth());
        // Debug.Log("Previous current health: " + CurrentOverallHealth);

        if (getOverallCurrentHealth() < CurrentOverallHealth && DrawCardCount < 4)
        {
            
            CurrentOverallHealth = getOverallCurrentHealth();
            DrawCardCount += 1;
            
        }
    }



    public float getOverallCurrentHealth()
    {
        float tempFloat = 0;
        foreach (BodyPart aPart in InGameManager.instance.Characters[OwnerCharacter.camp].bodyParts)
        {
            tempFloat += aPart.GetHealth();
        }
        // Debug.Log("getOverallCurrentHealth Processing");
        return tempFloat;
    }
}
