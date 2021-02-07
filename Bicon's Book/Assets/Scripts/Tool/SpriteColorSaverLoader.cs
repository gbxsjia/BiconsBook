using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorSaverLoader : MonoBehaviour
{
    [SerializeField] MapStatus thisMap;
    public UIManager.TimeType ThisTimeType;
    public GroundDepth groundDepth;
    public enum GroundDepth
    {
        FG,
        MG,
        BG
    }

    public void SaveColor()
    {
        SpriteRenderer thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Vector3 thisColorVector3 = new Vector3(thisSpriteRenderer.color.r, thisSpriteRenderer.color.g, thisSpriteRenderer.color.b);
        switch (groundDepth)
        {
            case (GroundDepth.BG):
                {
                    if (ThisTimeType == UIManager.TimeType.夜晚)
                    {
                        thisMap.BGColor.NightColor = thisColorVector3;
                    }
                    if (ThisTimeType == UIManager.TimeType.正午)
                    {
                        thisMap.BGColor.NoonColor = thisColorVector3;
                    }
                    if (ThisTimeType == UIManager.TimeType.清晨)
                    {
                        thisMap.BGColor.MorningColor = thisColorVector3;
                    }
                    if (ThisTimeType == UIManager.TimeType.黄昏)
                    {
                        thisMap.BGColor.SunsetColor = thisColorVector3;
                    }
                }
                break;
            case (GroundDepth.MG):
                {
                    if (ThisTimeType == UIManager.TimeType.夜晚)
                    {
                        thisMap.MGColor.NightColor = thisColorVector3;
                    }
                    if (ThisTimeType == UIManager.TimeType.正午)
                    {
                        thisMap.MGColor.NoonColor = thisColorVector3;
                    }
                    if (ThisTimeType == UIManager.TimeType.清晨)
                    {
                        thisMap.MGColor.MorningColor = thisColorVector3;
                    }
                    if (ThisTimeType == UIManager.TimeType.黄昏)
                    {
                        thisMap.MGColor.SunsetColor = thisColorVector3;
                    }
                }
                break;
            case (GroundDepth.FG):
                {
                    if (ThisTimeType == UIManager.TimeType.夜晚)
                    {
                        thisMap.FGColor.NightColor = thisColorVector3;
                    }
                    if (ThisTimeType == UIManager.TimeType.正午)
                    {
                        thisMap.FGColor.NoonColor = thisColorVector3;
                    }
                    if (ThisTimeType == UIManager.TimeType.清晨)
                    {
                        thisMap.FGColor.MorningColor = thisColorVector3;
                    }
                    if (ThisTimeType == UIManager.TimeType.黄昏)
                    {
                        thisMap.FGColor.SunsetColor = thisColorVector3;
                    }
                }
                break;
        }
    }

    public void LoadColor()
    {
        SpriteRenderer thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch (groundDepth)
        {
            case (GroundDepth.FG):
                {
                    if (ThisTimeType == UIManager.TimeType.夜晚)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.FGColor.NightColor);
                    }
                    if (ThisTimeType == UIManager.TimeType.正午)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.FGColor.NoonColor);
                    }
                    if (ThisTimeType == UIManager.TimeType.清晨)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.FGColor.MorningColor);
                    }
                    if (ThisTimeType == UIManager.TimeType.黄昏)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.FGColor.SunsetColor);
                    }

                }
                break;
            case (GroundDepth.MG):
                {
                    if (ThisTimeType == UIManager.TimeType.夜晚)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.MGColor.NightColor);
                    }
                    if (ThisTimeType == UIManager.TimeType.正午)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.MGColor.NoonColor);
                    }
                    if (ThisTimeType == UIManager.TimeType.清晨)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.MGColor.MorningColor);
                    }
                    if (ThisTimeType == UIManager.TimeType.黄昏)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.MGColor.SunsetColor);
                    }
                }
                break;
            case (GroundDepth.BG):
                {
                    if (ThisTimeType == UIManager.TimeType.夜晚)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.BGColor.NightColor);
                    }
                    if (ThisTimeType == UIManager.TimeType.正午)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.BGColor.NoonColor);
                    }
                    if (ThisTimeType == UIManager.TimeType.清晨)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.BGColor.MorningColor);
                    }
                    if (ThisTimeType == UIManager.TimeType.黄昏)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.BGColor.SunsetColor);
                    }
                }
                break;
        }
    }
    public Color Vector3ToColor(Vector3 a)
    {
        SpriteRenderer thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        return (new Color(a.x, a.y, a.z, thisSpriteRenderer.color.a));
    }
    private void Update()
    {
        SpriteRenderer thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch (groundDepth)
        {
            case (GroundDepth.FG):
                {
                    if (UIManager.instance.timeType == UIManager.TimeType.夜晚)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.FGColor.NightColor);
                    }
                    if (UIManager.instance.timeType == UIManager.TimeType.正午)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.FGColor.NoonColor);
                    }
                    if (UIManager.instance.timeType == UIManager.TimeType.清晨)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.FGColor.MorningColor);
                    }
                    if (UIManager.instance.timeType == UIManager.TimeType.黄昏)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.FGColor.SunsetColor);
                    }

                }
                break;
            case (GroundDepth.MG):
                {
                    if (UIManager.instance.timeType == UIManager.TimeType.夜晚)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.MGColor.NightColor);
                    }
                    if (UIManager.instance.timeType == UIManager.TimeType.正午)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.MGColor.NoonColor);
                    }
                    if (UIManager.instance.timeType == UIManager.TimeType.清晨)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.MGColor.MorningColor);
                    }
                    if (UIManager.instance.timeType == UIManager.TimeType.黄昏)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.MGColor.SunsetColor);
                    }
                }
                break;
            case (GroundDepth.BG):
                {
                    if (UIManager.instance.timeType == UIManager.TimeType.夜晚)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.BGColor.NightColor);
                    }
                    if (UIManager.instance.timeType == UIManager.TimeType.正午)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.BGColor.NoonColor);
                    }
                    if (UIManager.instance.timeType == UIManager.TimeType.清晨)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.BGColor.MorningColor);
                    }
                    if (UIManager.instance.timeType == UIManager.TimeType.黄昏)
                    {
                        thisSpriteRenderer.color = Vector3ToColor(thisMap.BGColor.SunsetColor);
                    }
                }
                break;
        }
    }

}
