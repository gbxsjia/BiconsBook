#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterManager))]
public class CharacterRenderTool : Editor
{
    override public void OnInspectorGUI()
    {
        CharacterManager skinTesterTool = (CharacterManager)target;
        if (GUILayout.Button("GetAllSkinRenders"))
        {
            skinTesterTool.GetAllSkinRenders();
        }
        if (GUILayout.Button("GetBodySprites"))
        {
            skinTesterTool.GetBodySprites();
        }
        DrawDefaultInspector();
    }
}
#endif