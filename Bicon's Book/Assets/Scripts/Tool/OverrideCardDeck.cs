using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialCardInfo
{
    public GameObject cardPrefab;
    public BodyPartType bodyType;
    public EquipmentType equipmentType;
}
public class OverrideCardDeck : MonoBehaviour
{
    private CharacterManager characterManager;
    private CardManager cardManager;
    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        cardManager = GetComponent<CardManager>();
    }
    public SpecialCardInfo[] specialCards;

    private void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.N))
        {
            ClearCardDeck();
            UseSpecialCard();
        }*/
    }
    public void UseSpecialCard()
    {
        for (int i = 0; i < specialCards.Length; i++)
        {
            Equipment e;
            SpecialCardInfo sci = specialCards[i];
            if(sci.equipmentType==EquipmentType.LeftWeapon|| sci.equipmentType == EquipmentType.RightWeapon)
            {
                e = new EM_Weapon();
                e.isDefaultEquipment = true;
                characterManager.GetBodyPart(sci.bodyType).EquipItem(e);
                cardManager.GenerateNewCard(e, sci.cardPrefab);
            }
            else
            {
                e = new EM_Armor();
                e.isDefaultEquipment = true;
                characterManager.GetBodyPart(sci.bodyType).EquipItem(e);
                cardManager.GenerateNewCard(e, sci.cardPrefab);
            }
        }
    }
    public void ClearCardDeck()
    {
        cardManager.ResetCardDeck();
    }
}
