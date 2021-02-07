using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Fear : MonoBehaviour
{
    public static void UseEntry(CardManager useManager,int stack )
    {
        BodyPart targetBodyPart = InGameManager.instance.Characters[1 - useManager.camp].GetBodyPart(BodyPartType.Chest);
        Buff_Fear buff = new Buff_Fear(BuffType.Fear, stack, 0);
        targetBodyPart.thisCharacter.GetComponent<BuffManager>().AddBuff(buff, targetBodyPart, true);
        
    }
}
