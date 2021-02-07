using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatus : MonoBehaviour
{
    [System.Serializable]
    public class TimeColor
    {
        public Vector3 MorningColor;
        public Vector3 NoonColor;
        public Vector3 SunsetColor;
        public Vector3 NightColor;
    }

   public TimeColor FGColor;
   public TimeColor MGColor;
   public TimeColor BGColor;

    [Header("Point Position")]
    public List<GameObject> m_AllPoint;
    public List<GameObject> m_ChaseEnemyPoint;
    public List<GameObject> m_EnemyPosList;
    public List<GameObject> m_Town_M_List;

    [Header("Current Point Prefab")]
    public List<GameObject> m_EnemyCurrentPrefabList;
    public List<GameObject> m_TownCurrentPrefabList;

    [Header("All Point Prefab")]
    public List<GameObject> m_EnemyPrefabList;
    public List<GameObject> m_TownPrefabList;

}
