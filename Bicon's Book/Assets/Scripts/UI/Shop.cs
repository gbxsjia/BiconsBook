using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    [SerializeField]
    private GameObject ItemInstancePrefab;

    [SerializeField]
    List<Equipment> InnItemPrefabList;

    [SerializeField] int RepairAllInitialPrice;
    [SerializeField] int HealAllInitialPrice;
    [SerializeField] int QuickHealInitialPrice;

    public static int RepairAllPrice;
    public static int HealAllPrice;
    public static int QuickHealPrice;
    public static int TrainingPrice=50;

    [SerializeField] TextMeshProUGUI RepairAllPriceText;
    [SerializeField] TextMeshProUGUI HealAllPriceText;
    [SerializeField] TextMeshProUGUI QuickHealPriceText;
    [SerializeField] TextMeshProUGUI TrainingPriceText;

    [SerializeField] Button m_TrainingButton;


    public void RefreshTraining()
    {
        m_TrainingButton.interactable = true;
    }

    public void Trained()
    {
        m_TrainingButton.interactable = false;
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EnemyIncubator.thisInstance.m_AfterFlyEvent += RefreshTraining;
        Invoke("RefreshItem", 1f);
    }

    public void GenerateInnItem(int CurrentPower, int Amount)
    {
        List<Equipment> AvaliablePotionList = new List<Equipment>();
        foreach (Equipment a in InnItemPrefabList)
        {
            if (CurrentPower + 20>= a.Value) 
            {
                AvaliablePotionList.Add(a);
            }
        }

        for(int i = 0; i<Amount;i++)
        {
            GenerateItem(AvaliablePotionList[Random.Range(0, AvaliablePotionList.Count)], UIManager.TownPlace.Inn);
        }
    }
    public List<Equipment> ShopInventoryItems = new List<Equipment>();
    public List<Equipment> InnInventoryItems = new List<Equipment>();

    public void RefreshItem()
    { 
        RepairAllPrice = Mathf.RoundToInt(RepairAllInitialPrice * (1 + EnemyIncubator.CurrentMapID / 12f));
        RepairAllPriceText.text = RepairAllPrice + "G";
        HealAllPrice = Mathf.RoundToInt(HealAllInitialPrice * (1 + EnemyIncubator.CurrentMapID / 12f));
        HealAllPriceText.text = HealAllPrice + "G";
        QuickHealPrice = Mathf.RoundToInt(QuickHealInitialPrice * (1 + EnemyIncubator.CurrentMapID / 12f));
        QuickHealPriceText.text = QuickHealPrice + "G";

        foreach (UI_ShopSlot a in UIManager.instance.EqmShopSlots)
        {
            if (a.thisItem != null)
            {
                
                Destroy(a.thisItem.equipment);
                Destroy(a.thisItem.gameObject);
                
            }a.thisItem = null;
        }

        foreach (UI_ShopSlot a in UIManager.instance.InnShopSlots)
        {
            if (a.thisItem != null)
            {

                Destroy(a.thisItem.equipment);
                Destroy(a.thisItem.gameObject);

            }
            a.thisItem = null;
        }

        ShopInventoryItems.Clear();
        InnInventoryItems.Clear();

        int ItemAmount = Random.Range(4,10);
        for(int i = 0;i< ItemAmount; i++)
        {
            if (i == 0)
            {
                GenerateItem(EnemyIncubator.thisInstance.GetRandomCurrentEquipment(10),UIManager.TownPlace.BlackSmith);
            }
            else
            {
                GenerateItem(EnemyIncubator.thisInstance.GetRandomCurrentEquipment(), UIManager.TownPlace.BlackSmith);
            }
        }
        ItemAmount = Random.Range(2, 6);
        GenerateInnItem(EnemyIncubator.thisInstance.CurrentEnemyPowerMin,ItemAmount);
    }

    public int FindFirstEmptySlot(UIManager.TownPlace townPlace)
    {
        switch (townPlace)
        {
            case (UIManager.TownPlace.BlackSmith):
                for (int i = 0; i < UIManager.instance.EqmShopSlots.Length; i++)
                {
                    if (UIManager.instance.EqmShopSlots[i].thisItem == null)
                    {
                        return i;
                    }
                }
                break;
            case (UIManager.TownPlace.Inn):
                for (int i = 0; i < UIManager.instance.InnShopSlots.Length; i++)
                {
                    if (UIManager.instance.InnShopSlots[i].thisItem == null)
                    {
                        return i;
                    }
                }
                break;
        }    
        return -1;
    }

    public void GenerateItem(Equipment item, UIManager.TownPlace shopType)
    {
        Equipment newItem = Instantiate(item);int slotIndex = -1;  GameObject g = null; ItemInstance itemInstance = null;
        switch (shopType)
        {
            case (UIManager.TownPlace.BlackSmith):
                ShopInventoryItems.Add(newItem); slotIndex = FindFirstEmptySlot(UIManager.TownPlace.BlackSmith);
                g = Instantiate(ItemInstancePrefab, UIManager.instance.EqmShopSlots[slotIndex].transform);
                g.transform.localPosition = Vector3.zero;
                g.transform.localScale = Vector3.one * 0.8f;
                itemInstance = g.GetComponent<ItemInstance>();
                itemInstance.UpdateItem(newItem);
                itemInstance.equipment.AttachedSlotIndex = slotIndex;
                UIManager.instance.EqmShopSlots[slotIndex].thisItem = itemInstance;

                break;
            case (UIManager.TownPlace.Inn):
                InnInventoryItems.Add(newItem); slotIndex = FindFirstEmptySlot(UIManager.TownPlace.Inn);
                g = Instantiate(ItemInstancePrefab, UIManager.instance.InnShopSlots[slotIndex].transform);
                g.transform.localPosition = Vector3.zero;
                g.transform.localScale = Vector3.one * 0.8f;
                itemInstance = g.GetComponent<ItemInstance>();
                itemInstance.UpdateItem(newItem);
                itemInstance.equipment.AttachedSlotIndex = slotIndex;
                UIManager.instance.InnShopSlots[slotIndex].thisItem = itemInstance;
                break;
        }           
    }

    public void AddItem(ItemInstance item, UIManager.TownPlace shopType)
    {
        int slotIndex = -1;
        switch (shopType)
        {
            case (UIManager.TownPlace.BlackSmith):
                slotIndex = FindFirstEmptySlot(UIManager.TownPlace.BlackSmith);
                item.equipment.AttachedSlotIndex = slotIndex;
                ShopInventoryItems.Add(item.equipment);
                UIManager.instance.EqmShopSlots[slotIndex].thisItem = item;
                item.transform.SetParent(UIManager.instance.EqmShopSlots[slotIndex].transform);
                item.transform.localPosition = Vector3.zero;
                item.transform.localScale = Vector3.one * 0.8f;
                break;
            case (UIManager.TownPlace.Inn):
                slotIndex = FindFirstEmptySlot(UIManager.TownPlace.Inn);
                item.equipment.AttachedSlotIndex = slotIndex;
                InnInventoryItems.Add(item.equipment);
                UIManager.instance.InnShopSlots[slotIndex].thisItem = item;
                item.transform.SetParent(UIManager.instance.InnShopSlots[slotIndex].transform);
                item.transform.localPosition = Vector3.zero;
                item.transform.localScale = Vector3.one * 0.8f;
                break;
        }
    }

    public void RemoveItem(Equipment item)
    {
        if (UIManager.instance.InInn == true)
        {
            InnInventoryItems.Remove(item);
        }
        else if (UIManager.instance.InEqmShop == true)
        {
            ShopInventoryItems.Remove(item);
        }
    }

    public List<ItemInstance> AllTradingItem = new List<ItemInstance>();
    public void CancelTrade()
    {
        if (AllTradingItem.Count > 0)
        {
            if (UIManager.instance.InEqmShop)
            {
                for(int i = Inventory.instance.InventoryItems.Count; i>0 ;i-- )
                {
                    ItemInstance aItem = UIManager.instance.InventorySlots[Inventory.instance.InventoryItems[i-1].AttachedSlotIndex].thisItem;
                    if (aItem.IsTrading == true)
                    {
                        if (FindFirstEmptySlot(UIManager.TownPlace.BlackSmith) != -1)
                        {
                            aItem.IsTrading = false;
                            UIManager.instance.InventorySlots[Inventory.instance.InventoryItems[i - 1].AttachedSlotIndex].thisItem = null;
                            AddItem(aItem, UIManager.TownPlace.BlackSmith);
                            
                            Inventory.instance.RemoveItem(aItem.equipment);


                        }
                    }
                }

                for(int i = ShopInventoryItems.Count-1; i>=0;i--)
                {
                    ItemInstance aItem = UIManager.instance.EqmShopSlots[ShopInventoryItems[i].AttachedSlotIndex].thisItem;
                    if (aItem.IsTrading == true)
                    { aItem.IsTrading = false;  
                        UIManager.instance.EqmShopSlots[ShopInventoryItems[i].AttachedSlotIndex].thisItem = null;
                        RemoveItem(aItem.equipment);                          
                        Inventory.instance.AddItem(aItem);
                    
                       
                    }
                }
            }

            if (UIManager.instance.InInn)
            {
                for (int i = Inventory.instance.InventoryItems.Count; i >0 ; i--)
                {
                    ItemInstance aItem = UIManager.instance.InventorySlots[Inventory.instance.InventoryItems[i-1].AttachedSlotIndex].thisItem;
                    if (aItem.IsTrading == true)
                    {
                        if (FindFirstEmptySlot(UIManager.TownPlace.Inn) != -1)
                        {
                            aItem.IsTrading = false;
                            UIManager.instance.InventorySlots[Inventory.instance.InventoryItems[i-1].AttachedSlotIndex].thisItem = null;

                            AddItem(aItem, UIManager.TownPlace.Inn);
                            Inventory.instance.RemoveItem(aItem.equipment);
                           
                        }
                    }
                }

                for (int i = InnInventoryItems.Count ; i >0 ; i--)
                {
                    ItemInstance aItem = UIManager.instance.InnShopSlots[InnInventoryItems[i-1].AttachedSlotIndex].thisItem;
                    if (aItem.IsTrading == true)
                    {
                        aItem.IsTrading = false;
                        UIManager.instance.InnShopSlots[InnInventoryItems[i-1].AttachedSlotIndex].thisItem = null;
                        RemoveItem(aItem.equipment);
                        Inventory.instance.AddItem(aItem);
                        
                    }
                }
            }
        }
    }

    public int TradingPrice()
    {
       int TradePrice = 0;
      if(AllTradingItem.Count == 0)
        {
            return 0;
        }
       for(int i = Inventory.instance.InventoryItems.Count;i>0;i--)
        {
           ItemInstance aItem = UIManager.instance.InventorySlots[Inventory.instance.InventoryItems[i-1].AttachedSlotIndex].thisItem;
            if (aItem.IsTrading == true)
            {
                TradePrice += Inventory.instance.InventoryItems[i - 1].Value * 3;
            }
        }

        if (UIManager.instance.InEqmShop)
        {
            for(int i = ShopInventoryItems.Count; i > 0; i--)
            {
                ItemInstance aItem = UIManager.instance.EqmShopSlots[ShopInventoryItems[i-1].AttachedSlotIndex].thisItem;
                if (aItem.IsTrading == true)
                {
                    TradePrice -= Mathf.RoundToInt(ShopInventoryItems[i - 1].Value * 1.5f);
                }
            }
        }

        if (UIManager.instance.InInn)
        {
            for (int i = InnInventoryItems.Count; i > 0; i--)
            {
                ItemInstance aItem = UIManager.instance.InnShopSlots[InnInventoryItems[i - 1].AttachedSlotIndex].thisItem;
                if (aItem.IsTrading == true)
                {
                    TradePrice -= Mathf.RoundToInt(InnInventoryItems[i - 1].Value * 1.5f);
                }
            }
        }

        return TradePrice;
    }

    [SerializeField] Button CompleteTradeButton;
    [SerializeField] TextMeshProUGUI Price;
    private void Update()
    {

        if (AllTradingItem.Count > 0)
        {
            if(TradingPrice()>0)
            {
                Price.text = "支出" + TradingPrice() + "G";
            }
            else
            {
                Price.text = "获得" + Mathf.Abs(TradingPrice()) + "G";
            }

            if (CharacterManager.PlayerInstance.CanPayGold(TradingPrice()))
            {
                CompleteTradeButton.interactable = true;
            }
            else
            {
                CompleteTradeButton.interactable = false;
            }
        }
        else
        {
            Price.text = "";
        }
    }
    public void CompeleteTrade()
    {
        if (AllTradingItem.Count > 0)
        {
            if (CharacterManager.PlayerInstance.PayGold(TradingPrice()))
            {
                foreach (ItemInstance a in AllTradingItem)
                {
                    a.IsTrading = false;
                }
                AllTradingItem.Clear();
            }

        }
    }
}


