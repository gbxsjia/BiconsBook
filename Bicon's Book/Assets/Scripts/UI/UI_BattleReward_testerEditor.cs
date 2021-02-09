#if UNITY_EDITOR 

using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Anima2D;
using System.Linq;
using UnityEditor.SceneManagement;
[CustomEditor(typeof(UI_BattleReward_tester))]
public class UI_BattleReward_testerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UI_BattleReward_tester ui_battlereward = (UI_BattleReward_tester)target;
        if (GUILayout.Button("击破头"))
        {
            ui_battlereward.activeText(5);
            BodyPart aBodyPart = new BodyPart();
            aBodyPart.EquipItem(ui_battlereward.AvaliablePotionList[Random.Range(0, ui_battlereward.AvaliablePotionList.Count)]);
            ui_battlereward.AddReward(aBodyPart);
            ui_battlereward.BattleEndStartProcess_tester(true);
        }
        if (GUILayout.Button("击破左臂"))
        {
            ui_battlereward.activeText(1);
            BodyPart aBodyPart = new BodyPart();
            aBodyPart.EquipItem(ui_battlereward.AvaliablePotionList[Random.Range(0, ui_battlereward.AvaliablePotionList.Count)]);
            ui_battlereward.AddReward(aBodyPart);
            ui_battlereward.BattleEndStartProcess_tester(true);
        }
        if (GUILayout.Button("击破右臂"))
        {
            ui_battlereward.activeText(2);
            BodyPart aBodyPart = new BodyPart();
            aBodyPart.EquipItem(ui_battlereward.AvaliablePotionList[Random.Range(0, ui_battlereward.AvaliablePotionList.Count)]);
            ui_battlereward.AddReward(aBodyPart);
            ui_battlereward.BattleEndStartProcess_tester(true);
        }
        if (GUILayout.Button("清空面板"))
        {
            ui_battlereward.Cleaning();
            ui_battlereward.inactiveText();
        }
        DrawDefaultInspector();
    }
}
#endif