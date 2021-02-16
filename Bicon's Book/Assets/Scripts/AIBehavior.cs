using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    CardManager cardManager;
    CharacterManager characterManager;

    public int AISmart=0;
    public int ExpectDistance = 1;

    public bool IsControl;

    private void Start()
    {
        cardManager = GetComponent<CardManager>();
        characterManager = GetComponent<CharacterManager>();
        InGameManager.instance.OnTurnChanged += OnTurnChanged;

        //cardManager.TargetBodyPart = InGameManager.instance.GetEnemyBodyPart(cardManager, (BodyPartType)Random.Range(0, 5));
        //cardManager.AllyBodyPart = InGameManager.instance.GetAllyBodyPart(cardManager, (BodyPartType)Random.Range(0, 5));
    }

    private void OnTurnChanged(TurnState turn)
    {
        if (turn == TurnState.Card && InGameManager.instance.CampTurn == 1)
        {
            if (!IsControl)
            {
                StartCoroutine(PlayCardProcess());
            }
            else
            {
                UseCommands(Tu_StageManager.instance.AICommandGroups[InGameManager.instance.TurnCount]);
            }
        }
    }

    public  void SetControl(bool isControl)
    {
        IsControl = isControl;
    }
    public void UseCommands(AICommandGroup group)
    {
        StartCoroutine(CommandProcess(group));
    }
    public event System.Action CommandProcceedEvent;
    private IEnumerator CommandProcess(AICommandGroup group)
    {
        for (int i = 0; i < group.commands.Length; i++)
        {
            AICommand command = group.commands[i];
            switch (command.Type)
            {
                case AICommandType.ChooseTarget:
                    cardManager.TargetBodyPart = InGameManager.instance.Characters[0].GetBodyPart(command.TargetBodypartType);
                    break;
                case AICommandType.Move:
                    cardManager.Move(command.MoveDirection);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case AICommandType.UseCard:
                    GameObject card = null;
                    foreach(GameObject g in cardManager.Cards)
                    {
                        if(g.GetComponent<Card_Base>().Name== command.CardName)
                        {
                            card = g;
                        }
                    }
                    if (card != null)
                    {
                        cardManager.UseCard(card);
                    }
                    else
                    {
                        Debug.LogError("Can not find Target card : " + card);
                    }
                    yield return new WaitForSeconds(0.5f);

                    break;
            }
            if (CommandProcceedEvent != null)
            {
                CommandProcceedEvent();
            }
            while (InGameManager.IsPausing)
            {
                yield return null;
            }
        }
        yield return new WaitForSeconds(1);
        InGameManager.instance.ChangeState(TurnState.TurnEnd);
    }

    private IEnumerator PlayCardProcess()
    {
        bool haveCardToUse = true;
        while (haveCardToUse)
        {
            yield return new WaitForSeconds(0.5f);

            List<BodyPart> allyParts = new List<BodyPart>();
            foreach (BodyPart bp in characterManager.bodyParts)
            {
                if (bp.GetIsAlive())
                {
                    allyParts.Add(bp);
                }
            }
            List<BodyPart> enemyParts = new List<BodyPart>();
            foreach (BodyPart bp in InGameManager.instance.Characters[0].bodyParts)
            {
                if (bp.GetIsAlive())
                {
                    enemyParts.Add(bp);
                }
            }
            enemyParts.Sort((x, y) => (x.GetHealth() + x.GetArmor()).CompareTo(y.GetHealth() + y.GetArmor()));
            cardManager.TargetBodyPart = enemyParts[UnityEngine.Random.Range(0, Mathf.Min(AISmart, enemyParts.Count))];    

            cardManager.AllyBodyPart = allyParts[UnityEngine.Random.Range(0, allyParts.Count)];

            haveCardToUse = false;
            for (int i = 0; i < cardManager.Cards.Count; i++)
            {
                Card_Base card = cardManager.Cards[i].GetComponent<Card_Base>();
                if (card.CanUseCard() && card.HasEnoughStamina(cardManager) && card.IsTargetInRange()&&card.HasEffect(cardManager,TriggerType.Use))
                {
                    haveCardToUse = true;
                    cardManager.UseCard(cardManager.Cards[i]);
                    break;
                }
            }

            if (!haveCardToUse)
            {
                if (Mathf.Abs(InGameManager.instance.CharacterPositions[0] - InGameManager.instance.CharacterPositions[1]) > ExpectDistance)
                {
                    if (cardManager.CanMove(1))
                    {
                        haveCardToUse = true;
                        cardManager.Move(1);
                    }
                }
                else if (Mathf.Abs(InGameManager.instance.CharacterPositions[0] - InGameManager.instance.CharacterPositions[1]) < ExpectDistance)
                {
                    if (cardManager.CanMove(-1))
                    {
                        haveCardToUse = true;
                        cardManager.Move(-1);
                    }
                }
            }

        }
        while(AnimationManager.instance.isInAnimationMode)
        {
            yield return null;
        }
        InGameManager.instance.ChangeState(TurnState.TurnEnd);
    }
}


