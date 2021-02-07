using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectCardSetUI : MonoBehaviour
{
    [SerializeField] List<GameObject> CardDisplayList;
    [SerializeField] GameObject CardDisplayPrefab;
    [SerializeField] GameObject LayoutObject;
    [SerializeField] TextMeshProUGUI CardSetAmountText;
    [SerializeField] TextMeshProUGUI UsedCardSetAmountText;
    [SerializeField] TextMeshProUGUI VanishedSetAmountText;
    void Start()
    {
        
    }

    public void InspectUsedCardSet()
    {
        if (CardDisplayList.Count > 0)
        {
            foreach (GameObject a in CardDisplayList)
            {
                Destroy(a);
            }

            CardDisplayList.Clear();
        }

        foreach (GameObject a in InGameManager.instance.CardManagers[0].UsedCardsDeck)
        {
            GameObject aDisplayObj = Instantiate(CardDisplayPrefab);
            Card_Appearance aCardDisplay = aDisplayObj.GetComponent<Card_Appearance>();
            aCardDisplay.card = a.GetComponent<Card_Base>();
            aCardDisplay.UpdateUI();
            aDisplayObj.transform.SetParent(LayoutObject.transform);
            aDisplayObj.transform.localScale = Vector3.one;
            CardDisplayList.Add(aDisplayObj);
        }
    }
    public void InspectCardInCardSet()
    {
        if (CardDisplayList.Count > 0)
        {
            foreach (GameObject a in CardDisplayList)
            {
                Destroy(a);
            }

            CardDisplayList.Clear();
        }

        foreach(GameObject a in InGameManager.instance.CardManagers[0].InGameCardDeck)
        {
            GameObject aDisplayObj = Instantiate(CardDisplayPrefab);
            Card_Appearance aCardDisplay = aDisplayObj.GetComponent<Card_Appearance>();
            aCardDisplay.card = a.GetComponent<Card_Base>();
            aCardDisplay.UpdateUI();
            aDisplayObj.transform.SetParent(LayoutObject.transform);
            aDisplayObj.transform.localScale = Vector3.one;
            CardDisplayList.Add(aDisplayObj);
        }
    }
    // Update is called once per frame
    void Update()
    {
        CardSetAmountText.text = InGameManager.instance.CardManagers[0].InGameCardDeck.Count.ToString();
        UsedCardSetAmountText.text = InGameManager.instance.CardManagers[0].UsedCardsDeck.Count.ToString();
    }
}
