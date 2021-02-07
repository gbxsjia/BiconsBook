using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatusManager : MonoBehaviour
{
    static public BossStatusManager Instance;

    public BossStatus WDBossStatus;
    public BossStatus SunBossStatus;
    public BossStatus TFBossStatus;
    public BossStatus JudgementStatus;

    private void Awake()
    {
        Instance = this;
    } 
}
[System.Serializable]
public class BossStatus
{
    public int HeadHealth;
    public int BodyHealth;
    public int LArmHealth;
    public int RArmHealth;
    public int LLegHealth;
    public int RLegHealth;

    public bool DetailSetting = false;
    public int BossExpectDistance = 1;

    public List<Equipment> BossEqm;
}