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

[CustomEditor(typeof(SkinManager))]
public class SkinManagerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        SkinManager skinManager = (SkinManager)target;

        if (GUILayout.Button("NewSkin"))
        {
            skinManager.ReadSkinCodeFromJSON();
            skinManager.SaveSkinCodetoJSON();
            skinManager.selectedEnemySkinCodesHolder = skinManager.GeneralizeEnemySkinCode();
            skinManager.ChangeSkinByCode(skinManager.selectedEnemySkinCodesHolder);
            skinManager.current_SelectedEnemySkinCode_Index = -1;
        }
        if (GUILayout.Button("加入二级库"))
        {
            skinManager.AddSelectedEnemySkinCodes();
        }
        if (GUILayout.Button("随机二级库皮肤"))
        {
            if (skinManager.RandomSelectedEnemySkinCodes() != null)
            {
                skinManager.ChangeSkinByCode(skinManager.RandomSelectedEnemySkinCodes());
            }
        }
        if (GUILayout.Button("移除二级库皮肤"))
        {
            if (skinManager.selectedEnemySkinCodesList.Count != 0)
            {
                skinManager.ReadSkinCodeFromJSON();
                if (skinManager.current_SelectedEnemySkinCode_Index > -1)
                {
                    Debug.Log("删掉了:");
                    skinManager.selectedEnemySkinCodesList.RemoveAt(skinManager.current_SelectedEnemySkinCode_Index);
                }
                skinManager.ChangeSkinByCode(skinManager.RandomSelectedEnemySkinCodes());
                skinManager.SaveSkinCodetoJSON();
            }
            else
            {
                Debug.Log("全部都没有了！");
            }
        }
        if (GUILayout.Button("InitialAllSkin"))
        {
      
            skinManager.InitialPools();
            skinManager.GetRealSkinRenders();    
            skinManager.GetSkinRenders();
        }
        /// For Test Purpose Only ///
        //if (GUILayout.Button("Save"))
        //{
        //    skinManager.SaveSkinCodetoJSON();
        //}
        //if (GUILayout.Button("Read"))
        //{
        //    skinManager.ReadSkinCodeFromJSON();
        //}
        //if (GUILayout.Button("SelectedAllSkin"))
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        skinManager.selectedEnemySkinCodesHolder = skinManager.GeneralizeEnemySkinCode();
        //        skinManager.AddSelectedEnemySkinCodes();
        //    }
        //}
        if (GUILayout.Button("Tester"))
        {
            skinManager.tester();
        }

        DrawDefaultInspector();
    }
}
#endif