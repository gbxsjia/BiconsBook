using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChooseCharacterButton : MonoBehaviour
{
    public GameWorldInfo info;
    public void Choose()
    {
        GameWorldSetting.instance.currentInfo = info;
    }
}
