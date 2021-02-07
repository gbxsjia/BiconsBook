using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Equipment/Item")]
public class EM_Item : Equipment
{
    public int ConsumeTimes;
    public Sprite[] ItemSprite = new Sprite[12];
}
