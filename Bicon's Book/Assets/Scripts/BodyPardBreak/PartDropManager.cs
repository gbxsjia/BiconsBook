using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDropManager : MonoBehaviour
{
   public CharacterManager thisCharacter;
   [SerializeField] GameObject weaponDropPrefab;
    [SerializeField] SpriteRenderer BloodEffectRenderer;
    [SerializeField] Sprite BloodSprite;

    public void DropWeapon()
    {
        BloodEffectRenderer.sprite = BloodSprite;
        if(thisCharacter.GetBodyPart(BodyPartType.LeftArm).GetIsAlive())
        {
            EM_Weapon thisLeftWeapon = InGameManager.instance.EquipmentManagers[thisCharacter.camp].GetEquipment(EquipmentType.LeftWeapon) as EM_Weapon;
            if (thisLeftWeapon != null)
            {
                GameObject aDropObj = Instantiate(weaponDropPrefab);
                PartDrop aDrop = aDropObj.GetComponent<PartDrop>();
                aDrop.StartThis(thisCharacter, thisLeftWeapon, DropPartType.LeftWeapon, DropAnm.WeaponDeathDrop);
            }
        }
        if (thisCharacter.GetBodyPart(BodyPartType.RightArm).GetIsAlive())
        {
            EM_Weapon thisRightWeapon = InGameManager.instance.EquipmentManagers[thisCharacter.camp].GetEquipment(EquipmentType.RightWeapon) as EM_Weapon;
            GameObject aDropObj = Instantiate(weaponDropPrefab);
            PartDrop aDrop = aDropObj.GetComponent<PartDrop>();
            aDrop.StartThis(thisCharacter, thisRightWeapon, DropPartType.RightWeapon, DropAnm.WeaponDeathDrop);
        }
    }
    public void DropBodyPart(BodyPartType bodyPartType)
    {
        switch(bodyPartType)
        {
            case BodyPartType.LeftArm:
                break;
            case BodyPartType.RightArm:
                break;
            case BodyPartType.LeftLeg:
                break;
            case BodyPartType.RightLeg:
                break;
        }
    }
}

public enum DropPartType
{
    LeftWeapon,
    RightWeapon,
    LeftArm,
    RightArm,
    LeftLeg,
    RightLeg
}
public enum DropAnm
{
    WeaponDeathDrop
}