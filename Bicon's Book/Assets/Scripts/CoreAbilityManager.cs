using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoreAbilityManager : MonoBehaviour
{
    public static CoreAbilityManager instance;
    public List<CoreAbility_Base> AllCoreAbilitys;
    public List<CoreAbility_Base> AvaliableAbilitys = new List<CoreAbility_Base>();
    public List<CoreAbility_Base> CurrentCoreAbilitys = new List<CoreAbility_Base>();
    private void Awake()
    {
        instance = this;

        foreach(CoreAbility_Base a in AllCoreAbilitys)
        {
            AvaliableAbilitys.Add(a);
        }
    }

    public void GenerateCoreAbility()
    {
        CurrentCoreAbilitys.Clear();
        CurrentCoreAbilitys.Add(InGameManager.instance.AbilityManagers[0].m_CoreAbility);  
        
        for(int i = 0; i < 3;i++)
        {
            int aRandom = Random.Range(0, AvaliableAbilitys.Count);
            CurrentCoreAbilitys.Add(AvaliableAbilitys[aRandom]);
            AvaliableAbilitys.RemoveAt(aRandom);          
        }
    }
    public GameObject CoreAbilityObj;
    public List<Image> CoreAbilityIconList = new List<Image>();
    public List<TextMeshProUGUI> CoreAbilityTextList = new List<TextMeshProUGUI>();

    public void ShowCoreAbilityObj()
    {
        GenerateCoreAbility();
        UpdateUI();
        CoreAbilityObj.SetActive(true);
    }

    public void SelectAbility(int i)
    {
        InGameManager.instance.AbilityManagers[0].m_CoreAbility = Instantiate(CurrentCoreAbilitys[i]);

        CurrentCoreAbilitys.RemoveAt(i);

        for(int x = 0; x < CurrentCoreAbilitys.Count;x++)
        {
            AvaliableAbilitys.Add(CurrentCoreAbilitys[x]);
        }
    }
    public void UpdateUI()
    {
       for(int i = 0; i < CoreAbilityIconList.Count;i++)
        {
            CoreAbilityIconList[i].sprite = CurrentCoreAbilitys[i].m_Icon;
            CoreAbilityTextList[i].text = CurrentCoreAbilitys[i].m_CoreAbilityName;
        }
    }
}
