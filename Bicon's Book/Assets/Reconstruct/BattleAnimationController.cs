using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationController : MonoBehaviour
{
    public static bool isInAnimationMode;

    public Canvas CurtainCanvas;
    public GameObject AnimationCurtain;
    public Animator animator;
    public Transform TrailParent;
    public Transform LeftTrailPoint;
    public Transform RightTrailPoint;

    public GameObject CardDisplayPrefab;

    private BattleAnimData battleAnimData;
    private SingleAnimData[] singleAnims;


    private void Start()
    {
        CurtainCanvas.worldCamera = Camera.main;
    }
    private void EnterAnimationMode()
    {
        isInAnimationMode = true;
        if (CameraManager.instance)
        {
            CameraManager.instance.SetFollowCharacters(false);
        }

        AnimationCurtain.SetActive(true);
    }

    private void ExitAnimationMode()
    {
        isInAnimationMode = false;

        CharacterManager CasterCharacter = battleAnimData.CasterObject.GetComponent<CharacterManager>();
        CharacterManager OpponentCharacter = battleAnimData.OpponentObject.GetComponent<CharacterManager>();

        CasterCharacter.PlayIdle();
        OpponentCharacter.PlayIdle();

        //InGameManager.instance.Characters[0].SetSortingLayers(SortingLayerNames[2]);
        //InGameManager.instance.Characters[1].SetSortingLayers(SortingLayerNames[1]);

        if (CameraManager.instance)
        {
            CameraManager.instance.SetFollowCharacters(true);
            // Camera Effect Reset
            CameraManager.instance.CameraReset();
        }

        AnimationCurtain.SetActive(false);
    }
    public void StartBattleAnimation(BattleAnimData data, SingleAnimData[] singleData)
    {
        if (!isInAnimationMode)
        {
            EnterAnimationMode();
        }

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
        // Card display part
        GameObject casterCardDisplay = Instantiate(CardDisplayPrefab,CurtainCanvas.transform);
        Card_Appearance casterCardAppearance = casterCardDisplay.GetComponent<Card_Appearance>();
        casterCardAppearance.card = battleAnimData.CasterCard;
        casterCardDisplay.transform.position = battleAnimData.CasterObject.transform.position;

        GameObject opponentCardDisplay = null;
        if (battleAnimData.OpponentCard)
        {
            opponentCardDisplay = Instantiate(CardDisplayPrefab, CurtainCanvas.transform);
            Card_Appearance opponentCardAppearance = casterCardDisplay.GetComponent<Card_Appearance>();
            opponentCardAppearance.card = battleAnimData.OpponentCard;
            opponentCardDisplay.transform.position = battleAnimData.OpponentCard.transform.position;
        }

        float timer = 2;
        while (timer > 0)
        {

            timer -= Time.deltaTime;
            yield return null;
        }
        Destroy(casterCardDisplay);

        if (battleAnimData.OpponentObject)
        {
            Destroy(opponentCardDisplay);
        }
        // Character animation part

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
            timer = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
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
        ExitAnimationMode();
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