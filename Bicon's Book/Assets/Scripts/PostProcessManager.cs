using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume[] postProcessVolumes;
    [SerializeField]
    private float ChangeDuration;
    void Start()
    {
        UIManager.instance.DayTimeChangeEvent += Instance_DayTimeChangeEvent;
    }
    Coroutine PostProcessE;
    private int lastDayTime = 0;
    private void Instance_DayTimeChangeEvent(int time)
    {
        if (PostProcessE != null)
        {
            StopCoroutine(PostProcessE);
        } 
        PostProcessE= StartCoroutine(PostProcessChange(time));
    }

    private IEnumerator PostProcessChange(int time)
    {        
        for (int i = 0; i < postProcessVolumes.Length; i++)
        {
            postProcessVolumes[i].enabled = time == i || lastDayTime == i;
        }
        if (time == lastDayTime)
        {
            postProcessVolumes[lastDayTime].weight = 1;
            yield break;
        }

        float timer = ChangeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            postProcessVolumes[time].weight = 1- timer / ChangeDuration;
            postProcessVolumes[lastDayTime].weight = timer / ChangeDuration;
            yield return null;
        }
        postProcessVolumes[time].weight = 1;
        postProcessVolumes[lastDayTime].weight = 0;
        postProcessVolumes[lastDayTime].enabled = false;
        lastDayTime = time;
    }
}
