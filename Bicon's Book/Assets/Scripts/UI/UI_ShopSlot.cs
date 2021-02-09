using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ShopSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public int index;
    public ItemInstance thisItem;

    public void ThisMouseEnter()
    {
        UIManager.InShopScrollArea = true;
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
        UIManager.InShopScrollArea = false;
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
                if (Inventory.instance.FindFirstEmpetSlot() != -1)
                {
                    Shop.instance.RemoveItem(thisItem.equipment);
                    Inventory.instance.AddItem(thisItem);
                    thisItem = null;

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

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (thisItem != null)
        {
            thisItem.transform.position = eventData.position + new Vector2(50f * Screen.width / 1920f, -50f * Screen.height / 1080f);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        UI_InventorySlot InvSlot = eventData.pointerDrag.GetComponent<UI_InventorySlot>();
        if (InvSlot != null && InvSlot.thisItem != null)
        {
            if (UIManager.instance.CurrentPlace == UIManager.TownPlace.BlackSmith)
            {
                if (Shop.instance.FindFirstEmptySlot(UIManager.TownPlace.BlackSmith) != -1)
                {
                    if (InvSlot.thisItem.IsTrading == false)
                    {
                        InvSlot.thisItem.IsTrading = true;
                        Shop.instance.AllTradingItem.Add(InvSlot.thisItem);
                    }
                    else
                    {
                        InvSlot.thisItem.IsTrading = false;
                        Shop.instance.AllTradingItem.Remove(InvSlot.thisItem);
                    }
                    Shop.instance.AddItem(InvSlot.thisItem, UIManager.TownPlace.BlackSmith);
                        Inventory.instance.RemoveItem(InvSlot.thisItem.equipment);
                        InvSlot.thisItem = null;
          
                    
                }

            }
            else if (UIManager.instance.CurrentPlace == UIManager.TownPlace.Inn)
            {
                if (Shop.instance.FindFirstEmptySlot(UIManager.TownPlace.Inn) != -1)
                {
                    if (InvSlot.thisItem.IsTrading == false)
                    {
                        InvSlot.thisItem.IsTrading = true;
                        Shop.instance.AllTradingItem.Add(InvSlot.thisItem);
                    }
                    else
                    {
                        InvSlot.thisItem.IsTrading = false;
                        Shop.instance.AllTradingItem.Remove(InvSlot.thisItem);
                    }
                    Shop.instance.AddItem(InvSlot.thisItem, UIManager.TownPlace.Inn);
                        Inventory.instance.RemoveItem(InvSlot.thisItem.equipment);
                        InvSlot.thisItem = null;

                }

            }


        }

        UIManager.instance.UpdateAllEquipmentValue();
    }
}
