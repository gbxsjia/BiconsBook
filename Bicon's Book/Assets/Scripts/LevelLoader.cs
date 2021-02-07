using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public Slider slider;

    private void Start()
    {
        LoadLevel(1);
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsuchronously(sceneIndex));

    }

    IEnumerator LoadAsuchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);


            slider.value = progress;

            Debug.Log(progress);

            yield return null;
        }
    }

    IEnumerator LoadAsuchronously_2(int sceneIndex)
    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            toProgress = (int) (operation.progress * 100);
            

            while (displayProgress < toProgress)
            {
                ++displayProgress;
                
                Debug.Log(displayProgress);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();
        }

    }
}

