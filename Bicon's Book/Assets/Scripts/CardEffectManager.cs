using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    public static CardEffectManager instance;
    [SerializeField]
    private GameObject FakeCardPrefab;

    [SerializeField]
    Vector3[] CardDestoryPositions;
    private void Awake()
    {
        instance = this;
    }

    private List<CardEffectInfo> cardEffectInfos = new List<CardEffectInfo>();
    public void AddCardEffect(CardEffectInfo effectInfo)
    {
        cardEffectInfos.Add(effectInfo);
    }
    public List<CardEffectInfo> GetCardEffectInfo()
    {
        List<CardEffectInfo> newList = new List<CardEffectInfo>();
        newList.AddRange(cardEffectInfos);
        cardEffectInfos.Clear();
        return newList;
    }

    public GameObject SpawnCardEffect(CardEffectInfo info)
    {
        GameObject g = Instantiate(FakeCardPrefab, UIManager.instance.transform);
        Card_Appearance casterCardAppearance = g.GetComponent<Card_Appearance>();
        casterCardAppearance.card = info.Card;

        switch (info.Type)
        {
            case CardEffectType.Destroy:
                g.transform.localPosition = CardDestoryPositions[info.Camp];
                break;
        }
        g.GetComponent<Animator>().Play("DisplayCard_Fade");
        StartCoroutine(MoveCardEffectProcess(g, info.Camp, 1.5f));
        Destroy(g, 1.5f);
        return g;
    }
    private IEnumerator MoveCardEffectProcess(GameObject g, int camp , float duration)
    {
        Vector3 MoveDirection = new Vector3((2 * camp - 1) * 70f, Random.Range(-80, 80));
        g.transform.position += MoveDirection * 1f;
        float timer = duration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (g != null)
            {
                g.transform.position += MoveDirection * Time.deltaTime;
            }
            yield return null;
        }
    }
}
public class CardEffectInfo
{
    public Card_Base Card;
    public CardEffectType Type;
    public int Camp;
    public CardEffectInfo(Card_Base card, CardEffectType type, int camp)
    {
        Card = card;
        Type = type;
        Camp = camp;
    }
}
public enum CardEffectType
{
    Destroy,
    Insert,

}