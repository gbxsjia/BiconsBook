using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFeedBack
{

    public AttackFeedBack(BodyPart bodyPart, AnimType animType)
    {
        newBodyPart = bodyPart;
        newAnimType = animType;
    }
    public BodyPart newBodyPart;
    public AnimType newAnimType;
    public bool isDefended;
    public Card_Base defendCard;
}
