using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public BodyPart bodyPart;

    [SerializeField]
    Image healthBarSlider;

    [SerializeField]
    Image armorBarSlider;

    GameObject m_Holder;

    [SerializeField]
    TextMeshProUGUI healthNumber;
    [SerializeField]
    TextMeshProUGUI armorNumber;

    [SerializeField]
    Animator animator;
    private BuffManager buffManager;

    private float HealthPercent = 0;
    private float ArmorPercent = 0;

    float newHealthPercent;
    float newArmorPercent;

    public bool isWaitingForUpdate;

    [SerializeField]
    private GameObject BuffIconPrefab;
    [SerializeField]
    private Transform BuffIconHolder;
    private List<UI_BuffIcon> buffIconInstances = new List<UI_BuffIcon>();
    private void Awake()
    {
        bodyPart.UpdateUIEvent += UpdateUI;
        bodyPart.UpdateBuffUIEvent += StartWaitingForUpdate;
    }
    private void Start()
    {
        m_Holder = gameObject.transform.GetChild(0).gameObject;
        InGameManager.instance.BattleStartEvent += UpdateUI;
        if (healthNumber.transform.lossyScale.x <= 0)
        {
            Vector3 scale = healthNumber.transform.localScale;
            scale.x *= -1;
            healthNumber.transform.localScale = scale;
            armorNumber.transform.localScale = scale;
        }
    }
    private void UpdateUI()
    {
        newHealthPercent = bodyPart.GetHealth() / bodyPart.GetMaxHealth();
        if (bodyPart.GetArmorMax() > 0 && bodyPart.GetArmor() > 0)
        {
            armorBarSlider.gameObject.SetActive(true);
            newArmorPercent = bodyPart.GetArmor() / bodyPart.GetArmorMax();
        }
        else
        {
            armorBarSlider.gameObject.SetActive(false);
            newArmorPercent = 0;
        }
        if(Mathf.Abs(newHealthPercent-HealthPercent)>=0.001f || Mathf.Abs(newArmorPercent - ArmorPercent) >= 0.001f)
        {
            isWaitingForUpdate = true;
        }
    }

    public void StartWaitingForUpdate()
    {
        isWaitingForUpdate = true;
    }
    private void UpdateBuffUI()
    {
        if (buffManager != null)
        {
            if (buffManager.Buffs.ContainsKey(bodyPart))
            {
                List<Buff_base> myBuffs = buffManager.Buffs[bodyPart];
                for (int i = 0; i < buffIconInstances.Count; i++)
                {
                    if (myBuffs.Count > i)
                    {
                        buffIconInstances[i].UpdateUI(myBuffs[i]);
                    }
                    else
                    {
                        buffIconInstances[i].UpdateUI(null);
                    }
                }
                for (int i = buffIconInstances.Count; i < myBuffs.Count; i++)
                {
                    GameObject g = Instantiate(BuffIconPrefab, BuffIconHolder);
                    UI_BuffIcon icon = g.GetComponent<UI_BuffIcon>();
                    icon.UpdateUI(myBuffs[i]);
                    buffIconInstances.Add(icon);
                }
            }
        }
        else
        {
            buffManager = InGameManager.instance.BuffManagers[bodyPart.thisCharacter.camp];
        }
    }
    private IEnumerator UpdateUIProcess()
    {        
        animator.Play("Large");
        UpdateBuffUI();
        healthNumber.text = Mathf.CeilToInt(Mathf.Max(0,bodyPart.GetMaxHealth() * newHealthPercent)) + " / " + Mathf.CeilToInt(bodyPart.GetMaxHealth());
        armorNumber.text = Mathf.CeilToInt(Mathf.Max(0,bodyPart.GetArmorMax() * newArmorPercent)) + " / " + Mathf.CeilToInt(bodyPart.GetArmorMax());
        float timer = 0.5f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            healthBarSlider.fillAmount = Mathf.Lerp(healthBarSlider.fillAmount, newHealthPercent, 0.1f);


            armorBarSlider.fillAmount = Mathf.Lerp(armorBarSlider.fillAmount, newArmorPercent, 0.1f);

            yield return null;
        }
        healthBarSlider.fillAmount = newHealthPercent;
        armorBarSlider.fillAmount = newArmorPercent;
        HealthPercent = newHealthPercent;
        ArmorPercent = newArmorPercent;
    }

    private void Update()
    {
        m_Holder.transform.localScale = new Vector3(1, 1, 1);
        if (isWaitingForUpdate && AnimationManager.instance.IsAnimationAllDone())
        {
            StartCoroutine(UpdateUIProcess());
            isWaitingForUpdate = false;
        }
    }
}
