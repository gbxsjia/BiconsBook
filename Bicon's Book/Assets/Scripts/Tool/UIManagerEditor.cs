#if UNITY_EDITOR 

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UIManager manager = (UIManager)target;
        if (GUILayout.Button("装备栏初始化"))
        {
            manager.InitializeEquipmentSlots();
        }

        DrawDefaultInspector();
    }
}
#endif
