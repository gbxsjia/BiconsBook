using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInstance : MonoBehaviour
{
    public Image BackgroundImage;
    public Transform CharacterParent;
    public Transform EquipmentParent;
    public Image[] CharacterImages;
    public Image[] EquipmentImages;
    [SerializeField]
    private Image ValueBGImage;

    public Image ArmorValueImage;
    public TextMeshProUGUI UnTradedText;
    public TextMeshProUGUI ValueText;

    public Equipment equipment;

    public bool IsTrading = false;

    public bool isDragging=false;
    public void UpdateItem(Equipment e)
    {
        equipment = e;
        ArtResourceManager.instance.SetItemInstanceDisplay(e, this);
    }

    private void Update()
    {
        if (UnTradedText != null)
        {
            if (IsTrading)
            {
                UnTradedText.gameObject.SetActive(true);
            }
            else
            {
                UnTradedText.gameObject.SetActive(false);
            }
        }
    }
    private void Start()
    {
        InGameManager.instance.ReturnToCampEvent += UIUpdateArmorAmount;
        CharacterManager.PlayerInstance.RepairAllEvent += UIUpdateArmorAmount;
    }
    public void UIUpdateArmorAmount()
    {
        if (ArmorValueImage != null)
        {
            ArmorValueImage.fillAmount = (float)equipment.CurrentArmor / equipment.MaxArmor;
        }
        
    }
    public void UpdateItemValue(float ratio)
    {
      
        ArtResourceManager.instance.UpdateValue(equipment, this,ratio);
    }
}
public enum ItemType
{
    Weapon,
    Arm,
    Leg,
    Armor,
    Helmet,
    Hand,
    Item,
    Foot,
    Tail,
    Ear
}
