using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntryInfo
{
    public TriggerType triggerType;
    public EntryTargetMode TargetMode;
    public EntryType type;
    public Condition_Base condition;
    public int i1;
    public int i2;
    public bool b1;
    public bool b2;
    public GameObject g1;
    public GameObject g2;
    public string s1;
    public string s2;
    public BuffType TBT1;
    public enum EntryTargetMode
    {
        Single,
        Hori
    }
}

