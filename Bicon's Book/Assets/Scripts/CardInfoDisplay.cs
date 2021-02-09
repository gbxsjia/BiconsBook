using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfoDisplay : CardAprInventory
{
    public List<string> EntryInfoNameList = new List<string>();
    public List<string> EntryInfoList = new List<string>();
    public void UpdateCardInfo()
    {
        EntryInfoNameList.Clear();
        EntryInfoList.Clear();
        UpdateUI();
        for (int i = 0; i < Entry.Length; i++)
        {
            if (card.Entries.Count > i)
            {

                EntryInfo e = card.Entries[i];
                Equipment thisEquipment = UI_EqmStatusPanel.m_CurrentEquipment;
                EM_Weapon weapon = UI_EqmStatusPanel.m_CurrentEquipment as EM_Weapon;
                if (e.condition != null)
                {
                    switch (e.condition.AppearenceText)
                    {
                        case "连击1：":
                            EntryInfoNameList.Add("连击");
                            EntryInfoList.Add("使用X张攻击牌时，自动打出。");
                            break;
                        case "连击2：":
                            EntryInfoNameList.Add("连击");
                            EntryInfoList.Add("使用X张攻击牌时，自动打出。");
                            break;
                        case "连击3：":
                            EntryInfoNameList.Add("连击");
                            EntryInfoList.Add("使用X张攻击牌时，自动打出。");
                            break;
                    }
                }


                switch (e.type)
                {
                    case EntryType.Damage:

                        break;
                    case EntryType.Bleed:
                        EntryInfoNameList.Add("流血：");
                        EntryInfoList.Add("每回合对部位造成X点伤害，持续3回合。");
                        break;
                    case EntryType.ArmorHeal:

                        break;
                    case EntryType.HealthHeal:

                        break;
                    case EntryType.Overload:
                        EntryInfoNameList.Add("超载：");
                        EntryInfoList.Add("下回合体力减少X点");
                        break;
                    case EntryType.Push:

                        break;
                    case EntryType.RecoverLoad:

                        break;
                    case EntryType.StaminaRecover:
                        break;
                    case EntryType.Stanch:
                        break;
                    case EntryType.Butcher:
                        break;
                    case EntryType.Movement:

                        break;
                    case EntryType.ArmorAttack:

                        break;
                    case EntryType.QuickReact:
                        EntryInfoNameList.Add("快速反应：");
                        EntryInfoList.Add("即刻抽取此装备所在手臂的X张武器牌。");
                        break;

                    case EntryType.Block:
                        EntryInfoNameList.Add("格挡：");
                        EntryInfoList.Add("将对方攻击目标改为本牌的所属部位。");
                        EntryInfoNameList.Add("临时护甲：");
                        EntryInfoList.Add("暂时增加X点护甲，回合开始时消失。");
                        break;
                    case EntryType.DrawCard:

                        break;
                    case EntryType.Combo:
                        break;
                    case EntryType.WeakPoint_Random:
                        break;
                    case EntryType.WeakPoint_Target:
                        break;
                    case EntryType.HealthHealAll:
                        break;
                    case EntryType.Counsume:
                        EntryInfoNameList.Add("消耗：");
                        EntryInfoList.Add("此牌在使用后不进入弃牌库，而进入逝牌库");
                        break;
                    case EntryType.GenerateCard:
                        break;

                    case EntryType.DestroyCardOnPart:
                        break;

                    case EntryType.Shock:
                        EntryInfoNameList.Add("震撼：");
                        EntryInfoList.Add("下回合对方抽牌减少X张");
                        break;

                    case EntryType.Charge:
                        EntryInfoNameList.Add("冲锋:");
                        EntryInfoList.Add("前进X格,每前进1格,提升30%的伤害");
                        break;
                    case EntryType.Stable:
                        EntryInfoNameList.Add("稳固：");
                        EntryInfoList.Add("不会被位移");
                        break;

                    case EntryType.Defence_OneFrame:
                        break;
                    case EntryType.Defence_Attacks:
                        break;
                    case EntryType.Defence_Turns:
                        break;
                    case EntryType.Attack_OneFrame:
                        break;
                    case EntryType.Attack_Attacks:
                        break;
                    case EntryType.Attack_Turns:
                        break;

                    case EntryType.Sacrifice:
                        EntryInfoNameList.Add("献祭：");
                        EntryInfoList.Add("添加一层献祭，所属肢体失去X%血量");
                        break;

                    case EntryType.BleedRefresh:
                        EntryInfoNameList.Add("刷新：");
                        EntryInfoList.Add("重置buff的持续回合数");
                        break;
                    case EntryType.ComboDamage:
                        break;
                    case EntryType.ShieldPose_Kingkong:
                        EntryInfoNameList.Add("金刚姿态：");
                        EntryInfoList.Add("改变姿态为金刚，每回合获得一张会被消耗的格挡牌");
                        break;
                    case EntryType.ShieldPose_Thorn:
                        EntryInfoNameList.Add("荆棘姿态：");
                        EntryInfoList.Add("改变姿态为荆棘，每回合在指定自身部位添加6层荆棘");
                        EntryInfoNameList.Add("荆棘：");
                        EntryInfoList.Add("当敌人攻击所属部位时，反击敌人等同于层数的伤害，并失去2层荆棘");
                        break;
                    case EntryType.Thorn:
                        EntryInfoNameList.Add("荆棘：");
                        EntryInfoList.Add("当敌人攻击所属部位时，反击敌人等同于层数的伤害，并失去2层荆棘");
                        break;
                    case EntryType.SlowRecovery:
                        EntryInfoNameList.Add("缓慢恢复：");
                        EntryInfoList.Add("每回合获得肢体 3X 的基础值 加（4 + 0.5X）% 最大生命值的生命恢复");
                        break;
                    default:
                        break;
                }
            }
        }

        CardFrameImage.sprite = ArtResourceManager.instance.GetCardBackground(card.rareLevel);
        CardBackImage.sprite = ArtResourceManager.instance.GetCardBackgroundSprite(card.cardType);
    }


}
