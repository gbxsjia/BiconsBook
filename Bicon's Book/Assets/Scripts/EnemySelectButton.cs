using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;  

public enum ButtonType
    {
        Enemy,
        GoToTown,
        BackCamp,
        Inn,
        Map,
        Equipment,
        Upgrade,
        Smithy,
        TrainingHall,
        Sleep,
        SpecialEvent
    }

public enum MapPointDiffculty
{
    NoEnemy,
    Easy,
    Medium,
    Hard
}
public class EnemySelectButton : MonoBehaviour
{

    public int SelectEnemyID;
    public string MapName;
    public BattleSceneType sceneType;

    public bool LostGameOver = false;
    public int EnemyLevel = 1;

    public string MapDiscription;
    public EnemyStyle m_EnemyStyle;
    public MapPointDiffculty mapPointDiffculty;

    public SpriteRenderer spriteRenderer;
    public int thisMapID = 0;
    public int AppearPower = 0;
    public List<GameObject> ShowObjPrefabList;
    bool ButtonEnabled = true;
    bool AlreadyPressed = false;
    Color thisColor;

    public Animator animator;
    public Animator MapAnimator;

    public bool Active = false;

    [SerializeField] List<Sprite> ThisSprites;

    TextMeshProUGUI PointNameText;

    private void OnMouseExit()
    {
        if (ButtonEnabled == true)
        {
            Color c = new Color(thisColor.r * 0.8f, thisColor.g * 0.8f, thisColor.b * 0.8f);

            spriteRenderer.color = c;


            if (animator != null)
            {
                animator.SetBool("MouseEnter", false);
            }
        }
    }
    public void BigMapPoint()
    {
         if (thisPoint == ButtonType.SpecialEvent)
        {
            if (ShowObjPrefabList.Count > 0)
            {
                int aRandom = Random.Range(0, ShowObjPrefabList.Count);
                GameObject Panel = Instantiate(ShowObjPrefabList[aRandom]);
                
                Panel.transform.SetParent(UIManager.instance.transform);
                Panel.transform.localScale = new Vector3(1, 1, 1);
                Panel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                EnemyIncubator.thisInstance.InCameraMotion = true;     
            }
 
        }
        else if (ButtonEnabled == true)
        {
            ArtResourceManager.instance.GenerateBattleScene(sceneType);
         //   StartCoroutine(ButtonPressCool());
            StartCoroutine(MapPointMove(0.3f, 2f, 1f));
        }
    }
    private void OnMouseDown()
    {
        if (AlreadyPressed == false && ButtonEnabled == true)
        {
            if (thisPoint == ButtonType.BackCamp)
            {
                    UIManager.instance.InEqmShop = false;
                    StartCoroutine(ButtonPressCool());
                    StartCoroutine(MapPointMove(0.3f, 2f, 1f));
                    UIManager.instance.UpdateAllEquipmentValue();
                
            }
            else if (thisPoint == ButtonType.Smithy)
            {
                EnemyIncubator.thisInstance.InCameraMotion = true;
                    animator.Play("Open");
                    UIManager.instance.OpenInventoryInShop();
                    UIManager.instance.InEqmShop = true;
                    UIManager.instance.UpdateAllEquipmentValue();
                    UIManager.instance.SetTownPlace(UIManager.TownPlace.BlackSmith);
                    UIManager.instance.BlackSmithSwitchButton.interactable = false;
                    UIManager.instance.ExpendTownSwitchPanel();
                

            }
            else if (thisPoint == ButtonType.TrainingHall)
            {
                EnemyIncubator.thisInstance.InCameraMotion = true;
                animator.Play("Open");
                UIManager.instance.OpenUpgradeInInn();
                UIManager.instance.InTrainingHall = true;
                UIManager.instance.UpdateAllEquipmentValue();
                UIManager.instance.SetTownPlace(UIManager.TownPlace.TrainingHall);
                UIManager.instance.TrainingHallSwitchButton.interactable = false;
                UIManager.instance.ExpendTownSwitchPanel();


            }
            else if (thisPoint == ButtonType.Inn)
            {
                EnemyIncubator.thisInstance.InCameraMotion = true;
                animator.Play("Open");
                    UIManager.instance.OpenInventoryInShop();
                    UIManager.instance.InInn = true;
                    UIManager.instance.UpdateAllEquipmentValue();
                    UIManager.instance.SetTownPlace(UIManager.TownPlace.Inn);
                    UIManager.instance.InnSwitchButton.interactable = false;
                   UIManager.instance.ExpendTownSwitchPanel();
                

            }
            else if (thisPoint == ButtonType.Equipment)
            { }
            else if (thisPoint == ButtonType.Map)
            {
                if (UIManager.MapInfoOpen == true)
                { MapAnimator.Play("Close"); UIManager.MapInfoOpen = false; }
            }
            else if (thisPoint == ButtonType.Upgrade)
            { }
            else if (thisPoint == ButtonType.Sleep)
            { }
            else if (ButtonEnabled == true)
            {
                UIManager.CurrentMapButton = this;
             

                if (thisPoint == ButtonType.Enemy)
                {
                    CharacterManager.EnemyInstance.characterLevel = EnemyLevel;
                    switch (m_EnemyStyle)
                    {
                       
                        case (EnemyStyle.WDBoss):
                            {
                                InGameManager.instance.IsBoss = true;
                                BossStatus Boss = BossStatusManager.Instance.WDBossStatus;
                                EnemyIncubator.thisInstance.SelectBossEnemy(EnemyStyle.WDBoss,Boss);
                                EnemyIncubator.thisInstance.SetEnemyPartsHealth(Boss.HeadHealth, Boss.BodyHealth, Boss.LArmHealth, Boss.RArmHealth, Boss.LLegHealth, Boss.RLegHealth);
                            }
                            break;
                        case (EnemyStyle.Judgement):
                            {
                                InGameManager.instance.IsBoss = true;
                                BossStatus Boss = BossStatusManager.Instance.JudgementStatus;
                                EnemyIncubator.thisInstance.SelectBossEnemy(EnemyStyle.Judgement, Boss);
                                EnemyIncubator.thisInstance.SetEnemyPartsHealth(Boss.HeadHealth, Boss.BodyHealth, Boss.LArmHealth, Boss.RArmHealth, Boss.LLegHealth, Boss.RLegHealth);
                            }
                            break;
                        case (EnemyStyle.TFBoss):
                            {
                                InGameManager.instance.IsBoss = true;
                                BossStatus Boss = BossStatusManager.Instance.TFBossStatus;
                                EnemyIncubator.thisInstance.SelectBossEnemy(EnemyStyle.TFBoss, Boss);
                                EnemyIncubator.thisInstance.SetEnemyPartsHealth(Boss.HeadHealth, Boss.BodyHealth, Boss.LArmHealth, Boss.RArmHealth, Boss.LLegHealth, Boss.RLegHealth);
                            }
                            break;
                        case (EnemyStyle.SunBoss):
                            {
                                InGameManager.instance.IsBoss = true;
                                BossStatus Boss = BossStatusManager.Instance.SunBossStatus;
                                EnemyIncubator.thisInstance.SelectBossEnemy(EnemyStyle.SunBoss, Boss);
                                EnemyIncubator.thisInstance.SetEnemyPartsHealth(Boss.HeadHealth, Boss.BodyHealth, Boss.LArmHealth, Boss.RArmHealth, Boss.LLegHealth, Boss.RLegHealth);
                            }
                            break;

                        default:
                            {
                                InGameManager.instance.IsBoss = false;
                                EnemyIncubator.thisInstance.SelectEnemy(SelectEnemyID);
                            }
                            break;
                    }

                }

                InGameManager.instance.LostGameOver = LostGameOver;
                MapPointInfoUI.Instance.OnMapPointPressed();
                MapPointInfoUI.Instance.UpdateMapPointInfo();
            }
        }

        if (ButtonEnabled == true)
        {
            if (thisPoint == ButtonType.Map || thisPoint == ButtonType.Equipment || thisPoint == ButtonType.Upgrade || thisPoint == ButtonType.Sleep)
            {
                UIManager.instance.OnCampBuildingClicked(thisPoint);
                if (animator != null)
                {
                    if (Active == true)
                    {
                        animator.SetBool("MouseClick", false);
                        Active = false;
                    }
                    else
                    {
                        animator.SetBool("MouseClick", true);
                        Active = true;

                    }
                }
            }
        }
    }

    public ButtonType thisPoint = ButtonType.Enemy;

    private void Update()
    {       
        if (ThisSprites.Count > 0)
        {
            if (thisMapID == EnemyIncubator.CurrentMapID + 2)
            {
                spriteRenderer.sprite = ThisSprites[2];
            }
            else if (thisMapID == EnemyIncubator.CurrentMapID + 1)
            {
                spriteRenderer.sprite = ThisSprites[1];
            }
            else if (thisMapID == EnemyIncubator.CurrentMapID)
            {
                spriteRenderer.sprite = ThisSprites[0];
            }
        }
        if(EnemyIncubator.thisInstance.InCameraMotion == true)
        {
            ButtonEnabled = false;
            return;
        }   
        
         
        if (thisPoint == ButtonType.Equipment)
        {
            if (CameraManager.instance.GetCameraState() == CameraPositionState.Camp || CameraManager.instance.GetCameraState() == CameraPositionState.Equipment)
            {
                ButtonEnabled = true;
            }
            else
            {
                ButtonEnabled =false;
            }
        }
        else if (thisPoint == ButtonType.Upgrade)
        {
            if (CameraManager.instance.GetCameraState() == CameraPositionState.Camp || CameraManager.instance.GetCameraState() == CameraPositionState.Equipment)
            {
                ButtonEnabled = true;
            }
            else
            {
                ButtonEnabled = false;
            }
        }
        else if (thisPoint == ButtonType.Sleep)
        {
            if (CameraManager.instance.GetCameraState() == CameraPositionState.Camp)
            {
                ButtonEnabled = true;
            }
            else
            {
                ButtonEnabled = false;
            }
        }
        else if (thisPoint == ButtonType.Map)
        {
            if (CameraManager.instance.GetCameraState() == CameraPositionState.Camp || CameraManager.instance.GetCameraState() == CameraPositionState.Map)
            {
                ButtonEnabled = true;
            }
            else
            {
                ButtonEnabled = false;
            }
        }
        else if (thisPoint != ButtonType.BackCamp && thisPoint != ButtonType.Inn && thisPoint != ButtonType.TrainingHall && thisPoint != ButtonType.Smithy)
        {

            if (thisMapID == EnemyIncubator.CurrentMapID && AlreadyPressed == false && CameraManager.instance.GetCameraState() == CameraPositionState.Map)
            {
                ButtonEnabled = true;
            }
            else
            {
                ButtonEnabled = false;
            }
        }
        else 
        {
            ButtonEnabled = true;
        }
    }

    
    private void Start()
    {
        if (gameObject.TryGetComponent(out Animator a))
        {
            animator = a;
        }

        MapAnimator = UIManager.instance.m_MapInfoUI.GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        thisColor = spriteRenderer.color;
        spriteRenderer.color = thisColor * new Vector4(0.8f,0.8f,0.8f,1f);
    }
    private void OnMouseEnter()
    {
        if (ButtonEnabled == true)
        {
            Color c = thisColor;

            spriteRenderer.color = c;


            if (animator != null)
            {
                animator.SetBool("MouseEnter", true);
            }
        }
    }
    IEnumerator MapPointMove(float LaMuTime, float LaMuMiddleTime, float WaitTime)
    {
        if (thisPoint == ButtonType.Enemy)
        { AudioManager.instance.PlayBattleStartSound(); }

        UIManager.instance.UseCurtain(LaMuTime, LaMuMiddleTime);
        
        CameraManager.instance.SetCameraState(CameraPositionState.ToPoint, gameObject);

        yield return new WaitForSeconds(WaitTime);

        if (thisPoint == ButtonType.Enemy)
        {
            UIManager.instance.BattleStart();
           
        }
        else if (thisPoint == ButtonType.GoToTown || thisPoint == ButtonType.BackCamp)
        {
            UIManager.instance.GoToTown();
        }
        
    }

    IEnumerator ButtonPressCool()
    {
        AlreadyPressed = true;

       yield return new WaitForSeconds(2f);

        AlreadyPressed = false;
    }

}
