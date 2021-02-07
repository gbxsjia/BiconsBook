using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_StaminaRecover : MonoBehaviour
{
    public static void UseEntry(CardManager a,int b)
    {
        a.StaminaCurrent += b;
    }
}
