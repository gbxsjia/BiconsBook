using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityComboNeed
{
    public ComboBodyPart m_BodyPartType;
    public bool m_Triggered;
}

public enum ComboBodyPart
{
    Head,
    Chest,
    Weapon,
    Arm,
    Leg,
    None
}

[CreateAssetMenu(menuName = "CoreAbility")]
public class CoreAbility_Base : ScriptableObject
{
   public string m_CoreAbilityName;
    public Sprite m_Icon;
    public Card_Base m_AbilityCard;
    public List<AbilityComboNeed> m_AbilityComboNeedList;
    public bool AbilityAlreadyTriggered = false;

    public ComboBodyPart GetComboBodyPart(BodyPartType bodyPart)
    {
        switch (bodyPart)
        {
            case (BodyPartType.Chest):
                {
                    return ComboBodyPart.Chest;
                }

            case (BodyPartType.Head):
                {
                    return ComboBodyPart.Head;
                }

            case (BodyPartType.LeftArm):
                {
                    return ComboBodyPart.Arm;
                }

            case (BodyPartType.RightArm):
                {
                    return ComboBodyPart.Arm;
                }
            case (BodyPartType.RightLeg):
                {
                    return ComboBodyPart.Leg;
                }

            case (BodyPartType.LeftLeg):
                {
                    return ComboBodyPart.Leg;
                }

        }
        return ComboBodyPart.None;
    }

    public void OnEquip()
    {
        InGameManager.instance.BattleEndEvent += RefreshCombo;
        InGameManager.instance.OnTurnChanged += TurnRefresh;
        RefreshCombo();
    }

    public bool AllTriggered()
    {
        foreach(AbilityComboNeed ability in m_AbilityComboNeedList)
        {
            if(!ability.m_Triggered)
            {
                return false;
            }
        }
        AbilityAlreadyTriggered = true;
        return true;
    }
    public void TurnRefresh(TurnState turnState)
    {
        AbilityAlreadyTriggered = false;
        if (InGameManager.instance.CampTurn == 0 && turnState == TurnState.TurnStart)
        {
            RefreshCombo();
        }
    }
    public void RefreshCombo()
    {
        foreach (AbilityComboNeed aNeed in m_AbilityComboNeedList)
        {
            aNeed.m_Triggered = false;
        }

        UIManager.instance.CoreAbilityUIBattleUpdate();
    }
}

