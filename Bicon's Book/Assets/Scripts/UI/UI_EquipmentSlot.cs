
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler,IDropHandler
{
    public Button thisButton;
    public EquipmentType type;
    public ItemInstance thisItem;
    public void ThisMouseEnter()
    {
        if (thisItem != null)
        {
            thisItem.BackgroundImage.color = new Color(1, 1, 1);
        }
    }
    private void Update()
    {
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
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
            thisItem.transform.position = eventData.position + new Vector2(50f * Screen.width / 1920f, -50f * Screen.height / 1080f);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (thisItem!=null && thisItem.isDragging)
        {
            thisItem.transform.SetParent(transform);
            thisItem.transform.localPosition = Vector3.zero;
            thisItem.isDragging = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        UI_EquipmentSlot EqmSlot = eventData.pointerDrag.GetComponent<UI_EquipmentSlot>();

        if (EqmSlot != null && EqmSlot.thisItem != null && EqmSlot.thisItem.equipment.SlotTypes.Contains(type))
        {
            ItemInstance item = EqmSlot.thisItem;
            AudioManager.instance.PlayEquipSound();

            if (thisItem != null)
            {
                EqmSlot.thisItem = thisItem;
                EqmSlot.thisItem.transform.SetParent(EqmSlot.transform);
                EqmSlot.thisItem.transform.localPosition = Vector3.zero;
                EqmSlot.thisItem.transform.localScale = EqmSlot.thisButton.GetComponent<RectTransform>().rect.width / 100 * Vector3.one; 
                InGameManager.instance.EquipmentManagers[0].Equip(null, type);
                InGameManager.instance.EquipmentManagers[0].Equip(EqmSlot.thisItem.equipment, EqmSlot.type);
            }
            else
            {
                EqmSlot.thisItem = null;
                InGameManager.instance.EquipmentManagers[0].Equip(null, EqmSlot.type);
            }
           

            thisItem = item;
            thisItem.transform.SetParent(transform);
            thisItem.transform.localPosition = Vector3.zero;
            thisItem.transform.localScale = thisButton.GetComponent<RectTransform>().rect.width / 100 * Vector3.one;

            InGameManager.instance.EquipmentManagers[0].Equip(item.equipment, type);

            CharacterManager.PlayerInstance.PlayIdle();

            return;
        }
        
        UI_InventorySlot InvSlot = eventData.pointerDrag.GetComponent<UI_InventorySlot>();
        if (InvSlot != null && InvSlot.thisItem != null && InvSlot.thisItem.equipment.SlotTypes.Contains(type))
        {
            if (InvSlot.thisItem.IsTrading)
            {
                return;
            }
            ItemInstance item = InvSlot.thisItem;

            UnEquipItem();
            AudioManager.instance.PlayEquipSound();

            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = thisButton.GetComponent<RectTransform>().rect.width / 100 * Vector3.one;
            Inventory.instance.RemoveItem(item.equipment);

            InGameManager.instance.EquipmentManagers[0].Equip(item.equipment, type);
            UI_EqmStatusPanel.m_Instance.AddEquipmentCard(item.equipment);

            InGameManager.instance.CardManagers[0].UpdateWeightAndStamina();
            UIManager.instance.UpdateWeight();

            CharacterManager.PlayerInstance.PlayIdle();
            thisItem = item;
            InvSlot.thisItem = null;

        }
    }
    public void ThisMouseExit()
    {
        if (thisItem != null)
        {
            thisItem.BackgroundImage.color = new Color(0.9f, 0.9f, 0.9f);
        }
    }
    public void OnRightClick()
    {
        if (thisItem != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectItem();
            }
            if (Input.GetMouseButtonDown(1))
            {
                AudioManager.instance.PlayEquipSound();
                UnEquipItem();
                CharacterManager.PlayerInstance.PlayIdle();
            }
        }
    }
    public void SelectItem()
    {
        UI_EqmStatusPanel.m_Instance.SetSelectOutlinePos(this);
        UI_EqmStatusPanel.m_CurrentEquipment = thisItem.equipment;
        UI_EqmStatusPanel.m_Instance.UpdateUIInformation();
    }
    public void UnEquipItem()
    {
        if (thisItem != null)
        {
            UI_EqmStatusPanel.m_Instance.RemoveEquipmentCard(thisItem.equipment);
            InGameManager.instance.EquipmentManagers[0].Equip(null, type);
            Inventory.instance.AddItem(thisItem);
            thisItem = null;

            InGameManager.instance.CardManagers[0].UpdateWeightAndStamina();
            UIManager.instance.UpdateWeight();
        }
    }


}