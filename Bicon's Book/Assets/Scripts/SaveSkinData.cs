using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSkinData : MonoBehaviour
{
    [SerializeField] public PotionData _PotionData = new PotionData();
    [SerializeField] public SkinCodeListsJsonClass _SkinCodeJsonClass = new SkinCodeListsJsonClass();
   

    public void SaveIntoJson()
    {
        if (System.IO.File.Exists(Application.dataPath + "/Scripts/PotionData.json"))
        {
            System.IO.File.Delete(Application.dataPath + "/Scripts/PotionData.json");
        }
        string potion = JsonUtility.ToJson(_SkinCodeJsonClass);
        System.IO.File.WriteAllText(Application.dataPath + "/Scripts/PotionData.json", potion);
        // Debug.Log("Saving Skin Data...");
    }

    public void ReadFromJson()
    {
        if (System.IO.File.Exists(Application.dataPath + "/Scripts/PotionData.json"))
        {
            string json = System.IO.File.ReadAllText(Application.dataPath + "/Scripts/PotionData.json");
            _SkinCodeJsonClass = JsonUtility.FromJson<SkinCodeListsJsonClass>(json);
        }
        // Debug.Log("Reading Skin Data...");
    }
}

[System.Serializable]
public class SkinCodeListsJsonClass
{
    public List<SkinCodeJsonClass> skinCodeListsJson = new List<SkinCodeJsonClass>();
}

[System.Serializable]
public class SkinCodeJsonClass
{
    public List<int> skinCodeJson = new List<int>();
}

[System.Serializable]
public class PotionData
{
    public static PotionData instance;
    //private void Awake()
    //{
    //    instance = this;
    //}
    //public string potion_name;
    //public int value;
    //public List<Effect> effect = new List<Effect>();
    public List<int> skinCodeHolderJson = new List<int>();
}

[System.Serializable]
public class Effect
{
    public string name;
    public string desc;
}