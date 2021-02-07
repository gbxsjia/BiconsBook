using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SE_Profiteer : SE_Base
{
    Equipment rewardEquipment;
    int Price;
    [SerializeField] Button BuyButton;
    List<Equipment> GambleRewardEquipments;
    List<GameObject> GambleRewardEquipmentsDisplay;
    public void GambleRule()
    {
        KeyWord_1.text = "你支付一点小钱，便可以获得以下随机一件好东西，是不是很划算~";
        GambleRewardEquipments = new List<Equipment>();
        GambleRewardEquipmentsDisplay = new List<GameObject>();

        for (int i = 0; i < 2; i++)
        {
           Equipment aEquipment = Instantiate(EnemyIncubator.thisInstance.GetRandomCurrentEquipment(15));
            GameObject g = Instantiate(RewardItemPrefab);
            g.GetComponent<ItemInstance>().UpdateItem(aEquipment);
            g.transform.SetParent(RewardParent);
            g.transform.localScale = Vector3.one * 0.8f;
            g.transform.localPosition = new Vector3(-60 + 120 * i, 80, 0);
            GambleRewardEquipments.Add(aEquipment);
            GambleRewardEquipmentsDisplay.Add(g);
        }

        for (int i = 0; i < 3; i++)
        {
            Equipment aEquipment = Instantiate(EnemyIncubator.thisInstance.GetRandomCurrentEquipment());
            GameObject g = Instantiate(RewardItemPrefab);
            g.GetComponent<ItemInstance>().UpdateItem(aEquipment);
            g.transform.SetParent(RewardParent);
            g.transform.localScale = Vector3.one * 0.8f;
            g.transform.localPosition = new Vector3(-100 + i * 100, -50, 0);
            GambleRewardEquipments.Add(aEquipment);
            GambleRewardEquipmentsDisplay.Add(g);

        }
        int GamblePrice = 0;
        foreach(Equipment a in GambleRewardEquipments)
        {
            GamblePrice += a.Value;
        }

        Price = Mathf.RoundToInt(GamblePrice / 5f * 2.5f);
        KeyWord_2.text = "抽奖，需要" + Price + "G";

        if (CharacterManager.PlayerInstance.m_Gold >= Price)
        {
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.interactable = false;
        }
    }
    public void EqmTrade()
    {
        KeyWord_1.text = "他向你展示了他的货";
      

        rewardEquipment = Instantiate(EnemyIncubator.thisInstance.GetRandomCurrentEquipment(15));
        GameObject g = Instantiate(RewardItemPrefab);
        Price = Mathf.RoundToInt(rewardEquipment.Value * 3f);
        g.GetComponent<ItemInstance>().UpdateItem(rewardEquipment);
        g.transform.SetParent(RewardParent);     
        g.transform.localScale = Vector3.one * 0.8f;
        g.transform.localPosition = Vector3.zero;  
        KeyWord_2.text = Price +"G";

        if(CharacterManager.PlayerInstance.m_Gold >= Price)
        {
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.interactable = false;
        }
    }

    public void EqmTradeCompelete()
    {
        KeyWord_1.text = "嘿嘿，客人你真是识货，有缘再见";
        KeyWord_2.text = "已购买";
        CharacterManager.PlayerInstance.PayGold(Price);
        CameraManager.instance.CameraShake(0.1f);
        Inventory.instance.GenerateItem(rewardEquipment);
    }

    public void EqmGambleCompelete()
    {
        KeyWord_1.text = "嘿嘿，客人运气不错，这就是你的奖品了，有缘再见";
        KeyWord_2.text = "已抽奖";
        CharacterManager.PlayerInstance.PayGold(Price);
        CameraManager.instance.CameraShake(0.1f);
        int RewardId = Random.Range(0, 5);
        for(int i = 0; i < 5;i++)
        {
            if (i != RewardId)
            {
                GambleRewardEquipmentsDisplay[i].SetActive(false);
            }
        }
        GambleRewardEquipmentsDisplay[RewardId].transform.localPosition = Vector3.zero;
        Inventory.instance.GenerateItem(GambleRewardEquipments[RewardId]);
    }

}
