using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tu_SoftHint : MonoBehaviour
{
    public TextMeshProUGUI contentText;
    public CharacterManager character;
    private Vector3 PositionOffset=new Vector3(5.2f,6.2f,0);
    private Vector3 CurrentOffset;
    Camera cam;

    public void UpdateSoftHint(SoftHintInfo info)
    {
        gameObject.SetActive(info.Active);
        contentText.text = info.Content;
        character = InGameManager.instance.Characters[info.Camp];
        CurrentOffset = PositionOffset;
        CurrentOffset.x = PositionOffset.x * 1 - 2 * info.Camp;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (character.gameObject != null)
        {
            transform.position = cam.WorldToScreenPoint(character.transform.position + CurrentOffset);
        }
    }
}
