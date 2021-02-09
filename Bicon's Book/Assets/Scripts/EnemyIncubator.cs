using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyIncubator : MonoBehaviour
{
  public static EnemyIncubator thisInstance;
    public static int CurrentMapID = -11;
    public static int EnemyMapID = -10;
    public static int DayTime = 0;
    public static int Day = 0;
    public int CurrentEnemyPowerMax = 3;
    public int CurrentEnemyPowerMin = 3;

    public int WeakEnemyPowerOffset = -5;

    public int FinalBossMapID = 99; 

    int EmptyEqpRandomLimit = 35;

    [SerializeField] ParticleSystemRenderer[] CloudParticleRenderers;
    [SerializeField] ParticleSystem CloudParticle;

    [Header("Enemy Equipment")]

   public List<Equipment> CurrentEnemyEquipmentList = new List<Equipment>();

   public List<List<Equipment>> EnemyEqm_List1 = new List<List<Equipment>>(); 
   public List<List<Equipment>> EnemyEqm_List2 = new List<List<Equipment>>();
   public List<List<Equipment>> EnemyEqm_List3 = new List<List<Equipment>>();
   public List<List<Equipment>> EnemyEqm_List4 = new List<List<Equipment>>();

    public List<int> EnemyLooking1 = new List<int>();
    public List<int> EnemyLooking2 = new List<int>();
    public List<int> EnemyLooking3 = new List<int>();
    public List<int> EnemyLooking4 = new List<int>();

    public List<List<Equipment>> EventEnemyEqm_List = new List<List<Equipment>>();

    [Header("All Equipment")]

    [SerializeField] List<Equipment> AllEquipment;

    [SerializeField] List<Equipment> AllWeapons;
    [SerializeField] List<Equipment> AllArmArmor;
    [SerializeField] List<Equipment> AllLegArmor;
    [SerializeField] List<Equipment> AllArmor;
    [SerializeField] List<Equipment> AllHelmetArmor;
    [SerializeField] List<Equipment> AllHandArmor;
    [SerializeField] List<Equipment> AllShoulderArmor;
    [SerializeField] List<Equipment> AllFoot;
    [SerializeField] List<Equipment> AllTail;
    [SerializeField] List<Equipment> AllEar;

    [Header("All Equipment In Style")]

    [SerializeField] List<StyleEquipment> AllEquipmentInStyleList;
    [System.Serializable]
    public class StyleEquipment
    {
       public string m_Name;   
        
        [Header("BodyPartEqmCheck")]
        public bool HasBody=true;
        public bool HasHead=true;
        public bool HasWeapon=true;
        public bool TwoWeapon=true;
        public bool HasArm=true;
        public bool TwoArm=true;
        public bool HasLeg=true;
        public bool TwoLeg=true;
        public bool HasHand=true;
        public bool TwoHand=true;
        public bool HasShoulder=true;
        public bool TwoShoulder=true;
        public bool HasShoe=true;
        public bool TwoShoe=true;
        public bool HasTail=true;
        public bool HasEar=true;

        [Header("EqmPool")]
        public List<Equipment> Equipments;

        public List<Equipment> Weapons;
        public List<Equipment> ArmArmor;
        public List<Equipment> LegArmor;
        public List<Equipment> Armor;
        public List<Equipment> HelmetArmor;
        public List<Equipment> HandArmor;
        public List<Equipment> ShoulderArmor;
        public List<Equipment> Foot;
        public List<Equipment> Tail;
        public List<Equipment> Ear;
    }

    [Header("Map Status")]

    [SerializeField] GameObject BackgroundObj;    
    [SerializeField] GameObject MiddlegroundObj;
    [SerializeField] GameObject ForegroundObj;
    [SerializeField] GameObject DisppearedObj;

    [SerializeField] Vector3 BGPos;
    [SerializeField] Vector3 MGPos;
    [SerializeField] Vector3 FGPos;
    [SerializeField] Vector3 DisPos;

    float BGMovePercent=0;
    float MGMovePercent=0;
    float FGMovePercent=0;
    float DisMovePercent =0;

    [SerializeField] List<GameObject> MapPrefabList;

    [SerializeField] Transform CampUIObj;
    [SerializeField] TextMeshProUGUI OpeningText;

    
    float m_CameraSpd = 2;
    private void Update()
    {
        if(BGMovePercent < 1)
        {BGMovePercent += 0.03f * m_CameraSpd;
        }
        if (MGMovePercent < 1)
        {
            MGMovePercent += 0.03f * m_CameraSpd;
        }
        if (FGMovePercent < 1)
        {
            FGMovePercent += 0.03f * m_CameraSpd;
        }
        if (DisMovePercent < 1)
        {
            DisMovePercent += 0.03f * m_CameraSpd;
        }

        if (BackgroundObj != null)
        {
            BackgroundObj.transform.localPosition = Vector3.Lerp(BGPos + new Vector3(0, 20, 50), BGPos, BGMovePercent);
            SpriteRenderer BackSR2 = BackgroundObj.transform.GetChild(2).GetComponent<SpriteRenderer>();
            Color BackColor2 = BackSR2.color;
            BackSR2.color = new Color(1, 1, 1, Mathf.Lerp(BackColor2.a, 1f, 0.2f));
        }

        if (MiddlegroundObj != null)
        {
            MiddlegroundObj.transform.localPosition = Vector3.Lerp(BGPos, MGPos, MGMovePercent);
            SpriteRenderer MiddleSR1 = MiddlegroundObj.transform.GetChild(1).GetComponent<SpriteRenderer>();
            SpriteRenderer MiddleSR2 = MiddlegroundObj.transform.GetChild(2).GetComponent<SpriteRenderer>();
            Color MiddleColor1 = MiddleSR1.color;
            Color MiddleColor2 = MiddleSR2.color;
            MiddleSR1.color = new Color(1, 1, 1, Mathf.Lerp(MiddleColor1.a, 1f, 0.2f));
            MiddleSR2.color = new Color(1, 1, 1, Mathf.Lerp(MiddleColor2.a, 0f, 0.05f));
        }

        if (ForegroundObj != null)
        {
            ForegroundObj.transform.localPosition = Vector3.Lerp(MGPos, FGPos, FGMovePercent);
            SpriteRenderer ForeSR0 = ForegroundObj.transform.GetChild(0).GetComponent<SpriteRenderer>();
            SpriteRenderer ForeSR1 = ForegroundObj.transform.GetChild(1).GetComponent<SpriteRenderer>();
            Color ForeColor0 = ForeSR0.color;
            Color ForeColor1 = ForeSR1.color;
            ForeSR0.color = new Color(1, 1, 1, Mathf.Lerp(ForeColor0.a, 1f, 0.2f));
            ForeSR1.color = new Color(1, 1, 1, Mathf.Lerp(ForeColor1.a, 0f, 0.05f));
        }

        if (DisppearedObj != null)
        {
            DisppearedObj.transform.localPosition = Vector3.Lerp(FGPos, DisPos, DisMovePercent);
        }

 
    }

    public void FlyMap(int Amount)
    {
        
        StartCoroutine(FlyToSky(Amount));
    }

    public void PassTime()
    {
        DayTime += 1;
        if(DayTime == 4)
        {
            Day += 1;
            DayTime = 0;
        }
        EnemyMapID += 1;

        UIManager.instance.UpdateDayTimeUI();
        ReplaceToChase();
        UIManager.instance.UpdateDistanceUIInfo();

    }

    public bool InCameraMotion = false;

    public void SetInCameraMotion(bool a)
    {
        InCameraMotion = a;
    }
    public void Opening()
    {
        StartCoroutine(OpeningTextShow());
        StartCoroutine(InitialFly());
    }

    IEnumerator OpeningTextShow()
    {
       OpeningText.gameObject.SetActive(true);
        while (OpeningText.color.a < 0.95)
        {
            OpeningText.color = new Color(0, 0, 0, Mathf.Lerp(OpeningText.color.a, 1f, 0.04f));
            yield return new WaitForFixedUpdate();
        }
        OpeningText.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1f);
        while (OpeningText.color.a > 0.05)
        {
            OpeningText.color = new Color(0, 0, 0, Mathf.Lerp(OpeningText.color.a, 0f, 0.04f));
            yield return new WaitForFixedUpdate();
        }
        OpeningText.color = new Color(0, 0, 0, 0);
        OpeningText.gameObject.SetActive(false);
    }

    IEnumerator InitialFly()
    {
        InCameraMotion = true;

        yield return new WaitForSeconds(0.5f);     
        AudioManager.instance.PlayInitialFlyWindSound();
        yield return new WaitForSeconds(0.5f);
        CameraManager.instance.SetCameraState(CameraPositionState.Sky);
        float aCloudSpeed = 100;
        for (int i = 0; i < 6; i++)
        {
            CloudParticle.playbackSpeed = aCloudSpeed;
            MapMove();
            yield return new WaitForSeconds(33.3f / m_CameraSpd * Time.deltaTime);
        }
        for (int i = 0; i < 4; i++)
        {
            if (aCloudSpeed > 1)
            {
                m_CameraSpd -= 0.4f;
                aCloudSpeed -= 22f;
            }
            CloudParticle.playbackSpeed = aCloudSpeed;          
            MapMove();
            yield return new WaitForSeconds(33.3f / m_CameraSpd * Time.deltaTime);
           
        }
        CurrentEnemyPowerMax += 1;
        MapMove();
        yield return new WaitForSeconds(33.3f/m_CameraSpd * Time.deltaTime + 0.3f);
        foreach (ParticleSystemRenderer a in CloudParticleRenderers)
        {
            a.sortingLayerID = SortingLayer.NameToID("BattleScene");
        }
        CameraManager.instance.SetCameraState(CameraPositionState.Map);
        yield return new WaitForSeconds(1f);
        CameraManager.instance.SetCameraState(CameraPositionState.Camp);
        if (GameWorldSetting.TutorialOpen == true)
        {
            Tu_BigWorld.instance.StartTutorial(0);
        }
        else
        {
            InCameraMotion = false;
        }
        m_CameraSpd = 0.4f; 
        CloudParticle.playbackSpeed = 1;
        yield return new WaitForSeconds(0.6f);
        UIManager.instance.m_DayUI.GetComponent<Animator>().Play("Expand");
        UIManager.instance.ChaseUIExpand();
    }
    IEnumerator FlyToSky(int Amount)
    {


        CameraManager.instance.SetCameraState(CameraPositionState.Sky);
        CloudParticle.playbackSpeed = 2f;
        yield return new WaitForSeconds(0.75f);
        foreach (ParticleSystemRenderer a in CloudParticleRenderers)
        {
            a.sortingLayerID = SortingLayer.NameToID("Default");
        }
        yield return new WaitForSeconds(1.75f);                   
        PassTime(); 
        CloudParticle.playbackSpeed = 10f + 10 * Amount;
        m_CameraSpd = 0.2f + 0.2f * Amount;
        for (int i = 0; i < Amount; i++)
        {
            CurrentEnemyPowerMax += 2;
            CurrentEnemyPowerMin += 1;        
            MapMove();
            AudioManager.instance.PlayFlyWindSound(0.5f + 0.2f * Amount);
            yield return new WaitForSeconds(33.3f / m_CameraSpd * Time.deltaTime);                     
        } 
        CloudParticle.playbackSpeed = 3f;
            yield return new WaitForSeconds(0.2f);
            CloudParticle.playbackSpeed = 1f;
            foreach (ParticleSystemRenderer a in CloudParticleRenderers)
            {
                a.sortingLayerID = SortingLayer.NameToID("BattleScene");
            }
            
            UIManager.instance.UpdateDistanceUIInfo();
        
        CameraManager.instance.SetCameraState(CameraPositionState.Map);
        if (m_AfterFlyEvent != null)
        {
            m_AfterFlyEvent();
        }
        InCameraMotion = false;
    }
    public event System.Action m_AfterFlyEvent;
    int NextAngle = 1;

    [SerializeField] GameObject m_DarkChaserPoint;

    [SerializeField] int NextBossId = 0;
    [SerializeField] GameObject TestMap;
    [SerializeField] List<GameObject> BossMapPrefabList;
    public List<int> BossMapId;

    public bool InAppearRange(int appearPower)
    {
        if (appearPower > CurrentEnemyPowerMin && appearPower < CurrentEnemyPowerMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    [System.Serializable]
    public class EliteEnemy
    {
        public int AppearMap = 99;
        public List<GameObject> EliteMapPointList;
    }

    [SerializeField] List<EliteEnemy> EliteEnemyList;

    public int AddtionalMapPoint = 0;
    public int AddtionalEventPoint = 0;
    GameObject MapCreate()
    {
        GameObject newMap;

        if (BossMapId.Count > NextBossId && CurrentMapID + 2 == BossMapId[NextBossId])
        {
            newMap = Instantiate(BossMapPrefabList[NextBossId]);
            NextBossId += 1;
        }
        else
        {
            newMap = Instantiate(MapPrefabList[Random.Range(0, MapPrefabList.Count)]);

            MapStatus thisStatus = newMap.GetComponent<MapStatus>();

            for (int i = 0; i < thisStatus.m_EnemyPrefabList.Count; i++)
            {
                int thisPointPower = thisStatus.m_EnemyPrefabList[i].GetComponent<EnemySelectButton>().AppearPower;
                if (InAppearRange(thisPointPower))
                {
                    thisStatus.m_EnemyCurrentPrefabList.Add(thisStatus.m_EnemyPrefabList[i]);
                }
            }

            int MapTotalAmount = Random.Range(3+AddtionalMapPoint, 5+AddtionalMapPoint);

            int EnemyAmount = 0;

            int SpecialAmount = 0;

            int EnemyMaxAmount = 3;

            int SpecialMaxAmount = 1+AddtionalEventPoint;      
            
            int SelectEnemyId = 0;   
            thisStatus.m_AllPoint = new List<GameObject>();

            for(int i = 0; i < EliteEnemyList.Count;i++)
            {
                if (CurrentMapID + 2 == EliteEnemyList[i].AppearMap)
                {
                    EnemyMaxAmount -= 1;
                    GameObject thisPoint;

                    thisPoint = Instantiate(EliteEnemyList[i].EliteMapPointList[Random.Range(0, EliteEnemyList[i].EliteMapPointList.Count)]);
                    thisPoint.GetComponent<EnemySelectButton>().SelectEnemyID = SelectEnemyId;
                    SelectEnemyId += 1;

                    int thisPrefabId = Random.Range(0, thisStatus.m_EnemyPosList.Count);

                    thisPoint.transform.position = thisStatus.m_EnemyPosList[thisPrefabId].transform.position;
                    thisStatus.m_EnemyPosList.Remove(thisStatus.m_EnemyPosList[thisPrefabId]);
                    thisPoint.transform.SetParent(newMap.transform);
                    Vector3 thisPointScale = thisPoint.transform.localScale;

                    int randomX = 1 - Random.Range(0, 2) * 2;

                    thisPoint.transform.localScale = new Vector3(thisPointScale.x * randomX, thisPointScale.y, thisPointScale.z);
                    thisStatus.m_AllPoint.Add(thisPoint);
                    break;
                }
            }


            for (int i = 0; i < MapTotalAmount; i++)
            {
                bool CanFindNext = false;
                while (CanFindNext == false)
                {
                    int aRandom = Random.Range(0, 2);
                    switch (aRandom)
                    {
                        case (0):
                            {
                                if (EnemyAmount < EnemyMaxAmount)
                                {
                                    EnemyAmount += 1;
                                    CanFindNext = true;
                                }
                            }
                            break;
                        case (1):
                            {
                                if (SpecialAmount < SpecialMaxAmount)
                                {
                                    SpecialAmount += 1;
                                    CanFindNext = true;
                                }
                            }
                            break;
                    }
                }
            }

      

            if (thisStatus.m_EnemyCurrentPrefabList.Count > 0)
            {
                for (int i = 0; i < EnemyAmount; i++)
                {
                    GameObject thisPoint;

                    thisPoint = Instantiate(thisStatus.m_EnemyCurrentPrefabList[Random.Range(0, thisStatus.m_EnemyCurrentPrefabList.Count)]);
                    thisPoint.GetComponent<EnemySelectButton>().SelectEnemyID = SelectEnemyId;
                    SelectEnemyId += 1;

                    int thisPrefabId = Random.Range(0, thisStatus.m_EnemyPosList.Count);

                    thisPoint.transform.position = thisStatus.m_EnemyPosList[thisPrefabId].transform.position;
                    thisStatus.m_EnemyPosList.Remove(thisStatus.m_EnemyPosList[thisPrefabId]);
                    thisPoint.transform.SetParent(newMap.transform);
                    Vector3 thisPointScale = thisPoint.transform.localScale;

                    int randomX = 1 - Random.Range(0, 2) * 2;

                    thisPoint.transform.localScale = new Vector3(thisPointScale.x * randomX, thisPointScale.y, thisPointScale.z);
                    thisStatus.m_AllPoint.Add(thisPoint);
                }
            }
         
            int lastBossID = 0;
            if (NextBossId > 0 && NextBossId <= BossMapId.Count)
            {
                lastBossID = NextBossId - 1;
            }


            for (int i = 0; i < SpecialAmount; i++)
            { 
                int aRandomPrefabID = Random.Range(0, thisStatus.m_TownCurrentPrefabList.Count);
                if (BossMapId.Count > NextBossId && (CurrentMapID + 3 == BossMapId[NextBossId] || CurrentMapID + 1 == BossMapId[lastBossID]))
                {
                    aRandomPrefabID = 0;
                   if(i>0)
                    {
                        break;
                    }
                }
                GameObject thisPoint = Instantiate(thisStatus.m_TownCurrentPrefabList[aRandomPrefabID]);
                thisPoint.GetComponent<EnemySelectButton>().SelectEnemyID = i + 4;
                int thisPrefabId = Random.Range(0, thisStatus.m_Town_M_List.Count);

                thisPoint.transform.position = thisStatus.m_Town_M_List[thisPrefabId].transform.position;

                thisStatus.m_Town_M_List.Remove(thisStatus.m_Town_M_List[thisPrefabId]);

                thisPoint.transform.SetParent(newMap.transform);
                Vector3 thisPointScale = thisPoint.transform.localScale;

                int randomX = 1 - Random.Range(0, 2) * 2;

                thisPoint.transform.localScale = new Vector3(thisPointScale.x * randomX, thisPointScale.y, thisPointScale.z);
                thisStatus.m_AllPoint.Add(thisPoint);
            }



        }
        newMap.transform.SetParent(CampUIObj);
        if (NextAngle == 1)
        {
            NextAngle = -1;
        }
        else
        {
            NextAngle = 1;
        }
        newMap.transform.localScale = new Vector3(newMap.transform.localScale.x * NextAngle, newMap.transform.localScale.y, newMap.transform.localScale.z);
        newMap.transform.localPosition = BGPos + new Vector3(0, 20, 50);
        newMap.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        newMap.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        newMap.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        return newMap;
    }

    public bool OnFinalBossFight()
    {
        return (CurrentMapID == FinalBossMapID);
    }
    public void TestMapCreate()
    {

       GameObject newMap = Instantiate(TestMap);
        
        newMap.transform.SetParent(CampUIObj);

        newMap.transform.localScale = new Vector3(newMap.transform.localScale.x * NextAngle, newMap.transform.localScale.y, newMap.transform.localScale.z);
        newMap.transform.localPosition = FGPos;
        newMap.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        newMap.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        newMap.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        Destroy(ForegroundObj);
        ForegroundObj = newMap;

        EnemySelectButton[] NewSelectList = ForegroundObj.GetComponentsInChildren<EnemySelectButton>();
        foreach (EnemySelectButton a in NewSelectList)
        {
            a.thisMapID = CurrentMapID;

        }

        SpriteRenderer[] ForespriteRenderers = ForegroundObj.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer a in ForespriteRenderers)
        {
            a.sortingLayerID = SortingLayer.NameToID("FG"); 
        }

       

    }
    private void MapMove()
    {
        CurrentMapID += 1;

        BGMovePercent = 0;
        MGMovePercent = 0;
        FGMovePercent = 0;
        DisMovePercent = 0;
        
        GameObject newMap = MapCreate();
        newMap.transform.SetParent(CampUIObj);

        if (CurrentMapID > 0)
        {
            Shop.instance.RefreshItem();
        }

        

        EnemySelectButton[] NewSelectList = newMap.GetComponentsInChildren<EnemySelectButton>();
        foreach (EnemySelectButton a in NewSelectList)
        {
            a.thisMapID = CurrentMapID + 2;

        }


        if (ForegroundObj != null)
        {
            SpriteRenderer[] ForespriteRenderers = ForegroundObj.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer a in ForespriteRenderers)
            {
                a.sortingLayerID = SortingLayer.NameToID("FG");
                if(a.gameObject.TryGetComponent(out Cloud cloud))
                {
                    cloud.TargetProcess = 1;
                }

            }
           // ForegroundObj.GetComponent<MapStatus>().CloudObj.transform.localPosition += new Vector3(0, 1, 0);
        }

        if (MiddlegroundObj != null)
        {


            SpriteRenderer[] MiddlespriteRenderers = MiddlegroundObj.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer a in MiddlespriteRenderers)
            {
                a.sortingLayerID = SortingLayer.NameToID("MG");
                if (a.gameObject.TryGetComponent(out Cloud cloud))
                {
                    cloud.TargetProcess = 1f;
                }
            }
         //   MiddlegroundObj.GetComponent<MapStatus>().CloudObj.transform.localPosition += new Vector3(0, 1, 0);

        }
        if (BackgroundObj != null)
        {
            SpriteRenderer[] BackspriteRenderers = BackgroundObj.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer a in BackspriteRenderers)
            {
                a.sortingLayerID = SortingLayer.NameToID("BG");
                if (a.gameObject.TryGetComponent(out Cloud cloud))
                {
                    cloud.TargetProcess = 0.66f;
                }
            }//BackgroundObj.GetComponent<MapStatus>().CloudObj.transform.localPosition += new Vector3(0, 1, 0);
        }

        Destroy(DisppearedObj);
        DisppearedObj = ForegroundObj;
        ForegroundObj = MiddlegroundObj;
        MiddlegroundObj = BackgroundObj;
        BackgroundObj = newMap;

        if (ForegroundObj != null)
        {
            EnemySelectButton[] CurrentEnemyPointList = ForegroundObj.GetComponentsInChildren<EnemySelectButton>();
            for (int i = 0; i < CurrentEnemyPointList.Length; i++)
            {
                if (CurrentEnemyPointList[i].m_EnemyStyle == EnemyStyle.WDBoss)
                {
                    CreateNewStyleEnemy(i, EnemyStyle.WDBoss, CurrentEnemyPointList[i].mapPointDiffculty);
                }
                else if (CurrentEnemyPointList[i].m_EnemyStyle == EnemyStyle.Judgement)
                {
                    CreateNewStyleEnemy(i, EnemyStyle.Judgement, CurrentEnemyPointList[i].mapPointDiffculty);
                }
                else
                {
                    CreateNewStyleEnemy(i, CurrentEnemyPointList[i].m_EnemyStyle, CurrentEnemyPointList[i].mapPointDiffculty);
                }
            }
            SelectEnemy(0);
            EquipAllItem();

        }

        ReplaceToChase();
        
        UIManager.instance.UpdateDistanceUIInfo();



    }

    public void ReplaceToChase()
    {
        if(ForegroundObj == null)
        {
            return;
        }
        MapStatus thisMap = ForegroundObj.GetComponent<MapStatus>();
        int EnemyDanger = EnemyMapID - CurrentMapID + 1;
        for (int i = 0; i < EnemyDanger; i++)
        {
            if (EnemyDanger > thisMap.m_ChaseEnemyPoint.Count && thisMap.m_AllPoint.Count>0)
            {
                GameObject RandomExistPoint = thisMap.m_AllPoint[Random.Range(0, thisMap.m_AllPoint.Count)];
                GameObject thisPoint = Instantiate(m_DarkChaserPoint);
             //   SpriteRenderer thisRenderer = thisPoint.GetComponent<SpriteRenderer>();
                SpriteRenderer RandomExistRenderer = RandomExistPoint.GetComponent<SpriteRenderer>();
                EnemySelectButton thisPointButton = thisPoint.GetComponent<EnemySelectButton>();
                thisPointButton.SelectEnemyID = RandomExistPoint.GetComponent<EnemySelectButton>().SelectEnemyID;
                thisPointButton.Active = true;
                thisPoint.transform.position = RandomExistPoint.transform.position;

                foreach (SpriteRenderer a in thisPoint.GetComponentsInChildren<SpriteRenderer>())
                {
                    a.sortingLayerID = RandomExistRenderer.sortingLayerID;
                }
                thisPointButton.thisMapID = RandomExistPoint.GetComponent<EnemySelectButton>().thisMapID;
                

                thisPoint.transform.SetParent(ForegroundObj.transform);
                Vector3 thisPointScale = thisPoint.transform.localScale;

                int randomX = 1 - Random.Range(0, 2) * 2;
                thisPoint.transform.localScale = new Vector3(thisPointScale.x * randomX, thisPointScale.y, thisPointScale.z);

                thisMap.m_AllPoint.Remove(RandomExistPoint);
                thisMap.m_ChaseEnemyPoint.Add(thisPoint);

                CreateNewStyleEnemy(thisPointButton.SelectEnemyID, EnemyStyle.Judgement, MapPointDiffculty.Medium);

                Destroy(RandomExistPoint);
                SelectEnemy(0);
                EquipAllItem();
            }
        }

    }
    private void Awake()
    {
        DayTime = 0;
        Day = 0;
        CurrentMapID = -11;
        EnemyMapID = -10;
        FinalBossMapID = BossMapId[BossMapId.Count - 1];
        thisInstance = this;
    }
    private void Start()
    {
       // StartCoroutine(InitialFly());

    }

    
    public Equipment GetRandomCurrentEquipment(int i = 0)
    {
        List<Equipment> AvaliableEquipment = new List<Equipment>();

        for (int y = 0; y < AllEquipment.Count; y++)
        {
            if (AllEquipment[y].Value <= CurrentEnemyPowerMax + i && AllEquipment[y].Value >= CurrentEnemyPowerMin + i)
            {
                AvaliableEquipment.Add(AllEquipment[y]);
            }
        }

        int RandomA = Random.Range(0, AvaliableEquipment.Count);
        return AvaliableEquipment[RandomA];

    }
    public void EquipmentSet()
    {
        AllWeapons = new List<Equipment>();
        AllArmArmor = new List<Equipment>();
        AllLegArmor = new List<Equipment>();
        AllArmor = new List<Equipment>();
        AllHelmetArmor = new List<Equipment>();
        AllHandArmor = new List<Equipment>();
        AllShoulderArmor = new List<Equipment>();
        AllFoot = new List<Equipment>();
        AllTail = new List<Equipment>();
        AllEar = new List<Equipment>();

        foreach (Equipment a in AllEquipment)
        {
            for (int x = 0; x < a.SlotTypes.Count; x++)
            {
                switch (a.SlotTypes[x])
                {
                    case (EquipmentType.LeftFoot):
                        {
                            if (!AllFoot.Contains(a))
                            {
                                AllFoot.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.RightFoot):
                        {
                            if (!AllFoot.Contains(a))
                            {
                                AllFoot.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.LeftPocket):
                        {
                            if (!AllFoot.Contains(a))
                            {
                                AllFoot.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.RightPocket):
                        {
                            if (!AllFoot.Contains(a))
                            {
                                AllFoot.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.LeftWeapon):
                        {
                            if (!AllWeapons.Contains(a))
                            {
                                AllWeapons.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.RightWeapon):
                        {
                            if (!AllWeapons.Contains(a))
                            {
                                AllWeapons.Add(a);
                            }
                        }
                        break;

                    case (EquipmentType.Armor):
                        {
                            if (!AllArmor.Contains(a))
                            {
                                AllArmor.Add(a);
                            }
                        }
                        break;

                    case (EquipmentType.Ear):
                        {
                            if (!AllEar.Contains(a))
                            {
                                AllEar.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.Tail):
                        {
                            if (!AllTail.Contains(a))
                            {
                                AllTail.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.Helmet):
                        {
                            if (!AllHelmetArmor.Contains(a))
                            {
                                AllHelmetArmor.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.LeftArm):
                        {
                            if (!AllArmArmor.Contains(a))
                            {
                                AllArmArmor.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.RightArm):
                        {
                            if (!AllArmArmor.Contains(a))
                            {
                                AllArmArmor.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.LeftLeg):
                        {
                            if (!AllLegArmor.Contains(a))
                            {
                                AllLegArmor.Add(a);
                            }
                        } break;
                    case (EquipmentType.RightLeg):
                        {
                            if (!AllLegArmor.Contains(a))
                            {
                                AllLegArmor.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.LeftHand):
                        {
                            if (!AllHandArmor.Contains(a))
                            {
                                AllHandArmor.Add(a);
                            }
                        }
                        break;
                    case (EquipmentType.RightHand):
                        {
                            if (!AllHandArmor.Contains(a))
                            {
                                AllHandArmor.Add(a);
                            }
                        }
                        break;


                }
            }
        }

        for(int i = 0; i < AllEquipmentInStyleList.Count;i++)
        {

            AllEquipmentInStyleList[i].Weapons = new List<Equipment>();
            AllEquipmentInStyleList[i].ArmArmor = new List<Equipment>();
            AllEquipmentInStyleList[i].LegArmor = new List<Equipment>();
            AllEquipmentInStyleList[i].Armor = new List<Equipment>();
            AllEquipmentInStyleList[i].HelmetArmor = new List<Equipment>();
            AllEquipmentInStyleList[i].HandArmor = new List<Equipment>();
            AllEquipmentInStyleList[i].ShoulderArmor = new List<Equipment>();
            AllEquipmentInStyleList[i].Foot = new List<Equipment>();
            AllEquipmentInStyleList[i].Tail = new List<Equipment>();
            AllEquipmentInStyleList[i].Ear = new List<Equipment>();

            foreach (Equipment a in AllEquipmentInStyleList[i].Equipments)
            {
                for (int x = 0; x < a.SlotTypes.Count; x++)
                {
                    switch (a.SlotTypes[x])
                    {
                        case (EquipmentType.LeftFoot):
                            {
                                if (!AllEquipmentInStyleList[i].Foot.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].Foot.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.RightFoot):
                            {
                                if (!AllEquipmentInStyleList[i].Foot.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].Foot.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.LeftPocket):
                            {

                            }
                            break;
                        case (EquipmentType.RightPocket):
                            {

                            }
                            break;
                        case (EquipmentType.LeftWeapon):
                            {
                                if (!AllEquipmentInStyleList[i].Weapons.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].Weapons.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.RightWeapon):
                            {
                                if (!AllEquipmentInStyleList[i].Weapons.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].Weapons.Add(a);
                                }
                            }
                            break;

                        case (EquipmentType.Armor):
                            {
                                if (!AllEquipmentInStyleList[i].Armor.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].Armor.Add(a);
                                }
                            }
                            break;

                        case (EquipmentType.Ear):
                            {
                                if (!AllEquipmentInStyleList[i].Ear.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].Ear.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.Tail):
                            {
                                if (!AllEquipmentInStyleList[i].Tail.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].Tail.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.Helmet):
                            {
                                if (!AllEquipmentInStyleList[i].HelmetArmor.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].HelmetArmor.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.LeftArm):
                            {
                                if (!AllEquipmentInStyleList[i].ArmArmor.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].ArmArmor.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.RightArm):
                            {
                                if (!AllEquipmentInStyleList[i].ArmArmor.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].ArmArmor.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.LeftLeg):
                            {
                                if (!AllEquipmentInStyleList[i].LegArmor.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].LegArmor.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.RightLeg):
                            {
                                if (!AllEquipmentInStyleList[i].LegArmor.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].LegArmor.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.LeftHand):
                            {
                                if (!AllEquipmentInStyleList[i].HandArmor.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].HandArmor.Add(a);
                                }
                            }
                            break;
                        case (EquipmentType.RightHand):
                            {
                                if (!AllEquipmentInStyleList[i].HandArmor.Contains(a))
                                {
                                    AllEquipmentInStyleList[i].HandArmor.Add(a);
                                }
                            }
                            break;


                    }
                }
            }
        }
    }

    void AddEqm_Enemy(int EmptyAmount, List<Equipment> aList, List<Equipment> equipment)
    {
        equipment.Add(null);                                                                                               
        List<Equipment> AvaliableEquipment = new List<Equipment>();
        List<int> AvaliableEquipmentAmount = new List<int>();
        int RandomMax = 0;
        int LastValue = 0;
        int thisValue = 0;

        for (int y = 0; y < aList.Count; y++)
        {

            AvaliableEquipment.Add(aList[y]);

            thisValue = 10;
            LastValue = thisValue + LastValue;

            AvaliableEquipmentAmount.Add(LastValue);

            RandomMax += thisValue;

        }

        int FinalNum = Random.Range(0, RandomMax + EmptyAmount);


        for (int y = 0; y < AvaliableEquipment.Count; y++)
        {
            if (FinalNum < AvaliableEquipmentAmount[y])
            {
                if (y == 0)
                {

                    equipment[equipment.Count - 1] = AvaliableEquipment[0];
                    break;

                }
                else if (FinalNum > AvaliableEquipmentAmount[y - 1])
                {
                    equipment[equipment.Count - 1] = AvaliableEquipment[y];
                    break;
                }
            }
        }
    }

    public List<Equipment> Boss_EnemyEqm = new List<Equipment>();


    void CreateEqmOnParts(int power, int EmptyAmount, List<Equipment> a, StyleEquipment b)
    {
        for (int x = 0; x < 14; x++)
        {
            switch (x)
            {
                case 0:

                    if (b.HasWeapon == true)
                    {
                        AddEqm_Enemy( 0, b.Weapons, a);
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 1:

                    if (b.HasWeapon == true && b.TwoWeapon == true)
                    {
                        AddEqm_Enemy(0, b.Weapons, a);
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;

                case 2:

                    if (b.HasArm == true)
                    {
                        AddEqm_Enemy( 0, b.ArmArmor, a);
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 3:

                    if (b.HasArm == true && b.TwoArm == true)
                    {

                            AddEqm_Enemy( 0, b.ArmArmor, a);
                        
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 4:

                    if (b.HasLeg == true)
                    {
                        AddEqm_Enemy( 0, b.LegArmor, a);
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 5:

                    if (b.HasLeg == true && b.TwoLeg == true)
                    {
     
                            AddEqm_Enemy(0, b.LegArmor, a);
                        
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 6:

                    if (b.HasBody == true)
                    {
                        AddEqm_Enemy(0, b.Armor, a);
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 7:

                    if (b.HasHead == true)
                    {
                        AddEqm_Enemy(0, b.HelmetArmor, a);
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 8:

                    if (b.HasHand == true)
                    {
                        AddEqm_Enemy( 0, b.HandArmor, a);
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 9:

                    if (b.HasHand == true && b.TwoHand == true)
                    {
               
                            AddEqm_Enemy( 0, b.HandArmor, a);
                        
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 10:

                    if (b.HasShoulder == true)
                    {
                        AddEqm_Enemy( 0, b.ShoulderArmor, a);
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 11:

                    if (b.HasShoulder == true && b.TwoShoulder == true)
                    {
            
       
                            AddEqm_Enemy(0, b.ShoulderArmor, a);
                        
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 12:

                    if (b.HasShoe == true)
                    {
                        AddEqm_Enemy(0, b.Foot, a);
                    }
                    else
                    {
                        a.Add(null);
                    }
                    break;
                case 13:

                    if (b.HasShoe == true && b.TwoShoe == true)
                    {
  
                            AddEqm_Enemy( 0, b.Foot, a);
                        
                    }
                    else
                    {
                        a.Add(null);
                    }

                    break;
                case 14:

                    if (b.HasTail == true)
                    {
                        AddEqm_Enemy( 0, b.Tail, a);
                    }
                    else
                    {
                        a.Add(null);
                    }
                    break;
                case 15:

                    if (b.HasEar == true)
                    {
                        AddEqm_Enemy(0, b.Ear, a);
                    }
                    else
                    {
                        a.Add(null);
                    }
                    break;

            }

        }

    }

    public void CreateNewStyleEnemy(int id, EnemyStyle enemyStyle,MapPointDiffculty Diffculty )
    {
       switch(enemyStyle)
        {
           
            case (EnemyStyle.WDBoss):
                {
                    Boss_EnemyEqm.Clear();
                    CreateEqmOnParts(0, 0, Boss_EnemyEqm, AllEquipmentInStyleList[(int)EnemyStyle.WDBoss]);

                }
                break;
            case (EnemyStyle.Judgement):
                {
                    Boss_EnemyEqm.Clear();
                    CreateEqmOnParts(0, 0, Boss_EnemyEqm, AllEquipmentInStyleList[(int)EnemyStyle.Judgement]);

                }
                break;
            case (EnemyStyle.SunBoss):
                {
                    Boss_EnemyEqm.Clear();

                }
                break;
            case (EnemyStyle.TFBoss):
                {
                    Boss_EnemyEqm.Clear();

                }
                break;
            default:
                CreateNewEnemy(id, Diffculty, AllEquipmentInStyleList[(int)enemyStyle]);
                break;
        }
    }
    void CreateNewEnemy(int id, MapPointDiffculty Difficulty, StyleEquipment a)
    {
        SkinManager.instance.PrintList(SkinManager.instance.RandomSelectedEnemySkinCodes());
        List<List<Equipment>> thisEnemyEqmList;
        switch(id)
        {
            case 0: 
                EnemyEqm_List1.Clear();
                thisEnemyEqmList = EnemyEqm_List1;
                EnemyLooking1 = SkinManager.instance.RandomSelectedEnemySkinCodes();

                break;
            case 1:
                EnemyEqm_List2.Clear();
                thisEnemyEqmList = EnemyEqm_List2;
                EnemyLooking2 = SkinManager.instance.RandomSelectedEnemySkinCodes();

                break;
            case 2:
                EnemyEqm_List3.Clear();
                thisEnemyEqmList = EnemyEqm_List3;
                EnemyLooking3 = SkinManager.instance.RandomSelectedEnemySkinCodes();

                break;
            case 3:
                EnemyEqm_List4.Clear();
                thisEnemyEqmList = EnemyEqm_List4;
                EnemyLooking4 = SkinManager.instance.RandomSelectedEnemySkinCodes();
                break;

            case 4:
               
                EventEnemyEqm_List.Clear();
                thisEnemyEqmList = EventEnemyEqm_List;
                break;

            default:
                return;
        } 

        int EmptyAmount = EmptyEqpRandomLimit - CurrentEnemyPowerMax;
        if (EmptyAmount < 0)
        {
            EmptyAmount = 0;
        }
        int thisWeakPowerOffset = 0;
        switch(Difficulty)
        {
            case MapPointDiffculty.Easy:  
                thisWeakPowerOffset = WeakEnemyPowerOffset;             
                break;
            case MapPointDiffculty.Medium:       
                break;
        }
        thisEnemyEqmList.Add(new List<Equipment>());

        for (int x = 0; x < 14; x++)
        {
            switch (x)
            {
                case (0):
                    {
                        if (Difficulty == MapPointDiffculty.Easy)
                        {
                            AddEqm_Enemy(EmptyAmount, a.Weapons, thisEnemyEqmList[0]);
                        }
                        else
                        {
                            AddEqm_Enemy(0, a.Weapons, thisEnemyEqmList[0]);
                        }
                    }
                    break;
                case (1):
                    {
                        AddEqm_Enemy(EmptyAmount, a.Weapons, thisEnemyEqmList[0]);
                    }
                    break;
                case (2):
                    {
                        AddEqm_Enemy( EmptyAmount, a.ArmArmor, thisEnemyEqmList[0]);
                    }
                    break;
                case (3):
                    {
                        AddEqm_Enemy( EmptyAmount, a.ArmArmor, thisEnemyEqmList[0]);
                    }
                    break;
                case (4):
                    {
                        AddEqm_Enemy( EmptyAmount, a.LegArmor, thisEnemyEqmList[0]);
                    }
                    break;
                case (5):
                    {
                        AddEqm_Enemy( EmptyAmount, a.LegArmor, thisEnemyEqmList[0]);
                    }
                    break;
                case (6):
                    {
                        if (Difficulty == MapPointDiffculty.Easy)
                        {
                            AddEqm_Enemy(EmptyAmount, a.Armor, thisEnemyEqmList[0]);
                        }
                        else
                        {
                            AddEqm_Enemy(0, a.Armor, thisEnemyEqmList[0]);
                        }
                    }
                    break;
                case (7):
                    {
                        if (Difficulty == MapPointDiffculty.Easy)
                        {
                            AddEqm_Enemy(EmptyAmount, a.HelmetArmor, thisEnemyEqmList[0]);
                        }
                        else
                        {
                            AddEqm_Enemy(0, a.HelmetArmor, thisEnemyEqmList[0]);
                        }
                    }
                    break;
                case (8):
                    {
                        AddEqm_Enemy( EmptyAmount, a.HandArmor, thisEnemyEqmList[0]);
                    }
                    break;
                case (9):
                    {
                        AddEqm_Enemy( EmptyAmount, a.HandArmor, thisEnemyEqmList[0]);
                    }
                    break;
                case (10):
                    {
                        AddEqm_Enemy( EmptyAmount, a.ShoulderArmor, thisEnemyEqmList[0]);
                    }
                    break;
                case (11):
                    {
                        AddEqm_Enemy( EmptyAmount, a.ShoulderArmor, thisEnemyEqmList[0]);
                    }
                    break;
                case (12):
                    {
                        AddEqm_Enemy( EmptyAmount, a.Foot, thisEnemyEqmList[0]);
                    }
                    break;
                case (13):
                    {
                        AddEqm_Enemy( EmptyAmount, a.Foot, thisEnemyEqmList[0]);
                    }
                    break;
                case (14):
                    {
                        AddEqm_Enemy( EmptyAmount, a.Tail, thisEnemyEqmList[0]);
                    }
                    break;
                case (15):
                    {
                        AddEqm_Enemy( EmptyAmount, a.Ear, thisEnemyEqmList[0]);
                    }
                    break;

            }

        }
    }

    public void SelectBossEnemy(EnemyStyle enemyStyle,BossStatus bossStatus)
    {
        CurrentEnemyEquipmentList.Clear();
        InGameManager.instance.Characters[1].m_Gold = Random.Range(CurrentEnemyPowerMin, CurrentEnemyPowerMax) * 5;
        List<Equipment> equipments = new List<Equipment>();
        RandomEnemyAILevel(true,bossStatus);

        if (bossStatus.DetailSetting == false)
        {

            switch (enemyStyle)
            {
                case (EnemyStyle.WDBoss):
                    {
                        equipments = Boss_EnemyEqm;

                    }
                    break;
                case (EnemyStyle.Judgement):
                    {
                        equipments = Boss_EnemyEqm;

                    }
                    break;
            }


            foreach (Equipment a in equipments)
            {
                CurrentEnemyEquipmentList.Add(a);
            }
        }
        else
        {
            foreach (Equipment a in bossStatus.BossEqm)
            {
                CurrentEnemyEquipmentList.Add(a);
            }
        }
        EquipAllItem();
    }
    public void SelectEnemy(int id)
    {
        CurrentEnemyEquipmentList.Clear();
        InGameManager.instance.Characters[1].m_Gold = Random.Range(CurrentEnemyPowerMin, CurrentEnemyPowerMax);
        List<List<Equipment>> equipments = new List<List<Equipment>>();


        if (id == 0)
        {
            equipments = EnemyEqm_List1;
            SkinManager.instance.ChangeSkinByCode(EnemyLooking1);
            
            RandomEnemyPartHealth(CurrentEnemyPowerMin - 20);
        }
      else  if (id == 1)
        {
            equipments = EnemyEqm_List2;
            SkinManager.instance.ChangeSkinByCode(EnemyLooking2);
            RandomEnemyPartHealth(CurrentEnemyPowerMin - 20);
        }
      else  if (id == 2)
        {
            equipments = EnemyEqm_List3;
            SkinManager.instance.ChangeSkinByCode(EnemyLooking3);
            RandomEnemyPartHealth(CurrentEnemyPowerMin - 20);
        }
       else if (id == 3)
        {
            equipments = EnemyEqm_List4;
            SkinManager.instance.ChangeSkinByCode(EnemyLooking4);
            RandomEnemyPartHealth(CurrentEnemyPowerMin - 20);
        }
        else if (id == 4)
        {
            equipments = EventEnemyEqm_List;
            RandomEnemyPartHealth(CurrentEnemyPowerMin - 10);
        }


        RandomEnemyAILevel(false,null);

        if (equipments[0] != null && equipments[0].Count > 0)
        {
            foreach (Equipment a in equipments[0])
            {
                CurrentEnemyEquipmentList.Add(a);
            }
        }

        EquipAllItem();

    }


    public void SetEnemyPartsHealth(int Head, int Body, int LArm, int RArm, int LLeg, int RLeg)
    {
        float HardPercentage = 1f;
        switch (GameWorldSetting.Hardness)
        {
            case -2:
                HardPercentage = 0.6f;
                break;
            case -1:
                HardPercentage = 0.75f;
                break;
            case 0:
                HardPercentage = 1f;
                break;
            case 1:
                HardPercentage = 1.25f;
                break;
            case 2:
                HardPercentage = 1.25f;
                break;
        }

        foreach (BodyPart aPart in CharacterManager.EnemyInstance.bodyParts)
        {
            switch(aPart.GetBodyPartType())
            {
                case BodyPartType.Chest:
                    aPart.HealthMax = Mathf.RoundToInt(Body * HardPercentage);
                    break;
                case BodyPartType.Head:
                    aPart.HealthMax = Mathf.RoundToInt(Head * HardPercentage);
                    break;
                case BodyPartType.LeftArm:
                    aPart.HealthMax = Mathf.RoundToInt(LArm * HardPercentage);
                    break;
                case BodyPartType.RightArm:
                    aPart.HealthMax = Mathf.RoundToInt(RArm * HardPercentage);
                    break;
                case BodyPartType.LeftLeg:
                    aPart.HealthMax = Mathf.RoundToInt(LLeg * HardPercentage);
                    break;
                case BodyPartType.RightLeg:
                    aPart.HealthMax = Mathf.RoundToInt(RLeg * HardPercentage);
                    break;
            }
        }
    }

    public void RandomEnemyPartHealth(int Power)
    {
        float HardPercentage = 1f;
        switch (GameWorldSetting.Hardness)
        {
            case -2:
                HardPercentage = 0.75f;
                break;
            case -1:
                HardPercentage = 1f;
                break;
            case 0:
                HardPercentage = 1f;
                break;
            case 1:
                HardPercentage = 1f;
                break;
            case 2:
                HardPercentage = 1.25f;
                break;
        }

        foreach (BodyPart aPart in CharacterManager.EnemyInstance.bodyParts)
        {
            switch (aPart.GetBodyPartType())
            {
                case BodyPartType.Chest:
                    aPart.HealthMax = Mathf.RoundToInt(80 * (1 + Random.Range(Power - 10f, Power) /40f) * HardPercentage);
                    break;
                case BodyPartType.Head:
                    aPart.HealthMax = Mathf.RoundToInt(60 * (1 + Random.Range(Power - 10f, Power) / 40) * HardPercentage);
                    break;
                case BodyPartType.LeftArm:
                    aPart.HealthMax = Mathf.RoundToInt(50 * (1 + Random.Range(Power - 10f, Power) / 40) * HardPercentage);
                    break;
                case BodyPartType.RightArm:
                    aPart.HealthMax = Mathf.RoundToInt(50 * (1 + Random.Range(Power - 10f, Power) / 40) * HardPercentage);
                    break;
                case BodyPartType.LeftLeg:
                    aPart.HealthMax = Mathf.RoundToInt(50 * (1 + Random.Range(Power - 10f, Power) / 40) * HardPercentage);
                    break;
                case BodyPartType.RightLeg:
                    aPart.HealthMax = Mathf.RoundToInt(50 * (1 + Random.Range(Power - 10f, Power) / 40) * HardPercentage);
                    break;
            }
        }

    }

    public void RandomEnemyAILevel(bool IsBoss, BossStatus bossStatus)
    {
        if (IsBoss == false)
        {
            CharacterManager.EnemyInstance.GetComponent<AIBehavior>().ExpectDistance = 1;
            int AILevel = 6;
            switch (GameWorldSetting.Hardness)
            {
                case -2:
                    AILevel = 6;
                    break;
                case 2:
                    AILevel = 2;
                    break;

                case 1:
                    AILevel = 6 - Mathf.RoundToInt(CurrentEnemyPowerMax / 15f);
                    if (AILevel < 3)
                    {
                        AILevel = 3;
                    }
                    break;

                default:
                    AILevel = 6 - Mathf.RoundToInt(CurrentEnemyPowerMax / 20f);
                    if (AILevel < 3)
                    {
                        AILevel = 3;
                    }
                    break;
            }   


            CharacterManager.EnemyInstance.GetComponent<AIBehavior>().AISmart = AILevel;
        }
        else
        {
            CharacterManager.EnemyInstance.GetComponent<AIBehavior>().ExpectDistance = bossStatus.BossExpectDistance;

            if (GameWorldSetting.Hardness == -2)
            {
                CharacterManager.EnemyInstance.GetComponent<AIBehavior>().AISmart = 6;
            }
            else
            {
                CharacterManager.EnemyInstance.GetComponent<AIBehavior>().AISmart = 2;
            }
            
        }
    }
    public void EquipAllItem()
    {
        for (int i = 0; i < 14; i++)
        {
            InGameManager.instance.EquipmentManagers[1].Equip(null, (EquipmentType)i);
        }

        for (int i = 0; i < 14; i++)
        {
            if (CurrentEnemyEquipmentList.Count > i && CurrentEnemyEquipmentList[i] != null)
            {
                InGameManager.instance.EquipmentManagers[1].Equip(Instantiate(CurrentEnemyEquipmentList[i]), (EquipmentType)i);
            }
        }

        CharacterManager.EnemyInstance.PlayIdle();

    }

}
public enum EnemyStyle
{
    Ym,
    YmAndTf,
    Tf,
    Tf_2,
    Wd_1,
    Wd_2,
    Wd_3,   
    Sun_1,
    Sun_2,
    XiBei_1,
    XiBei_2,
    XiBei_3,
    WDBoss,
    Judgement,
    TFBoss,
    SunBoss,
    Wb_1,
    Wb_2,
    Qs_1,
    Qs_2,
    Qs_3
}
