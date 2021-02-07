using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public CharacterManager thisCharacter;
    [SerializeField]
    private BodyPartType PartType;

    public List<Equipment> AttachedEquipments = new List<Equipment>();

    private bool IsAlive = true;
    [SerializeField]
    public int HealthMax = 30;
    public int HealthCurrent;

    public int ArmorMax;
    public int ArmorCurrent;

    public int TempArmor;


   public  List<BodyPart> SameHoriBodyParts(BodyPart aPartType)
    {
        BodyPartType bodyPartType = aPartType.GetBodyPartType();
        List<BodyPart> bodyParts = new List<BodyPart>();
        if (bodyPartType == BodyPartType.Chest || bodyPartType == BodyPartType.LeftArm || bodyPartType == BodyPartType.RightArm)
        {
            bodyParts.Add(thisCharacter.GetBodyPart(BodyPartType.Chest));
            bodyParts.Add(thisCharacter.GetBodyPart(BodyPartType.LeftArm));
            bodyParts.Add(thisCharacter.GetBodyPart(BodyPartType.RightArm));
        }
        else if(bodyPartType == BodyPartType.LeftLeg || bodyPartType == BodyPartType.RightLeg)
        {
            bodyParts.Add(thisCharacter.GetBodyPart(BodyPartType.LeftLeg));
            bodyParts.Add(thisCharacter.GetBodyPart(BodyPartType.RightLeg));
        }
        else if(bodyPartType == BodyPartType.Head)
        {
            bodyParts.Add(thisCharacter.GetBodyPart(BodyPartType.Head));
        }
        else
        {
            bodyParts.Add(aPartType);
        }

        return bodyParts;
           
    }
    public void SetBodyPartType(BodyPartType bodyPart)
    {
        PartType = bodyPart;
    }
    public BodyPartType GetBodyPartType()
    {
        return PartType;
    }
    public bool GetIsAlive()
    {
        return IsAlive;
    }
    public float GetHealth()
    {
        return HealthCurrent;
    }
    public float GetMaxHealth()
    {
        return HealthMax;
    }
    public float GetHealthPrecent()
    {
        return HealthCurrent / HealthMax;
    }
    public float GetArmor()
    {
        return ArmorCurrent;
    }
    public float GetArmorMax()
    {
        return ArmorMax;
    }
    public void HealthHeal(float amount)
    {
        if (IsAlive)
        {

            HealthCurrent += Mathf.CeilToInt(amount);
            if (HealthCurrent > HealthMax)
            {
                HealthCurrent = HealthMax;
            }

            UpdateUI();
        }
    }
    public void ArmorHeal(float amount)
    {
        if (IsAlive)
        {

            ArmorCurrent += Mathf.CeilToInt(amount);
            if (ArmorCurrent > ArmorMax)
            {
                ArmorCurrent = ArmorMax;
            }

            UpdateUI();
        }
    }
    public bool TakeDirectHealthDamage(float amount, bool isFromAttack=true, bool isFatal=true)
    {
        float mutiplier = 1;
        if (OnDamageFloatEvent != null)
        {
            mutiplier = OnDamageFloatEvent(mutiplier);
        }
        if (isFromAttack)
        {
            BuffManager enemyBuffManager = InGameManager.instance.BuffManagers[1 - thisCharacter.camp];
            BuffManager myBuffManager = InGameManager.instance.BuffManagers[thisCharacter.camp];
            mutiplier += enemyBuffManager.GetAttack() / 100 - myBuffManager.GetDefence() / 100;
        }
        amount *= mutiplier;
        InGameManager.instance.CardManagers[1 - thisCharacter.camp].OnCauseDamage(this, amount, ArmorCurrent + TempArmor + HealthCurrent <= amount);

        if (IsAlive)
        {
            HealthCurrent -= Mathf.RoundToInt(amount);

            bool isDead = false;
          
            if (HealthCurrent <= 0)
            {
                if (isFatal)
                {
                    BodyPartBreak();
                    HealthCurrent = 0;
                    isDead= true;
                }
                else
                {
                    HealthCurrent = 1;
                    isDead= false;
                }
            }
            else
            {
                isDead= false;
            }
            UpdateUI();
            return isDead;
        }
        else
        {
            return false;
        }
    }

    private void OnDamage()
    {

    }
    public bool TakeDamage(float amount, BodyPart AttackerBodyPart, out int FinalDmg, bool isFromAttack = true, bool isFatal = true)
    {
      if(PartType == BodyPartType.None)
        {
            FinalDmg = 0;
            return false;
        }
        float mutiplier = 1;
        if (OnDamageFloatEvent != null)
        {
            mutiplier = OnDamageFloatEvent(mutiplier);
        }
        if (isFromAttack)
        {
            BuffManager enemyBuffManager = InGameManager.instance.BuffManagers[1 - thisCharacter.camp];
            BuffManager myBuffManager = InGameManager.instance.BuffManagers[thisCharacter.camp];
            mutiplier += enemyBuffManager.GetAttack() / 100f - myBuffManager.GetDefence() / 100f;
        }
        amount *= mutiplier;
  
        InGameManager.instance.CardManagers[1 - thisCharacter.camp].OnCauseDamage(this, amount, ArmorCurrent + TempArmor + HealthCurrent <= amount);

        if (IsAlive)
        {
            if (OnDamageEvent != null && AttackerBodyPart != null)
            {
                OnDamageEvent.Invoke(AttackerBodyPart, this);
            }
            //     OnDamageEvent?.Invoke(AttackerBodyPart, this);

            int ShowDmg = Mathf.CeilToInt(amount) - TempArmor;
            TempArmor -= Mathf.CeilToInt(amount);     
         
            if (TempArmor < 0)
            {
                FinalDmg = ShowDmg;
                ArmorCurrent -= Mathf.Abs(TempArmor);
                TempArmor = 0;

                if (ArmorCurrent < 0)
                {
                    HealthCurrent -= Mathf.Abs(ArmorCurrent);
                    ArmorCurrent = 0;
                    ArmorBreak();
                }
            }
            else
            {  
                FinalDmg = 0;
            }

            UpdateUI();
            if (HealthCurrent <= 0)
            {
                if (isFatal)
                {
                    BodyPartBreak();
                    HealthCurrent = 0;
                    UpdateUI();
                    return true;
                }
                else
                {
                    HealthCurrent = 1;
                    UpdateUI();
                    return false;
                }
            }
            else
            {
                return false;
            }


        }
        else
        {
            FinalDmg = 0;
            return false;
        }
    }
    public bool TakeDamage(float amount,BodyPart AttackerBodyPart, bool isFromAttack=true, bool isFatal = true)
    {
        if (PartType == BodyPartType.None)
        {
            return false;
        }
        float mutiplier = 1;
        if (OnDamageFloatEvent != null)
        {            
            mutiplier = OnDamageFloatEvent(mutiplier);            
        }
        if (isFromAttack)
        {
            BuffManager enemyBuffManager = InGameManager.instance.BuffManagers[1 - thisCharacter.camp];
            BuffManager myBuffManager = InGameManager.instance.BuffManagers[thisCharacter.camp];
            mutiplier += enemyBuffManager.GetAttack() / 100f - myBuffManager.GetDefence() / 100f;
        }
        amount *= mutiplier;
        InGameManager.instance.CardManagers[1 - thisCharacter.camp].OnCauseDamage(this, amount, ArmorCurrent + TempArmor + HealthCurrent <= amount);

        if (IsAlive)
        {
            if(OnDamageEvent != null)
            {
                OnDamageEvent.Invoke(AttackerBodyPart, this);
            }
       //     OnDamageEvent?.Invoke(AttackerBodyPart, this);

            TempArmor -= Mathf.CeilToInt(amount);
            if (TempArmor < 0)
            {
                ArmorCurrent -= Mathf.Abs(TempArmor);
                TempArmor = 0;

                if (ArmorCurrent < 0)
                {
                    HealthCurrent -= Mathf.Abs(ArmorCurrent);
                    ArmorCurrent = 0;
                    ArmorBreak();
                }
            }

           UpdateUI();
            if (HealthCurrent <= 0)
            {
                if (isFatal)
                {
                    BodyPartBreak();
                    HealthCurrent = 0;
                    UpdateUI();
                    return true;
                }
                else
                {
                    HealthCurrent = 1;
                    UpdateUI();
                    return false;
                }
            }
            else
            {
                return false;
            } 

            
        }
        else
        {
            return false;
        }
    }
    public void AddTempArmor(float amount)
    {
        TempArmor += Mathf.RoundToInt(amount);
        UpdateUI();
    }
    public delegate float DamageDelegate(float multiplier);
    public event DamageDelegate OnDamageFloatEvent;
    public event System.Action<BodyPart,BodyPart> OnDamageEvent;
    public event System.Action<BodyPart> BodyPartBreakEvent;
    public event System.Action<BodyPart> ArmorBreakEvent;
    public void ArmorBreak()
    {
        thisCharacter.ArmorBreak(this);
        if (ArmorBreakEvent != null)
        {
            ArmorBreakEvent(this);
        }
    }
    public void BodyPartBreak()
    {
        if (PartType != BodyPartType.None)
        {
            IsAlive = false;
            if (PartType == BodyPartType.Chest || PartType == BodyPartType.Head)
            {

                InGameManager.instance.BattleEnd(thisCharacter.camp == 1);
            }
            else
            {
                BodyPart chest = thisCharacter.GetBodyPart(BodyPartType.Chest);
                chest.TakeDirectHealthDamage(Mathf.Ceil(chest.GetMaxHealth() * 0.5f));
            }

            if (PartType == BodyPartType.LeftLeg || PartType == BodyPartType.RightLeg)
            {
                InGameManager.instance.CardManagers[thisCharacter.camp].CurrentMoveStaminaCost += 1;
                Buff_BrokenLeg buff = new Buff_BrokenLeg(BuffType.BrokenLeg, 1, -1);
                InGameManager.instance.BuffManagers[thisCharacter.camp].AddBuff(buff, thisCharacter.GetBodyPart(BodyPartType.Chest), true);
            }
            thisCharacter.BodyPartBreak(this);
            if (BodyPartBreakEvent != null)
            {
                BodyPartBreakEvent(this);
            }
        }
    }

    public void AddHealthMax(float Amount)
    {
        HealthMax += Mathf.RoundToInt(Amount);
        UpdateUI();
    }
    public void AddArmorMax(float Amount)
    {
        ArmorMax += Mathf.RoundToInt(Amount);
        UpdateUI();
    }

    public void EquipItem(Equipment equipment)
    {
        if (equipment != null)
        {
            AttachedEquipments.Add(equipment);
            AddArmorMax(equipment.MaxArmor);
            AddHealthMax(equipment.ExtraHealth);
            ArmorCurrent += Mathf.RoundToInt(equipment.CurrentArmor);
            equipment.AttachedBodyPart = this;
        }
        UpdateAllPartsArmor();
    }

    public void UnEquipItem(Equipment equipment)
    {
        if (equipment != null)
        {
            AttachedEquipments.Remove(equipment);
            AddArmorMax(-equipment.MaxArmor);
            AddHealthMax(-equipment.ExtraHealth);
            ArmorCurrent -= Mathf.RoundToInt(equipment.CurrentArmor);
            equipment.AttachedBodyPart = null;
        }
        UpdateAllPartsArmor();
    }

    private void Start()
    {
        HealthCurrent = HealthMax;
        ArmorCurrent = ArmorMax;

        InGameManager.instance.ReturnToCampEvent += OnReturnToCamp;
        InGameManager.instance.BattleStartEvent += BattleStartEvent;
        InGameManager.instance.OnTurnChanged += OnTurnChanged;
    }

    private void OnTurnChanged(TurnState turnState)
    {
        if (turnState == TurnState.TurnStart && InGameManager.instance.CampTurn == thisCharacter.camp)
        {
            TempArmor = 0;
            UpdateUI();
        }
    }

    private void BattleStartEvent()
    {
        if (thisCharacter.camp == 0)
        {
            ArmorCurrent = 0;
            foreach (Equipment a in AttachedEquipments)
            {
                ArmorCurrent += a.CurrentArmor;
            }
        }
        else
        {
            HealthCurrent = HealthMax;
            UpdateUI();
        }
    }

    public void UpdateAllPartsArmor()
    {

        ArmorCurrent = 0;
        foreach (Equipment a in AttachedEquipments)
        {
            ArmorCurrent += a.CurrentArmor;
        }
        UpdateUI();
    }

    public event System.Action UpdateUIEvent;
    public void UpdateUI()
    {
        if (UpdateUIEvent != null)
        {
            UpdateUIEvent();
        }
    }
    public event System.Action UpdateBuffUIEvent;
    public void UpdateBuffUI()
    {
        if (UpdateBuffUIEvent != null)
        {
            UpdateBuffUIEvent();
        }
    }
    public event System.Action MouseEnterEvent;
    public void MouseOver()
    {
        if (MouseEnterEvent != null)
        {
            MouseEnterEvent();
        }
    }
    public event System.Action MouseExitEvent;
    public void MouseExit()
    {
        if (MouseExitEvent != null)
        {
            MouseExitEvent();
        }
    }

   public  float AutoRepairAmount = 0;
    private void OnReturnToCamp()
    {
       
        if (thisCharacter.camp == 0)
        {
           if(HealthCurrent <= 0)
            {
                HealthCurrent = 1;
              
            }
           if (ArmorMax > 0)
            {
                float ArmorLeftPercent = (float)ArmorCurrent / (float)ArmorMax;


                if (ArmorLeftPercent < AutoRepairAmount)
                {
                    ArmorLeftPercent = AutoRepairAmount;
                }

                
                foreach (Equipment a in AttachedEquipments)
                {
                    a.CurrentArmor = Mathf.RoundToInt(a.MaxArmor * ArmorLeftPercent);
                }
            }

            ArmorCurrent = 0;
            foreach (Equipment a in AttachedEquipments)
            {
                ArmorCurrent += Mathf.RoundToInt(a.CurrentArmor);
            }
            UpdateUI();
        }
        else
        {
            HealthCurrent = HealthMax;
            ArmorCurrent = ArmorMax;          
        }  

        IsAlive = true;

        UpGradeManager.instance.UpdateThisUi();
    }
}
public enum BodyPartType
{
    LeftArm,
    RightArm,
    LeftLeg,
    RightLeg,
    Chest,
    Head,
    None
}