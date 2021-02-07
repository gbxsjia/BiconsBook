using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Tu_HardHint : MonoBehaviour
{
    public TextMeshProUGUI contentText;
    public Transform arrow;
    public Transform HintParentTransform;
    public GameObject InputCollectButton;

    public void UpdateHardHint(HardHintInfo info)
    {
        HintParentTransform.localPosition = info.MainPos;
        gameObject.SetActive(info.Active);
        contentText.text = info.Content;
        arrow.gameObject.SetActive(info.HasPointer);
        arrow.localPosition = info.PointerPos;
        arrow.localRotation = Quaternion.Euler(0, 0, info.PointerRotZ);
        InputCollectButton.SetActive(Tu_StageManager.instance.stageInfos[Tu_StageManager.instance.stageIndex].targetType == StageTargetType.ClickAnyPlace);
    }

    public void FinishStage()
    {
        Tu_StageManager.instance.StageFinish();
    }
}