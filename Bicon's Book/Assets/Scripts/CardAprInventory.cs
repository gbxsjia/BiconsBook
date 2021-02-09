using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardAprInventory : MonoBehaviour
{
    [SerializeField]
    public  Image CardBackImage;
    public Image CardFrameImage;

    public TextMeshProUGUI[] Entry;
    [SerializeField]
    private TextMeshProUGUI Name;
    [SerializeField]
    private TextMeshProUGUI Range;
    [SerializeField]
    private TextMeshProUGUI StaminaCost;
    [SerializeField] GameObject Outline;
    [SerializeField] Image CardImage;
    public Card_Base card;
    [SerializeField]
    private GameObject Holder;

    private bool PointStay;

    public void MouseEnter()
    {
    }
    public void UpdateUI()
    {
        if (card != null)
        {
            CardImage.sprite = card.GetComponent<Card_Appearance>().CardIconImage.sprite;
            Name.text = card.Name;
            for (int i = 0; i < Entry.Length; i++)
            {
                if (card.Entries.Count > i)
                {
                    string EntryText = "";
                    EntryInfo e = card.Entries[i];
                    Equipment thisEquipment = UI_EqmStatusPanel.m_CurrentEquipment;
                    EM_Weapon weapon = UI_EqmStatusPanel.m_CurrentEquipment as EM_Weapon;
                    switch (e.triggerType)
                    {
                        case TriggerType.AfterBeingAttacked:
                            EntryText += "反击：";
                            break;
                        case TriggerType.OnAbandon:
                            EntryText += "弃掉时：";
                            break;
                        case TriggerType.OnDraw:
                            EntryText += "抽取时：";
                            break;
                        case TriggerType.ReceivingAttack:
                            EntryText += "防御：";
                            break;
                    }
                    if (e.condition != null)
                    {
                        EntryText += e.condition.GetAppearenceText();
                    }
                    float tempFloat;
                    int tempInt;
                    switch (e.type)
                    {
                        case EntryType.Damage:
                            if (weapon != null)
                            {
                                tempFloat = Mathf.Round(e.i1 + weapon.WeaponDamage * e.i2 / 100f);
                            }
                            else
                            {
                                tempFloat = Mathf.Round(e.i1);
                            }
                            EntryText += "造成 " + tempFloat + " 点伤害";
                            break;
                        case EntryType.Bleed:
                            if (weapon != null)
                            {
                                tempInt = e.i1 + weapon.WeaponSpecial;
                            }
                            else
                            {
                                tempInt = e.i1;
                            }

                            EntryText += "流血" + tempInt;
                            break;
                        case EntryType.ArmorHeal:
                            tempFloat = Mathf.Round(e.i1 + (float)thisEquipment.MaxArmor * e.i2 / 100f);
                            EntryText += "恢复目标 " + tempFloat + " 点护甲";
                            break;
                        case EntryType.HealthHeal:
                            EntryText += "恢复目标 " + e.i1 + "+" + e.i2 + "% 血量";
                            break;
                        case EntryType.Overload:
                            EntryText += "过载 " + e.i1;
                            break;
                        case EntryType.Push:
                            if (e.i1 > 0)
                            {
                                EntryText += "推 " + e.i1 + " 格";
                            }
                            else
                            {
                                EntryText += "拉 " + -e.i1 + " 格";
                            }
                            break;
                        case EntryType.RecoverLoad:
                            EntryText += "清除过载";
                            break;
                        case EntryType.StaminaRecover:
                            EntryText += "恢复 " + e.i1 + " 体力";
                            break;
                        case EntryType.Stanch:
                            EntryText += "目标部位止血";
                            break;
                        case EntryType.Butcher:
                            EntryText += "对没有护甲部位造成 " + e.i2 + "% 额外伤害";
                            break;
                        case EntryType.Movement:
                            if (e.i1 > 0)
                            {
                                EntryText += "前进 " + e.i1 + " 格";
                            }
                            else
                            {
                                EntryText += "后退 " + -e.i1 + " 格";
                            }
                            break;
                        case EntryType.ArmorAttack:
                            tempFloat = Mathf.Round(e.i1 + thisEquipment.MaxArmor * e.i2 / 100f);
                            EntryText += "造成 " + tempFloat + " 点伤害";
                            break;
                        case EntryType.QuickReact:
                            EntryText += "快速反应" + e.i1;
                            break;


                        case EntryType.Block:
                            if (weapon != null)
                            {
                                tempFloat = weapon.MaxArmor * e.i2 / 100f;
                            }
                            else
                            {
                                tempFloat = thisEquipment.MaxArmor * e.i2 / 100f;
                            }
                            EntryText += "格挡,临时增加 " + tempFloat + " 点护甲";
                            break;
                        case EntryType.DrawCard:
                            EntryText += "抽取 " + e.i1 + " 张牌";
                            break;
                        case EntryType.Combo:
                            break;
                        case EntryType.WeakPoint_Random:
                            EntryText += "随机施加 " + e.i1 + " 个破绽";
                            break;
                        case EntryType.WeakPoint_Target:
                            EntryText += "施加破绽";
                            break;
                        case EntryType.HealthHealAll:
                            EntryText += "恢复全身 " + e.i1 + "+" + e.i2 + "% 血量";
                            break;
                        case EntryType.Counsume:
                            EntryText += "消耗";
                            break;

                        case EntryType.GenerateCard:
                            EntryText += "生成 " + e.i1 + " 张 " + e.g1.GetComponent<Card_Base>().Name;
                            break;

                        case EntryType.DestroyCardOnPart:
                            EntryText += "丢弃目标部位附带的 " + e.i1 + " 张牌";
                            break;

                        case EntryType.Shock:
                            EntryText += "震撼" + e.i1;
                            break;

                        case EntryType.Charge:
                            EntryText += "冲锋";
                            break;

                        case EntryType.Stable:
                            EntryText += "对自己施加稳固一回合";
                            break;
                        case EntryType.Defence_Turns:
                            EntryText += "伤害减免 " + e.i2 + " %";
                            break;
                        case EntryType.Attack_Turns:
                            EntryText += "伤害提升 " + e.i2 + " %";
                            break;
                        case EntryType.Sacrifice:
                            EntryText += "献祭此部位 " + e.i1 + "% 的血量";
                            break;

                        case EntryType.BleedRefresh:
                            EntryText += "流血刷新";
                            break;
                        case EntryType.ComboDamage:
                            int EachDmg = e.i1 + weapon.WeaponDamage * e.i2 / 100;
                            EntryText += "该回合每使用一张攻击牌，伤害增加 " + EachDmg;
                            break;
                        case EntryType.ShieldPose_Kingkong:
                            EntryText += "切换姿态：金刚";
                            break;
                        case EntryType.ShieldPose_Thorn:
                            EntryText += "切换姿态：荆棘";
                            break;
                        case EntryType.Thorn:
                            EntryText += "添加 " + e.i1 + " 层荆棘";
                            break;
                        case EntryType.HealthHealLowest:
                            EntryText += "恢复血量最低肢体 " + e.i1 + "% 血量";
                            break;
                        case EntryType.BleedSeed:
                            EntryText += "施加 " + e.i1 + " 层血种";
                            break;
                        case EntryType.AllTypeCardInHandReplace:
                            EntryText += "替换手中非完美拳牌为 " + e.g1.GetComponent<Card_Base>().Name;
                            break;
                        case EntryType.ReplaceCard:
                            EntryText += "替换此牌，变为 " + e.g1.GetComponent<Card_Base>().Name;
                            break;
                        case EntryType.ConsumeItem:
                            EntryText += "消耗品";
                            break;
                        case EntryType.SlowRecovery:
                            EntryText += "添加" + e.i1 + "层 缓慢恢复";
                            break;
                        case EntryType.Passive:
                            EntryText += "被动触发";
                            break;
                        case EntryType.SpearWall:
                            EntryText += "添加" + e.i1 + "层 矛墙";
                            break;
                        case EntryType.Dodge:
                            EntryText += "添加" + e.i1 + "层 闪避";
                            break;
                        case EntryType.Fear:
                            EntryText += "添加" + e.i1 + "层 畏惧";
                            break;
                        case EntryType.BladeMind:
                            EntryText += "添加" + e.i1 + "层 青石元气";
                            break;
                        case EntryType.ThrowRandomCard:
                            EntryText += "使对方丢弃一张随机牌";
                            break;
                        case EntryType.Confuse:
                            EntryText += "添加" + e.i1 + "层 混乱";
                            break;
                    }
                    Entry[i].gameObject.SetActive(true);
                    Entry[i].text = EntryText;
                }
                else
                {
                    Entry[i].gameObject.SetActive(false);
                }
            }
            if (card.StaminaCost >= 0)
            {
                StaminaCost.text = card.StaminaCost.ToString();
            }
            else
            {
                StaminaCost.text = "-";
            }
            CardFrameImage.sprite = ArtResourceManager.instance.GetCardBackground(card.rareLevel);
            CardBackImage.sprite = ArtResourceManager.instance.GetCardBackgroundSprite(card.cardType);
            if (card.MinDistance <= 1)
            {
                Range.text = card.MaxDistance.ToString();
            }
            else if(card.MaxDistance > 99)
            {
                Range.text = ">" + (card.MinDistance -1).ToString();
            }
            else
            {
                Range.text = card.MinDistance.ToString() + "~" + card.MaxDistance.ToString();
            }
        }
        else
        {
            print("The card you have selected is null.");
        }
    }    
    
}
