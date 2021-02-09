using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_BleedSeed : MonoBehaviour
{
    public static void UseEntry(BodyPart targetBodyPart, int stack)
    {
        BodyPart EnemyChest = targetBodyPart.thisCharacter.GetBodyPart(BodyPartType.Chest);
        Buff_BleedSeed thisBuff = new Buff_BleedSeed(BuffType.BleedSeed, 1, stack);

        targetBodyPart.thisCharacter.GetComponent<BuffManager>().AddBuff(thisBuff, EnemyChest, true);
    }
}
