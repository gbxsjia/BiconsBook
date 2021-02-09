using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectManager : MonoBehaviour
{
    public static TextEffectManager instance;
    [SerializeField]
    private GameObject TextEffectPrefab;
    private void Awake()
    {
        instance = this;
    }

    private List<TextEffectInfo> textEffectInfos=new List<TextEffectInfo>();
    public void AddTextEffectInfos(TextEffectInfo effectInfo)
    {
        bool hasSame = false;
        foreach (TextEffectInfo info in textEffectInfos)
        {
            if (info.Type == effectInfo.Type && info.Bodypart == effectInfo.Bodypart)
            {
                hasSame = true;
                info.Value += effectInfo.Value;
                break;
            }
        }
        if (!hasSame)
        {
            textEffectInfos.Add(effectInfo);
        }
    }       
    public List<TextEffectInfo> GetTextEffectInfo()
    {
        List<TextEffectInfo> newList = new List<TextEffectInfo>();
        newList.AddRange(textEffectInfos);
        textEffectInfos.Clear();
        return newList;
    }

    public GameObject SpawnTextEffect(TextEffectInfo info)
    {
        BodyPart TargetBody = info.Bodypart;       
        string content="";
        float size = 100;   
        string animationName = "TextEffect_Float";    
        
        if (info.Value > 0)
        {
            content = info.Value.ToString();
        }
        else
        {
            content = info.Type.ToString();
        }
        GameObject g = Instantiate(TextEffectPrefab, UIManager.instance.transform);

        if (info.Bodypart.GetBodyPartType() == BodyPartType.None)
        {
            TargetBody = info.Bodypart.thisCharacter.GetBodyPart(BodyPartType.Chest);
            content = "闪避";
        }

        g.transform.position = UIManager.instance.MainCam.WorldToScreenPoint(TargetBody.transform.position + new Vector3(Random.Range(0f,1f), Random.Range(0f, 1f),0));

        Color c = Color.red;
     
        g.GetComponent<UI_TextEffect>().ShowTextMesh(content, c, size, animationName);

        Destroy(g, 1);
        return g;
    }
}
public class TextEffectInfo
{
    public EntryType Type;
    public BodyPart Bodypart;
    public float Value;
    public TextEffectInfo(EntryType type,BodyPart bodyPart,float value)
    {
        Type = type;
        Bodypart = bodyPart;
        Value = value;
    }
}