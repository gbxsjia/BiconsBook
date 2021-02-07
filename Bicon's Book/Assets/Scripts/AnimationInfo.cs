using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInfo
{
    public AnimationInfo()
    {
        isSpecialPerformance = true;
    }
    public AnimationInfo(int casterCamp,int opponentCamp,AnimType casterAnimType,AnimType opponentAnimType,
        TargetHeightType casterHeightType, TargetHeightType opponentHeightType, 
        AnimDirection casterAnimDirection, AnimDirection opponentAnimDirection, 
        AnimationMovementType casterMoveType, AnimationMovementType opponentMoveType, 
        bool hasTarget,bool useCameraEffect,
        Card_Base casterCard, Card_Base opponentCard,BodyPart targetBodyPart,
        List<TextEffectInfo> textEffectInfos,
        AudioClip castSound,AudioClip hitSound ,List<CardEffectInfo> cardEffectInfos=null)
    {
        CasterCamp = casterCamp;
        OpponentCamp = opponentCamp;
        CasterAnimType = casterAnimType;
        OpponentAnimType = opponentAnimType;
        CasterHeightType = casterHeightType;
        OpponentHeightType = opponentHeightType;
        CasterAnimDirection = casterAnimDirection;
        OpponentAnimDirection = opponentAnimDirection;
        CasterMoveType = casterMoveType;
        OpponentMoveType = opponentMoveType;
        HasTarget = hasTarget;
        useCameraEffect = UseCmeraEffect;
        CasterCard = casterCard;
        OpponentCard = opponentCard;
        TargetBodypart = targetBodyPart;
        TextEffectInfos = textEffectInfos;
        CardEffectInfos = cardEffectInfos;
        CastSound = castSound;
        HitSound = hitSound;
    }

    public AnimationInfo (int casterCamp,int casterStart,int casterEnd,int opponentPos)
    {
        CasterCamp = casterCamp;
        isMovement = true;
        CasterStart = casterStart;
        CasterEnd = casterEnd;
        OpponentPos = opponentPos;
    }

    public int CasterCamp;
    public int OpponentCamp;
    public AudioClip CastSound;
    public AudioClip HitSound;
    public AnimType CasterAnimType;
    public AnimType OpponentAnimType;
    public TargetHeightType CasterHeightType;
    public TargetHeightType OpponentHeightType;
    public AnimDirection CasterAnimDirection;
    public AnimDirection OpponentAnimDirection;
    public AnimationMovementType CasterMoveType;
    public AnimationMovementType OpponentMoveType;
    public bool HasTarget;
    public bool UseCmeraEffect;
    public Card_Base CasterCard;
    public Card_Base OpponentCard;
    public BodyPart TargetBodypart;
    public List<TextEffectInfo> TextEffectInfos;
    public List<CardEffectInfo> CardEffectInfos;

    public bool isMovement=false;
    public bool isSpecialPerformance = false;
    public int CasterStart;
    public int CasterEnd;
    public int OpponentPos;
}
public enum AnimationMovementType
{
    None,
    SmallForward,
    SmallBackward,
    MediumForward,
    MediumBackward,
    BigForward,
    BigBackward,
}