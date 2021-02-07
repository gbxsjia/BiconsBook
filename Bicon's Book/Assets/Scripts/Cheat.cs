using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(inputField.gameObject.active == true)
            {
                inputField.gameObject.SetActive(false);
            }
            else
            {
                inputField.gameObject.SetActive(true);
            }
        }

        if(inputField.gameObject.active == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LetsCheat();
            }
        }
    }
    public void LetsCheat()
    {
        string[] PlayerString = inputField.text.Split(new string[] { "_" }, System.StringSplitOptions.RemoveEmptyEntries);
        string Command = inputField.text;
        if (PlayerString.Length > 0)
        {
            Command = PlayerString[0];
            switch (Command)
            {
                case ("TTCC"):
                    {
                        InGameManager.instance.CardManagers[0].StaminaInitialMax += 99;
                    }
                    break;
                case ("AbilityUnblock"):
                    {
                        UI_AbilityMain.thisInstance.AbilityUnblock();
                    }
                    break;
                case ("Fly"):
                    {
                        EnemyIncubator.thisInstance.FlyMap(Convert.ToInt32(PlayerString[1]));
                    }
                    break;

                case ("SetLevel"):
                    {

                        UpGradeManager.instance.SetLevel( Convert.ToInt32(PlayerString[1]));

                    }
                    break;
                case ("TestMap"):
                    {

                        EnemyIncubator.thisInstance.TestMapCreate();

                    }
                    break;
                case ("TimePass"):
                    {
                        for (int i = 0; i < Convert.ToInt32(PlayerString[1]); i++)
                        {
                              EnemyIncubator.thisInstance.PassTime();  
                        }                        
                    }
                    break;
                case ("Gold"):
                    {
                        CharacterManager.PlayerInstance.AddGold(Convert.ToInt32(PlayerString[1]));
                    }
                    break;
                case ("FinalScoreLost"):
                    {
                        UIManager.instance.GameOver();
                        UIManager.instance.UpdateFinalScore(false);

                    }
                    break;
                case ("FinalScoreWin"):
                    {
                        UIManager.instance.GameWinUI.SetActive(true);
                        UIManager.instance.UpdateFinalScore(true);

                    }
                    break;
                case ("DeleteData"):
                    {
                        PlayerPrefs.DeleteAll();

                    }
                    break;
            }
        }
        else
        {
            Command = PlayerString[0];
            switch (Command)
            {
                case ("Fly"):
                    {

                        EnemyIncubator.thisInstance.FlyMap(Convert.ToInt32(1));

                    }
                    break;

                case ("TestMap"):
                    {

                        EnemyIncubator.thisInstance.TestMapCreate();

                    }
                    break;
                case ("TimePass"):
                    {
                            EnemyIncubator.thisInstance.PassTime();
                        
                    }
                    break;
                case ("FinalScoreLost"):
                    {
                        UIManager.instance.GameOver();
                        UIManager.instance.UpdateFinalScore(false);

                    }
                    break;
                case ("FinalScoreWin"):
                    {
                       UIManager.instance.GameWinUI.SetActive(true);
                       UIManager.instance.UpdateFinalScore(true);

                    }
                    break;
            }
        }
    }
}
