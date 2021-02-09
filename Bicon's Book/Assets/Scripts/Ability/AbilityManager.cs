using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public CharacterManager thisCharacter;
    public List<Ability_Base> m_AbilityList;
    public CoreAbility_Base m_CoreAbility;

    [Header("AbilityCondition")]
    public bool AutoRepair;
    public bool ExtraReward;
    public bool HeavyBearer;
    public bool WeakPointRemoved = false;
    private void Awake()
    {
    
        thisCharacter = gameObject.GetComponent<CharacterManager>();
        if (thisCharacter.camp == 0)
        {
            CoreAbility_Base aClone = Instantiate(m_CoreAbility);
           
            m_CoreAbility = aClone;
        }
    }
    private void Start()
    {
        InGameManager.instance.AbilityManagers[thisCharacter.camp] = this;

        if (thisCharacter.camp == 0)
        {
            m_CoreAbility.OnEquip();
        }
    }
    public void EquipAbility(Ability_Base ability)
    {
        if (ability.Equiped == false)
        {
            ability.OnAbilityEquip(thisCharacter);
            m_AbilityList.Add(ability);
        }
    }

    public void UnEquipAbility(Ability_Base ability)
    {
        if (ability.Equiped == true)
        {
            if (m_AbilityList.Contains(ability))
            {
                ability.OnAbilityUnEquip();
                m_AbilityList.Remove(ability);
            }
        }
    }
}

public enum CoreAbilityType
{

}
public enum AbilityType
{
    None,
    UnKnown,
    SuiJiYingBian,
    FlashMind,
    MasterMind,
    PreparePerfectly,
    Superman,
    Popeye,
    Exercise,
    HeavyBearer,
    CarefulFight,
    Reckless,
    GoForward,
    GodMove,
    Excitaion,  
    FireOn,
    WeakPoints,   
    PriceOfGreed,
    KingOfFighter,   
    FireTecArmor,
    FoxLike,
    InnLike,
    SmithLike,
    TrainerLike,
    MasterExplorer,
    SpringFinder,
    Collector,
    HighQualitySleep,
    Student,
    Bird_Speed,
    Bird_Stamina,
    Bird_Support
}
