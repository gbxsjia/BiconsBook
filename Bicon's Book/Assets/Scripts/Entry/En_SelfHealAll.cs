using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_SelfHealAll : MonoBehaviour
{
    public static void UseEntry(CharacterManager aC,int b,int c)
    {
        foreach(BodyPart a in aC.bodyParts)
        { 
            a.HealthHeal(b+a.HealthMax * c / 100);
        }
       
    }
}
