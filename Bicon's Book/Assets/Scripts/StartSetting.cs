using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSetting : MonoBehaviour
{
    [SerializeField] List<Equipment> StartEquipment;

    public void StartGoldSet(int Amount)
    {
        CharacterManager.PlayerInstance.AddGold(Amount);
    }
    public void StartEquipmentSet()
    {
        EquipmentManager PlayerEqmManager = InGameManager.instance.EquipmentManagers[0];
        for (int i = 0; i < StartEquipment.Count; i++)
        {
            foreach (EquipmentType a in StartEquipment[i].SlotTypes)
            {

                if(PlayerEqmManager.GetEquipment(a) == null)
                {
                    
                    PlayerEqmManager.Equip(Instantiate(StartEquipment[i]),a);
                    UI_EqmStatusPanel.m_Instance.AddEquipmentCard(StartEquipment[i]);
                    break;
                }
               
            }           
        }
        
        Inventory.instance.InitializeAllEqm();
        InGameManager.instance.CardManagers[0].UpdateWeightAndStamina();
        UIManager.instance.UpdateGoldText();
        UIManager.instance.UpdateWeight();
    }
}
