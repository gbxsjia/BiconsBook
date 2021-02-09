using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class UpGradeManager : MonoBehaviour
{
    public static UpGradeManager instance;

  //  int LevelUpPointAmount = 0;
    int Currentexp = 0;
    int CurrentExpNeed = 6;
    int MaxLevel = 8;

   [SerializeField] List<int> ExpNeedList;

    public List<BodyPartHpUI> BodyPartsUI;


    [System.Serializable]
    public class BodyPartHpUI
    {
        
        public BodyPart bodyPart;
        public float HealthPercent;
        public Image healthBarSlider;
        public TextMeshProUGUI HealthAmountText;
        public Button UpgradeButton;
    }

    public int GainExp()
    {
        int EnemyLevel = InGameManager.instance.Characters[1].characterLevel;
        int GainAmount =1;
        Currentexp += GainAmount;
        if(Currentexp >= CurrentExpNeed)
        {
            Currentexp -= CurrentExpNeed;
            CharacterManager.PlayerInstance.characterLevel += 1;
            CurrentExpNeed = ExpNeedList[CharacterManager.PlayerInstance.characterLevel - 1];
          //  LevelUpPointAmount += 1;
        }
        UpdateThisUi();
        return GainAmount;
    }

    public void GainExp(int GainAmount)
    {
        Currentexp += GainAmount;
        if (Currentexp >= CurrentExpNeed)
        {
            Currentexp -= CurrentExpNeed;
            CharacterManager.PlayerInstance.characterLevel += 1;
            CurrentExpNeed = ExpNeedList[CharacterManager.PlayerInstance.characterLevel - 1];
          //  LevelUpPointAmount += 1;
        }
        UpdateThisUi();
    }

    public void SetLevel(int i )
    {
        
        //if (i - CharacterManager.PlayerInstance.characterLevel > 0)
        //{
        //    LevelUpPointAmount += i - CharacterManager.PlayerInstance.characterLevel;

        //}
        CharacterManager.PlayerInstance.characterLevel = i;
        
        CurrentExpNeed = ExpNeedList[CharacterManager.PlayerInstance.characterLevel - 1];
        UpdateThisUi();
    }

    public void UpgradeAllPart(int Amount)
    {
        foreach (BodyPart a in CharacterManager.PlayerInstance.bodyParts)
        {
            a.AddHealthMax(Amount);
            a.HealthCurrent += Amount;
        }
        UpdateThisUi();
    }

    public void UpgradePart(int Id, int Amount)
    {
        switch (Id)
        {
            case (0):
                {
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).AddHealthMax(Amount + 4);
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).HealthCurrent += (Amount + 4);
                }
                break;
            case (1):
                {
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Head).AddHealthMax(Amount + 2);
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Head).HealthCurrent += (Amount + 2);
                }
                break;
            case (2):
                {
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.RightArm).AddHealthMax(Amount);
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.RightArm).HealthCurrent += Amount;

                }
                break;
            case (3):
                {
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.LeftArm).AddHealthMax(Amount);
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.LeftArm).HealthCurrent += Amount;
                }
                break;
            case (4):
                {
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.RightLeg).AddHealthMax(Amount);
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.RightLeg).HealthCurrent += Amount;
                }
                break;
            case (5):
                {
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.LeftLeg).AddHealthMax(Amount);
                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.LeftLeg).HealthCurrent += Amount;
                }
                break;

        }
        UpdateThisUi();
    }
    //    public void UpgradePart(int Id)
    //{
    //    if (LevelUpPointAmount > 0)
    //    {
    //        switch (Id)
    //        {
    //            case (0):
    //                {
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Head).AddHealthMax(6);
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).AddHealthMax(2);

    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Head).HealthCurrent += 6;
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).HealthCurrent += 2;
    //                }
    //                break;
    //            case (1):
    //                {
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.RightArm).AddHealthMax(6);
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).AddHealthMax(2);

    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.RightArm).HealthCurrent += 6;
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).HealthCurrent += 2;
    //                }
    //                break;
    //            case (2):
    //                {
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.LeftArm).AddHealthMax(6);
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).AddHealthMax(2);
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.LeftArm).HealthCurrent += 6;
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).HealthCurrent += 2;
    //                }
    //                break;
    //            case (3):
    //                {
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.RightLeg).AddHealthMax(6);
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).AddHealthMax(2);
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.RightLeg).HealthCurrent += 6;
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).HealthCurrent += 2;
    //                }
    //                break;
    //            case (4):
    //                {
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.LeftLeg).AddHealthMax(6);
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).AddHealthMax(2);
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.LeftLeg).HealthCurrent += 6;
    //                    CharacterManager.PlayerInstance.GetBodyPart(BodyPartType.Chest).HealthCurrent += 2;
    //                }
    //                break;

    //        }
    //        //LevelUpPointAmount -= 1;
    //    }

    //    UpdateThisUi();
    //}
    [SerializeField] List<Image> ExpPageUiList;
    [SerializeField] List<Image> ExpPageActiveUiList;
    [SerializeField] List<Sprite> LevelAmountSpriteList;
    [SerializeField] Image LevelAmountImage;
    public void UpdateThisUi()
    {
        LevelAmountImage.sprite = LevelAmountSpriteList[CharacterManager.PlayerInstance.characterLevel -1];
        //ExpCurrentAmountText.text = Currentexp + "";
        //ExpNeedAmountText.text = CurrentExpNeed + "";
        //ExpBar.fillAmount = (float)Currentexp / (float)CurrentExpNeed;
        for(int i = 0; i < CurrentExpNeed;i++)
        {
            ExpPageUiList[i].gameObject.SetActive(true);
            if(i<Currentexp)
            {
                ExpPageActiveUiList[i].gameObject.SetActive(true);
            }
            else
            {
                ExpPageActiveUiList[i].gameObject.SetActive(false);
            }
        }

        foreach(BodyPartHpUI a in BodyPartsUI)
        {
            //if (a.UpgradeButton != null)
            //{
            //    if (LevelUpPointAmount > 0)
            //    {
            //        a.UpgradeButton.interactable = true;
            //    }
            //    else
            //    {
            //        a.UpgradeButton.interactable = false;
            //    }
            //}
            a.HealthAmountText.text = a.bodyPart.HealthCurrent + "/" + a.bodyPart.HealthMax;
            a.HealthPercent = (float)a.bodyPart.HealthCurrent / (float)a.bodyPart.HealthMax;
            a.healthBarSlider.fillAmount = a.HealthPercent;
        }
        UI_AbilityMain.thisInstance.UpdateAbilitySlotInList();
    }
    private void Start()
    {
        instance = this;
        InGameManager.instance.BattleEndEvent += UpdateThisUi;
        StartCoroutine(DelayStart());
        UpdateThisUi();
    }

    void Update()
    {

    }
    IEnumerator DelayStart()
    {
      yield return new  WaitForSeconds(0.2f);UpdateThisUi();
    }
}
