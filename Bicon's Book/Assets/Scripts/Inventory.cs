using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField]
    private GameObject ItemInstancePrefab;
    private void Awake()
    {
        instance = this;
    }

    public List<Equipment> InventoryItems = new List<Equipment>();

    public Equipment[] StartEquipments;

    private void Start()
    {
        InitializeInventory();
    }
    private void InitializeInventory()
    {
        StartCoroutine(InitializeProcess());
    }

    public void InitializeAllEqm()
    {
        StartCoroutine(InitializeProcess());
    }
    private IEnumerator InitializeProcess()
    {
        yield return new WaitForSeconds(0.01f);
        foreach (Equipment e in StartEquipments)
        {
            GenerateItem(e);
        }
        EquipmentManager equipmentManager= InGameManager.instance.EquipmentManagers[0];
        foreach (UI_EquipmentSlot slot in UIManager.instance.EquipmentSlots)
        {
            Equipment e = equipmentManager.GetEquipment(slot.type);
            if (e != null && !e.isDefaultEquipment)
            {
                GameObject g = Instantiate(ItemInstancePrefab, slot.transform);
                g.transform.localPosition = Vector3.zero;
                g.transform.localScale = slot.thisButton.GetComponent<RectTransform>().rect.width / 100 *Vector3.one;
                ItemInstance itemInstance = g.GetComponent<ItemInstance>();
                itemInstance.UpdateItem(e);
                slot.thisItem = itemInstance;
            }
        }
        CharacterManager.PlayerInstance.OnInventoryOpen(false);
    }

    public int FindFirstEmpetSlot()
    {
        for (int i = 0; i < UIManager.instance.InventorySlots.Length; i++)
        {
            if (UIManager.instance.InventorySlots[i].thisItem == null)
            {
                return i;
            }
        }
        return -1;
    }

    public void GenerateItem(Equipment item)
    {
        Equipment newItem = Instantiate(item);
        InventoryItems.Add(newItem);
        int slotIndex = FindFirstEmpetSlot();
        if (slotIndex != -1)
        {
            newItem.AttachedSlotIndex = slotIndex;
            GameObject g = Instantiate(ItemInstancePrefab, UIManager.instance.InventorySlots[slotIndex].transform);
            g.transform.localPosition = Vector3.zero;
            g.transform.localScale = Vector3.one * 0.8f;
            ItemInstance itemInstance = g.GetComponent<ItemInstance>();
            itemInstance.UpdateItem(newItem);
            UIManager.instance.InventorySlots[slotIndex].thisItem = itemInstance;
        }
    }
    public void AddItem(ItemInstance item)
    {
        int slotIndex = FindFirstEmpetSlot();
        if (slotIndex != -1)
        {
            UIManager.instance.InventorySlots[slotIndex].thisItem = item;
            item.equipment.AttachedSlotIndex = slotIndex;
            InventoryItems.Add(item.equipment);
            item.transform.SetParent(UIManager.instance.InventorySlots[slotIndex].transform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one * 0.8f;

        }
    }

    public void RemoveItem(Equipment item)
    {
        InventoryItems.Remove(item);
    }
}
