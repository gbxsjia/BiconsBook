#if UNITY_EDITOR 

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EqMeshEditorTool))]
public class EqMeshEditor : Editor
{
    override public void OnInspectorGUI()
    {
        EqMeshEditorTool weaponTesterTool = (EqMeshEditorTool)target;     
        if (GUILayout.Button("盖章"))
        {
            weaponTesterTool.AddMesh();
        }
        if (GUILayout.Button("RenewListAndType"))
        {
            weaponTesterTool.RenewListAndTypeList();
        }
        DrawDefaultInspector();
    }
}
#endif