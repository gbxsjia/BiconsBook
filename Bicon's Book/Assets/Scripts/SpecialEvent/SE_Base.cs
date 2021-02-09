using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SE_Base : MonoBehaviour
{
   [SerializeField] protected TextMeshProUGUI KeyWord_1;
   [SerializeField] protected TextMeshProUGUI KeyWord_2;
    [SerializeField]
    protected Transform RewardParent;
    [SerializeField]
    protected GameObject RewardItemPrefab;
    void Start()
    {
        
    }
    public void Back()
    {
        EnemyIncubator.thisInstance.SetInCameraMotion(false);
    }
    public void Left()
    {
        EnemyIncubator.thisInstance.FlyMap(1);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
