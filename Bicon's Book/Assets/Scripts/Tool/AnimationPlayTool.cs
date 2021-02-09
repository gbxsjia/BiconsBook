#if UNITY_EDITOR 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class AnimationPlayTool : MonoBehaviour
{
    [System.Serializable]
    public class AnimationSelect
    {
        public Animator m_Animator;
        public AnimationList m_AnimationType;

    }

    public AnimationSelect[] Animations;


    bool AlreadyStart = false;

    public void StartPreview()
    {
        if (AlreadyStart == false)
        {
            EditorApplication.update += PlayAnimation;
            AlreadyStart = true;
        }
    }

    public void EndPreview()
    {
        if (AlreadyStart == true)
        {
            EditorApplication.update -= PlayAnimation;
            AlreadyStart = false;
        }
    }



    List<string> AnimationNameList = new List<string>();
    void load()
    {
        AnimationNameList = new List<string>();
        List<string> filePaths = new List<string>();
        string imgtype = "*.ANIM";

        string[] dirs = Directory.GetFiles(Application.dataPath + "/Animation", imgtype);
        for (int j = 0; j < dirs.Length; j++)
        {
            filePaths.Add(dirs[j]);
        }

        print(filePaths.Count);

        for (int i = 0; i < filePaths.Count; i++)
        {
;
            AnimationNameList.Add(Path.GetFileNameWithoutExtension(filePaths[i]));
        }

    }

    public void GenAnimationNameEnum()
    {
        load();

        var arg = "";
        foreach (var PngName in AnimationNameList)
        {
            arg += "\t" + PngName + ",\n";
        }
        var res = "public enum AnimationList\n{\n" + arg + "}\n";
        var path = Application.dataPath + "/Scripts/Tool/AnimationList.cs";
        File.WriteAllText(path, res, Encoding.UTF8);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


    void PlayAnimation()
    {
        foreach (AnimationSelect a in Animations)
        {
            a.m_Animator.Play(a.m_AnimationType.ToString());
            a.m_Animator.Update(Time.deltaTime);
        }

    }
}
#endif