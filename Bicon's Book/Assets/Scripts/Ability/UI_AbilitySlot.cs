using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_AbilitySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] int AbilitySlotId = 0;
    public bool m_CharacterAbilitySlot;
    public bool m_SlotEnabled;
    public bool m_Locked;

    public AbilityType thisAbility;
    public UI_AbilityIcon CurrentIcon;
    public Image m_SlotItemImage;
    public Image m_OutlineImage;
    public void OnDrop(PointerEventData eventData)
    {

        if (m_SlotEnabled == false)
        {
            return;
        }
        if (m_CharacterAbilitySlot || m_Locked)
        {
            return;
        }

        if (UI_AbilityMain.m_AllLock)
        {
         
            UI_AbilityMain.thisInstance.SelectAbilitySlot = this;   
            UI_AbilityMain.thisInstance.SelectAbility = eventData.pointerDrag.GetComponent<UI_AbilityIcon>();

            UI_AbilityMain.thisInstance.EquipAbilityCheckPage.SetActive(true);
        }
        else
        {

            UI_AbilityIcon aAbilityIcon = eventData.pointerDrag.GetComponent<UI_AbilityIcon>();
            if (aAbilityIcon == null || aAbilityIcon.m_AbilityEnabled == false)
            {
                return;
            }
            if (aAbilityIcon.AlreadyEquiped)
            {
                return;
            }
            if (thisAbility != AbilityType.None)
            {
                UnEquip();
            }

            Equip(aAbilityIcon);
        }


    }
    public void Equip(UI_AbilityIcon aAbilityIcon)
    {
        if (thisAbility != AbilityType.None)
        {
            UnEquip();
        }

        thisAbility = aAbilityIcon.thisAbilityType;
        CurrentIcon = aAbilityIcon;
        aAbilityIcon.OnEquip();
        m_SlotItemImage.sprite = aAbilityIcon.GetComponent<Image>().sprite;
        m_SlotItemImage.color = Vector4.one;
    }


    public void ThisMouseClick()
    {
        if (m_SlotEnabled == false)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentIcon != null)
            {
                UI_AbilityMain.thisInstance.SelectAbility = CurrentIcon;
                UI_AbilityMain.thisInstance.UpdateAbilityInfo();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (m_CharacterAbilitySlot || m_Locked)
            {
                return;
            }

            if (thisAbility != AbilityType.None)
            {
                UnEquip();
            }
        }

    }

    public void UnEquip()
    {
        thisAbility = AbilityType.None;
        CurrentIcon.OnUnEquip();
        CurrentIcon = null;
        m_SlotItemImage.sprite = null;
        m_SlotItemImage.color = Vector4.zero;
    }
    public void ThisMouseEnter()
    {
        string aString = "";
        if (m_CharacterAbilitySlot)
        {
            aString = "初始天赋，无法更换";
            UI_AbilityMain.thisInstance.ShowToolTip(aString, gameObject.transform.position + new Vector3(0,-40,0));
        }
        else if(m_SlotEnabled == false)
        {
            switch (AbilitySlotId)
            {
                case 2:
                    aString = "3级解锁";
                    break;
                case 3:
                    aString = "6级解锁";
                    break;
                case 4:
                    aString = "9级解锁";
                    break;
                case 5:
                    aString = "12级解锁";
                    break;
                case 6:
                    aString = "15级解锁";
                    break;

            }
            UI_AbilityMain.thisInstance.ShowToolTip(aString, gameObject.transform.position + new Vector3(0, -40, 0));
        }

        
    }
    public void ThisMouseExit()
    { UI_AbilityMain.thisInstance.UnshowToolTip(); }
}
