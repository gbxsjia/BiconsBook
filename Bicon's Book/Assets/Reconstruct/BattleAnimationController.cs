using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationController : MonoBehaviour
{
    public Animator animator;
    public Transform TrailParent;
    public Transform LeftTrailPoint;
    public Transform RightTrailPoint;

    private BattleAnimData battleAnimData;
    private SingleAnimData[] singleAnims;
    public void StartBattleAnimation(BattleAnimData data, SingleAnimData[] singleData)
    {
        battleAnimData = data;
        singleAnims = singleData;
        if (!data.CasterOnLeft)
        {
            TrailParent.transform.localScale = new Vector3(-1, 1, 1);
        }
        StartCoroutine(OneCardAnimProgress());
    }
    private IEnumerator OneCardAnimProgress()
    {
        int animTimes = singleAnims.Length;
        int currentIndex=0;

        CharacterManager CasterCharacter = battleAnimData.CasterObject.GetComponent<CharacterManager>();
        CharacterManager OpponentCharacter = battleAnimData.OpponentObject.GetComponent<CharacterManager>();

        while (currentIndex < animTimes)
        {
            animator.Play(singleAnims[currentIndex].TrailClipName,0,0);

            CasterCharacter.PlayAnimation(singleAnims[currentIndex].CasterAnimName);
            OpponentCharacter.PlayAnimation(singleAnims[currentIndex].OpponentAnimName);

            yield return null;
            float timer = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                battleAnimData.CasterObject.transform.position = LeftTrailPoint.position;
                battleAnimData.OpponentObject.transform.position = RightTrailPoint.position;
                yield return null;
            }

            currentIndex++;
            yield return null;
        }
    }
}

[System.Serializable]
public class CardAnimInfo
{
    public string TrailClipName;
    public bool ShowOpponent;
    public AnimType CasterType;
    public AnimType OpponentType;
}

[System.Serializable]
public class SingleAnimData
{
    public string TrailClipName;
    public bool ShowOpponent;
    public string CasterAnimName;
    public string OpponentAnimName;
}

[System.Serializable]
public class BattleAnimData
{
    public BattleAnimData(GameObject casterObject,GameObject opponentObject,Card_Base casterCard,Card_Base opponentCard, bool casterOnLeft ,float playRate=1,string opponentAnimOverride = "")
    {
        CasterObject = casterObject;
        OpponentObject = opponentObject;
        CasterCard = casterCard;
        OpponentCard = opponentCard;
        CasterOnLeft = casterOnLeft;
        PlayRate = playRate;
        OpponentAnimOverride = opponentAnimOverride;
    }

    public GameObject CasterObject;
    public GameObject OpponentObject;
    public float PlayRate;
    public Card_Base CasterCard;
    public Card_Base OpponentCard;
    public bool CasterOnLeft;
    public string OpponentAnimOverride;
}