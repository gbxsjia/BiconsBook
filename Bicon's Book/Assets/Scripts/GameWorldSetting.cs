using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameWorldInfo
{
    public bool canChooseStartEquipment;
    public bool isTutorial;
}
public class GameWorldSetting : MonoBehaviour
{
    public static GameWorldSetting instance; 
    static public int Hardness = 0;
    static public int HighScore = 0;
    public int FinalScoreCaculate(bool Win)
    {
        int FinalScore = 0;
        float DifficultyMulti = 1;
         switch(Hardness)
        {
            case -2:
                DifficultyMulti = 0.5f;
                break;
            case -1:
                DifficultyMulti = 0.75f;
                break;
            case 0:
                DifficultyMulti = 1f;
                break;
            case 1:
                DifficultyMulti = 1.25f;
                break;
            case 2:
                DifficultyMulti = 1.5f;
                break;
        }

        int EnemyDefeatScore = InGameManager.EnemyDefeat * 10;
        int BossDefeatScore = InGameManager.BossDefeat * 200;
        int GoldScore = CharacterManager.PlayerInstance.m_Gold;
        
        for(int i = 0; i < 15;i++)
        {
            Equipment a =  InGameManager.instance.EquipmentManagers[0].GetEquipment((EquipmentType)i);
            if (a != null)
            {
                GoldScore += a.Value;
            }
        }
        if (Inventory.instance.InventoryItems.Count > 0)
        {
            foreach (Equipment a in Inventory.instance.InventoryItems)
            {
                GoldScore += a.Value;
            }
        }
        InGameManager.FinalGold = GoldScore;
        if(Win)
        {

            switch (Hardness)
            {
                case -2:
                    PlayerPrefs.SetInt("tongClear", 1);
                    break;
                case -1:
                    PlayerPrefs.SetInt("yinClear", 1);
                    break;
                case 0:
                    PlayerPrefs.SetInt("jinClear", 1);
                    break;
                case 1:
                    PlayerPrefs.SetInt("hongClear", 1);
                    break;
                case 2:
                    PlayerPrefs.SetInt("dahongClear", 1);
                    break;
            }
            FinalScore += 500;
        }

        FinalScore += GoldScore + EnemyDefeatScore + BossDefeatScore;
        FinalScore = Mathf.RoundToInt(FinalScore * DifficultyMulti);

        return FinalScore;

    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance= this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static bool TutorialOpen=true;
    public GameWorldInfo currentInfo;
}
