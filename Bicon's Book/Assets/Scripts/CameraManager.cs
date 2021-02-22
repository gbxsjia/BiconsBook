using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public static CameraManager instance;
    [SerializeField]
    private CameraShaker cameraShaker;
    public Camera MainCam;

    public AnimationCurve YAngleCurve;
    public AnimationCurve ZAngleCurve;

    public Vector3[] ChacaterAnimPositions = { new Vector3(5, 0, 14.5f), new Vector3(-5, 0, 14.5f) };

    public Vector3 EquipPannelCharacterPosition;
    public Vector3 CampCameraPosition;
    public Vector3 MapCameraPosition;
    public Vector3 SkyCameraPosition;
    public Vector3 TownCameraPosition;

    [SerializeField] private CameraPositionState cameraPositionState = CameraPositionState.Sky;

    [SerializeField]
    private float YRot = 12f;
    [SerializeField]
    private float InitialZ = -20f;
    [SerializeField]
    private float ZChangeRate = 0;
    
    private void Awake()
    {
        instance = this;
        MainCam = GetComponent<Camera>();
        for (int i = 0; i < ChacaterAnimPositions.Length; i++)
        {
            ChacaterAnimPositions[i].y = YOffset;
        }
    }

    private void Start()
    {
        if (InGameManager.instance)
        {
            InGameManager.instance.OnTurnChanged += OnTurnChange;
            InGameManager.instance.BattleStartEvent += OnBattleStart;
            InGameManager.instance.ReturnToCampEvent += CameraReset;
        }
    }

    private void OnBattleStart()
    {
        FollowCharacters = true;
    }
    public void CameraShake(float degree)
    {
        cameraShaker.UseCameraShake(degree);
    }
    IEnumerator WinCameraFlyMove()
    {
        UIManager.instance.UseCurtain(0.1f,0.6f);
        yield return new WaitForSeconds(1.3f);
        EnemyIncubator.thisInstance.FlyMap(InGameManager.instance.FlyAmount);
        InGameManager.instance.FlyAmount = 1;
    }

    IEnumerator LoseCameraMove()
    {
        UIManager.instance.UseCurtain(0.1f, 0.6f);
        yield return new WaitForSeconds(1.3f);
        InGameManager.instance.FlyAmount = 1;
        CameraManager.instance.cameraPositionState = CameraPositionState.Camp;
    }
    public void OnTurnChange(TurnState state)
    {
        switch (state)
        {
            case TurnState.TurnStart:
                cameraPositionState = CameraPositionState.Battle;
                FollowCharacters = true;
                StartCoroutine(TurnCameraAngle(YRot*(1-2*InGameManager.instance.CampTurn)));

                break;
            case TurnState.Null:
                if (EnemyIncubator.CurrentMapID >= 0)
                {
                    if (InGameManager.instance.PlayerWin == true)
                    {
                        StartCoroutine(WinCameraFlyMove());
                    }
                    else
                    {
                        InGameManager.instance.PlayerWin = true;
                        StartCoroutine(LoseCameraMove());
                    }
                }
                FollowCharacters = false;
                break;
        }
    }

    [SerializeField]
    private float ZAgnel = 12;
    [SerializeField]
    private float Duration = 0.5f;
    [SerializeField]
    private float YOffset;
    private bool FollowCharacters = false;
    public void SetFollowCharacters(bool isFollow)
    {
        FollowCharacters = isFollow;
    }

    public void TurnZAngle(float duration)
    {
        StartCoroutine(TurnZAngleProcess(duration));
    }

    private IEnumerator TurnZAngleProcess(float duration)
    {
        float timer = 0;

        Quaternion TargetRot = Quaternion.Euler(0, transform.eulerAngles.y, 1 - 2 * ZAgnel);
        Quaternion StartRot = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        while (timer <=duration)
        {
            transform.rotation = Quaternion.Slerp(StartRot, TargetRot, ZAngleCurve.Evaluate(timer / duration));
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void CameraReset()
    {
        StartCoroutine(CameraResetProcess());
    }

    public void SetCameraState(CameraPositionState state)
    {
        cameraPositionState = state;
    }

    public void SetCameraStateCamp()
    {
        cameraPositionState = CameraPositionState.Camp;
    }
    public CameraPositionState GetCameraState()
    {
        return cameraPositionState;
    }

    [SerializeField] GameObject focusObj;
    public void SetCameraState(CameraPositionState state, GameObject aGameObject)
    {
        focusObj = aGameObject;
        cameraPositionState = state;

    }
    private IEnumerator CameraResetProcess()
    {
        float timer = 0.5f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 0.1f);
            yield return null;
        }
        transform.rotation = Quaternion.identity;       
    }

    private IEnumerator TurnCameraAngle(float TargetAngle)
    {
        float timer = 0;

        Quaternion TargetRot = Quaternion.Euler(0, TargetAngle, transform.eulerAngles.z);
        Quaternion StartRot = transform.rotation;

        while (timer <= Duration)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(StartRot, TargetRot, YAngleCurve.Evaluate(timer / Duration));
            yield return null;
        }
    }

    private void LateUpdate()
    {
        switch (cameraPositionState)
        {
            case CameraPositionState.ToPoint:
                {
                    Vector3 position = focusObj.transform.position;
                    transform.position = Vector3.Lerp(transform.position, position, 0.06f);
                }
                break;

            case CameraPositionState.Town:
                {
                    transform.position = Vector3.Lerp(transform.position, TownCameraPosition, 0.06f);
                }
                break;


            case CameraPositionState.Battle:
                if (FollowCharacters)
                {
                    Vector3 position = (InGameManager.instance.Characters[0].transform.position + InGameManager.instance.Characters[1].transform.position) / 2;
                    position.z = InitialZ - ZChangeRate * InGameManager.instance.GetDistance();
                    position.y -= YOffset;
                    transform.position = Vector3.Lerp(transform.position, position, 0.06f);
                }
                break;
            case CameraPositionState.Camp:
                transform.position = Vector3.Lerp(transform.position, CampCameraPosition, 0.06f);
                break;
            case CameraPositionState.Equipment:
                transform.position = Vector3.Lerp(transform.position, EquipPannelCharacterPosition, 0.06f);
                break;
            case CameraPositionState.Map:
                transform.position = Vector3.Lerp(transform.position, MapCameraPosition, 0.06f);
                break;
            case CameraPositionState.Sky:
                transform.position = Vector3.Lerp(transform.position, SkyCameraPosition, 0.12f);
                break;
        }

    }
}

public enum CameraPositionState
{
    Battle,
    Camp,
    ToPoint,
    Equipment,
    Map,
    Sky,
    Town,
    Sleep
}