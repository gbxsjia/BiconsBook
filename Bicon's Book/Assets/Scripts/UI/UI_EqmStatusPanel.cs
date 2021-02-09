
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UI_EqmStatusPanel : MonoBehaviour
{
    public static Equipment m_CurrentEquipment;
    public static Card_Base m_CurrentCard;
    public static UI_EqmStatusPanel m_Instance;

    [SerializeField] GameObject MainPanel;

    [SerializeField] TextMeshProUGUI EqmNameText;
    [SerializeField] TextMeshProUGUI ArmorAmountText;
    [SerializeField] TextMeshProUGUI ArmorStatusNameText;
    [SerializeField] TextMeshProUGUI WeightText;
    [SerializeField] TextMeshProUGUI ValueText;
    [SerializeField] TextMeshProUGUI CardDri;
    public GameObject SelectedOutline;
    [SerializeField] List<GameObject> CardInCardSetList;
    private void Start()
    {
        m_Instance = this;
  
    }
    
    private void Update()
    {
        switch (SlotMode)
        {
            case 0:
                if (CurrentSelectShopSlot != null)
                {
                    SelectedOutline.transform.position = CurrentSelectShopSlot.transform.position;
                }
                break;
            case 1:
                if (CurrentSelectEqmSlot != null)
                {
                    SelectedOutline.transform.position = CurrentSelectEqmSlot.transform.position;
                }
                break;
            case 2:
                if (CurrentSelectInventorySlot != null)
                {
                    SelectedOutline.transform.position = CurrentSelectInventorySlot.transform.position;
                }
                break;
        }
    }

    int SlotMode = 2;
    UI_ShopSlot CurrentSelectShopSlot;
    UI_EquipmentSlot CurrentSelectEqmSlot;
    UI_InventorySlot CurrentSelectInventorySlot;
    public void SetSelectOutlinePos(UI_ShopSlot aSlot)
    {
        SelectedOutline.GetComponent<RectTransform>().sizeDelta = aSlot.GetComponent<RectTransform>().sizeDelta;
        SlotMode = 0;
        CurrentSelectShopSlot = aSlot;
        SelectedOutline.transform.SetParent(aSlot.transform);
    }
    public void SetSelectOutlinePos(UI_EquipmentSlot aSlot)
    {
        SelectedOutline.GetComponent<RectTransform>().sizeDelta = aSlot.GetComponent<RectTransform>().sizeDelta;
        SlotMode = 1;
        CurrentSelectEqmSlot = aSlot;
        SelectedOutline.transform.SetParent(aSlot.transform);
    }
    public void SetSelectOutlinePos(UI_InventorySlot aSlot)
    {
        SelectedOutline.GetComponent<RectTransform>().sizeDelta = aSlot.GetComponent<RectTransform>().sizeDelta;
        SlotMode = 2;
        CurrentSelectInventorySlot = aSlot;
        SelectedOutline.transform.SetParent(aSlot.transform.parent.parent);
    }
    public void ShowPanel(Vector3 aPos)
    {

         //   AutoPosition(aPos);
            UpdateUIInformation();
         //   MainPanel.SetActive(true);
               
    }
    public void UnShowPanel()
    {

         //   MainPanel.SetActive(false);
        
    }
    public void AutoPosition(Vector3 aPos)
    {
        if (aPos.y > 512)
        {
            gameObject.transform.position = new Vector3(aPos.x + 50,aPos.y - 50,aPos.z);
            gameObject.transform.GetChild(0).GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        }
        else
        {
            gameObject.transform.position = new Vector3(aPos.x + 50, aPos.y - 50, aPos.z);
            gameObject.transform.GetChild(0).GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        }
    }

    [SerializeField] GameObject[] CardDisplay;
    [SerializeField] CardInfoDisplay CardInfoDisplay;
    [SerializeField] List<TextMeshProUGUI> CardInfoNameTextList;
    [SerializeField] List<TextMeshProUGUI> CardInfoTextList;
    public GameObject CardInCardSet_Prefab;
    public Transform CardSetT;
    public void AddEquipmentCard(Equipment c)
    {
        for (int i = 0; i < c.EquipmentCards.Length; i++)
        {
            if (CardInCardSetList.Count == 0)
            {
                GameObject aNewApr = Instantiate(CardInCardSet_Prefab);
                aNewApr.GetComponent<CardAprInCardSet>().card = c.EquipmentCards[i].GetComponent<Card_Base>();
                aNewApr.GetComponent<CardAprInCardSet>().UpdateUI();
                aNewApr.transform.SetParent(CardSetT);
                aNewApr.transform.localScale = Vector3.one;
                CardInCardSetList.Add(aNewApr);
            }
            else
            {
                bool FindSame = false;
                for (int x = 0; x < CardInCardSetList.Count; x++)
                {
                    CardAprInCardSet aCardApr = CardInCardSetList[x].GetComponent<CardAprInCardSet>();
                    if (aCardApr.card.name == c.EquipmentCards[i].name)
                    {
                        aCardApr.m_Amount += 1;
                        aCardApr.UpdateUI();
                        FindSame = true;
                        break;
                    }
                }
                if (FindSame == false)
                {
                    GameObject aNewApr = Instantiate(CardInCardSet_Prefab);
                    aNewApr.GetComponent<CardAprInCardSet>().card = c.EquipmentCards[i].GetComponent<Card_Base>();
                    aNewApr.GetComponent<CardAprInCardSet>().UpdateUI();
                    aNewApr.transform.SetParent(CardSetT);
                    aNewApr.transform.localScale = Vector3.one;
                    CardInCardSetList.Add(aNewApr);
                }
            }

        }
    }

    public void RemoveEquipmentCard(Equipment c)
    {
        for (int x = 0; x < c.EquipmentCards.Length; x++)
        {
            for (int i = CardInCardSetList.Count - 1; i >= 0; i--)
            {
                CardAprInCardSet aCardApr = CardInCardSetList[i].GetComponent<CardAprInCardSet>();
                if (aCardApr.card.name == c.EquipmentCards[x].name)
                {
                    aCardApr.m_Amount -= 1;
                    aCardApr.UpdateUI();
                    if (aCardApr.m_Amount <= 0)
                    {
                        Destroy(CardInCardSetList[i]);
                        CardInCardSetList.RemoveAt(i);
                    }
                }
            }
        }
    }
    public void UpdateUIInformation()
    {
        EqmNameText.text = m_CurrentEquipment.EqmName;
        EM_Weapon aWeapon = m_CurrentEquipment as EM_Weapon;
        EM_Item aItem = m_CurrentEquipment as EM_Item;
        if (aWeapon != null && aWeapon.weaponType != WeaponType.Shield)
        {
            ArmorStatusNameText.text = "攻击";
            ArmorAmountText.text = "" + aWeapon.WeaponDamage;
        }
        else if(aItem != null)
        {
            ArmorStatusNameText.text = "数量";
            ArmorAmountText.text = "" + aItem.ConsumeTimes;
        }
        else
        {
            ArmorStatusNameText.text = "护甲";
            ArmorAmountText.text = m_CurrentEquipment.CurrentArmor + " / " + m_CurrentEquipment.MaxArmor;
        }
       
        WeightText.text = "" + m_CurrentEquipment.Weight;
        ValueText.text = "" + m_CurrentEquipment.Value;

        for(int i = 0; i < 4; i++)
        {
            if (i < m_CurrentEquipment.EquipmentCards.Length)
            {
                CardDisplay[i].SetActive(true);
                CardDisplay[i].transform.parent.GetComponent<CardAprInventory>().card = m_CurrentEquipment.EquipmentCards[i].GetComponent<Card_Base>();
                CardDisplay[i].transform.parent.GetComponent<CardAprInventory>().UpdateUI();
            }
            else
            {
                CardDisplay[i].SetActive(false);
            }
        }
    }

    public void ChooseCurrentCard(int i)
    {
        if (m_CurrentEquipment != null && m_CurrentEquipment.EquipmentCards.Length > 0)
        {
            m_CurrentCard = m_CurrentEquipment.EquipmentCards[i].GetComponent<Card_Base>();
        }

    }
    public void UpdateUICardInfo()
    {
        CardInfoDisplay.card = m_CurrentCard;
        CardInfoDisplay.UpdateCardInfo();
        CardDri.text = m_CurrentCard.CardDescription;

        for(int i = 0; i < CardInfoTextList.Count;i++)
        {
            if (CardInfoDisplay.EntryInfoNameList.Count > i)
            {
                CardInfoNameTextList[i].transform.parent.gameObject.SetActive(true);
                CardInfoNameTextList[i].text = CardInfoDisplay.EntryInfoNameList[i];
                CardInfoTextList[i].text = CardInfoDisplay.EntryInfoList[i];
            }
            else
            {
                CardInfoNameTextList[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
