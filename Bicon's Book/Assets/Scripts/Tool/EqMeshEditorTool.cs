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

public class EqMeshEditorTool : MonoBehaviour
{
    [SerializeField] List<Equipment> equipments;

    [SerializeField] ArmorList ArmorStyle;

    public List<ArmorTypeList> ArmorTypeLists;

    [System.Serializable]
    public class ArmorTypeList
    {

        public string name;
        public List<SpriteMesh> ArmorMeshList;
    }

    public void RenewListAndTypeList()
    {
        GenArmorTypeEnum();
        UpdateArmorTypeListName();
    }

    public enum EqmFileNameType
    {
        weapon,
        arm,
        leg,
        body,
        back,
        ass,
        hubo,
        feet,
        hand,
        shoulder,
        tail,
        ear,
        hair
    }

    

    public List<EqmFileNameType> EqmToMeshNameTransfer(EquipmentType equipmentType)
    {
        List<EqmFileNameType> eqmFileNameTypes = new List<EqmFileNameType>();
        switch (equipmentType)
        {
            case (EquipmentType.Armor):
                {
                    eqmFileNameTypes.Add(EqmFileNameType.ass);
                    eqmFileNameTypes.Add(EqmFileNameType.back);
                    eqmFileNameTypes.Add(EqmFileNameType.body);
                    eqmFileNameTypes.Add(EqmFileNameType.hubo);

                }
                break;

            case (EquipmentType.LeftFoot):
                {
                    eqmFileNameTypes.Add(EqmFileNameType.feet);
                }
                break;
            case (EquipmentType.LeftArm):
                {
                    eqmFileNameTypes.Add(EqmFileNameType.arm);
                }
                break;
            case (EquipmentType.RightArm):
                {
                    eqmFileNameTypes.Add(EqmFileNameType.arm);
                }
                break;

            case (EquipmentType.LeftLeg):
                {
                    eqmFileNameTypes.Add(EqmFileNameType.leg);
                }
                break;
            case (EquipmentType.RightLeg):
                {
                    eqmFileNameTypes.Add(EqmFileNameType.leg);
                }
                break;

            case (EquipmentType.LeftHand):
                {
                    eqmFileNameTypes.Add(EqmFileNameType.hand);
                }
                break;
            case (EquipmentType.RightHand):
                {
                    eqmFileNameTypes.Add(EqmFileNameType.hand);
                }
                break;
            case (EquipmentType.Helmet):
                {
                    eqmFileNameTypes.Add(EqmFileNameType.hair);
                }
                break;
        }

        return eqmFileNameTypes;
    }

    public void AddMesh()
    {
        for (int z = 0; z < equipments.Count; z++)
        {
            List<SpriteMesh> bodymeshes = new List<SpriteMesh>();
            for (int i = 0; i < ArmorTypeLists.Count; i++)
            {
                if (ArmorStyle.ToString() == ArmorTypeLists[i].name)
                {
                    List<EqmFileNameType> aNameList = EqmToMeshNameTransfer(equipments[z].SlotTypes[0]);
                    for (int x = 0; x < aNameList.Count; x++)
                    {
                        for (int y = 0; y < ArmorTypeLists[i].ArmorMeshList.Count; y++)
                        {
                            int TypestringLength = aNameList[x].ToString().Length;
                            string MeshName = ArmorTypeLists[i].ArmorMeshList[y].name;
                            string MeshNameFront = MeshName.Substring(0, TypestringLength);

                            if (MeshNameFront == aNameList[x].ToString())
                            {
                                bodymeshes.Add(ArmorTypeLists[i].ArmorMeshList[y]);
                            }
                        }
                    }
                }
            }

            var sortedList = bodymeshes.OrderBy(go => go.name).ToList();

            equipments[z].spriteMeshes = new SpriteMesh[bodymeshes.Count];

            for (int i = 0; i < equipments[z].spriteMeshes.Length; i++)
            {
                equipments[z].spriteMeshes[i] = bodymeshes[i];
            }

            equipments[z].UpdateCheckID = Random.Range(0, 999999);

            EditorUtility.SetDirty(equipments[z]);
        }
    }
    public void UpdateArmorTypeListName()
    {
        ArmorTypeLists = new List<ArmorTypeList>();

        for(int i = 0; i < (int)ArmorList.End;i++)
        {
            ArmorTypeLists.Add(new ArmorTypeList());
            ArmorTypeLists[i].name = System.Enum.GetName(typeof(ArmorList), i);
        }
        
    }
    public void GenArmorTypeEnum()
    {    
        
        string fullPath = "Assets/ArtResource/Armor";

        if (Directory.Exists(fullPath))
        {
            string[] dirs = Directory.GetFiles(Application.dataPath + "/ArtResource/Armor");


            List<string> ArmorTypeNameList = new List<string>();
            for (int i = 0; i < dirs.Length; i++)
            {
                ArmorTypeNameList.Add(Path.GetFileNameWithoutExtension(dirs[i]));
            }

            var arg = "";
            foreach (var PngName in ArmorTypeNameList)
            {
                arg += "\t" + PngName + ",\n";
            }

            arg += "\t" + "End" + ",\n";
            var res = "public enum ArmorList\n{\n" + arg + "}\n";
            var path = Application.dataPath + "/Scripts/Tool/ArmorList.cs";
            File.WriteAllText(path, res, Encoding.UTF8);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }



}

#endif