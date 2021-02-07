#if UNITY_EDITOR 

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponTesterTool))]
public class WeaponTesterToolEditor : Editor
{
    override public void OnInspectorGUI()
    {
        WeaponTesterTool weaponTesterTool = (WeaponTesterTool)target;     
        if (GUILayout.Button("更新图片资源"))
        {
            weaponTesterTool.GenWeaponPngEnum();
        }
        if (GUILayout.Button("切换武器"))
        {
            weaponTesterTool.ChangeWeapons();
        }
        if (GUILayout.Button("交换双手武器"))
        {
            weaponTesterTool.ExchangeHands();
        }

        DrawDefaultInspector();
    }
}
#endif