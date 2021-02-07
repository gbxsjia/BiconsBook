using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_LifeSpring : SE_Base
{
    public void TakeWash()
    {        
        int RandomNum = Random.Range(0, 3);
        
        
        if(AchievementSystem.instance.a_SpringFinderUnblock)
        {
            RandomNum = Random.Range(0, 4);
        }
        EnemyIncubator.thisInstance.PassTime();
        switch(RandomNum)
        {
            case (0):
                KeyWord_1.text = "在一段时间的浸泡后，你的身体得到了巨大的恢复（75%）, 并且你感觉你的身体变得更强壮了，全体生命上限 + 1。";
                CharacterManager.PlayerInstance.RecoverAllBodyHealth(0, 0.75f);
                UpGradeManager.instance.UpgradeAllPart(1);
                break;
            case (1):
                KeyWord_1.text = "在一段时间的浸泡后，你的身体得到了一定的恢复（50%），并且你感觉你的身体变得更强壮了，全体生命上限 + 3。";
                CharacterManager.PlayerInstance.RecoverAllBodyHealth(0, 0.5f);
                UpGradeManager.instance.UpgradeAllPart(3);
                break;
            case (2):
                AchievementSystem.instance.a_SpringFinderCount += 1;
                KeyWord_1.text = "在一段时间的浸泡后，你的身体得到了小量的恢复（25%），并且你感觉你的身体变得更强壮了，全体生命上限 + 5。";
                CharacterManager.PlayerInstance.RecoverAllBodyHealth(0, 0.25f);
                UpGradeManager.instance.UpgradeAllPart(5);
                break;
            case (3):
                KeyWord_1.text = "在一段时间的浸泡后，你的身体得到了小量的恢复（100%），并且你感觉你的身体变得更强壮了，全体生命上限 + 12。";
                CharacterManager.PlayerInstance.RecoverAllBodyHealth(0, 1f);
                UpGradeManager.instance.UpgradeAllPart(8);
                break;
        }

        CameraManager.instance.CameraShake(0.1f);
    }

    
}
