using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.PlayerLoop;

public class StartPageUIManager : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField] GameObject tongChengjiu;
    [SerializeField] GameObject yinChengjiu;
    [SerializeField] GameObject jinChengjiu;
    [SerializeField] GameObject hongChengjiu;
    [SerializeField] GameObject dahongChengjiu;
    public void StartNewGame()
    {
        animator.Play("NewGame");
    }
    public void StartPage()
    {
        animator.Play("StartPage");
    }
    private void Start()
    {
        if(PlayerPrefs.GetInt("tongClear") == 1)
        {
            tongChengjiu.SetActive(true);
        }
        if (PlayerPrefs.GetInt("yinClear") == 1)
        {
            yinChengjiu.SetActive(true);
        }
        if (PlayerPrefs.GetInt("jinClear") == 1)
        {
            jinChengjiu.SetActive(true);
        }
        if (PlayerPrefs.GetInt("hongClear") == 1)
        {
            hongChengjiu.SetActive(true);
        }
        if (PlayerPrefs.GetInt("dahongClear") == 1)
        {
            dahongChengjiu.SetActive(true);
        }
    }

    [SerializeField] Button HardPlusButton;
    [SerializeField] Button HardMinusButton;
    [SerializeField] Text HardText;
    [SerializeField] Text HardDesText;

    private void Update()
    {
        switch(GameWorldSetting.Hardness)
        {
            case (-2):
                HardText.text = "星火教众";
                HardDesText.text = "150%的初始血量，初始金钱200，弱小的领主，较弱的敌人，全体低AI。";
                break;
            case (-1):
                HardText.text = "烈火门徒";
                HardDesText.text = "125%的初始血量，初始金钱100，较弱的领主。";
                break;
            case (0):
                HardText.text = "焦火侍卫";
                HardDesText.text = "标准的游戏规则，本游戏难度较高，即使是熟悉卡牌游戏的玩家也推荐从烈火门徒开始。";
                break;
            case (1):
                HardText.text = "黑火判官";
                HardDesText.text = "80%的初始血量，更强的领主，更快成长的AI。";
                break;
            case (2):
                HardText.text = "狱火之鬼";
                HardDesText.text = "65%的初始血量，初始金钱25，更强的领主，更强的敌人，全体高AI。";
                break;
        }

        if (GameWorldSetting.TutorialOpen == false)
        {
            CheckMark.SetActive(false);
        }
        else
        {
            CheckMark.SetActive(true);
        }
    }
    public void HardnessChange(int i)
    {
  
        GameWorldSetting.Hardness += i;   
        if(GameWorldSetting.Hardness >= 2)
        {
            GameWorldSetting.Hardness = 2;
            HardPlusButton.interactable = false;
        }
        else
        {
            HardPlusButton.interactable = true;
        }

        if (GameWorldSetting.Hardness <= -2)
        {
            GameWorldSetting.Hardness = -2;
            HardMinusButton.interactable = false;
        }
        else
        {
            HardMinusButton.interactable = true;
        }
    }

    [SerializeField] GameObject CheckMark;

    public void TutorialOpen()
    {
        if(GameWorldSetting.TutorialOpen == false)
        {
            GameWorldSetting.TutorialOpen = true;
        }
        else
        {
            GameWorldSetting.TutorialOpen = false;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChooseCharacter()
    {
        SceneManager.LoadScene(1);
    }
}
