using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDisplayInfo
{
    public EquipmentType equipmentType;
    public WeaponType weaponType;
    public Vector3 LocalPosition;
    public Vector3 LocalRotationEuler;
    public Vector3 LocalScale;
    public Sprite[] CharacterDisplaySprites;
    public int[] SlotIndex;
}
[System.Serializable]
public class VFXPrefabInfo
{
    public string name;
    public GameObject prefab;
}
public class ArtResourceManager : MonoBehaviour
{
    public static ArtResourceManager instance;
    private void Awake()
    {
        InGameManager.instance.BattleStartEvent += ShowBattleScene;

        instance = this;
        foreach(ItemDisplayInfo info in itemDisplayInfos)
        {
            if (info.equipmentType == EquipmentType.LeftWeapon)
            {
                WeaponItemInfoDict.Add(info.weaponType, info);
            }
            else
            {
                ArmorItemInfoDict.Add(info.equipmentType, info);
            }
        }
        foreach(VFXPrefabInfo info in VFXInfos)
        {
            VFXDict.Add(info.name, info.prefab);
        }
    }
    public void UnShowBattleScene()
    {
        BattleSceneInstance.SetActive(false);
    }
    void ShowBattleScene()
    {
        BattleSceneInstance.SetActive(true);
    }
    [SerializeField]
    private Sprite[] EquipmentIcons;
    [SerializeField]
    private Sprite[] ItemBackgrounds;
    [SerializeField]
    private Sprite[] CardFrames;
    [SerializeField]
    private Sprite[] ItemValueBackground;
    [SerializeField]
    private Sprite[] BuffIcon;
    [SerializeField]
    private Sprite[] BuffEffectImage;
    [SerializeField]
    private GameObject[] BattleScenePrefabs;
    public GameObject BattleSceneInstance;
    [SerializeField]
    private Sprite[] CardBackgrounds;
    [SerializeField]
    private Sprite[] CardBodypartIcons;
    [SerializeField]
    private Sprite[] AbilityBodypartIcons;

    public Sprite GetAbilityBodypartIcon(ComboBodyPart comboBodyPart)
    {
        return AbilityBodypartIcons[(int)comboBodyPart];
    }
    public Sprite GetCardBackgroundSprite(CardType type)
    {
        int i = (int)type;
        return CardBackgrounds[i];
    }

    public Sprite GetEquipmentIcon(EquipmentType type)
    {
        return EquipmentIcons[(int)type];
    }
    public Sprite GetItemBackground(RareLevel level)
    {
        return ItemBackgrounds[(int)level];
    }

    public Sprite GetCardBackground(RareLevel level)
    {
        return CardFrames[(int)level];
    }
    public Sprite GetBuffIcon(BuffType buffType)
    {
        return BuffIcon[(int)buffType];
    }

    public Sprite GetBuffEffectImage(BuffType buffType)
    {
        return BuffEffectImage[(int)buffType];
    }
    public Sprite GetItemValueBackground(bool isAttack)
    {
        if (isAttack)
        {
            return ItemValueBackground[1];
        }
        else
        {
            return ItemValueBackground[0];
        }
    }
    public Sprite GetCardBodypartIcon(int index)
    {
        return CardBodypartIcons[index];
    }
    [SerializeField]
    private ItemDisplayInfo[] itemDisplayInfos;
    [SerializeField]
    private Dictionary<EquipmentType, ItemDisplayInfo> ArmorItemInfoDict=new Dictionary<EquipmentType, ItemDisplayInfo>();
    private Dictionary<WeaponType, ItemDisplayInfo> WeaponItemInfoDict = new Dictionary<WeaponType, ItemDisplayInfo>();
    public void UpdateValue(Equipment equipment, ItemInstance itemInstance, float Ratio)
    {
        EM_Weapon weapon = equipment as EM_Weapon;
        ItemDisplayInfo info;


        itemInstance.ValueText.color = new Color(214f / 255f, 47f / 255f, 47f / 255f);

        if (weapon != null)
        {
            itemInstance.ValueText.text = weapon.WeaponDamage + "";
            info = WeaponItemInfoDict[weapon.weaponType];
        }
        else
        {
            itemInstance.ValueText.text = equipment.CurrentArmor + "";
            info = ArmorItemInfoDict[equipment.SlotTypes[0]];
        }

        if (UIManager.instance.InEqmShop == true || UIManager.instance.InInn == true)
        {
        
                itemInstance.ValueText.color = new Color(241f / 255f, 174f / 255f, 76f / 255f);  

                itemInstance.ValueText.text = Mathf.RoundToInt( equipment.Value * Ratio) + "G";
            
        }

    }
    public void SetItemInstanceDisplay(Equipment equipment, ItemInstance itemInstance)
    {
        EM_Weapon weapon = equipment as EM_Weapon;
        EM_Item Item = equipment as EM_Item;
        ItemDisplayInfo info;

        itemInstance.BackgroundImage.sprite = GetItemBackground(equipment.Rare);

        if (weapon != null)
        {
            itemInstance.ValueText.text = weapon.WeaponDamage + "";
            info = WeaponItemInfoDict[weapon.weaponType];
        }
        else
        {
            itemInstance.ValueText.text = equipment.CurrentArmor + "";
            info = ArmorItemInfoDict[equipment.SlotTypes[0]];
        }

        if (UIManager.instance.InEqmShop == true|| UIManager.instance.InInn == true)
        {
            itemInstance.ValueText.text = equipment.Value * 3 + "G";
        }

        for (int i = 0; i < itemInstance.CharacterImages.Length; i++)
        {
            if (info.CharacterDisplaySprites.Length>i && info.CharacterDisplaySprites[i] != null)
            {
                itemInstance.CharacterImages[i].enabled = true;
                itemInstance.CharacterImages[i].sprite = info.CharacterDisplaySprites[i];
            }
            else
            {
                itemInstance.CharacterImages[i].enabled = false;
            }
        }

        for (int i = 0; i < itemInstance.EquipmentImages.Length; i++)
        {
            if (weapon != null)
            {
                if (weapon.weaponSprite.Length > i && weapon.weaponSprite[i] != null)
                {
                    itemInstance.EquipmentImages[i].enabled = true;
                    itemInstance.EquipmentImages[i].sprite = weapon.weaponSprite[i];
                }
                else
                {
                    itemInstance.EquipmentImages[i].enabled = false;
                }
            }
            else if (Item != null)
            {
                if (Item.ItemSprite.Length > i && Item.ItemSprite[i] != null)
                {
                    itemInstance.EquipmentImages[i].enabled = true;
                    itemInstance.EquipmentImages[i].sprite = Item.ItemSprite[i];
                }
                else
                {
                    itemInstance.EquipmentImages[i].enabled = false;
                }
            }
            else
            {
                if (info.SlotIndex.Length > i)
                {
                    itemInstance.EquipmentImages[i].enabled = true;
                    itemInstance.EquipmentImages[i].sprite = equipment.spriteMeshes[info.SlotIndex[i]].sprite;
                }
                else
                {
                    itemInstance.EquipmentImages[i].enabled = false;
                }
            }      
        }
        itemInstance.CharacterParent.localPosition = info.LocalPosition;
        itemInstance.CharacterParent.localRotation = Quaternion.Euler(info.LocalRotationEuler);
        itemInstance.CharacterParent.localScale = info.LocalScale;
        itemInstance.EquipmentParent.localPosition = info.LocalPosition;
        itemInstance.EquipmentParent.localRotation = Quaternion.Euler(info.LocalRotationEuler);
        itemInstance.EquipmentParent.localScale = info.LocalScale;
    }

    [SerializeField]
    private VFXPrefabInfo[] VFXInfos;
    public Dictionary<string, GameObject> VFXDict = new Dictionary<string, GameObject>();
    public GameObject GenerateVFXObject(string name,Vector3 position,Quaternion rotation)
    {
        return Instantiate(VFXDict[name],position, rotation);
    }
    public void GenerateBattleScene(BattleSceneType type)
    {
        if (BattleSceneInstance != null)
        {
            Destroy(BattleSceneInstance);
        }
        int i = (int)type;
        BattleSceneInstance = Instantiate(BattleScenePrefabs[i]);
    }
}

public enum BattleSceneType
{
    荒野,
    树洞,
    碎石
}
public enum RareLevel
{
    Normal,
    Rare,
    Master,
    Epic,
    Legend
}