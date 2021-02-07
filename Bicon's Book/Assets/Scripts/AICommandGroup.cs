using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AICommand")]
public class AICommandGroup : ScriptableObject
{
    public AICommand[] commands;
}
[System.Serializable]
public class AICommand
{
    public AICommandType Type;
    public int MoveDirection;
    public BodyPartType TargetBodypartType;
    public string CardName;
}
public enum AICommandType
{
    Move,
    ChooseTarget,
    UseCard
}