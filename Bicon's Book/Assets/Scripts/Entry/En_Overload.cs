using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Overload : MonoBehaviour
{
    public static void UseEntry(CardManager a,int b)
    {
        a.StaminaChangeMinus += b;
        Debug.Log(a.StaminaChangeMinus);
    }
}
