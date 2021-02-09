using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_RecoverLoad : MonoBehaviour
{
    public static void UseEntry(CardManager a, BodyPart targetBodyPart, int DamageAmount)
    {
        //Debug.Log(a.StaminaChangeMinus);
        for (int i = a.StaminaChangeMinus - 1; i >= 0; i--)
        {
            targetBodyPart.TakeDamage(DamageAmount, targetBodyPart);
        }
        a.StaminaChangeMinus = 0;
        //Debug.Log(a.StaminaChangeMinus);
    }
}
