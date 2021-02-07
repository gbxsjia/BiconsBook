using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Tu_BigWorld : MonoBehaviour
{
    public static Tu_BigWorld instance;
    [SerializeField] GameObject BlackPanel;
    [SerializeField] TextMeshProUGUI HintContextText;
    public bool InTutorial;
    public List<Tu_StageList_BigWorld> TutorialType;
    

    [System.Serializable]
    public class Tu_StageList_BigWorld
    {
        public string StageName;
        public int StageId;
        public List<Tu_SingleStage_BigWorld> SingleStageList;
    }

    [System.Serializable]
    public class Tu_SingleStage_BigWorld
    {
        public Vector3 BlackPanelPos;
        public Vector3 BlackPanelScale;
        public Vector3 ContextPos;
        public string TutorialContext;
        public UnityEvent TriggerEvent; 
    }

    private void Awake()
    {
        instance = this;
        if(GameWorldSetting.TutorialOpen == false)
        {
            enabled = false;
        }
    }

    public void StartTutorial(int TutorialId)
    {
        StartCoroutine(Tutorial(TutorialId));

    }
    IEnumerator Tutorial(int TutorialId)
    {
        if (InTutorial)
        {


            EnemyIncubator.thisInstance.InCameraMotion = true;



            yield return new WaitForSeconds(0.8f);

            BlackPanel.transform.parent.gameObject.SetActive(true);


            while (TutorialType[TutorialId].StageId < TutorialType[TutorialId].SingleStageList.Count)
            {

                Tu_SingleStage_BigWorld thisStage = TutorialType[TutorialId].SingleStageList[TutorialType[TutorialId].StageId];
                thisStage.TriggerEvent.Invoke();
                BlackPanel.GetComponent<RectTransform>().localPosition = thisStage.BlackPanelPos;
                BlackPanel.transform.localScale = thisStage.BlackPanelScale;
                HintContextText.transform.parent.gameObject.GetComponent<RectTransform>().localPosition = thisStage.ContextPos;
                HintContextText.text = thisStage.TutorialContext;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialType[TutorialId].StageId += 1;
                }
                yield return null;
            }
        }
        BlackPanel.transform.parent.gameObject.SetActive(false);
        EnemyIncubator.thisInstance.InCameraMotion = false;
        yield return null;
    }
}
