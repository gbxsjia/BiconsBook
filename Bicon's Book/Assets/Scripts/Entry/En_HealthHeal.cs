using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_HealthHeal : MonoBehaviour
{
    public static void UseEntry(BodyPart a,int b,int c)
    {
        a.HealthHeal(a.HealthMax * c/100 + b);
    }
}
