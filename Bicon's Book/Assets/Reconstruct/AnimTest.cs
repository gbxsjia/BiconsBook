using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    public AnimDirection direction;
    public BodyPartType targetType;

    [SerializeField]
    BattleAnimationController animationController;
    public BattleAnimData data;
    private void Start()
    {
        var CardAnimInfo = data.CasterCard.AnimData;
        SingleAnimData[] single= new SingleAnimData[CardAnimInfo.Length];
        for (int i = 0; i < single.Length; i++)
        {
            single[i] = new SingleAnimData();
            single[i].TrailClipName = CardAnimInfo[i].TrailClipName;
            single[i].ShowOpponent = CardAnimInfo[i].ShowOpponent;
            single[i].CasterAnimName = AnimationManager.GetAnimationName(CardAnimInfo[i].CasterType, InGameManager.BodyTypeToHeightIndex(targetType), direction);
            single[i].OpponentAnimName = AnimationManager.GetAnimationName(CardAnimInfo[i].OpponentType, InGameManager.BodyTypeToHeightIndex(targetType), CardManager.BodyPartToDirection(targetType));
        }
        animationController.StartBattleAnimation(data,single);
    }
}
