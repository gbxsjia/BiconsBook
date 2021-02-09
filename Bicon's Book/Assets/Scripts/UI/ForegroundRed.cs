using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForegroundRed : MonoBehaviour
{
    public GameObject ForegroundRedBar;
    public GameObject ForegroundRedBackground;
    public float aLevel = 0;
    public BodyPart[] bodyParts;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(aLevel / 255 * 100);
        // Debug.Log(aLevel);
        if (aLevel < (int)ColoringByInjure())
        {
            aLevel += 1;
            ForegroundRedBar.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)aLevel);
            ForegroundRedBackground.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)aLevel);
        }
        else if (aLevel > (int)ColoringByInjure())
        {
            aLevel -= 1;
            ForegroundRedBar.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)aLevel);
            ForegroundRedBackground.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)aLevel);
        }
    }

    public float ColoringByInjure()
    {
        float minHpPrecent = 0;
        float ChestHp = CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).HealthCurrent;
        float ChestHpMax = CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Head).HealthMax;

        float HeadHp = CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Head).HealthCurrent;
        float HeadHpMax = CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Head).HealthMax;

        foreach (BodyPart aPart in bodyParts)
        {
            if (minHpPrecent > aPart.GetHealthPrecent()
                || aPart.GetBodyPartType() != BodyPartType.Chest
                || aPart.GetBodyPartType() != BodyPartType.Head)
            {
                minHpPrecent = aPart.GetHealthPrecent();
            }
            // Head 100
            // Chest 100 -> 75 -> 50

            // 100 -> 50 -> 0
        }

        float ChestHpPrecent = (ChestHp / ChestHpMax - (1/2 * minHpPrecent));

        
        if (ChestHp / ChestHpMax > HeadHp / HeadHpMax)
        {
            // coloring by injured level of Head
            return (255 - ((HeadHp / HeadHpMax) * 255));
        }
        else
        {
            // coloring by injured level of Chest
            return (255 - ((ChestHp / ChestHpMax) * 255));
        }

    }
}

