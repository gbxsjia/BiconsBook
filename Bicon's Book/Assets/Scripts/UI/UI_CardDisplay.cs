using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CardDisplay : MonoBehaviour
{
    [SerializeField]
    private Image Icon;
    public EquipmentType type;
    EquipmentManager manager;

    [SerializeField]
    private Transform CardHolder;
    private void Start()
    {
        StartCoroutine(InitialProcess());
    }

    private IEnumerator InitialProcess()
    {
        yield return null;
        manager = InGameManager.instance.EquipmentManagers[0];
        manager.EquipmentChangeEvent += OnEquipmentChange;
        Icon.sprite = ArtResourceManager.instance.GetEquipmentIcon(type);
    }
    public void OnEquipmentChange(Equipment oldItem, Equipment newItem,EquipmentType slot)
    {
        if (slot==type)
        {
            for (int i = 0; i < manager.GetDeck().Count; i++)
            {
                GameObject g = manager.GetDeck()[i];
                
                if (g.GetComponent<Card_Base>().ownerEquipment == newItem)
                {
                    g.transform.SetParent(CardHolder);
                }
            }
        }
   
    }
}
