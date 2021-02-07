#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimationPlayTool))]
public class AnimationPlayEditor : Editor
{
    
    override public void OnInspectorGUI()
    {
        AnimationPlayTool AnimationTesterTool = (AnimationPlayTool)target;
        if (GUILayout.Button("更新动画列表"))
        {
            AnimationTesterTool.GenAnimationNameEnum();
        }
        if (GUILayout.Button("播放动画(未完成)"))
        {
            AnimationTesterTool.StartPreview();
        }
        if (GUILayout.Button("暂停动画（未完成）"))
        {
            AnimationTesterTool.EndPreview();
        }

        DrawDefaultInspector();
    }
}
#endif