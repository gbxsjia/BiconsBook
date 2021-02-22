using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationController : MonoBehaviour
{
    public static bool isInAnimationMode;

    public Canvas CurtainCanvas;
    public Canvas CardCanvas;
    public GameObject AnimationCurtain;
    public Animator animator;
    public Transform TrailParent;
    public Transform LeftTrailPoint;
    public Transform RightTrailPoint;

    public GameObject CardDisplayPrefab;

    private BattleAnimData battleAnimData;
    private SingleAnimData[] singleAnims;

    private Camera MainCam;

    private float DurationMoveToCenter=0.5f;
    private float DurationMoveToSide=0.5f;
    private float DurationShowCard=2;
    [SerializeField]
    private float CameraZDistance;

    private Vector3 CasterStartPos;
    private Vector3 OpponentStartPos;
    private void Start()
    {
        MainCam = Camera.main;
        CurtainCanvas.worldCamera = MainCam; 
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

        battleAnimData.CasterObject.transform.position = CasterStartPos;
        battleAnimData.OpponentObject.transform.position = OpponentStartPos;
    }
    public void StartBattleAnimation(BattleAnimData data, SingleAnimData[] singleData)
    {
        if (!isInAnimationMode)
        {
            EnterAnimationMode();
        }
        CasterStartPos = data.CasterObject.transform.position;
        OpponentStartPos = data.OpponentObject.transform.position;

        battleAnimData = data;
        singleAnims = singleData;
        if (!data.CasterOnLeft)
        {
            TrailParent.transform.localScale = new Vector3(-1, 1, 1);
        }
        StartCoroutine(ShowCardProgress());
    }

    private IEnumerator ShowCardProgress()
    {
        // Card display part
        GameObject casterCardDisplay = Instantiate(CardDisplayPrefab, CardCanvas.transform);
        Card_Appearance casterCardAppearance = casterCardDisplay.GetComponent<Card_Appearance>();
        casterCardAppearance.card = battleAnimData.CasterCard;
        casterCardDisplay.transform.position = new Vector3(0, -200);

        float timer = DurationMoveToCenter;;
        Vector3 startPos = casterCardDisplay.transform.position;
        Vector3 endPos = Vector3.zero;
        while (timer > 0)
        {
            casterCardDisplay.transform.localPosition = Vector3.Lerp(startPos, endPos, 1- timer / DurationMoveToCenter);
            timer -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        GameObject opponentCardDisplay = null;
        if (battleAnimData.OpponentCard)
        { 

            timer = DurationMoveToSide;
            startPos = Vector3.zero;
            endPos = new Vector3(150,0);
            if (battleAnimData.CasterOnLeft)
            {
                endPos = new Vector3(-150, 0);
            }
            while (timer > 0)
            {
                casterCardDisplay.transform.localPosition = Vector3.Lerp(startPos, endPos, 1- timer / DurationMoveToSide);
                timer -= Time.deltaTime;
                yield return null;
            }

            opponentCardDisplay = Instantiate(CardDisplayPrefab, CardCanvas.transform);
            Card_Appearance opponentCardAppearance = opponentCardDisplay.GetComponent<Card_Appearance>();
            opponentCardAppearance.card = battleAnimData.OpponentCard;
            opponentCardDisplay.transform.localPosition = new Vector3(150, 0);
        }

        // Play some Effect!

        timer = 2;
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


        StartCoroutine(BattleAnimProgress());
    }
    private IEnumerator BattleAnimProgress()
    {

        int animTimes = singleAnims.Length;
        int currentIndex=0;

        CharacterManager CasterCharacter = battleAnimData.CasterObject.GetComponent<CharacterManager>();
        CharacterManager OpponentCharacter = battleAnimData.OpponentObject.GetComponent<CharacterManager>();

        while (currentIndex < animTimes)
        {
            // Camera Shake
            if (CameraManager.instance)
            {
                CameraManager.instance.CameraShake(0.2f);
            }

            animator.Play(singleAnims[currentIndex].TrailClipName,0,0);

            CasterCharacter.PlayAnimation(singleAnims[currentIndex].CasterAnimName);
            OpponentCharacter.PlayAnimation(singleAnims[currentIndex].OpponentAnimName);

            yield return null;
            float timer = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            while (timer > 0)
            {
                timer -= Time.deltaTime;

                Vector3 position = LeftTrailPoint.position;
                position.z = MainCam.transform.position.z + CameraZDistance;
                battleAnimData.CasterObject.transform.position = position;

                position = RightTrailPoint.position;
                position.z = MainCam.transform.position.z + CameraZDistance;
                battleAnimData.OpponentObject.transform.position = position;
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