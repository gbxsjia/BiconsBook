using System.Collections;
#if UNITY_EDITOR 

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteColorSaverLoader))]
public class ColorSnLEditor : Editor
{
    override public void OnInspectorGUI()
    {
        SpriteColorSaverLoader ColorTesterTool = (SpriteColorSaverLoader)target;
        if (GUILayout.Button("颜色保存"))
        {
            ColorTesterTool.SaveColor();
        }

        if (GUILayout.Button("颜色读取"))
        {
            ColorTesterTool.LoadColor();
        }

        DrawDefaultInspector();
    }
}
#endif