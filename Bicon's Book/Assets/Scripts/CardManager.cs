using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private EquipmentManager equipmentManager;
    private CharacterManager characterManager;

    public int camp;

    [Header("Cards")]
    public List<GameObject> Cards = new List<GameObject>();
    public List<GameObject> InGameCardDeck = new List<GameObject>();
    public List<GameObject> UsedCardsDeck = new List<GameObject>();
    public List<GameObject> VanishedCardsDeck = new List<GameObject>();

    public int MaxCardAmount = 6;
    public int DrawCardAmount = 2;
    public int DrawCardAmountFix = 0;
    public int StartDrawCardAmount = 2;

    public int StaminaInitialMax = 10;
    public int StaminaMax = 10;
    public int StaminaCurrent = 10;
    public int StaminaChangePlus = 0;
    public int StaminaChangeMinus = 0;
    public int StaminaChange = 0;

    public int tempInt = 0;

    public BodyPart TargetBodyPart;
    public BodyPart AllyBodyPart;
    [SerializeField]
    private GameObject TargetEffectPrefab;
    private GameObject TargetEffectInstance;
    [SerializeField]
    private GameObject AllyEffectPrefab;
    private GameObject AllyEffectInstance;

    public int InitialMoveStaminaCost = 2;
    public int CurrentMoveStaminaCost = 2;

    public AttackFeedBack attackFeedBack;

    public int AttackCardUsedAmount = 0;

    public List<GameObject> UsedCardRecordList = new List<GameObject>();
    public static List<GameObject> Fisher_Yates_CardDeck_Shuffle(List<GameObject> aList)
    {

        System.Random _random = new System.Random();

        GameObject myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }
    private void Awake()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        characterManager = GetComponent<CharacterManager>();
    }
    private void Start()
    {
        InGameManager.instance.BattleStartEvent += ResetMoveStamina;
        InitialCardManager();
    }
    public float AllWeight;
    private BodyPart mouseOverBodypart;


    public void UpdateWeightAndStamina()
    {
        AllWeight = 0;
        int WeightReduceStamina = 0;
        for (int i = 0; i < 14; i++)
        {
            if (equipmentManager.GetEquipment((EquipmentType)i) != null)
            {
                AllWeight += equipmentManager.GetEquipment((EquipmentType)i).Weight;
            }
        }
        if (CharacterManager.PlayerInstance.GetComponent<AbilityManager>().HeavyBearer)
        {
            AllWeight = (int)(AllWeight * 0.75);
            Debug.Log(AllWeight);
        }
        if (AllWeight >= 20)
        {
           int StaminaWeight = (int)(AllWeight - 20f);
            WeightReduceStamina = StaminaWeight / 10;
        }
        StaminaMax = StaminaInitialMax - WeightReduceStamina;
    }
    private void Update()
    {
        SelfTriggering();
        UpdateWeightAndStamina();
        if (camp == 0  && InGameManager.instance.IsBattle)
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                BodyPart bodyPart = hitInfo.collider.GetComponent<BodyPart>();
                if (bodyPart != null )
                {
                    if (bodyPart != mouseOverBodypart)
                    {
                        if (mouseOverBodypart != null)
                        {
                            mouseOverBodypart.MouseExit();
                        }                    
                        mouseOverBodypart = bodyPart;
                        mouseOverBodypart.MouseOver();
                    }
                    if (Input.GetMouseButtonDown(0) && bodyPart.GetIsAlive())
                    {
                        ChangeTargetType(bodyPart);
                    }  
                }
                else
                {
                    if (mouseOverBodypart != null)
                    {
                        mouseOverBodypart.MouseExit();
                    }
                }
            }
            else
            {
                if (mouseOverBodypart != null)
                {
                    mouseOverBodypart.MouseExit();
                }
            }
        }
        if (AbilityUpdateEvent != null)
        {
            AbilityUpdateEvent.Invoke();
        }
    }
    public void ChangeTargetType(BodyPart bodyPart)
    {
        if (Tu_StageManager.instance == null || !Tu_StageManager.instance.canDoAction(StageTargetType.Bodypart, bodyPart, null, -1) && camp == 0)
        {
            return;
        }

        if (bodyPart != null)
        {
            if (bodyPart.thisCharacter.camp == 1)
            {
                if (TargetEffectInstance != null)
                {
                    Destroy(TargetEffectInstance);
                }
                if (camp == 0)
                {
                    TargetEffectInstance = Instantiate(TargetEffectPrefab, bodyPart.transform);
                    TargetEffectInstance.transform.localPosition = Vector3.zero;
                }
                TargetBodyPart = bodyPart;
            }
            else
            {
                if (AllyEffectInstance != null)
                {
                    Destroy(AllyEffectInstance);
                }
                if (camp == 0)
                {
                    AllyEffectInstance = Instantiate(AllyEffectPrefab, bodyPart.transform);
                    AllyEffectInstance.transform.localPosition = Vector3.zero;
                }
                AllyBodyPart = bodyPart;
            }
        }
        else
        {
            if ((TargetBodyPart == null || !TargetBodyPart.GetIsAlive()) && TargetEffectInstance != null)
            {
                TargetBodyPart = null;
                Destroy(TargetEffectInstance);
            }
            if ((AllyBodyPart == null || !AllyBodyPart.GetIsAlive()) && AllyEffectInstance != null)
            {
                for (int i = 0; i < characterManager.bodyParts.Length; i++)
                {
                    if (characterManager.bodyParts[i].GetIsAlive())
                    {
                        AllyBodyPart = characterManager.bodyParts[i];
                        if (AllyEffectInstance != null)
                        {
                            Destroy(AllyEffectInstance);
                        }

                        AllyEffectInstance = Instantiate(AllyEffectPrefab, AllyBodyPart.transform);
                        AllyEffectInstance.transform.localPosition = Vector3.zero;
                        break;
                    }
                }
            }
        }
        if (ChangeTargetBodypartEvent != null)
        {
            ChangeTargetBodypartEvent(bodyPart);
        }
    }
    public event System.Action<BodyPart> ChangeTargetBodypartEvent;
    private void InitialCardManager()
    {
        InGameManager.instance.OnTurnChanged += OnTurnChanged;
        InGameManager.instance.BattleStartEvent += InitialDeck;
        InGameManager.instance.BattleEndEvent += OnBattleEnd;
        InGameManager.instance.CardManagers[camp] = this;
        if (camp == 0)
        {
            UIManager.instance.playerCardManager = this;
        }
        foreach (BodyPart bp in characterManager.bodyParts)
        {
            bp.BodyPartBreakEvent += OnBodyPartBreak;
        }
    }
    public void OnBattleEnd()
    {
        TargetBodyPart = null;
        AllyBodyPart = null;
        Destroy(AllyEffectInstance);
        Destroy(TargetEffectInstance);
    }
    public void OnBodyPartBreak(BodyPart bodyPart)
    { 
        Card_Base card;
        for (int i = UsedCardsDeck.Count - 1; i >= 0; i--)
        {
            card = UsedCardsDeck[i].GetComponent<Card_Base>();
            if (card.ownerEquipment != null)
            {
                if (card.ownerEquipment.AttachedBodyPart == bodyPart)
                {
                    Destroy(UsedCardsDeck[i]);
                    UsedCardsDeck.RemoveAt(i);
                }
            }
        }
        for (int i = InGameCardDeck.Count - 1; i >= 0; i--)
        {
            card = InGameCardDeck[i].GetComponent<Card_Base>();
            if (card.ownerEquipment != null)
            {
                if (InGameCardDeck[i].GetComponent<Card_Base>().ownerEquipment.AttachedBodyPart == bodyPart)
                {
                    Destroy(InGameCardDeck[i]);
                    InGameCardDeck.RemoveAt(i);
                }
            }
        }
        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            card = Cards[i].GetComponent<Card_Base>();
            if (card.ownerEquipment != null)
            {
                if (Cards[i].GetComponent<Card_Base>().ownerEquipment.AttachedBodyPart == bodyPart)
                {
                    Destroy(Cards[i]);
                    Cards.RemoveAt(i);
                }
            }
        }

 
        if (bodyPart == AllyBodyPart)
        {
            ChangeTargetType(null);
        }
        if (InGameManager.instance.CardManagers[1 - camp].TargetBodyPart == bodyPart)
        {
            InGameManager.instance.CardManagers[1 - camp].ChangeTargetType(null);
        }

        if(camp == 1)
        {
            UI_BattleReward.instance.AddReward(bodyPart);
        }
    }

    public Card_Base DestroyRandomCard()
    {
        if(Cards.Count > 0)
        { 
            Card_Base card = Cards[Random.Range(0,Cards.Count)].GetComponent<Card_Base>();
            if (card.ContainsTriggerType(TriggerType.OnAbandon))
            {
                UseCard(card.gameObject, TriggerType.OnAbandon, true, false, false);
            }
            else
            {
                AddToUsedCards(card.gameObject, true);
            }
            return card;

        }
        return null;
    }
    
    
    public Card_Base ThrowBodypartCard(BodyPart bodyPart)
    {
        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            if (Cards[i].GetComponent<Card_Base>().ownerEquipment.AttachedBodyPart == bodyPart)
            {
                Card_Base card = Cards[i].GetComponent<Card_Base>();
                AddToUsedCards(Cards[i],true);
                return card;
            }
        }
        return null;
    }
    public void InitialDeck()
    {
        StaminaChangeMinus = 0;
        StaminaChangePlus = 0;
        DrawCardAmountFix = 0;
        InGameCardDeck.Clear();
        GenerateDeck();
        ShuffleCards();
        InitialDrawItemCard();
        if(Tu_StageManager.instance!=null && Tu_StageManager.instance.isTutorialMode)
        {
            StartDrawCardAmount = Tu_StageManager.instance.deckReplacer.StartDrawCardAmount;
            DrawCardAmount = Tu_StageManager.instance.deckReplacer.DrawCardAmount;
        }

        for (int i = 0; i < StartDrawCardAmount; i++)
        {
            DrawCard();
        }
    }
    public void InitialDrawItemCard()
    {
        for (int i = InGameCardDeck.Count - 1; i >= 0; i--)
        {
            if (InGameCardDeck[i].GetComponent<Card_Base>().ContainsEntry(EntryType.ConsumeItem))
            {
                GameObject cardObj = InGameCardDeck[i];

                Card_Base card = cardObj.GetComponent<Card_Base>();
                card.isActive = true;
                card.CardEffect(this, TargetBodyPart, AllyBodyPart, TriggerType.OnDraw);

                Cards.Add(cardObj);
                cardObj.SetActive(true);
                UIManager.instance.AddCard(cardObj, camp);
                InGameCardDeck.RemoveAt(i);
            }
        }
    }
        public void ResetCardDeck()
    {
        for (int i = UsedCardsDeck.Count - 1; i >= 0; i--)
        {
            Destroy(UsedCardsDeck[i]);
        }
        UsedCardsDeck.Clear();
        for (int i = InGameCardDeck.Count - 1; i >= 0; i--)
        {

            Destroy(InGameCardDeck[i]);
        }
        InGameCardDeck.Clear();
        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            Destroy(Cards[i]);
            Cards.RemoveAt(i);
        }
        Cards.Clear();
    }

    public void ShuffleCards()
    {
        InGameCardDeck = Fisher_Yates_CardDeck_Shuffle(InGameCardDeck);
    }

    private void GenerateDeck()
    {
        InGameCardDeck.AddRange(equipmentManager.GetCopyOfDeck());
    }
    public void OnTurnChanged(TurnState state)
    {
        if (state == TurnState.TurnStart && camp == InGameManager.instance.CampTurn)
        {
            if (TargetEffectInstance == null)
            {
                ChangeTargetType(InGameManager.instance.GetEnemyBodyPart(this, BodyPartType.Chest));
            }
            if (AllyEffectInstance == null)
            {
                ChangeTargetType(InGameManager.instance.Characters[0].GetBodyPart(BodyPartType.Chest));
            }
            StaminaCurrent = StaminaMax + StaminaChangePlus - StaminaChangeMinus;
            StaminaChangePlus = 0;
            StaminaChangeMinus = 0;
            for (int i = 0; i < DrawCardAmount + DrawCardAmountFix; i++)
            {
                if (Tu_StageManager.instance!=null && Tu_StageManager.instance.isTutorialMode)
                {
                    Tu_StageManager.instance.DrawCard(this);
                }
                else
                {
                    DrawCard();
                }             
            }
            DrawCardAmountFix = 0;
        }
        if(state == TurnState.TurnEnd)
        {AudioManager.instance.PlayTurnChangeSound();

        }
        if (state == TurnState.TurnStart)
        {
            AttackCardUsedAmount = 0;
            
        }
        if (state == TurnState.Null)
        {
            ResetCardDeck();
        }
    }

    public void ResetMoveStamina()
    {
        CurrentMoveStaminaCost = 2;
    }

   public int ForwardMoveStaminaOffset = 0;
   public int BackwardMoveStaminaOffset = 0;

    public int RealDirection(int aDirection,int aCamp)
    {
        return -aDirection * (2 * aCamp - 1);
    }
    public bool CanMove(int direction) // 1和-1不再是左右 而是相对于角色的前后 1就是向前 -1就是后退
    {
        bool result = true;
        int aRealDirection = RealDirection(direction,camp);
        int FinalStaminaCost = CurrentMoveStaminaCost;
        int newPos = InGameManager.instance.CharacterPositions[camp] + aRealDirection;
        if (direction == 1)
        {
            FinalStaminaCost += ForwardMoveStaminaOffset;
        }
        else if (direction == -1)
        {
            FinalStaminaCost += BackwardMoveStaminaOffset;
        }

        if (newPos < 0 || newPos >= InGameManager.instance.GridAmount || StaminaCurrent < FinalStaminaCost || newPos == InGameManager.instance.CharacterPositions[0] || newPos == InGameManager.instance.CharacterPositions[1])
        {
            result = false;
        }

        if (Tu_StageManager.instance == null || !Tu_StageManager.instance.canDoAction(StageTargetType.Move, null, null, direction) && camp == 0)
        {
            result = false;
        }
        return result;
    }
    public bool CanMove(int direction,out int FinalStaminaCost) // 1和-1不再是左右 而是相对于角色的前后 1就是向前 -1就是后退
    {
        bool result = true;
        int aRealDirection = RealDirection(direction, camp);
        FinalStaminaCost = CurrentMoveStaminaCost;
        int newPos = InGameManager.instance.CharacterPositions[camp] + aRealDirection;
        if(direction==1)
        {
            FinalStaminaCost += ForwardMoveStaminaOffset;
        }
        else if(direction == -1)
        {
            FinalStaminaCost += BackwardMoveStaminaOffset;
        }

        if (newPos < 0 || newPos >= InGameManager.instance.GridAmount || StaminaCurrent < FinalStaminaCost || newPos == InGameManager.instance.CharacterPositions[0] || newPos == InGameManager.instance.CharacterPositions[1])
        {
            result = false;
        }


        //if (CharacterManager.PlayerInstance.GetComponent<AbilityManager>().GoForward)
        //{
        //    if (IsForward(direction))
        //    {
        //        if (newPos < 0 || newPos >= InGameManager.instance.GridAmount || StaminaCurrent < (CurrentMoveStaminaCost - 1) || newPos == InGameManager.instance.CharacterPositions[0] || newPos == InGameManager.instance.CharacterPositions[1])
        //        {
        //            result = false;
        //        }
        //    }
        //    else
        //    {
        //        if (newPos < 0 || newPos >= InGameManager.instance.GridAmount || StaminaCurrent < (CurrentMoveStaminaCost + 1) || newPos == InGameManager.instance.CharacterPositions[0] || newPos == InGameManager.instance.CharacterPositions[1])
        //        {
        //            result = false;
        //        }
        //    }
        //}
        //else
        //{
        //    if (newPos < 0 || newPos >= InGameManager.instance.GridAmount || StaminaCurrent < CurrentMoveStaminaCost || newPos == InGameManager.instance.CharacterPositions[0] || newPos == InGameManager.instance.CharacterPositions[1])
        //    {
        //        result = false;
        //    }
        //}

        if (Tu_StageManager.instance == null || !Tu_StageManager.instance.canDoAction(StageTargetType.Move, null, null, direction) && camp == 0)
        {
            result = false;
        }
        return result;
    }

    public bool CanForceMove(int direction)
    {
        bool result = true;
        int newPos = InGameManager.instance.CharacterPositions[camp] + direction;
        if (newPos < 0 || newPos >= InGameManager.instance.GridAmount || newPos == InGameManager.instance.CharacterPositions[0] || newPos == InGameManager.instance.CharacterPositions[1])
        {
            result = false;
        }
        return result;
    }

    public bool IsForward(int direction)
    {
        if ((camp == 0 && direction == 1) || (camp == 1 && direction == -1))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public void ForceMove(int direction)
    {
        if (CanForceMove(direction))
        {
            int start = InGameManager.instance.CharacterPositions[camp];
            InGameManager.instance.CharacterMove(camp, direction);

            AnimationInfo info = new AnimationInfo(camp, start, InGameManager.instance.CharacterPositions[camp], InGameManager.instance.CharacterPositions[1 - camp]);
            AnimationManager.instance.AddAnimationToEnd(info);

            InGameManager.instance.OnCharacterMoveEvent();

        }
    }

    public void Move(int direction)
    {
        int FinalStaminaCost = CurrentMoveStaminaCost;
        if (CanMove(direction,out FinalStaminaCost) && InGameManager.instance.CanUseAction(this))
        {
           
            int start = InGameManager.instance.CharacterPositions[camp]; 
            StaminaCurrent -= FinalStaminaCost;
            int aRealDirection = RealDirection(direction, camp);
            //if (CharacterManager.PlayerInstance.GetComponent<AbilityManager>().GoForward)
            //{
            //    print(1);
            //    if (IsForward(direction))
            //    {
            //        print(2);
            //        StaminaCurrent -= (CurrentMoveStaminaCost - 1);
            //    }
            //    else
            //    {
            //        StaminaCurrent -= (CurrentMoveStaminaCost + 1);
            //    }
            //}
            //else
            //{
            //    StaminaCurrent -= CurrentMoveStaminaCost;
            //}
            if (MoveEvent != null)
            {
                MoveEvent(1);
            }
            InGameManager.instance.CharacterMove(camp, aRealDirection);

            AnimationInfo info = new AnimationInfo(camp, start, InGameManager.instance.CharacterPositions[camp], InGameManager.instance.CharacterPositions[1 - camp]);
            AnimationManager.instance.AddAnimationToEnd(info);

            InGameManager.instance.OnCharacterMoveEvent();
            
        }
    }

    public event System.Action<int> MoveEvent;
    public void DrawCard(BodyPartType bodyPartType, EquipmentType ownerEquipmentType)
    {
        if (InGameCardDeck.Count == 0)
        {
            RefreshInGameCardDeck();
        }

        if (InGameCardDeck.Count == 0)
        {
            return;
        }
        for (int i = InGameCardDeck.Count - 1; i >= 0; i--)
        {
            if (InGameCardDeck[i].GetComponent<Card_Base>().ownerEquipment.AttachedBodyPart.GetBodyPartType() == bodyPartType 
                && InGameCardDeck[i].GetComponent<Card_Base>().ownerEquipment.AttachedSlotIndex == (int)ownerEquipmentType)
            {
                GameObject cardObj = InGameCardDeck[i];

                Card_Base card = cardObj.GetComponent<Card_Base>();
                card.isActive = true;
                card.CardEffect(this, TargetBodyPart, AllyBodyPart, TriggerType.OnDraw);

                Cards.Add(cardObj);
                cardObj.SetActive(true);
                UIManager.instance.AddCard(cardObj, camp);
                InGameCardDeck.RemoveAt(i);
                return;
            }
        }
        for (int i = UsedCardsDeck.Count - 1; i >= 0; i--)
        {
            if (UsedCardsDeck[i].GetComponent<Card_Base>().ownerEquipment.AttachedBodyPart.GetBodyPartType() == bodyPartType
                && UsedCardsDeck[i].GetComponent<Card_Base>().ownerEquipment.AttachedSlotIndex == (int)ownerEquipmentType)
            {
                GameObject cardObj = UsedCardsDeck[i];

                Card_Base card = cardObj.GetComponent<Card_Base>();
                card.isActive = true;
                card.CardEffect(this, TargetBodyPart, AllyBodyPart, TriggerType.OnDraw);

                Cards.Add(cardObj);
                cardObj.SetActive(true);
                UIManager.instance.AddCard(cardObj, camp);
                UsedCardsDeck.RemoveAt(i);
            }
        }
    }
    public void DrawCard(string cardName)
    {
        if (InGameCardDeck.Count == 0)
        {
            RefreshInGameCardDeck();
        }

        if (InGameCardDeck.Count == 0)
        {
            return;
        }

        for (int i = 0; i < InGameCardDeck.Count; i++)
        {
            if (InGameCardDeck[i].GetComponent<Card_Base>().Name == cardName)
            {
                DrawCard(i);
                return;
            }
        }
    }
    public void DrawCard()
    {
        DrawCard(0);
    }
    public void DrawCard(int index)
    {
        if (InGameCardDeck.Count == 0)
        {
            RefreshInGameCardDeck();
        }

        if (InGameCardDeck.Count == 0)
        {
            return;
        }
        GameObject cardObj = InGameCardDeck[index];

        Card_Base card = cardObj.GetComponent<Card_Base>();
        card.isActive = true;

        if (card.ContainsTriggerType(TriggerType.OnDraw))
        {
            //        card.CardEffect(this, TargetBodyPart, AllyBodyPart, TriggerType.OnDraw);
            //        TargetHeightType AnimHeight = InGameManager.BodyTypeToHeightIndex(TargetBodyPart.GetBodyPartType());

            //        AnimationInfo newInfo = new AnimationInfo(camp, 1 - camp, card.animType, AnimType.Damaged, AnimHeight, AnimHeight, BodyPartToDirection(card.ownerEquipment.AttachedBodyPart.GetBodyPartType()),
            //BodyPartToDirection(TargetBodyPart.GetBodyPartType()), card.casterMoveType, card.opponentMoveType, card.IsTargetInRange(), true, card, TargetBodyPart);
            //        AnimationManager.instance.AddAnimationToEnd(newInfo);
            UseCard(card.gameObject, TriggerType.OnDraw, true, true, false);
        }

        Cards.Add(cardObj);
        cardObj.SetActive(true);
        UIManager.instance.AddCard(cardObj, camp);
        InGameCardDeck.RemoveAt(index);

    }

    public void DrawPunchCard()
    {
        for (int i = 0; i < InGameCardDeck.Count; i++)
        {
            if (InGameCardDeck[i].tag == "Punch"|| InGameCardDeck[i].tag == "PerfectPunch")
            {
                GameObject cardObj = InGameCardDeck[i];
                Card_Base card = cardObj.GetComponent<Card_Base>();
                card.isActive = true;
                Cards.Add(cardObj);
                cardObj.SetActive(true);
                UIManager.instance.AddCard(cardObj, camp);
                InGameCardDeck.RemoveAt(i);
                Debug.Log("DrawPunchCard Processing");
                return;
            }
        }
        for (int i = 0; i < UsedCardsDeck.Count; i++)
        {
            if (UsedCardsDeck[i].tag == "Punch"|| UsedCardsDeck[i].tag == "PerfectPunch")
            {
                GameObject cardObj = UsedCardsDeck[i];
                Card_Base card = cardObj.GetComponent<Card_Base>();
                card.isActive = true;
                Cards.Add(cardObj);
                cardObj.SetActive(true);
                UIManager.instance.AddCard(cardObj, camp);
                UsedCardsDeck.RemoveAt(i);
                Debug.Log("DrawPunchCard Processing");
                return;
            }
        }

    }

    public void AddToUsedCards(GameObject cardObj, bool isAbandon = false)
    {
        Card_Base card = cardObj.GetComponent<Card_Base>();
        if (isAbandon && card.ContainsTriggerType(TriggerType.OnAbandon))
        {
            //        card.CardEffect(this, TargetBodyPart, AllyBodyPart, TriggerType.OnDraw);
            //        TargetHeightType AnimHeight = InGameManager.BodyTypeToHeightIndex(TargetBodyPart.GetBodyPartType());

            //        AnimationInfo newInfo = new AnimationInfo(camp, 1 - camp, card.animType, AnimType.Damaged, AnimHeight, AnimHeight, BodyPartToDirection(card.ownerEquipment.AttachedBodyPart.GetBodyPartType()),
            //BodyPartToDirection(TargetBodyPart.GetBodyPartType()), card.casterMoveType, card.opponentMoveType, card.IsTargetInRange(), true, card, TargetBodyPart);
            //        AnimationManager.instance.AddAnimationToEnd(newInfo);
            UseCard(card.gameObject, TriggerType.OnAbandon, true, false, false);
        }

        UsedCardsDeck.Add(cardObj);
        cardObj.SetActive(false);

        card.isActive = false;

        Cards.Remove(cardObj);
    }
    public void AddToVanishedCards(GameObject cardObj)
    {
        VanishedCardsDeck.Add(cardObj);
        cardObj.SetActive(false);
        Cards.Remove(cardObj);
    }
    public GameObject GenerateNewCard(Equipment e, GameObject cardPrefab)
    {
        GameObject g = Instantiate(cardPrefab);
        UIManager.instance.AddCard(g, camp);
        g.GetComponent<Card_Base>().ownerEquipment = e;
        g.GetComponent<Card_Base>().isActive = true;
        Cards.Add(g);
        return g;
    }

    public void RefreshInGameCardDeck()
    {
        foreach (GameObject cardObj in UsedCardsDeck)
        {
            InGameCardDeck.Add(cardObj);
        }
        UsedCardsDeck.Clear();
        ShuffleCards();
    }
    public event System.Action AbilityUpdateEvent;
    public void UseCard(GameObject cardObj, TriggerType triggerType = TriggerType.Use, bool isTriggeredCard = false, bool costStamina = true, bool consume = true)
    {
        Card_Base card = cardObj.GetComponent<Card_Base>();
        if (Tu_StageManager.instance == null || !Tu_StageManager.instance.canDoAction(StageTargetType.Card, null, card, -1) && camp == 0)
        {
            return;
        }
        if (card != null)
        {
            bool TargetIsSet = card.MaxDistance == 0 || TargetBodyPart != null;
            if ((InGameManager.instance.CanUseAction(this) && card.HasEnoughStamina(this) && card.CanUseCard() && TargetIsSet || isTriggeredCard)
                && card.HasEffect(this, triggerType))
            {
                Cards.Remove(cardObj);
                cardObj.SetActive(false);

                if (UseCardEvent != null)
                {
                    UseCardEvent(card, triggerType);
                }

                if (costStamina)
                {
                    StaminaCurrent -= card.StaminaCost;
                }

                bool hasTarget = card.MaxDistance>=1 && card.IsTargetInRange();
                
                // attack target body part, if target body part is null, then choose a random enemy body part.
                BodyPart attackTarget = TargetBodyPart;
                if (attackTarget == null)
                {
                    List<BodyPart> enemyParts = new List<BodyPart>();
                    foreach (BodyPart bp in InGameManager.instance.Characters[1 - camp].bodyParts)
                    {
                        if (bp.GetIsAlive())
                        {
                            enemyParts.Add(bp);
                        }
                    }
                    attackTarget = enemyParts[Random.Range(0, enemyParts.Count)];
                }
                attackFeedBack = new AttackFeedBack(attackTarget, AnimType.Damaged);


                if (hasTarget && card.cardType == CardType.Attack)
                {
                    InGameManager.instance.CardManagers[1 - camp].ReceivingAttack(card, this, attackFeedBack);
                    attackTarget = attackFeedBack.newBodyPart;
                }
                BuffManager ThisBuffManager = InGameManager.instance.BuffManagers[camp];

                if (ThisBuffManager.IsBuffExistOnBodyPart(InGameManager.instance.Characters[camp].GetBodyPart(BodyPartType.Chest), BuffType.Confuse))
                {
                    attackTarget = InGameManager.instance.Characters[1 - camp].GetRandomAliveBodyPart();
                    AllyBodyPart = InGameManager.instance.Characters[camp].GetRandomAliveBodyPart();
                }

                TargetHeightType AnimHeight = InGameManager.BodyTypeToHeightIndex(attackTarget.GetBodyPartType());
                AnimDirection EnemyAnimDirection = BodyPartToDirection(attackTarget.GetBodyPartType());
                AnimationInfo newInfo;
                if (card.ownerEquipment != null)
                {
                    newInfo = new AnimationInfo(camp, 1 - camp, card.animType, attackFeedBack.newAnimType, AnimHeight, AnimHeight, BodyPartToDirection(card.ownerEquipment.AttachedBodyPart.GetBodyPartType()),
       EnemyAnimDirection, card.casterMoveType, card.opponentMoveType, hasTarget, true, card, attackFeedBack.defendCard, attackTarget, null, card.CastSound, card.HitSound);

                }
                else
                {
                    newInfo = new AnimationInfo(camp, 1 - camp, card.animType, attackFeedBack.newAnimType, AnimHeight, AnimHeight, BodyPartToDirection(BodyPartType.Chest),
       EnemyAnimDirection, card.casterMoveType, card.opponentMoveType, hasTarget, true, card, attackFeedBack.defendCard, attackTarget, null, card.CastSound, card.HitSound);

                }


                AnimationManager.instance.AddAnimationToEnd(newInfo);


                card.CardEffect(this, attackTarget, AllyBodyPart, triggerType);
                newInfo.TextEffectInfos = TextEffectManager.instance.GetTextEffectInfo();
                newInfo.CardEffectInfos = CardEffectManager.instance.GetCardEffectInfo();

                if (card.cardType == CardType.Attack)
                {
                    if (card.StaminaCost >= 0)
                    {
                        AttackCardUsedAmount++;
                    }
                    InGameManager.instance.CardManagers[1 - camp].OnAfterBeingAttacked(card, attackTarget);
                }

          

                if(card.ContainsEntry(EntryType.ReplaceCard) || card.ContainsEntry(EntryType.ConsumeItem) || card.CoreAbilityCard)
                {
                    Destroy(cardObj, 5);

                }
                else if (consume)
                {
                    if (card.ContainsEntry(EntryType.Counsume))
                    {
                        AddToVanishedCards(cardObj);
                    }
                    else
                    {
                        AddToUsedCards(cardObj);
                    }
                }    
                
                AfterUseCard(card);
            }
        }
    }
    public event System.Action<Card_Base,TriggerType> UseCardEvent;
    public event System.Action<Card_Base, CardManager, AttackFeedBack> ReceiveAttackEvent;
    public void ReceivingAttack(Card_Base card, CardManager cardManager, AttackFeedBack feedBack)
    {
        if (ReceiveAttackEvent != null)
        {
            ReceiveAttackEvent(card, cardManager, feedBack);
        }
        for (int i = 0; i < Cards.Count; i++)
        {
            Card_Base myCard = Cards[i].GetComponent<Card_Base>();
            if (myCard.ContainsTriggerType(TriggerType.ReceivingAttack))
            {
                if (myCard.HasEnoughStamina(this) && myCard.IsTargetInRange())
                {
                    myCard.CardEffect(this, TargetBodyPart, AllyBodyPart, TriggerType.ReceivingAttack);
                    if (!myCard.ContainsEntry(EntryType.Counsume))
                    {
                        AddToUsedCards(myCard.gameObject);
                    }
                    else
                    {
                        AddToVanishedCards(myCard.gameObject);
                    }
                    break;
                }
            }
        }
    }

    public event System.Action<Card_Base, BodyPart> AfterBeingAttackedEvent;
    public void OnAfterBeingAttacked(Card_Base card, BodyPart bodyPart)
    {
        if (AfterBeingAttackedEvent != null)
        {
            AfterBeingAttackedEvent(card, bodyPart);
        }
        for (int i = 0; i < Cards.Count; i++)
        {
            Card_Base myCard = Cards[i].GetComponent<Card_Base>();
            if (myCard.ContainsTriggerType(TriggerType.AfterBeingAttacked))
            {
                if (myCard.HasEnoughStamina(this) && myCard.IsTargetInRange())
                {
                    //                  myCard.CardEffect(this, TargetBodyPart, AllyBodyPart,TriggerType.AfterBeingAttacked);
                    //                  AddToUsedCards(myCard.gameObject);

                    //                  TargetHeightType AnimHeight = InGameManager.BodyTypeToHeightIndex(TargetBodyPart.GetBodyPartType());

                    //                  AnimationInfo newInfo = new AnimationInfo(camp, 1 - camp, myCard.animType, AnimType.Damaged, AnimHeight, AnimHeight, BodyPartToDirection(myCard.ownerEquipment.AttachedBodyPart.GetBodyPartType()),
                    //BodyPartToDirection(TargetBodyPart.GetBodyPartType()), myCard.casterMoveType, myCard.opponentMoveType, myCard.IsTargetInRange(), true, myCard, TargetBodyPart);
                    //                  AnimationManager.instance.AddAnimationToEnd(newInfo);
                    UseCard(myCard.gameObject, TriggerType.AfterBeingAttacked, true, true);

                    break;
                }
            }
        }
    }
    public System.Action<BodyPart,float,bool> CauseDamageEvent;
    public void OnCauseDamage(BodyPart target,float amount, bool causeDeath)
    {
        if (CauseDamageEvent != null)
        {
            CauseDamageEvent(target, amount, causeDeath);
        }
    }
    public event System.Action<Card_Base> AfterUsedCard;

    public void RecordUsedCard(Card_Base card)
    {
        if(UsedCardRecordList.Count >= 10)
        {
            UsedCardRecordList.RemoveAt(0);
        }
        UsedCardRecordList.Add(card.gameObject);
    }
    public void AfterUseCard(Card_Base card)
    {
        if (AfterUsedCard != null)
        {
            AfterUsedCard(card);
        }

        RecordUsedCard(card);

        if (camp == 0 && card.ownerEquipment != null)
        {
            CoreAbility_Base aCore = InGameManager.instance.AbilityManagers[0].m_CoreAbility;

            if (aCore != null && aCore.AbilityAlreadyTriggered == false)
            {
                for (int i = 0; i < aCore.m_AbilityComboNeedList.Count;i++)
                {
                    if (aCore.m_AbilityComboNeedList[i].m_Triggered)
                    {
                        continue;
                    }
                    else
                    {
                        EM_Weapon aWeapon = card.ownerEquipment as EM_Weapon;
                        if (aWeapon != null)
                        {
                            if (ComboBodyPart.Weapon == aCore.m_AbilityComboNeedList[i].m_BodyPartType)
                            {
                                aCore.m_AbilityComboNeedList[i].m_Triggered = true;
                                UIManager.instance.CoreAbilityTriggered(i);
                                break;
                            }
                        }
                        else if (aCore.GetComboBodyPart(card.ownerEquipment.AttachedBodyPart.GetBodyPartType()) == aCore.m_AbilityComboNeedList[i].m_BodyPartType)
                        {
                            aCore.m_AbilityComboNeedList[i].m_Triggered = true;
                            UIManager.instance.CoreAbilityTriggered(i);
                            break;
                        }
                    }

                }

                if (aCore.AllTriggered())
                {
                    AnimationManager.instance.AddAnimationToEnd(new AnimationInfo());
                    CoreAbilityTrigger();
                    
                }
            }
        }



        for (int i = 0; i < Cards.Count; i++)
        {
            Card_Base myCard = Cards[i].GetComponent<Card_Base>();
            if (myCard.ContainsTriggerType(TriggerType.AfterUseCard))
            {
                if (myCard.HasEnoughStamina(this) && myCard.IsTargetInRange() && myCard.HasEffect(this, TriggerType.AfterUseCard,card))
                {
                    UseCard(myCard.gameObject, TriggerType.AfterUseCard, true, false);
                    return;
                }
            }
        }



    }

    public void CoreAbilityTrigger()
    {
        Card_Base aCard = Instantiate(InGameManager.instance.AbilityManagers[camp].m_CoreAbility.m_AbilityCard);
        UseCard(aCard.gameObject,TriggerType.Use,true,false,true);
    }


    public void SelfTriggering()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Card_Base myCard = Cards[i].GetComponent<Card_Base>();
            if (myCard.ContainsTriggerType(TriggerType.SelfTriggering))
            {
                UseCard(myCard.gameObject, TriggerType.SelfTriggering, true, false);
                return;
            }
        }
    }
    public static AnimDirection BodyPartToDirection(BodyPartType type)
    {
        switch (type)
        {
            case BodyPartType.Chest:
                return AnimDirection.Center;
            case BodyPartType.Head:
                return AnimDirection.Center;
            case BodyPartType.LeftArm:
                return AnimDirection.Left;
            case BodyPartType.LeftLeg:
                return AnimDirection.Left;
            case BodyPartType.RightArm:
                return AnimDirection.Right;
            case BodyPartType.RightLeg:
                return AnimDirection.Right;

            default:
                return AnimDirection.Center;
        }
    }

    public static AnimDirection SlotToAnimDirection(EquipmentType slot)
    {
        switch (slot)
        {
            case EquipmentType.Armor:
                return AnimDirection.Center;
            case EquipmentType.Helmet:
                return AnimDirection.Center;
            case EquipmentType.LeftArm:
                return AnimDirection.Left;
            case EquipmentType.LeftLeg:
                return AnimDirection.Left;
            case EquipmentType.LeftWeapon:
                return AnimDirection.Left;
            case EquipmentType.RightLeg:
                return AnimDirection.Right;
            case EquipmentType.RightArm:
                return AnimDirection.Right;
            case EquipmentType.RightWeapon:
                return AnimDirection.Right;
            default:
                return AnimDirection.Center;
        }
    }

}
