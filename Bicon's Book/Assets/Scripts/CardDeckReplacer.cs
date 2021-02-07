using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckReplacer : MonoBehaviour
{
    public int DrawCardAmount;
    public int StartDrawCardAmount;
    //public List<GameObject> Deck = new List<GameObject>();

    public bool PlayerReplace;
    public Equipment[] PlayerEquipments;
    public bool EnemyReplace;
    public Equipment[] EnemyEquipments;

    public Equipment[] OldEquipments= new Equipment[14];
    //public List<GameObject> GetCopyOfDeck()
    //{
    //    List<GameObject> deck = new List<GameObject>();
    //    foreach (GameObject card in Deck)
    //    {
    //        deck.Add(Instantiate(card));
    //    }
    //    return deck;
    //}

    private void Start()
    {
        InGameManager.instance.BeforeBattleStartEvent += OnBeforeBattleStart;
        InGameManager.instance.BattleEndEvent += OnBattleEnd;
    }

    private void OnBattleEnd()
    {
        ReturnEquipment();
    }

    private void OnBeforeBattleStart()
    {
        InGameManager.instance.BeforeBattleStartEvent -= OnBeforeBattleStart;
        ReplaceEquipment();
    }
    public void ReplaceEquipment()
    {
        if (PlayerReplace)
        {
            EquipmentManager EM = InGameManager.instance.EquipmentManagers[0];
            for (int i = 0; i < 14; i++)
            {
                EquipmentType type = (EquipmentType)i;

                OldEquipments[i] = EM.GetEquipment(type);
                if (PlayerEquipments[i] != null)
                {
                    EM.Equip(Instantiate(PlayerEquipments[i]), type);
                }
                else
                {
                    EM.Equip(null, (EquipmentType)i);
                }
            }
        }
        if (EnemyReplace)
        {
            EquipmentManager EM = InGameManager.instance.EquipmentManagers[1];
            for (int i = 0; i < 14; i++)
            {
                EquipmentType type = (EquipmentType)i;

                if (EnemyEquipments[i] != null)
                {
                    EM.Equip(Instantiate(EnemyEquipments[i]), type);
                }
                else
                {
                    EM.Equip(null, (EquipmentType)i);
                }
            }
        }
    }

    public void ReturnEquipment()
    {
        if (PlayerReplace)
        {
            EquipmentManager EM = InGameManager.instance.EquipmentManagers[0];
            for (int i = 0; i < 14; i++)
            {
                EquipmentType type = (EquipmentType)i;

                if (OldEquipments[i] != null)
                {
                    EM.Equip(Instantiate(OldEquipments[i]), type);
                }
                else
                {
                    EM.Equip(null, (EquipmentType)i);
                }
            }
        }
    }
}
