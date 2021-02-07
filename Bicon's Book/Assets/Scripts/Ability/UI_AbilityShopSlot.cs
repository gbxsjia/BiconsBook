using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_AbilityShopSlot : MonoBehaviour
{
    public AbilityType thisAbility;
    public UI_AbilityIcon CurrentIcon;
    public Image m_Outline;
    public bool AlreadyBaught = false;
    [SerializeField] TextMeshProUGUI BuyText;
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] TextMeshProUGUI PriceText2;
    [SerializeField] int price;

    private void Start()
    {
        if(price > 0)
        {
            PriceText2.text = price + "G";
            PriceText.text = price + "G";
        }
        else
        {
            PriceText2.text = "未开放";
            PriceText.text = "未开放";
        }
        CurrentIcon = UI_AbilityMain.thisInstance.FindAbilityWithType(thisAbility);
    }
    private void Update()
    {
        if(CurrentIcon == null)
        {
            LearnButton.interactable = false;
            return;
        }
        if (AlreadyBaught == false)
        {
            if (CharacterManager.PlayerInstance.CanPayGold(price))
            {
                LearnButton.interactable = true;
            }
            else
            {
                LearnButton.interactable = false;
            }
        }
        if(UI_AbilityMain.thisInstance.SelectAbility == CurrentIcon)
        {
            m_Outline.enabled = true;
        }
        else
        {
            m_Outline.enabled = false;
        }
    }
    public void ThisMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentIcon != null)
            {
                UI_AbilityMain.thisInstance.SelectAbility = CurrentIcon;
                UI_AbilityMain.thisInstance.UpdateAbilityInfo();
            }
        }
    }
    [SerializeField] Button LearnButton;
    public void LearnAbility()
    {
        if (AlreadyBaught == false && CharacterManager.PlayerInstance.PayGold(Mathf.RoundToInt(price * (1 - AchievementSystem.LearnAbilityDiscount))))
        {
            UI_AbilityMain.thisInstance.LearnAbility(thisAbility);
            AlreadyBaught = false;
            BuyText.gameObject.SetActive(true);
            LearnButton.interactable = false;
            AchievementSystem.instance.OnLearnAbility();
        }
    }
}
