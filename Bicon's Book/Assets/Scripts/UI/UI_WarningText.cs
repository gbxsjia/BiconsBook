using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_WarningText : MonoBehaviour
{
    public float timer;
    [SerializeField]
    TextMeshProUGUI WarningText;
    public void ShowText(string text, float duration)
    {
        gameObject.SetActive(true);
        timer = duration;
        WarningText.text = text;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
