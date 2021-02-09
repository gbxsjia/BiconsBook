using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Sacrifice : MonoBehaviour
{
    public static void UseEntry(CardManager useManager, BodyPart targetBodyPart, int percent)
    {
        Buff_Sacrifice buff = new Buff_Sacrifice(BuffType.Sacrifice, 1, 1);
        BuffManager buffManager = useManager.GetComponent<BuffManager>();
        CharacterManager characterManager = useManager.GetComponent<CharacterManager>();
        buffManager.AddBuff(buff, characterManager.GetBodyPart(BodyPartType.Chest), true);

        targetBodyPart.TakeDirectHealthDamage(targetBodyPart.GetMaxHealth() * percent/100, false, false);
    }
}
