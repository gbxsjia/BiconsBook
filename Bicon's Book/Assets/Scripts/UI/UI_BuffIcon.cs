using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_BuffIcon : MonoBehaviour
{
    public Image IconImage;
    public TextMeshProUGUI StackText;

    public Buff_base Buff;

    private void Start()
    {
        if (transform.lossyScale.x <= 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    public void UpdateUI(Buff_base buff)
    {
        Buff = buff;
        if (buff != null)
        {           
            IconImage.sprite = ArtResourceManager.instance.GetBuffIcon(buff.Type);
            if (buff.Amount == -1)
            { StackText.text = "∞"; }
            else
            {
                StackText.text = buff.Amount.ToString();
            }
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void MouseEnter()
    {
        UI_BuffDescription.instance.ShowUI(Buff.GetName(), Buff.GetDescription());
    }
    public void MouseExit()
    {
        UI_BuffDescription.instance.HideUI();
    }
}
