using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_BattleField : SE_Base
{
    public void EqmTreasure()
    {
        KeyWord_1.text = "你找到了一些好东西。";

        CameraManager.instance.CameraShake(0.1f);
        int ItemAmount = 1;
        AchievementSystem.instance.a_CollectorCount += 1;

        if(AchievementSystem.instance.a_CollectorUnblock)
        {
            ItemAmount = 2;
        }
        for (int i = 0; i < ItemAmount; i++)
        {
            Equipment rewardEquipment = Instantiate(EnemyIncubator.thisInstance.GetRandomCurrentEquipment(10));
            GameObject g = Instantiate(RewardItemPrefab);

            g.GetComponent<ItemInstance>().UpdateItem(rewardEquipment);
            g.transform.SetParent(RewardParent);
            g.transform.localScale = Vector3.one * 0.8f;

            g.transform.localPosition = new Vector3((2*i-1)*80f*(ItemAmount-1),0,0);

            Inventory.instance.GenerateItem(rewardEquipment);
        }
    }

    
}
