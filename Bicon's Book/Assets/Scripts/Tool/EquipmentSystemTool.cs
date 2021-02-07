#if UNITY_EDITOR 

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyIncubator))]
public class EquipmentSystemTool : Editor
{
    
    override public void OnInspectorGUI()
    {
        EnemyIncubator EnemyIncubatorTool = (EnemyIncubator)target;
        if (GUILayout.Button("装备种类分类"))
        {
            EnemyIncubatorTool.EquipmentSet();
        }


        DrawDefaultInspector();
    }
}
#endif
