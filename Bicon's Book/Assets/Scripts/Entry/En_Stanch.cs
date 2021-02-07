using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Stanch : MonoBehaviour
{
    public static void UseEntry(BodyPart BodyPart, CardManager cardManager, int amount)
    {
        cardManager.GetComponent<BuffManager>().RemoveBleedBuff(BodyPart,amount);
    }
}
