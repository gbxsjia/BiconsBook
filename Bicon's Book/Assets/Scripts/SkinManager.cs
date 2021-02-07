#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using System.Linq;

public class SkinManager : MonoBehaviour
{
    /// <summary>
    /// A new data structure that located by 3D array including list of selectedEnemySkinCodes.
    /// </summary>
    //[System.Serializable]
    public class SkinModelInstanceList
    {
        public List<SkinModelInstance> skinModelInstanceList = new List<SkinModelInstance>();
    }
    [SerializeField]
    public SkinModelInstanceList[, ,] models = new SkinModelInstanceList[3,2,7];
    //public List<List<List<List<SkinModelInstance>>>> models = new List<List<List<List<SkinModelInstance>>>>();

    /// <summary>
    /// 
    /// </summary>

    public void tester()
    {
        for (int i = 0; i < AllModules.Length; i++)
        {
            models[(int)AllModules[i].raceType, (int)AllModules[i].sexType,(int)AllModules[i].modelType].skinModelInstanceList.Add(AllModules[i]);
        }
        printAllModules();
    }
    /// <summary>
    /// Test Tools
    /// </summary>
    ///
    //public void InitialAllModules()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        models.Add(null);
    //        for (int j = 0; j < 2; j++)
    //        {
    //            models[i].Add(null);
    //            for (int k = 0; k < 8; k++)
    //            {
    //                models[i][j].Add(null);
    //            }
    //        }
    //    }
    //}
    public void printAllModules()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (models[i,j,k] != null)
                    {
                        Debug.Log(models[i,j,k].skinModelInstanceList.Count);
                    }
                    else
                    {
                        Debug.Log("Null");
                    }
                }
            }
        }
    }    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public List<selectedEnemySkinCodeClass> selectedEnemySkinCodesList = new List<selectedEnemySkinCodeClass>();

    [System.Serializable]
    public class selectedEnemySkinCodeClass
    {
        public List<int> selectedEnemySkinCodes = new List<int>();
    }

    [SerializeField] SaveSkinData saveSkinData;

    public void SaveSkinCodetoJSON()
    {
        if (selectedEnemySkinCodesList.Count != 0)
        {
            saveSkinData._SkinCodeJsonClass.skinCodeListsJson.Clear();
            for (int i = 0; i < selectedEnemySkinCodesList.Count; i++)
            {
                SkinCodeJsonClass aNewList = new SkinCodeJsonClass();
                aNewList.skinCodeJson = selectedEnemySkinCodesList[i].selectedEnemySkinCodes;
                saveSkinData._SkinCodeJsonClass.skinCodeListsJson.Add(aNewList);
            }
            saveSkinData.SaveIntoJson();
        }
    }

    private List<int> temp_list = new List<int>();
    public void ReadSkinCodeFromJSON()
    {
        saveSkinData.ReadFromJson();
        selectedEnemySkinCodesList.Clear();
        for (int i = 0; i < saveSkinData._SkinCodeJsonClass.skinCodeListsJson.Count; i++)
        {
            selectedEnemySkinCodeClass aNewList = new selectedEnemySkinCodeClass();
            aNewList.selectedEnemySkinCodes = saveSkinData._SkinCodeJsonClass.skinCodeListsJson[i].skinCodeJson;
            selectedEnemySkinCodesList.Add(aNewList);
        }
    }

    public List<int> selectedEnemySkinCodesHolder = new List<int>();
    [SerializeField]
    public int current_SelectedEnemySkinCode_Index;



    public void AddSelectedEnemySkinCodes()
    {
        ReadSkinCodeFromJSON();
        if (AddSelectedEnemySkinCodesHelper())
        {
            selectedEnemySkinCodeClass aNewList = new selectedEnemySkinCodeClass();
            selectedEnemySkinCodesList.Add(aNewList);
            aNewList.selectedEnemySkinCodes = selectedEnemySkinCodesHolder;
            Debug.Log("OK 加上了!!!");
            SaveSkinCodetoJSON();
        }
        else
        {
            Debug.Log("重复了!!!");
        }

    }

    public bool AddSelectedEnemySkinCodesHelper()
    {
        for (int i = 0; i < selectedEnemySkinCodesList.Count; i++)
        {
            if (Enumerable.SequenceEqual(selectedEnemySkinCodesList[i].selectedEnemySkinCodes, selectedEnemySkinCodesHolder))
            {
                PrintList(selectedEnemySkinCodesList[i].selectedEnemySkinCodes);
                PrintList(selectedEnemySkinCodesHolder);
                return false;
            }                
        }
        return true;
    }

    public void PrintSelectedEnemySkinCodes()
    {
        if (selectedEnemySkinCodesList == null)
        {
            Debug.Log("Null selectedEnemySkinCodes List");
        }
        else
        {
            for (int i = 0; i < selectedEnemySkinCodesList.Count; i++)
            {
                PrintList(selectedEnemySkinCodesList[i].selectedEnemySkinCodes);
            }
        }
    }

    public List<int> RandomSelectedEnemySkinCodes()
    {
        if (selectedEnemySkinCodesList.Count == 0)
        {
            Debug.Log("全部都没有啦！！！");
            return null;
        }
        else
        {
            current_SelectedEnemySkinCode_Index = random.Next(selectedEnemySkinCodesList.Count);
            return selectedEnemySkinCodesList[current_SelectedEnemySkinCode_Index].selectedEnemySkinCodes;
        }
    }



    public static SkinManager instance;
    
   private void Awake()
    {
    instance = this;
    }
    
    //[SerializeField]
    //private float R_skin_Slider;
    //[SerializeField]
    //private float G_skin_Slider;
    //[SerializeField]
    //private float B_skin_Slider;

    //[SerializeField]
    //private float R_hair_Slider;
    //[SerializeField]
    //private float G_hair_Slider;
    //[SerializeField]
    //private float B_hair_Slider;

    [SerializeField]
    private float[,] human_skin_color = new float[8, 3]
    {
        {255,255,255 },
        {245,228,174 },
        {212,178,162 },
        {188,146,137 },
        {226,229,192 },
        {220,236,236 },
        {234,235,255 },
        {248,245,227 }
    };

    [SerializeField]
    private float[,] catgirl_skin_color = new float[6, 3]
    {
        {255,255,255 },
        {160,163,179 },
        {243,207,167 },
        {219,222,207 },
        {188,173,165 },
        {231,207,209 }
    };

    [SerializeField]
    private float[,] hair_color = new float[30, 3]
    {
        {255,255,255 },
        {82,109,133 },
        {166,166,166 },
        {99,64,64 },
        {68,13,24 },
        {163,168,227 },
        {226,193,162 },
        {255,158,70 },
        {255,98,70 },
        {255,70,119 },
        {148,104,215 },
        {150,150,150 },
        {57,59,65 },
        {90,47,73 },
        {106,66,68 },
        {109,96,65 },
        {253,178,0 },
        {104,25,7 },
        {176,86,65 },
        {253,72,135 },
        {183,44,89 },
        {210,184,174 },
        {150,101,55 },
        {207,239,142 },
        {162,57,40 },
        {169,174,181 },
        {73,90,128 },
        {116,90,90 },
        {253,64,0 },
        {177,147,198 }
    };

    // public SkinModelInstance[][][] models = new SkinModelInstance[3][][];
    public List<SpriteMeshInstance> SkinRenders;
    public SpriteMeshInstance[] RealSkinRenders;

    public SkinModelInstance[] AllModules;
    // public List<SkinModelInstance> AllModules = new List<SkinModelInstance>();

    public List<SkinModelInstance> HumanMaleHairModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanMaleMeimaoModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanMaleHuzhiModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanMaleZuixingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanMaleYanxingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanMaleZhanHengModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanMaleSkinColorModel = new List<SkinModelInstance>();


    public List<SkinModelInstance> HumanFemaleHairModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanFemaleMeimaoModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanFemaleHuzhiModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanFemaleZuixingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanFemaleYanxingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanFemaleZhanHengModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> HumanFemaleSkinColorModel = new List<SkinModelInstance>();


    public List<SkinModelInstance> CatgirlMaleHairModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlMaleMeimaoModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlMaleHuzhiModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlMaleZuixingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlMaleYanxingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlMaleZhanHengModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlMaleSkinColorModel = new List<SkinModelInstance>();


    public List<SkinModelInstance> CatgirlFemaleHairModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlFemaleMeimaoModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlFemaleHuzhiModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlFemaleZuixingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlFemaleYanxingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlFemaleZhanHengModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> CatgirlFemaleSkinColorModel = new List<SkinModelInstance>();


    public List<SkinModelInstance> DeluMaleHairModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluMaleMeimaoModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluMaleHuzhiModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluMaleZuixingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluMaleYanxingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluMaleZhanHengModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluMaleSkinColorModel = new List<SkinModelInstance>();


    public List<SkinModelInstance> DeluFemaleHairModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluFemaleMeimaoModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluFemaleHuzhiModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluFemaleZuixingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluFemaleYanxingModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluFemaleZhanHengModel = new List<SkinModelInstance>();
    public List<SkinModelInstance> DeluFemaleSkinColorModel = new List<SkinModelInstance>();

    public void GetRealSkinRenders()
    {
        Component[] components = GetComponentsInChildren(typeof(SpriteMeshInstance), true);
        RealSkinRenders = new SpriteMeshInstance[components.Length];
        for (int i = 0; i < components.Length; i++)
        {
            //   Debug.Log("ikk");
            for (int j = 0; j < components.Length; j++)
            {
                //       Debug.Log("j");
                CharacterSpriteMeshType type = (CharacterSpriteMeshType)j;
                if (type.ToString() == components[i].gameObject.name)
                {
                    //         Debug.Log("k");
                    RealSkinRenders[j] = components[i] as SpriteMeshInstance;
                }
            }
        }

    }
    
    public void GetSkinRenders()
    {
        SkinRenders.Clear();
        // get renders by order of hair model
        for (int i = 0; i < RealSkinRenders.Length; i++)
        {
            for (int j = 0; j < DeluMaleHairModel[0].SkinMeshes.Length; j++)
            {
                if(RealSkinRenders[i]== null)
                {
                    continue;
                }
                if (DeluMaleHairModel[0].SkinMeshes[j].name == RealSkinRenders[i].gameObject.name)
                {
                    SkinRenders.Add(RealSkinRenders[i]);
                }
            }
        }
        // DeluFemaleMeimaoModel
        for (int i = 0; i < RealSkinRenders.Length; i++)
        {
            for (int j = 0; j < DeluMaleMeimaoModel[0].SkinMeshes.Length; j++)
            {
                if (RealSkinRenders[i] == null)
                {
                    continue;
                }
                if (DeluMaleMeimaoModel[0].SkinMeshes[j].name == RealSkinRenders[i].gameObject.name)
                {
                    SkinRenders.Add(RealSkinRenders[i]); 
                }
            }
        }

        // DeluFemaleHuzhiModel
        /* for (int i = 0; i < components.Length; i++)
        {
            for (int j = 0; j < DeluFemaleHuzhiModel[0].SkinMeshes.Length; j++)
            {
                if (DeluFemaleHuzhiModel[0].SkinMeshes[j].name == components[i].gameObject.name)
                {
                    SkinRenders[skinRendersIndex] = components[i] as SpriteMeshInstance;
                    skinRendersIndex++;
                }
            }
        } */

        // DeluFemaleZuixingModel
        for (int i = 0; i < RealSkinRenders.Length; i++)
        {
            for (int j = 0; j < DeluFemaleZuixingModel[0].SkinMeshes.Length; j++)
            {
                if (RealSkinRenders[i] == null)
                {
                    continue;
                }
                if (DeluFemaleZuixingModel[0].SkinMeshes[j].name == RealSkinRenders[i].gameObject.name)
                {
                    SkinRenders.Add(RealSkinRenders[i]);
                }
            }
        }

        // DeluFemaleYanxingModel
        for (int i = 0; i < RealSkinRenders.Length; i++)
        {
            for (int j = 0; j < DeluFemaleYanxingModel[0].SkinMeshes.Length; j++)
            {
                if (RealSkinRenders[i] == null)
                {
                    continue;
                }
                if (DeluFemaleYanxingModel[0].SkinMeshes[j].name == RealSkinRenders[i].gameObject.name)
                {
                    SkinRenders.Add(RealSkinRenders[i]);
                }
            }
        }

        // DeluFemaleZhanHengModel
        /* for (int i = 0; i < components.Length; i++)
        {
            for (int j = 0; j < DeluFemaleZhanHengModel[0].SkinMeshes.Length; j++)
            {
                if (DeluFemaleZhanHengModel[0].SkinMeshes[j].name == components[i].gameObject.name)
                {
                    SkinRenders[skinRendersIndex] = components[i] as SpriteMeshInstance;
                    skinRendersIndex++;
                }
            }
        } */

        // DeluFemaleSkinColorModel
        for (int i = 0; i < RealSkinRenders.Length; i++)
        {
            for (int j = 0; j < DeluFemaleSkinColorModel[0].SkinMeshes.Length; j++)
            {
                if (RealSkinRenders[i] == null)
                {
                    continue;
                }
                if (DeluFemaleSkinColorModel[0].SkinMeshes[j].name == RealSkinRenders[i].gameObject.name)
                {
                    SkinRenders.Add(RealSkinRenders[i]);
                }
            }
        }
    }

    public void InitialPools()
    {
        ClearModelsList();
        for (int i = 0; i < AllModules.Length; i++)
        {
            switch (AllModules[i].raceType)
            {
                case RaceType.Human:
                    switch (AllModules[i].sexType)
                    {
                        case SexType.Male:
                            switch (AllModules[i].modelType)
                            {
                                case ModelType.HairModel:
                                    HumanMaleHairModel.Add(AllModules[i]);
                                    CatgirlMaleHairModel.Add(AllModules[i]);
                                    DeluMaleHairModel.Add(AllModules[i]);
                                    break;
                                case ModelType.MeimaoModel:
                                    HumanMaleMeimaoModel.Add(AllModules[i]);
                                    CatgirlMaleMeimaoModel.Add(AllModules[i]);
                                    DeluMaleMeimaoModel.Add(AllModules[i]);
                                    break;
                                case ModelType.HuzhiModel:
                                    HumanMaleHuzhiModel.Add(AllModules[i]);
                                    CatgirlMaleHuzhiModel.Add(AllModules[i]);
                                    DeluMaleHuzhiModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZuixingModel:
                                    HumanMaleZuixingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.YanxingModel:
                                    HumanMaleYanxingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZhanHengModel:
                                    HumanMaleZhanHengModel.Add(AllModules[i]);
                                    break;
                                case ModelType.SkinColorModel:
                                    HumanMaleSkinColorModel.Add(AllModules[i]);
                                    break;
                            }
                            break;

                        case SexType.Female:
                            switch (AllModules[i].modelType)
                            {
                                case ModelType.HairModel:
                                    HumanFemaleHairModel.Add(AllModules[i]);
                                    CatgirlFemaleHairModel.Add(AllModules[i]);
                                    DeluFemaleHairModel.Add(AllModules[i]);
                                    break;
                                case ModelType.MeimaoModel:
                                    HumanFemaleMeimaoModel.Add(AllModules[i]);
                                    CatgirlFemaleMeimaoModel.Add(AllModules[i]);
                                    DeluFemaleMeimaoModel.Add(AllModules[i]);
                                    break;
                                case ModelType.HuzhiModel:
                                    HumanFemaleHuzhiModel.Add(AllModules[i]);
                                    CatgirlFemaleHuzhiModel.Add(AllModules[i]);
                                    DeluFemaleHuzhiModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZuixingModel:
                                    HumanFemaleZuixingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.YanxingModel:
                                    HumanFemaleYanxingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZhanHengModel:
                                    HumanFemaleZhanHengModel.Add(AllModules[i]);
                                    break;
                                case ModelType.SkinColorModel:
                                    HumanFemaleSkinColorModel.Add(AllModules[i]);
                                    break;
                            }
                            break;
                    }
                    break;

                case RaceType.Catgirl:
                    switch (AllModules[i].sexType)
                    {
                        case SexType.Male:
                            switch (AllModules[i].modelType)
                            {
                                case ModelType.HairModel:
                                    HumanMaleHairModel.Add(AllModules[i]);
                                    CatgirlMaleHairModel.Add(AllModules[i]);
                                    DeluMaleHairModel.Add(AllModules[i]);
                                    break;
                                case ModelType.MeimaoModel:
                                    HumanMaleMeimaoModel.Add(AllModules[i]);
                                    CatgirlMaleMeimaoModel.Add(AllModules[i]);
                                    DeluMaleMeimaoModel.Add(AllModules[i]);
                                    break;
                                case ModelType.HuzhiModel:
                                    HumanMaleHuzhiModel.Add(AllModules[i]);
                                    CatgirlMaleHuzhiModel.Add(AllModules[i]);
                                    DeluMaleHuzhiModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZuixingModel:
                                    CatgirlMaleZuixingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.YanxingModel:
                                    CatgirlMaleYanxingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZhanHengModel:
                                    CatgirlMaleZhanHengModel.Add(AllModules[i]);
                                    break;
                                case ModelType.SkinColorModel:
                                    CatgirlMaleSkinColorModel.Add(AllModules[i]);
                                    break;
                            }
                            break;

                        case SexType.Female:
                            switch (AllModules[i].modelType)
                            {
                                case ModelType.HairModel:
                                    HumanFemaleHairModel.Add(AllModules[i]);
                                    CatgirlFemaleHairModel.Add(AllModules[i]);
                                    DeluFemaleHairModel.Add(AllModules[i]);
                                    break;
                                case ModelType.MeimaoModel:
                                    HumanFemaleMeimaoModel.Add(AllModules[i]);
                                    CatgirlFemaleMeimaoModel.Add(AllModules[i]);
                                    DeluFemaleMeimaoModel.Add(AllModules[i]);
                                    break;
                                case ModelType.HuzhiModel:
                                    HumanFemaleHuzhiModel.Add(AllModules[i]);
                                    CatgirlFemaleHuzhiModel.Add(AllModules[i]);
                                    DeluFemaleHuzhiModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZuixingModel:
                                    CatgirlFemaleZuixingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.YanxingModel:
                                    CatgirlFemaleYanxingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZhanHengModel:
                                    CatgirlFemaleZhanHengModel.Add(AllModules[i]);
                                    break;
                                case ModelType.SkinColorModel:
                                    CatgirlFemaleSkinColorModel.Add(AllModules[i]);
                                    break;
                            }
                            break;
                    }
                    break;

                case RaceType.Delu:
                    switch (AllModules[i].sexType)
                    {
                        case SexType.Male:
                            switch (AllModules[i].modelType)
                            {
                                case ModelType.HairModel:
                                    HumanMaleHairModel.Add(AllModules[i]);
                                    CatgirlMaleHairModel.Add(AllModules[i]);
                                    DeluMaleHairModel.Add(AllModules[i]);
                                    break;
                                case ModelType.MeimaoModel:
                                    HumanMaleMeimaoModel.Add(AllModules[i]);
                                    CatgirlMaleMeimaoModel.Add(AllModules[i]);
                                    DeluMaleMeimaoModel.Add(AllModules[i]);
                                    break;
                                case ModelType.HuzhiModel:
                                    HumanMaleHuzhiModel.Add(AllModules[i]);
                                    CatgirlMaleHuzhiModel.Add(AllModules[i]);
                                    DeluMaleHuzhiModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZuixingModel:
                                    DeluMaleZuixingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.YanxingModel:
                                    DeluMaleYanxingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZhanHengModel:
                                    DeluMaleZhanHengModel.Add(AllModules[i]);
                                    break;
                                case ModelType.SkinColorModel:
                                    DeluMaleSkinColorModel.Add(AllModules[i]);
                                    break;
                            }
                            break;

                        case SexType.Female:
                            switch (AllModules[i].modelType)
                            {
                                case ModelType.HairModel:
                                    HumanFemaleHairModel.Add(AllModules[i]);
                                    CatgirlFemaleHairModel.Add(AllModules[i]);
                                    DeluFemaleHairModel.Add(AllModules[i]);
                                    break;
                                case ModelType.MeimaoModel:
                                    HumanFemaleMeimaoModel.Add(AllModules[i]);
                                    CatgirlFemaleMeimaoModel.Add(AllModules[i]);
                                    DeluFemaleMeimaoModel.Add(AllModules[i]);
                                    break;
                                case ModelType.HuzhiModel:
                                    HumanFemaleHuzhiModel.Add(AllModules[i]);
                                    CatgirlFemaleHuzhiModel.Add(AllModules[i]);
                                    DeluFemaleHuzhiModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZuixingModel:
                                    DeluFemaleZuixingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.YanxingModel:
                                    DeluFemaleYanxingModel.Add(AllModules[i]);
                                    break;
                                case ModelType.ZhanHengModel:
                                    DeluFemaleZhanHengModel.Add(AllModules[i]);
                                    break;
                                case ModelType.SkinColorModel:
                                    DeluFemaleSkinColorModel.Add(AllModules[i]);
                                    break;
                            }
                            break;
                    }
                    break;



            }
        }
    }     
    
    System.Random random = new System.Random();
    public List<int> GeneralizeEnemySkinCode()
    {
        List<int> enemySkinCode = new List<int>();

        int N_race = Random.Range(0, 3); //0,1,2
        int N_sex = Random.Range(0, 2); //0,1

        enemySkinCode.Add(N_race);
        enemySkinCode.Add(N_sex);

        // Human
        if (N_race == 0)
        {
            if (N_sex == 1)
            {
                if (HumanMaleHairModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanMaleHairModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanMaleMeimaoModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanMaleMeimaoModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanMaleHuzhiModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanMaleHuzhiModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanMaleZuixingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanMaleZuixingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanMaleYanxingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanMaleYanxingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanMaleZhanHengModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanMaleZhanHengModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanMaleSkinColorModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanMaleSkinColorModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
            }
            else if (N_sex == 0)
            {
                if (HumanFemaleHairModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanFemaleHairModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanFemaleMeimaoModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanFemaleMeimaoModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanFemaleHuzhiModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanFemaleHuzhiModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanFemaleZuixingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanFemaleZuixingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanFemaleYanxingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanFemaleYanxingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanFemaleZhanHengModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanFemaleZhanHengModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (HumanFemaleSkinColorModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(HumanFemaleSkinColorModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
            }
        }
        // Catgirl
        else if (N_race == 1)
        {
            if (N_sex == 1)
            {
                if (CatgirlMaleHairModel.Count != 0)
                {
                    enemySkinCode.Add(Random.Range(0, CatgirlMaleHairModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlMaleMeimaoModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlMaleMeimaoModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlMaleHuzhiModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlMaleHuzhiModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlMaleZuixingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlMaleZuixingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlMaleYanxingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlMaleYanxingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlMaleZhanHengModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlMaleZhanHengModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlMaleSkinColorModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlMaleSkinColorModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
            }
            else if (N_sex == 0)
            {
                if (CatgirlFemaleHairModel.Count != 0)
                {
                    enemySkinCode.Add(Random.Range(0, CatgirlFemaleHairModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlFemaleMeimaoModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlFemaleMeimaoModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlFemaleHuzhiModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlFemaleHuzhiModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlFemaleZuixingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlFemaleZuixingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlFemaleYanxingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlFemaleYanxingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlFemaleZhanHengModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlFemaleZhanHengModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (CatgirlFemaleSkinColorModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(CatgirlFemaleSkinColorModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
            }
        }
        // Delu
        else if (N_race == 2)
        {
            if (N_sex == 1)
            {
                if (DeluMaleHairModel.Count != 0)
                {
                    enemySkinCode.Add(Random.Range(0,DeluMaleHairModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluMaleMeimaoModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluMaleMeimaoModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluMaleHuzhiModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluMaleHuzhiModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluMaleZuixingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluMaleZuixingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluMaleYanxingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluMaleYanxingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluMaleZhanHengModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluMaleZhanHengModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluMaleSkinColorModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluMaleSkinColorModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
            }
            else if (N_sex == 0)
            {
                if (DeluFemaleHairModel.Count != 0)
                {
                    enemySkinCode.Add(Random.Range(0, DeluFemaleHairModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluFemaleMeimaoModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluFemaleMeimaoModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluFemaleHuzhiModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluFemaleHuzhiModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluFemaleZuixingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluFemaleZuixingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluFemaleYanxingModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluFemaleYanxingModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluFemaleZhanHengModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluFemaleZhanHengModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
                if (DeluFemaleSkinColorModel.Count != 0)
                {
                    enemySkinCode.Add(random.Next(DeluFemaleSkinColorModel.Count));
                }
                else
                {
                    enemySkinCode.Add(-1);
                }
            }
        }

        // skin color
        if (N_race == 0)
        {
            enemySkinCode.Add(random.Next(human_skin_color.Length / 3));
        }
        else if (N_race == 1)
        {
            enemySkinCode.Add(random.Next(catgirl_skin_color.Length / 3));
        }
        else
        {
            enemySkinCode.Add(-1);
        }

        // hair color code
        enemySkinCode.Add(random.Next(hair_color.Length / 3));


        //for (int i = 0; i < enemySkinCode.Count; i++)
        //{
        //    Debug.Log("UUUU: "+i+"  "+enemySkinCode[i]);
        //} 
        return enemySkinCode;
    }

    public void ChangeSkinByCode(List<int> enemySkinCode)
    {
        if (enemySkinCode == null)
        {
            enemySkinCode = GeneralizeEnemySkinCode();
        }

        // Human
        if (enemySkinCode[0] == 0)
        {
            if (enemySkinCode[1] == 1)
            {
                if (HumanMaleHairModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanMaleHairModel[enemySkinCode[2]], 0, enemySkinCode);
                }
                if (HumanMaleMeimaoModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanMaleMeimaoModel[enemySkinCode[3]], 3, enemySkinCode);
                }
                if (HumanMaleHuzhiModel.Count != 0)
                {
                    //  ChangeModuleMeshes(HumanMaleHuzhiModel[enemySkinCode[4]],3, enemySkinCode);
                }
                if (HumanMaleZuixingModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanMaleZuixingModel[enemySkinCode[5]], 5, enemySkinCode);
                }
                if (HumanMaleYanxingModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanMaleYanxingModel[enemySkinCode[6]], 6, enemySkinCode);
                }
                if (HumanMaleZhanHengModel.Count != 0)
                {
                    //   ChangeModuleMeshes(HumanMaleZhanHengModel[enemySkinCode[7]],6, enemySkinCode);
                }
                if (HumanMaleSkinColorModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanMaleSkinColorModel[enemySkinCode[8]], 11, enemySkinCode);
                }
            }
            else if (enemySkinCode[1] == 0)
            {
                if (HumanFemaleHairModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanFemaleHairModel[enemySkinCode[2]], 0, enemySkinCode);
                }
                if (HumanFemaleMeimaoModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanFemaleMeimaoModel[enemySkinCode[3]], 3, enemySkinCode);
                }
                if (HumanFemaleHuzhiModel.Count != 0)
                {
                    //   ChangeModuleMeshes(HumanFemaleHuzhiModel[enemySkinCode[4]],3, enemySkinCode);
                }
                if (HumanFemaleZuixingModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanFemaleZuixingModel[enemySkinCode[5]], 5, enemySkinCode);
                }
                if (HumanFemaleYanxingModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanFemaleYanxingModel[enemySkinCode[6]], 6, enemySkinCode);
                }
                if (HumanFemaleZhanHengModel.Count != 0)
                {
                    //  ChangeModuleMeshes(HumanFemaleZhanHengModel[enemySkinCode[7]],6, enemySkinCode);
                }
                if (HumanFemaleSkinColorModel.Count != 0)
                {
                    ChangeModuleMeshes(HumanFemaleSkinColorModel[enemySkinCode[8]], 11, enemySkinCode);
                }
            }
        }
        // Catgirl
        else if (enemySkinCode[0] == 1)
        {
            if (enemySkinCode[1] == 1)
            {
                if (CatgirlMaleHairModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlMaleHairModel[enemySkinCode[2]], 0, enemySkinCode);
                }
                if (CatgirlMaleMeimaoModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlMaleMeimaoModel[enemySkinCode[3]], 3, enemySkinCode);
                }
                if (CatgirlMaleHuzhiModel.Count != 0)
                {
                    //  ChangeModuleMeshes(CatgirlMaleHuzhiModel[enemySkinCode[4]],3, enemySkinCode);
                }
                if (CatgirlMaleZuixingModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlMaleZuixingModel[enemySkinCode[5]], 5, enemySkinCode);
                }
                if (CatgirlMaleYanxingModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlMaleYanxingModel[enemySkinCode[6]], 6, enemySkinCode);
                }
                if (CatgirlMaleZhanHengModel.Count != 0)
                {
                    //    ChangeModuleMeshes(CatgirlMaleZhanHengModel[enemySkinCode[7]],6, enemySkinCode);
                }
                if (CatgirlMaleSkinColorModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlMaleSkinColorModel[enemySkinCode[8]], 11, enemySkinCode);
                }
            }
            else if (enemySkinCode[1] == 0)
            {
                if (CatgirlFemaleHairModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlFemaleHairModel[enemySkinCode[2]], 0, enemySkinCode);
                }
                if (CatgirlFemaleMeimaoModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlFemaleMeimaoModel[enemySkinCode[3]], 3, enemySkinCode);
                }
                if (CatgirlFemaleHuzhiModel.Count != 0)
                {
                    //    ChangeModuleMeshes(CatgirlFemaleHuzhiModel[enemySkinCode[4]],3, enemySkinCode);
                }
                if (CatgirlFemaleZuixingModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlFemaleZuixingModel[enemySkinCode[5]], 5, enemySkinCode);
                }
                if (CatgirlFemaleYanxingModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlFemaleYanxingModel[enemySkinCode[6]], 6, enemySkinCode);
                }
                if (CatgirlFemaleZhanHengModel.Count != 0)
                {
                    //   ChangeModuleMeshes(CatgirlFemaleZhanHengModel[enemySkinCode[7]],6, enemySkinCode);
                }
                if (CatgirlFemaleSkinColorModel.Count != 0)
                {
                    ChangeModuleMeshes(CatgirlFemaleSkinColorModel[enemySkinCode[8]], 11, enemySkinCode);
                }
            }
        }
        // Delu
        else if (enemySkinCode[0] == 2)
        {
            if (enemySkinCode[1] == 1)
            {
                if (DeluMaleHairModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluMaleHairModel[enemySkinCode[2]], 0, enemySkinCode);
                }
                if (DeluMaleMeimaoModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluMaleMeimaoModel[enemySkinCode[3]], 3, enemySkinCode);
                }
                if (DeluMaleHuzhiModel.Count != 0)
                {
                    //   ChangeModuleMeshes(DeluMaleHuzhiModel[enemySkinCode[4]],3, enemySkinCode);
                }
                if (DeluMaleZuixingModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluMaleZuixingModel[enemySkinCode[5]], 5, enemySkinCode);
                }
                if (DeluMaleYanxingModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluMaleYanxingModel[enemySkinCode[6]], 6, enemySkinCode);
                }
                if (DeluMaleZhanHengModel.Count != 0)
                {
                    //   ChangeModuleMeshes(DeluMaleZhanHengModel[enemySkinCode[7]],6, enemySkinCode);
                }
                if (DeluMaleSkinColorModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluMaleSkinColorModel[enemySkinCode[8]], 11, enemySkinCode);
                }
            }
            else if (enemySkinCode[1] == 0)
            {
                if (DeluFemaleHairModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluFemaleHairModel[enemySkinCode[2]], 0, enemySkinCode);
                }
                if (DeluFemaleMeimaoModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluFemaleMeimaoModel[enemySkinCode[3]], 3, enemySkinCode);
                }
                if (DeluFemaleHuzhiModel.Count != 0)
                {
                    //   ChangeModuleMeshes(DeluFemaleHuzhiModel[enemySkinCode[4]],3, enemySkinCode);
                }
                if (DeluFemaleZuixingModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluFemaleZuixingModel[enemySkinCode[5]], 5, enemySkinCode);
                }
                if (DeluFemaleYanxingModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluFemaleYanxingModel[enemySkinCode[6]], 6, enemySkinCode);
                }
                if (DeluFemaleZhanHengModel.Count != 0)
                {
                    //   ChangeModuleMeshes(DeluFemaleZhanHengModel[enemySkinCode[7]],6, enemySkinCode);
                }
                if (DeluFemaleSkinColorModel.Count != 0)
                {
                    ChangeModuleMeshes(DeluFemaleSkinColorModel[enemySkinCode[8]], 11, enemySkinCode);
                }
            }
        }
    }

    public void ChangeModuleMeshes(SkinModelInstance mod,int StartId,List<int> enemySkinCode)
    {
        System.Random random = new System.Random();

        if (mod != null)
        {

            for (int i = 0; i < mod.SkinMeshes.Length; i++)
            {
                int j = i + StartId;

                SkinRenders[j].spriteMesh = mod.SkinMeshes[i];
                if (mod.modelType == ModelType.SkinColorModel)
                {
                    // print(0);
                    if (mod.raceType == RaceType.Human)
                    {
                        // Debug.Log("SkinColor Code: " + enemySkinCode[9]);
                        SkinRenders[j].color = new Color(human_skin_color[enemySkinCode[8], 0] / 255, human_skin_color[enemySkinCode[8], 1] / 255, human_skin_color[enemySkinCode[8], 2] / 255);
                    //    Debug.Log(mod.modelType + " " + mod.sexType + " " + mod.modelType + " Human: " + human_skin_color[HumanSkinC, 0] + " " + human_skin_color[HumanSkinC, 1] + " " + human_skin_color[HumanSkinC, 2]);
                    }
                    else if (mod.raceType == RaceType.Catgirl)
                    {
                        SkinRenders[j].color = new Color(catgirl_skin_color[enemySkinCode[9], 0] / 255, catgirl_skin_color[enemySkinCode[9], 1] / 255, catgirl_skin_color[enemySkinCode[9], 2] / 255);
                    //    Debug.Log(mod.modelType + " " + mod.sexType + " " + mod.modelType + "Catgirl: " + catgirl_skin_color[CatgirlC, 0] + " " + catgirl_skin_color[CatgirlC, 1] + " " + catgirl_skin_color[CatgirlC, 2]);
                    }
                    else
                    {
                        SkinRenders[j].color = new Color(1, 1, 1);
                        //   Debug.Log(mod.raceType + "" + mod.sexType + "" + mod.modelType);
                    }

                }
                else if (mod.modelType == ModelType.HairModel || mod.modelType == ModelType.MeimaoModel)
                {
                    // Debug.Log("Hair Code: " + enemySkinCode[9]);

                    SkinRenders[j].color = new Color(hair_color[enemySkinCode[10], 0] / 255, hair_color[enemySkinCode[10], 1] / 255, hair_color[enemySkinCode[10], 2] / 255);
                }
                else
                {
                    SkinRenders[j].color = new Color(1, 1, 1);
                 //   Debug.Log(mod.raceType + "" + mod.sexType + "" + mod.modelType);
                }


            }
        }
        // xxx.spriteMeshRender.spriteMesh = mod.SkinMeshes[0];
    }

    //public void SkinColorController()
    //{
    //    for (int i = 0; i < HumanMaleSkinColorModel[0].SkinMeshes.Length; i++)
    //    {
    //        int j = i + 11;

    //        SkinRenders[j].color = new Color(R_skin_Slider / 255, G_skin_Slider / 255, B_skin_Slider / 255);

    //    }
    //}

    //public void hairColorController()
    //{
    //    for (int i = 0; i < HumanMaleHairModel[0].SkinMeshes.Length; i++)
    //    {
    //        for (int j = 0; j < SkinRenders.Count; j++)
    //        {
    //            CharacterSpriteMeshType type = (CharacterSpriteMeshType)j;
    //            if (type.ToString() == HumanMaleHairModel[0].SkinMeshes[i].name)
    //                SkinRenders[j].color = new Color(R_hair_Slider / 255, G_hair_Slider / 255, B_hair_Slider / 255);
    //        }
    //    }
    //}

    public void ClearModelsList()
    {
        HumanMaleHairModel.Clear();
        HumanMaleMeimaoModel.Clear();
        HumanMaleHuzhiModel.Clear();
        HumanMaleZuixingModel.Clear();
        HumanMaleYanxingModel.Clear();
        HumanMaleZhanHengModel.Clear();
        HumanMaleSkinColorModel.Clear();

        HumanFemaleHairModel.Clear();
        HumanFemaleMeimaoModel.Clear();
        HumanFemaleHuzhiModel.Clear();
        HumanFemaleZuixingModel.Clear();
        HumanFemaleYanxingModel.Clear();
        HumanFemaleZhanHengModel.Clear();
        HumanFemaleSkinColorModel.Clear();

        CatgirlMaleHairModel.Clear();
        CatgirlMaleMeimaoModel.Clear();
        CatgirlMaleHuzhiModel.Clear();
        CatgirlMaleZuixingModel.Clear();
        CatgirlMaleYanxingModel.Clear();
        CatgirlMaleZhanHengModel.Clear();
        CatgirlMaleSkinColorModel.Clear();

        CatgirlFemaleHairModel.Clear();
        CatgirlFemaleMeimaoModel.Clear();
        CatgirlFemaleHuzhiModel.Clear();
        CatgirlFemaleZuixingModel.Clear();
        CatgirlFemaleYanxingModel.Clear();
        CatgirlFemaleZhanHengModel.Clear();
        CatgirlFemaleSkinColorModel.Clear();

        DeluMaleHairModel.Clear();
        DeluMaleMeimaoModel.Clear();
        DeluMaleHuzhiModel.Clear();
        DeluMaleZuixingModel.Clear();
        DeluMaleYanxingModel.Clear();
        DeluMaleZhanHengModel.Clear();
        DeluMaleSkinColorModel.Clear();

        DeluFemaleHairModel.Clear();
        DeluFemaleMeimaoModel.Clear();
        DeluFemaleHuzhiModel.Clear();
        DeluFemaleZuixingModel.Clear();
        DeluFemaleYanxingModel.Clear();
        DeluFemaleZhanHengModel.Clear();
        DeluFemaleSkinColorModel.Clear();
    }

    public void printModelsList()
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!! Huamn Male !!!!!!!!!!!!!!!!!!!");
        Debug.Log("HumanMaleHairModel: " + HumanMaleHairModel.Count);
        Debug.Log("HumanMaleMeimaoModel: " + HumanMaleMeimaoModel.Count);
        Debug.Log("HumanMaleHuzhiModel: " + HumanMaleHuzhiModel.Count);
        Debug.Log("HumanMaleZuixingModel: " + HumanMaleZuixingModel.Count);
        Debug.Log("HumanMaleYanxingModel: " + HumanMaleYanxingModel.Count);
        Debug.Log("HumanMaleZhanHengModel: " + HumanMaleZhanHengModel.Count);
        Debug.Log("HumanMaleSkinColorModel: " + HumanMaleSkinColorModel.Count);

        Debug.Log("!!!!!!!!!!!!!!!!!!! Huamn Female !!!!!!!!!!!!!!!!!!!");
        Debug.Log("HumanFemaleHairModel: " + HumanFemaleHairModel.Count);
        Debug.Log("HumanFemaleMeimaoModel: " + HumanFemaleMeimaoModel.Count);
        Debug.Log("HumanFemaleHuzhiModel: " + HumanFemaleHuzhiModel.Count);
        Debug.Log("HumanFemaleZuixingModel: " + HumanFemaleZuixingModel.Count);
        Debug.Log("HumanFemaleYanxingModel: " + HumanFemaleYanxingModel.Count);
        Debug.Log("HumanFemaleZhanHengModel: " + HumanFemaleZhanHengModel.Count);
        Debug.Log("HumanFemaleSkinColorModel: " + HumanFemaleSkinColorModel.Count);

        Debug.Log("!!!!!!!!!!!!!!!!!!! Catgirl Male !!!!!!!!!!!!!!!!!!!");
        Debug.Log("CatgirlMaleHairModel: " + CatgirlMaleHairModel.Count);
        Debug.Log("CatgirlMaleMeimaoModel: " + CatgirlMaleMeimaoModel.Count);
        Debug.Log("CatgirlMaleHuzhiModel: " + CatgirlMaleHuzhiModel.Count);
        Debug.Log("CatgirlMaleZuixingModel: " + CatgirlMaleZuixingModel.Count);
        Debug.Log("CatgirlMaleYanxingModel: " + CatgirlMaleYanxingModel.Count);
        Debug.Log("CatgirlMaleZhanHengModel: " + CatgirlMaleZhanHengModel.Count);
        Debug.Log("CatgirlMaleSkinColorModel: " + CatgirlMaleSkinColorModel.Count);

        Debug.Log("!!!!!!!!!!!!!!!!!!! Catgirl Female !!!!!!!!!!!!!!!!!!!");
        Debug.Log("CatgirlFemaleHairModel: " + CatgirlFemaleHairModel.Count);
        Debug.Log("CatgirlFemaleMeimaoModel: " + CatgirlFemaleMeimaoModel.Count);
        Debug.Log("CatgirlFemaleHuzhiModel: " + CatgirlFemaleHuzhiModel.Count);
        Debug.Log("CatgirlFemaleZuixingModel: " + CatgirlFemaleZuixingModel.Count);
        Debug.Log("CatgirlFemaleYanxingModel: " + CatgirlFemaleYanxingModel.Count);
        Debug.Log("CatgirlFemaleZhanHengModel: " + CatgirlFemaleZhanHengModel.Count);
        Debug.Log("CatgirlFemaleSkinColorModel: " + CatgirlFemaleSkinColorModel.Count);


        Debug.Log("!!!!!!!!!!!!!!!!!!! Delu Male !!!!!!!!!!!!!!!!!!!");
        Debug.Log("DeluMaleHairModel: " + DeluMaleHairModel.Count);
        Debug.Log("DeluMaleMeimaoModel: " + DeluMaleMeimaoModel.Count);
        Debug.Log("DeluMaleHuzhiModel: " + DeluMaleHuzhiModel.Count);
        Debug.Log("DeluMaleZuixingModel: " + DeluMaleZuixingModel.Count);
        Debug.Log("DeluMaleYanxingModel: " + DeluMaleYanxingModel.Count);
        Debug.Log("DeluMaleZhanHengModel: " + DeluMaleZhanHengModel.Count);
        Debug.Log("DeluMaleSkinColorModel: " + DeluMaleSkinColorModel.Count);

        Debug.Log("!!!!!!!!!!!!!!!!!!! Delu Female !!!!!!!!!!!!!!!!!!!");
        Debug.Log("DeluFemaleHairModel: " + DeluFemaleHairModel.Count);
        Debug.Log("DeluFemaleMeimaoModel: " + DeluFemaleMeimaoModel.Count);
        Debug.Log("DeluFemaleHuzhiModel: " + DeluFemaleHuzhiModel.Count);
        Debug.Log("DeluFemaleZuixingModel: " + DeluFemaleZuixingModel.Count);
        Debug.Log("DeluFemaleYanxingModel: " + DeluFemaleYanxingModel.Count);
        Debug.Log("DeluFemaleZhanHengModel: " + DeluFemaleZhanHengModel.Count);
        Debug.Log("DeluFemaleSkinColorModel: " + DeluFemaleSkinColorModel.Count);
    }
    /// <summary>
    /// tools
    /// </summary>
    public void PrintList(List<int> aList)
    {
        string temp = " ";
        if (aList == null)
        {
            Debug.Log("NULL LLLisft");
        }
        else
        {
            for (int i = 0; i < aList.Count; i++)
            {
                temp = temp + aList[i] + " ";
            }
        }
        Debug.Log(temp);
    }
}

#endif