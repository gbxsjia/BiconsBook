
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public int index;
    public ItemInstance thisItem;

    public void ThisMouseEnter()
    {
       UIManager.InInventoryScrollArea = true;
        if (thisItem != null)
        {
            thisItem.BackgroundImage.color = new Color(1, 1, 1);
        }
    }
    private void Update()
    {
    }

    public void ThisMouseExit()
    {
        UIManager.InInventoryScrollArea = false;
        if (thisItem != null)
        {
            thisItem.BackgroundImage.color = new Color(0.9f, 0.9f, 0.9f);
        }
    }
    public void RightClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (thisItem != null)
            {
                UI_EqmStatusPanel.m_Instance.SetSelectOutlinePos(this);
                UI_EqmStatusPanel.m_CurrentEquipment = thisItem.equipment;
                UI_EqmStatusPanel.m_Instance.UpdateUIInformation();

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (thisItem != null)
            {

                if (UIManager.instance.InEqmShop == false && UIManager.instance.InInn == false)
                {
                    EquipmentManager manager = InGameManager.instance.EquipmentManagers[0];
                    for (int i = 0; i < thisItem.equipment.SlotTypes.Count; i++)
                    {
                        if (manager.GetEquipment(thisItem.equipment.SlotTypes[i]) == null)
                        {
                            UI_EqmStatusPanel.m_Instance.AddEquipmentCard(thisItem.equipment);
                            int typeIndex = (int)thisItem.equipment.SlotTypes[i];
                            UI_EquipmentSlot TargetSlot = UIManager.instance.EquipmentSlots[typeIndex];
                            TargetSlot.thisItem = thisItem;
                            thisItem.transform.SetParent(TargetSlot.transform);
                            thisItem.transform.localPosition = Vector3.zero;
                            thisItem.transform.localScale = TargetSlot.thisButton.GetComponent<RectTransform>().rect.width / 100 * Vector3.one;

                            AudioManager.instance.PlayEquipSound();
                            manager.Equip(thisItem.equipment, thisItem.equipment.SlotTypes[i]);

                            Inventory.instance.RemoveItem(thisItem.equipment);
                            thisItem = null;
                            break;
                        }
                    }
                    CharacterManager.PlayerInstance.PlayIdle();
                    InGameManager.instance.CardManagers[0].UpdateWeightAndStamina();
                    UIManager.instance.UpdateWeight();
                }
                else if (UIManager.instance.InEqmShop == true)
                {
                    if (Shop.instance.FindFirstEmptySlot(UIManager.TownPlace.BlackSmith) != -1)
                    {
                        if (thisItem.IsTrading == false)
                        {
                            thisItem.IsTrading = true;
                            Shop.instance.AllTradingItem.Add(thisItem);
                        }
                        else
                        {
                           thisItem.IsTrading = false;
                            Shop.instance.AllTradingItem.Remove(thisItem);
                        }
                        Shop.instance.AddItem(thisItem, UIManager.TownPlace.BlackSmith);
                            Inventory.instance.RemoveItem(thisItem.equipment);
                            thisItem = null;
                    }

                }
                else if (UIManager.instance.InInn == true)
                {
                    if (Shop.instance.FindFirstEmptySlot(UIManager.TownPlace.Inn) != -1)
                    {
                        if (thisItem.IsTrading == false)
                        {
                            thisItem.IsTrading = true;
                            Shop.instance.AllTradingItem.Add(thisItem);
                        }
                        else
                        {
                            thisItem.IsTrading = false;
                            Shop.instance.AllTradingItem.Remove(thisItem);
                        }
                        Shop.instance.AddItem(thisItem, UIManager.TownPlace.Inn);
                        Inventory.instance.RemoveItem(thisItem.equipment);
                            thisItem = null;                     
                    }
                }
                UIManager.instance.UpdateAllEquipmentValue();
            }
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (thisItem != null)
        {
            thisItem.transform.SetParent(UIManager.instance.transform);
            thisItem.transform.SetAsLastSibling();
            thisItem.isDragging = true;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (thisItem != null)
        {
            thisItem.transform.position = eventData.position +  new Vector2(50f* Screen.width/1920f, -50f * Screen.height/1080f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (thisItem != null)
        {
            thisItem.transform.SetParent(transform);
            thisItem.transform.localPosition = Vector3.zero;
        }

    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        UI_EquipmentSlot EqmSlot= eventData.pointerDrag.GetComponent<UI_EquipmentSlot>() as UI_EquipmentSlot;
        if (EqmSlot != null)
        {
            AudioManager.instance.PlayEquipSound();
            EqmSlot.UnEquipItem();
            CharacterManager.PlayerInstance.PlayIdle();
        }

        UI_ShopSlot ShopSlot = eventData.pointerDrag.GetComponent<UI_ShopSlot>();
        if (ShopSlot != null && ShopSlot.thisItem != null)
        { 
            if(ShopSlot.thisItem.IsTrading == false)
            {
                ShopSlot.thisItem.IsTrading = true;
                Shop.instance.AllTradingItem.Add(ShopSlot.thisItem);
            }
            else
            {
                ShopSlot.thisItem.IsTrading =false;
                Shop.instance.AllTradingItem.Remove(ShopSlot.thisItem);
            }

            Shop.instance.RemoveItem(ShopSlot.thisItem.equipment);
            Inventory.instance.AddItem(ShopSlot.thisItem);
            ShopSlot.thisItem = null;
           

        }

        UI_InventorySlot InventorySlot = eventData.pointerDrag.GetComponent<UI_InventorySlot>();
        if (InventorySlot != null && InventorySlot.thisItem != null)
        {
            if (thisItem != null)
            {
                ItemInstance item = thisItem;
                thisItem = InventorySlot.thisItem; 
                InventorySlot.thisItem = item;
                thisItem.transform.SetParent(transform);
                thisItem.transform.localPosition = Vector3.zero;
                thisItem.transform.localScale = Vector3.one;
                thisItem.equipment.AttachedSlotIndex = index;

              
                InventorySlot.thisItem.transform.SetParent(InventorySlot.transform);
                InventorySlot.thisItem.transform.localPosition = Vector3.zero;
                InventorySlot.thisItem.transform.localScale = Vector3.one;
                InventorySlot.thisItem.equipment.AttachedSlotIndex = InventorySlot.index;
            }
            else
            {
                thisItem = InventorySlot.thisItem;
                thisItem.equipment.AttachedSlotIndex = index;
                thisItem.transform.SetParent(transform);
                thisItem.transform.localPosition = Vector3.zero;
                thisItem.transform.localScale = Vector3.one;
                InventorySlot.thisItem = null;
            }
            AudioManager.instance.PlayEquipSound();


        }

        UIManager.instance.UpdateAllEquipmentValue();
    }
}
