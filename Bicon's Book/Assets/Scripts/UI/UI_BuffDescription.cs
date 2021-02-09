using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_BuffDescription : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public GameObject Holder;

    public static UI_BuffDescription instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InGameManager.instance.ReturnToCampEvent += Instance_ReturnToCampEvent;
    }

    private void Instance_ReturnToCampEvent()
    {
        HideUI();
    }

    public void ShowUI(string name,string description)
    {
        Holder.SetActive(true);
        NameText.text = name;
        DescriptionText.text = description;
    }
    public void HideUI()
    {
        Holder.SetActive(false);
    }
}
