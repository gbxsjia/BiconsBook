using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCardManager : MonoBehaviour
{
    public static DefaultCardManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject[] DefaultWeaponCards;

    public Equipment DefaultWeapon;
    public GameObject GetDefaultWeaponCard(Equipment equipment)
    {
        EquipmentType type = equipment.SlotTypes[0];
        EM_Weapon weapon = equipment as EM_Weapon;
        int typeIndex = (int)type;
        switch (type)
        {
            case EquipmentType.Armor:
                break;
            case EquipmentType.Helmet:
                break;
            case EquipmentType.LeftArm:
                break;
            case EquipmentType.LeftFoot:
                break;
            case EquipmentType.LeftHand:
                break;
            case EquipmentType.LeftLeg:
                break;
            case EquipmentType.LeftPocket:
                break;
            case EquipmentType.LeftWeapon:
                typeIndex = (int)weapon.weaponType;                
                break;
            case EquipmentType.RightArm:
                break;
            case EquipmentType.RightFoot:
                break;
            case EquipmentType.RightHand:
                break;
            case EquipmentType.RightLeg:
                break;
            case EquipmentType.RightPocket:
                break;
            case EquipmentType.RightWeapon:
                typeIndex = (int)weapon.weaponType;
                break;
            case EquipmentType.Tail:
                break;
            case EquipmentType.Ear:
                break;
        }
        
        return Instantiate(DefaultWeaponCards[typeIndex]);
    }
}
