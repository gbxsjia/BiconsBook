using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BattleReward : MonoBehaviour
{
    public static UI_BattleReward instance;

    [SerializeField]
    private Transform RewardParent;
    [SerializeField]
    private GameObject RewardItemPrefab;
    List<GameObject> rewardItemInstance = new List<GameObject>();

    [SerializeField]
    TextMeshProUGUI BattleEndTitleText;

    [SerializeField] TextMeshProUGUI m_GainExpAmountText;
    [SerializeField] TextMeshProUGUI m_GainGoldAmountText;

    public List<Equipment> dropEquipment = new List<Equipment>();

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
        UIManager.instance.BattleUI.SetActive(false);
    }

    public void AddReward(BodyPart bodyPart)
    {
        float GetMainItemPercent = 100 / (dropEquipment.Count + 1);


        foreach (Equipment a in bodyPart.AttachedEquipments)
        {
            if (a.SlotTypes[0] == EquipmentType.Armor || a.SlotTypes[0] == EquipmentType.Helmet || a.SlotTypes[0] == EquipmentType.LeftArm || a.SlotTypes[0] == EquipmentType.LeftLeg
            || a.SlotTypes[0] == EquipmentType.RightArm || a.SlotTypes[0] == EquipmentType.RightLeg || a.SlotTypes[0] == EquipmentType.LeftWeapon || a.SlotTypes[0] == EquipmentType.RightWeapon)
            {
                if (Random.Range(0, 100) < GetMainItemPercent)
                {
                    dropEquipment.Add(a);
                }
            }
            else
            {
                if (Random.Range(0, 100) < GetMainItemPercent / 2)
                {
                    dropEquipment.Add(a);
                }

            }

        }

                  
    }
    public IEnumerator BattleEndStartProcess(bool PlayerWin)
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(true);
        if (PlayerWin == true)
        {
            BattleEndTitleText.text = "胜利";
           m_GainExpAmountText.text = UpGradeManager.instance.GainExp().ToString();

            if(InGameManager.instance.IsBoss)
            {
                CoreAbilityManager.instance.ShowCoreAbilityObj();
                InGameManager.BossDefeat += 1;
            }else
            {
                InGameManager.EnemyDefeat += 1;
            }

            foreach (Equipment e in dropEquipment)
            {
                if (!e.isDefaultEquipment)
                {
                    Equipment rewardEquipment = Instantiate(e);
                    if (rewardEquipment.CurrentArmor < rewardEquipment.MaxArmor * 0.5f)
                    {
                        rewardEquipment.CurrentArmor = Mathf.RoundToInt(rewardEquipment.MaxArmor * 0.5f);
                    }
                    GameObject g = Instantiate(RewardItemPrefab);
                    g.transform.localScale = Vector3.one * 0.8f;
                    rewardItemInstance.Add(g);
                    g.GetComponent<ItemInstance>().UpdateItem(rewardEquipment);
                    g.transform.SetParent(RewardParent);
                    Inventory.instance.GenerateItem(rewardEquipment);
                }
            }

            int aGoldAmount = 0;

            if (CharacterManager.PlayerInstance.GetComponent<AbilityManager>().ExtraReward)
            {
                aGoldAmount = Mathf.RoundToInt(InGameManager.instance.Characters[1].m_Gold * 1.5f);
            }
            else
            {
               aGoldAmount = InGameManager.instance.Characters[1].m_Gold;
            }

            m_GainGoldAmountText.text = aGoldAmount.ToString();
            CharacterManager.PlayerInstance.AddGold(aGoldAmount);
        }
        else
        {
            m_GainExpAmountText.text = "0";
            m_GainGoldAmountText.text = "0";
            if (InGameManager.instance.LostGameOver == true)
            {
                UIManager.instance.GameOver();
                UIManager.instance.UpdateFinalScore(false);
            }
            BattleEndTitleText.text = "战败";
        }
        dropEquipment.Clear();
    }

    public void ReturnToCamp()
    {
        gameObject.SetActive(false);
        for (int i = rewardItemInstance.Count-1; i >=0; i--)
        {
            Destroy(rewardItemInstance[i]);
        }
        rewardItemInstance.Clear();
    }
}
