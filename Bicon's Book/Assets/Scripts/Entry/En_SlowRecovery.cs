using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_SlowRecovery : MonoBehaviour
{
    public static void UseEntry(int stack, BodyPart AllyBodyPart)
    {
        Buff_SlowRecovery thisBuff = new Buff_SlowRecovery(BuffType.SlowRecovery, 1, stack);

        AllyBodyPart.thisCharacter.GetComponent<BuffManager>().AddBuff(thisBuff, AllyBodyPart);
    }
}
