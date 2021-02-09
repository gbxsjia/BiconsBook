using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Equipment : ScriptableObject
{
    public string EqmName;

    public List<EquipmentType> SlotTypes;
    public GameObject[] EquipmentCards;
    public GameObject BounsCard;
    
    public SpriteMesh[] spriteMeshes;
    public bool isDefaultEquipment=false;

    public int MaxArmor;
    public int CurrentArmor;
    public float Weight;
    public int ExtraHealth;

    public int Value;

    public RareLevel Rare;

    [HideInInspector]
    public BodyPart AttachedBodyPart;

    public int AttachedSlotIndex;

    public int UpdateCheckID;

}
public enum EquipmentType
{
    LeftWeapon,
    RightWeapon,
    LeftArm,
    RightArm,
    LeftLeg,
    RightLeg,
    Armor,
    Helmet,
    LeftHand,
    RightHand,
    LeftPocket,
    RightPocket,
    LeftFoot,
    RightFoot,
    Tail,
    Ear
}