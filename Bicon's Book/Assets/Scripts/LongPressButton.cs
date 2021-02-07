using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;
    private float escTimer;

    public float requiredHoldTime;

    public GameObject loadingBackground;
    public GameObject Quit;

    [SerializeField]
    private Slider QuitSlider;

    private void Start()
    {
        escTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (pointerDown || Input.GetKey(KeyCode.Escape))
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                StartCoroutine(GoMain());
            }
            escTimer = 0;
        }
        else
        {
            Reset();
            escTimer += Time.deltaTime;
            if (escTimer >= 5)
            {
                Quit.SetActive(false);
                escTimer = 0;
            }

            if (Input.anyKey)
                Quit.SetActive(true);
        }

        QuitSlider.value = pointerDownTimer / requiredHoldTime;
    }

    IEnumerator GoMain()
    {
        loadingBackground.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        QuitSlider.value = pointerDownTimer / requiredHoldTime;
    }
}
