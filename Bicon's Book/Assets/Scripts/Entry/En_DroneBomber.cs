using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_DroneBomber : MonoBehaviour
{
    public static void UseEntry(CardManager useManager, BodyPart targetBodyPart)
    {
        BuffManager buffManager = InGameManager.instance.BuffManagers[useManager.camp];
        BodyPart Chest = InGameManager.instance.Characters[useManager.camp].GetBodyPart(BodyPartType.Chest);

        Buff_Drone drone = buffManager.GetBuffOnBodypart(Chest, BuffType.Drone) as Buff_Drone;
        if (drone != null)
        {
            for (int i = drone.Amount - 1; i >= 0; i--)
            {
                Debug.Log(i);
                targetBodyPart.TakeDamage(9, targetBodyPart);
            }
            buffManager.RemoveBuffByType(BuffType.Drone, Chest);
        }
    }
}
