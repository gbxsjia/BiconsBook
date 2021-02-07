#if UNITY_EDITOR 

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkinTesterTool))]
public class SkinTesterToolEditor : Editor
{
    override public void OnInspectorGUI()
    {
        SkinTesterTool skinTesterTool = (SkinTesterTool)target;
        if (GUILayout.Button("GetAllSkinRenders"))
        {
            skinTesterTool.GetAllSkinRenders(); 
        }
        if (GUILayout.Button("UseMeshGroup1"))
        {
            skinTesterTool.UseSkinMeshes1(); 
        }
        if (GUILayout.Button("UseMeshGroup2"))
        {
            skinTesterTool.UseSkinMeshes2(); 
        }
        if (GUILayout.Button("UseMeshGroup3"))
        {
            skinTesterTool.UseSkinMeshes3();
        }
        if (GUILayout.Button("UseMeshGroup4"))
        {
            skinTesterTool.UseSkinMeshes4(); 
        }
        if (GUILayout.Button("UseMeshGroup5"))
        {
            skinTesterTool.UseSkinMeshes5(); 
        }
        if (GUILayout.Button("ClearMesh"))
        {
            skinTesterTool.ClearSkinMesh();
        }

        DrawDefaultInspector();
    }
}
#endif