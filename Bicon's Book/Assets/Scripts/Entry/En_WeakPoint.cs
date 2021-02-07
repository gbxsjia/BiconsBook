using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_WeakPoint : MonoBehaviour
{
    public static void UseEntry(BodyPart bodyPart, int camp,int amount)
    {
        if (bodyPart!=null)
        {
            bodyPart.thisCharacter.GetComponent<BuffManager>().AddWeakPointBuff(bodyPart);
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                InGameManager.instance.Characters[1 - camp].GetComponent<BuffManager>().AddWeakPointBuff(bodyPart);
            }           
        }
    }
}
