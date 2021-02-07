using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class En_Shock : MonoBehaviour
{
    public static void UseEntry(Card_Base card, CardManager useManager, int amount)
    {
        BodyPart targetBodyPart = InGameManager.instance.Characters[1 - useManager.camp].GetBodyPart(BodyPartType.Chest);
        Buff_Shock buff = new Buff_Shock(BuffType.Shock, 1, amount);
        targetBodyPart.thisCharacter.GetComponent<BuffManager>().AddBuff(buff,targetBodyPart,true);
        InGameManager.instance.CardManagers[1 - useManager.camp].DrawCardAmountFix -= amount;
    }


}
