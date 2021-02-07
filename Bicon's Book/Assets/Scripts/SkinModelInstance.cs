using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[CreateAssetMenu(menuName ="Skin Module")]
public class SkinModelInstance : ScriptableObject
{
    public RaceType raceType;
    public SexType sexType;
    public ModelType modelType;

    public SpriteMesh[] SkinMeshes;
}

public enum RaceType
{
    Human,
    Catgirl,
    Delu
}

public enum SexType
{
    Male,
    Female
}

public enum ModelType
{
    HairModel,
    MeimaoModel,
    HuzhiModel,
    ZuixingModel,
    YanxingModel,
    ZhanHengModel,
    SkinColorModel
}