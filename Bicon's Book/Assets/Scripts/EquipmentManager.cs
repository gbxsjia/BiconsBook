using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class EquipmentManager : MonoBehaviour
{
    private CharacterManager characterManager;
    public int camp;
    [SerializeField]
    private Equipment[] StartEquipments;
    private Card_Base WeaponComboCard;
    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        camp = characterManager.camp;
    }

    public static ItemType ConvertEquipmentToItemType(Equipment e)
    {
        switch (e.SlotTypes[0])
        {
            case EquipmentType.Armor:
                return ItemType.Armor;
            case EquipmentType.Ear:
                return ItemType.Ear;
            case EquipmentType.Helmet:
                return ItemType.Helmet;
            case EquipmentType.LeftArm:
                return ItemType.Arm;
            case EquipmentType.LeftFoot:
                return ItemType.Foot;
            case EquipmentType.LeftHand:
                return ItemType.Hand;
            case EquipmentType.LeftLeg:
                return ItemType.Leg;
            case EquipmentType.LeftPocket:
                return ItemType.Item;
            case EquipmentType.LeftWeapon:
                return ItemType.Weapon;
            case EquipmentType.RightArm:
                return ItemType.Arm;
            case EquipmentType.RightFoot:
                return ItemType.Foot;
            case EquipmentType.RightHand:
                return ItemType.Hand;
            case EquipmentType.RightLeg:
                return ItemType.Leg;
            case EquipmentType.RightPocket:
                return ItemType.Item;
            case EquipmentType.RightWeapon:
                return ItemType.Weapon;
            default: return ItemType.Armor;
        }
    }
    private void Start()
    {

        InGameManager.instance.EquipmentManagers[camp] = this;

        for (int i = 0; i < 14; i++)
        {
            if (StartEquipments[i] != null)
            {
                Equip(Instantiate(StartEquipments[i]), (EquipmentType)i);
            }
            else
            {
                Equip(null, (EquipmentType)i);
            }
        }
    }
    [SerializeField]
    private Equipment[] equipments = new Equipment[15];

    public Equipment GetEquipment(EquipmentType slot)
    {
        int equipIndex = (int)slot;
        if (equipments[equipIndex] != null)
        {
            if (equipments[equipIndex].isDefaultEquipment)
            {
                return null;
            }
            else
            {
                return equipments[equipIndex];
            }
        }
        else
        {
            return null;          
        }
    }

    public void RecoverAllArmor(int amount, float percentage)
    { 
        print("StartRepair");
        foreach (Equipment a in equipments)
        {
            if (a != null)
            {
                print("Repairing");
                a.CurrentArmor += amount + Mathf.RoundToInt(a.MaxArmor * percentage);
                if (a.CurrentArmor > a.MaxArmor)
                {
                    a.CurrentArmor = a.MaxArmor;                    
                }
            }
        }

        foreach(BodyPart a in characterManager.bodyParts)
        {
            a.UpdateAllPartsArmor();
        }

       
    }
    public EM_Weapon GetWeapon(AnimDirection direction)
    {
        switch (direction)
        {
            case AnimDirection.Left:
                return GetWeapon(true);
            case AnimDirection.Right:
                return GetWeapon(false);
            default: return GetWeapon(true);
        }
    }
    // two hand weapon
    public HoldingWeaponType GetHoldingWeaponType()
    {
        bool hasLeftHandWeapon = equipments[0] != null && !equipments[0].isDefaultEquipment;
        bool hasRightHandWeapon = equipments[1] != null && !equipments[1].isDefaultEquipment;
        if (hasLeftHandWeapon && hasRightHandWeapon)
        {
            return HoldingWeaponType.Dual;
        }
        else if(hasLeftHandWeapon|| hasRightHandWeapon)
        {
            return HoldingWeaponType.Single;
        }
        else
        {
            return HoldingWeaponType.BareHand;
        }
    }
    public EM_Weapon GetWeapon(bool isLeft)
    {
        if (isLeft)
        {
            return equipments[0] as EM_Weapon; 
        }
        else
        {
            return equipments[1] as EM_Weapon;
        }
    }

    private List<GameObject> Deck = new List<GameObject>();
    public List<GameObject> GetDeck()
    {
        return Deck;
    }

    public List<GameObject> GetCopyOfDeck()
    {
        List<GameObject> deck = new List<GameObject>();
        foreach(GameObject card in Deck)
        {
            deck.Add(Instantiate(card));
        }
        return deck;
    }
    public void ChangeDeck(Equipment oldItem, Equipment newItem)
    {
        if (oldItem != null)
        {
            for (int i = Deck.Count - 1; i >= 0; i--)
            {
                if (Deck[i].GetComponent<Card_Base>().ownerEquipment == oldItem)
                {
                    Destroy(Deck[i].gameObject);
                    Deck.RemoveAt(i);
                }
            }
        }

        if (newItem != null)
        {
            for (int j = 0; j < newItem.EquipmentCards.Length; j++)
            {
                GameObject g = Instantiate(newItem.EquipmentCards[j]);
                Card_Base card = g.GetComponent<Card_Base>();
                card.ownerEquipment = newItem;
                Deck.Add(g);
            }
        }
        if(newItem is EM_Weapon || oldItem is EM_Weapon)
        {
            HoldingWeaponType hwt = GetHoldingWeaponType();
            if(hwt== HoldingWeaponType.Single)
            {
                EM_Weapon weapon;
                weapon = GetWeapon(AnimDirection.Left);
                if (weapon == null)
                {
                    weapon = GetWeapon(AnimDirection.Right);
                }

                if (weapon.BounsCard != null)
                {
                    GameObject g = Instantiate(weapon.BounsCard);
                    Card_Base card = g.GetComponent<Card_Base>();
                    card.ownerEquipment = weapon;
                    WeaponComboCard = card;
                    Deck.Add(g);
                }     
            }
            else if(WeaponComboCard!=null)
            {
                Deck.Remove(WeaponComboCard.gameObject);
            }
        } 
    }

    private void EquipmentRenderSlotChange(CharacterSpriteMeshType type, Equipment item)
    {
        if (item != null)
        {
            foreach (SpriteMesh spriteMesh in item.spriteMeshes)
            {
                if (spriteMesh.name == type.ToString())
                {
                    characterManager.GetEquipmentRenderer(type).spriteMesh = spriteMesh;
                    break;
                }
            }
        }
        else
        {
            characterManager.GetEquipmentRenderer(type).spriteMesh = null;
        }
    }

    // two hand weapon
    public bool Equip(Equipment item, EquipmentType slot)
    {

        if (item!=null && !item.SlotTypes.Contains(slot))
        {
            return false;
        }
        
        int equipIndex = (int)slot;

        Equipment oldItem = equipments[equipIndex];
        if (item != null)
        {
            item.AttachedSlotIndex = equipIndex;
        }

        switch (slot)
        {
            case EquipmentType.Armor:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.ass_back, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.ass_front, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.ass_front_r, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.back, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.body, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.body_r, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hujing_for, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hujing_for_l, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hujing_mid, item);

                characterManager.GetBodyPart(BodyPartType.Chest).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.Chest).EquipItem(item);
                break;
            case EquipmentType.Helmet:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hair_back, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hair_mid, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hair_top, item);

                characterManager.GetBodyPart(BodyPartType.Head).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.Head).EquipItem(item);

                EM_Helmet helmet = item as EM_Helmet;
                if (helmet != null)
                {
                    characterManager.SetHairVisibility(!helmet.HideHair);
                }
                else
                {
                    characterManager.SetHairVisibility(true);
                }
            
                break;
            case EquipmentType.LeftArm:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.arm_for_l, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.arm_mid_l, item);

                characterManager.GetBodyPart(BodyPartType.LeftArm).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.LeftArm).EquipItem(item);
                break;
            case EquipmentType.LeftFoot:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.feet_front_l, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.feet_side_l, item);

                characterManager.GetBodyPart(BodyPartType.LeftLeg).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.LeftLeg).EquipItem(item);
                break;
            case EquipmentType.LeftHand:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hand_back_l, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hand_front_l, item);

                characterManager.GetBodyPart(BodyPartType.LeftArm).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.LeftArm).EquipItem(item);
                break;
            case EquipmentType.LeftLeg:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.leg_down_l, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.leg_up_l, item);

                characterManager.GetBodyPart(BodyPartType.LeftLeg).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.LeftLeg).EquipItem(item);
                break;
            case EquipmentType.LeftPocket:

                characterManager.GetBodyPart(BodyPartType.Chest).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.Chest).EquipItem(item);
                break;
            case EquipmentType.LeftWeapon:
                EM_Weapon weaponL = item as EM_Weapon;
                if (weaponL != null)
                {
                    switch (weaponL.weaponType)
                    {
                        case WeaponType.Shield:
                            characterManager.GetWeaponRenderer(false, 1)[0].sprite = weaponL.weaponSprite[0];
                            characterManager.GetWeaponRenderer(false, 1)[1].sprite = weaponL.weaponSprite[1];
                            characterManager.GetWeaponRenderer(false, 1)[2].sprite = weaponL.weaponSprite[2];
                            characterManager.GetWeaponParent(false, 0).SetActive(false);
                            characterManager.GetWeaponParent(false, 1).SetActive(true);
                            characterManager.GetWeaponParent(false, 2).SetActive(false);
                            break;
                        default:
                            characterManager.GetWeaponRenderer(false, 0)[0].sprite = weaponL.weaponSprite[0];
                            characterManager.GetWeaponRenderer(false, 0)[1].sprite = weaponL.weaponSprite[1];
                            characterManager.GetWeaponRenderer(false, 0)[2].sprite = weaponL.weaponSprite[2];
                            characterManager.GetWeaponParent(false, 0).SetActive(true);
                            characterManager.GetWeaponParent(false, 1).SetActive(false);
                            characterManager.GetWeaponParent(false, 2).SetActive(false);
                            break;
                    }
                }
                else
                {
                    characterManager.GetWeaponRenderer(false, 0)[0].sprite = null;
                    characterManager.GetWeaponRenderer(false, 0)[1].sprite = null;
                    characterManager.GetWeaponRenderer(false, 0)[2].sprite = null;
                    characterManager.GetWeaponRenderer(false, 1)[0].sprite = null;
                    characterManager.GetWeaponRenderer(false, 1)[1].sprite = null;
                    characterManager.GetWeaponRenderer(false, 1)[2].sprite = null;
                    characterManager.GetWeaponParent(false, 0).SetActive(false);
                    characterManager.GetWeaponParent(false, 1).SetActive(false);
                    characterManager.GetWeaponParent(false, 2).SetActive(false);
                    item = Instantiate(DefaultCardManager.instance.DefaultWeapon);
                }
                characterManager.GetBodyPart(BodyPartType.LeftArm).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.LeftArm).EquipItem(item);
                break;
            case EquipmentType.RightLeg:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.leg_down_r, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.leg_up_r, item);

                characterManager.GetBodyPart(BodyPartType.RightLeg).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.RightLeg).EquipItem(item);
                break;
            case EquipmentType.RightArm:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.arm_for_r, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.arm_mid_r, item);

                characterManager.GetBodyPart(BodyPartType.RightArm).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.RightArm).EquipItem(item);
                break;
            case EquipmentType.RightFoot:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.feet_front_r, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.feet_side_r, item);

                characterManager.GetBodyPart(BodyPartType.RightLeg).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.RightLeg).EquipItem(item);
                break;
            case EquipmentType.RightHand:
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hand_back_r, item);
                EquipmentRenderSlotChange(CharacterSpriteMeshType.hand_front_r, item);

                characterManager.GetBodyPart(BodyPartType.RightArm).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.RightArm).EquipItem(item);
                break;
            case EquipmentType.RightPocket:

                characterManager.GetBodyPart(BodyPartType.Chest).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.Chest).EquipItem(item);
                break;
            case EquipmentType.RightWeapon:
                EM_Weapon weaponR = item as EM_Weapon;
                if (weaponR != null)
                {
                    switch (weaponR.weaponType)
                    {
                        case WeaponType.Shield:
                            characterManager.GetWeaponRenderer(true, 1)[0].sprite = weaponR.weaponSprite[0];
                            characterManager.GetWeaponRenderer(true, 1)[1].sprite = weaponR.weaponSprite[1];
                            characterManager.GetWeaponRenderer(true, 1)[2].sprite = weaponR.weaponSprite[2];
                            characterManager.GetWeaponParent(true, 0).SetActive(false);
                            characterManager.GetWeaponParent(true, 1).SetActive(true);
                            characterManager.GetWeaponParent(true, 2).SetActive(false);
                            break;
                        default:
                            characterManager.GetWeaponRenderer(true, 0)[0].sprite = weaponR.weaponSprite[0];
                            characterManager.GetWeaponRenderer(true, 0)[1].sprite = weaponR.weaponSprite[1];
                            characterManager.GetWeaponRenderer(true, 0)[2].sprite = weaponR.weaponSprite[2];
                            characterManager.GetWeaponParent(true, 0).SetActive(true);
                            characterManager.GetWeaponParent(true, 1).SetActive(false);
                            characterManager.GetWeaponParent(true, 2).SetActive(false);
                            break;
                    }
                }
                else
                {
                    characterManager.GetWeaponRenderer(true, 0)[0].sprite = null;
                    characterManager.GetWeaponRenderer(true, 0)[1].sprite = null;
                    characterManager.GetWeaponRenderer(true, 0)[2].sprite = null;
                    characterManager.GetWeaponRenderer(true, 1)[0].sprite = null;
                    characterManager.GetWeaponRenderer(true, 1)[1].sprite = null;
                    characterManager.GetWeaponRenderer(true, 1)[2].sprite = null;
                    characterManager.GetWeaponParent(true, 0).SetActive(false);
                    characterManager.GetWeaponParent(true, 1).SetActive(false);
                    characterManager.GetWeaponParent(true, 2).SetActive(false);
                    item = Instantiate(DefaultCardManager.instance.DefaultWeapon);
                }
                // two hand weapon
                characterManager.GetBodyPart(BodyPartType.RightArm).UnEquipItem(oldItem);
                characterManager.GetBodyPart(BodyPartType.RightArm).EquipItem(item);
                break;
            case EquipmentType.Tail:
                break;
            case EquipmentType.Ear:
                break;
        }

        equipments[equipIndex] = item;

        OnEquipmentChange(oldItem, item, slot);
        return true;
    }

    public bool TryEquipItem(Equipment item)
    {
        if (item != null)
        {
            for (int i = 0; i < item.SlotTypes.Count; i++)
            {
                if (GetEquipment(item.SlotTypes[i]) == null)
                {
                    Equip(item, item.SlotTypes[i]);
                }
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    public event System.Action<Equipment,Equipment, EquipmentType> EquipmentChangeEvent;
    public void OnEquipmentChange(Equipment oldItem,Equipment newItem,EquipmentType slot)
    {
        ChangeDeck(oldItem, newItem);
        if (EquipmentChangeEvent != null)
        {
            EquipmentChangeEvent(oldItem, newItem, slot);        
        }
        UIManager.instance.AllArmorHealthAmountUpdate();

    }
}

public enum HoldingWeaponType
{
    BareHand,
    Single,
    Dual,
    Twohand
}
