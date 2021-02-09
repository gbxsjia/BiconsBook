using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_AbilityIcon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool IsShadow;
    public bool AlreadyEquiped = false;
    public bool m_AbilityEnabled = false;
    public bool m_AbilityKnown = false;
    public AbilityType thisAbilityType;
    public Ability_Base thisAbility;
    GameObject thisShadowObj;
    [SerializeField] TextMeshProUGUI EquipText;
    [SerializeField] GameObject m_WenHaoObj;

    private void Awake()
    {
        
        thisAbility = gameObject.GetComponent<Ability_Base>();

            gameObject.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
        

        //暂用
        if (m_AbilityKnown == true || PlayerPrefs.HasKey(thisAbilityType.ToString()+"Known"))
        {
            m_AbilityKnown = true;
            m_WenHaoObj.SetActive(false);
        }
        else
        {
            m_AbilityKnown = false;
            m_WenHaoObj.SetActive(true);
        }
    }

    public void AbilityKnown()
    {
        if (!m_AbilityKnown)
        {
            m_AbilityKnown = true; 
            m_WenHaoObj.SetActive(false);
            PlayerPrefs.SetInt(thisAbilityType.ToString() + "Known", 1);

        }
    }

    [SerializeField] GameObject EnableHint;
    public void OnAbilityLearn()
    {
        if (m_AbilityEnabled == false)
        {
            AbilityKnown();
            m_AbilityEnabled = true;
            EnableHint.SetActive(false);
            gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);

        }
    }
    public void ThisMouseClick()
    {

    }
    public void ThisMouseEnter()
    {
        if (IsShadow)
        {
            return;
        }      
        UI_AbilityMain.thisInstance.SelectAbility = this;
            UI_AbilityMain.thisInstance.UpdateAbilityInfo();
            UI_AbilityMain.thisInstance.SetInfoObjActive(true); 
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
       
    }
                
    public void ThisMouseExit()
    {
        if (IsShadow)
        {
            return;
        } 
        UI_AbilityMain.thisInstance.SetInfoObjActive(false);
        gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
  
    }
    public void OnUnEquip()
    {
        AlreadyEquiped = false;
        EquipText.gameObject.SetActive(false);
    }
    public void OnEquip()
    {
        AlreadyEquiped = true;
        EquipText.gameObject.SetActive(true);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsShadow || m_AbilityEnabled == false || AlreadyEquiped)
        {
            return;
        }

        thisShadowObj = Instantiate(gameObject,transform.parent.parent.parent.parent);
        thisShadowObj.GetComponent<Image>().raycastTarget = false;
        thisShadowObj.GetComponent<UI_AbilityIcon>().IsShadow = false;
        thisShadowObj.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
    }
    public void OnDrag(PointerEventData eventData)
    {if (IsShadow || m_AbilityEnabled == false || AlreadyEquiped)
        {
            return;
        }
    thisShadowObj.transform.position = eventData.position + new Vector2( Screen.width / 1920f,  Screen.height / 1080f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        if (IsShadow || m_AbilityEnabled == false)
        {
            return;
        }
        Destroy(thisShadowObj);
    }
}
