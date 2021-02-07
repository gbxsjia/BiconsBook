using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_ArmorHeal : MonoBehaviour
{
    public static void UseEntry(BodyPart a,Equipment thisEqm,int b, float c)
    {
        a.ArmorHeal(thisEqm.MaxArmor * c/100f + b);
    }
}
