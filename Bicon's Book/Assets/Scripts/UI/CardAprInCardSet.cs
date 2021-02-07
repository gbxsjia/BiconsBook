using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardAprInCardSet : MonoBehaviour
{
    [SerializeField]
    private Image CardBackImage;
    [SerializeField]
    private Image CardFrameImage;

    [SerializeField]
    private Image EqmFrameImage;
    [SerializeField]
    private Image EqmImage;

    [SerializeField]
    private Image CardIconImage;

    [SerializeField]
    private TextMeshProUGUI Name;

    [SerializeField]
    private TextMeshProUGUI AmountText;

    public int m_Amount = 1;

    [SerializeField]
    private TextMeshProUGUI StaminaCost;

    public Card_Base card;


    public void UpdateUI()
    {
        AmountText.text = "x" + m_Amount ;
        Name.text = card.Name;
        CardIconImage.sprite = card.GetComponent<Card_Appearance>().CardIconImage.sprite;

        if (card.StaminaCost >= 0)
        {
            StaminaCost.text = card.StaminaCost.ToString();
        }
        else
        {
            StaminaCost.text = "-";
        }


    }
}
