using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_WindGate : SE_Base
{
    //风门事件----------------------------------------------
    private void Start()
    {
        if (KeyWord_1 != null)
        {
            if (EnemyIncubator.thisInstance.InAppearRange(27))
            {
                KeyWord_1.text = "流亡者";
            }
            else
            {
                KeyWord_1.text = "土匪";
            }
        }
    }
    public void UseWindGate()
    {
        if (EnemyIncubator.CurrentMapID == EnemyIncubator.thisInstance.FinalBossMapID - 1)
        { EnemyIncubator.thisInstance.FlyMap(1); }
        else
        {
            EnemyIncubator.thisInstance.FlyMap(2);
        }
        Destroy(gameObject);
    }
    public void UseWindGateEnemy()
    {
        if (EnemyIncubator.thisInstance.InAppearRange(27))
        {
            EnemyIncubator.thisInstance.CreateNewStyleEnemy(4, EnemyStyle.Wd_1, MapPointDiffculty.Medium);
        }
        else
        {
            EnemyIncubator.thisInstance.CreateNewStyleEnemy(4, EnemyStyle.Tf, MapPointDiffculty.Medium);
        }

        ArtResourceManager.instance.GenerateBattleScene(BattleSceneType.碎石);
        UIManager.instance.UseCurtain(0f, 2f);
        EnemyIncubator.thisInstance.SelectEnemy(4);
        EnemyIncubator.thisInstance.EquipAllItem();
        UIManager.instance.BattleStart();
        AudioManager.instance.PlayBattleStartSound();
        if (EnemyIncubator.CurrentMapID != EnemyIncubator.thisInstance.FinalBossMapID - 1)
        {
            InGameManager.instance.FlyAmount = 2;
        }
        Destroy(gameObject);
    }
}
