using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Charge : MonoBehaviour
{
    public static int DamagePerGrid = 35;
    public static void UseEntry(CardManager userCardManager, int distance)
    {
        
        int currentPos = InGameManager.instance.CharacterPositions[userCardManager.camp];
        En_Movement.UseEntry(userCardManager, distance);
        int newPos = InGameManager.instance.CharacterPositions[userCardManager.camp];
        if (newPos != currentPos)
        {
            int dis = Mathf.Abs(newPos - currentPos);
            BuffManager buffManager = InGameManager.instance.BuffManagers[userCardManager.camp];
            CharacterManager characterManager = InGameManager.instance.Characters[userCardManager.camp];
            Buff_AttackChange buff = new Buff_AttackChange(BuffType.AttackChange, 1, DamagePerGrid * dis,BuffDurationType.OneFrame);
            buffManager.AddBuff(buff, characterManager.GetBodyPart(BodyPartType.Chest));
        }
    }
}
