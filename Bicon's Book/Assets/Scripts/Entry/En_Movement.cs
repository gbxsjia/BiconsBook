using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Movement : MonoBehaviour
{
    public static void UseEntry(CardManager user, int distance)
    {
        InGameManager.instance.CharacterMoveAndUgradeGrids(user.camp, distance * (1 - 2 * user.camp));
    }
}
