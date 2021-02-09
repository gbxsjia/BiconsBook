using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_overLoadDamage : MonoBehaviour
{

    public static void UseEntry(CardManager a, BodyPart targetBodyPart, int damage, int baseDamage)
    {
        for (int i = a.tempInt - 1; i >= 0; i--)
        {
            targetBodyPart.TakeDamage(damage + baseDamage, targetBodyPart);
        }
    }
}
