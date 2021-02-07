using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public UIManager.TimeType SkyTimeType;
    SpriteRenderer thisSpriteRenderer;
    private void Start()
    {
        thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(UIManager.instance.timeType == SkyTimeType)
        {
            thisSpriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(thisSpriteRenderer.color.a, 1, 0.02f));
        }
        else
        {
            thisSpriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(thisSpriteRenderer.color.a, 0, 0.02f));
        }
    }


}
