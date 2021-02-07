using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class sceneLoader : MonoBehaviour
{
    public VideoPlayer vid;
    public GameObject quitToMain;


    private void Start()
    {
        quitToMain.SetActive(false);
        vid.loopPointReached += CheckOver;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            quitToMain.SetActive(true);
        }
    }

    public void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(2); // to Main Scene
    }

}
