using CardGame.CardModels.Characters;
using CardGame.CardModels.Items;
using CardGame.GameObjects;
using CardGame.ViewModels;
using System.Linq;

namespace CardGame.GameObjectsUI;

public partial class Board : ContentPage
{
    /// <summary>
    /// Card per person count.
    /// </summary>
    private const int cardPerPerson = 32;

    /// <summary>
    /// Highlight card animation time.
    /// </summary>
    private const int highlightCardAnimationTime = 500;

    /// <summary>
    /// Players in game.
    /// </summary>
    private readonly List<Player> players;

    public Board()
    {
        InitializeComponent();

        players = new List<Player>() { null, null };

        //todo rozdawanie kart graczom i ich tworzenie
        players[0] = new Player();
        players[1] = new ComputerPlayer();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Build(new()
            {
                Cards = Player.GetCards(cardPerPerson)
            });
        }

        players[0].SetLobby(PlayerCards);
        players[0].SetBoard(PlayerBoard);
        players[1].SetBoard(ComputerBoard);

        players[0].OnChosenCard += ChosenCardSelected;

        AddClickEventToSelect_PlayerCards();

        Loaded += (s, e) => ComputerTurn();
        //todo change loaded to start event
    }

    #region Events methods
    // powiązania istnieją wyłącznie z tym kodem,
    // po przeniesieniu tego na kartę Player.cs będzie można zlikwidować
    // te metody
    // * Wyjątkiem są objekty tali gracza i przypisywanie usuwania z niej (trzeba zrobić) * //
    // ----------------------------- //

    #endregion

    // domyślna akcja kliknięcia karty gracza
    private void AddClickEventToSelect_PlayerCards()
    {
        foreach (CardBase card in PlayerCards.Children.Cast<CardBase>())
        {
            card.OnCardTaped += players[0].ThrowNewCard;
            card.OnCardTaped += PostThrowNewCard;
        }
    }

    private void PostThrowNewCard(CardBase card)
    {
        card.OnCardTaped -= PostThrowNewCard;
        card.OnCardTaped += TargetedAllieCardSelected;
    }

    #region Game logic

    #region Player turn

    public void TargetedAllieCardSelected(CardBase card)
    {
        switch (players[0].TurnFaze)
        {
            // targeted allies:
            case Player.TurnFazeEnum.TargetingPlayerCard:
                if (players[0].ChosenCard is ItemCard)
                    TargetAllieCardToUseItemCard(card);
                break;

            case Player.TurnFazeEnum.TargetingPlayerCards:
                if (players[0].ChosenCard is ItemCard)
                    TargetAllieCardsToUseItemCard(card);
                break;

            default:
                return;

        }
    }

    private void ChosenCardSelected()
    {
        if (players[0].TurnFaze != Player.TurnFazeEnum.ChosePlayerCard)
            return;

        if (players[0].ChosenCard is CharacterCard)
        {
            players[0].TurnFaze = Player.TurnFazeEnum.TargetingEnemyCard;
            // Wybieranie postaci przeciwnika do zwykłego ataku
        }
        else if (players[0].ChosenCard is ItemCard)
        {
            switch ((((players[0].ChosenCard as ItemCard).BindingContext as ItemCardViewModel).CardModel as ItemBase).ItemType)
            {
                case ItemBase.ItemTypeEnum.ToOneEnemy:
                    players[0].TurnFaze = Player.TurnFazeEnum.TargetingEnemyCard;
                    //wybieranie jednej postaci przeciwnika
                    //players[0].HighlightTargetedCard((d, b) => SelectEnemyCardToUseTargetCard());
                    break;

                case ItemBase.ItemTypeEnum.ToMoreThanOneEnemy:
                    players[0].TurnFaze = Player.TurnFazeEnum.TargetingEnemyCards;
                    //wybieranie więcej niż jednej postaci przeciwnika
                    //players[0].HighlightTargetedCard((d, b) => SelectEnemyCardsToUseTargetCard());
                    break;

                case ItemBase.ItemTypeEnum.ToAllEnemies:
                    players[0].TurnFaze = Player.TurnFazeEnum.TargetingEnemyAllCards;

                    List<CardBase> computerBoardCharacters = new();
                    computerBoardCharacters.AddRange(ComputerBoard.Cast<CardBase>());
                    TargetAllEnemyCardsToUseItemCard(computerBoardCharacters);

                    //używanie itemu na wszystkich postaciach przeciwnika
                    //players[0].HighlightTargetedCard((d, b) => SelectAllEnemyCardsToUseTargetCard());
                    break;

                // ------- //

                case ItemBase.ItemTypeEnum.ToOneAllie:
                    players[0].TurnFaze = Player.TurnFazeEnum.TargetingPlayerCard;
                    //wybieranie jednej postaci sojusznika
                    //players[0].HighlightTargetedCard((d, b) => SelectAllieCardToUseTargetCard());
                    break;

                case ItemBase.ItemTypeEnum.ToMoreThanOneAllie:
                    players[0].TurnFaze = Player.TurnFazeEnum.TargetingPlayerCards;
                    //wybieranie więcej niż jednej postaci sojusznika
                    //players[0].HighlightTargetedCard((d, b) => SelectAllieCardsToUseTargetCard());
                    break;

                case ItemBase.ItemTypeEnum.ToAllAllies:
                    players[0].TurnFaze = Player.TurnFazeEnum.TargetingPlayerAllCards;

                    List<CardBase> playerBoardCharacters = new();
                    playerBoardCharacters.AddRange(PlayerBoard.Cast<CardBase>());
                    TargetAllAllieCardsToUseItemCard(playerBoardCharacters);

                    //używanie itemu na wszystkich postaciach sojusznika
                    //players[0].HighlightTargetedCard((d, b) => SelectAllAllieCardsToUseTargetCard());
                    break;
            }
        }
    }

    #region Select options

    private void TargetEnemyCardForChosenCharacter(CharacterCard card)
    {
        players[0].TargetedEnemiesCards.Add(card);
        players[0].TurnFaze = Player.TurnFazeEnum.Attacking;
        players[0].HighlightTargetedEnemyCard((d, b) => AttackTargetCard());
    }

    private void TargetEnemyCardToUseItemCard(CardBase card)
    {
        players[0].TargetedEnemiesCards.Add(card);
        players[0].TurnFaze = Player.TurnFazeEnum.UsingItem;
        // todo zmienić kolor animacji
        players[0].HighlightTargetedEnemyCardForItem((d, b) => UseItemToTargetCards());
    }

    private void TargetEnemyCardsToUseItemCard(CardBase card)
    {

    }

    private void TargetAllEnemyCardsToUseItemCard(List<CardBase> cards)
    {
        players[0].TargetedEnemiesCards.AddRange(cards);
        players[0].TurnFaze = Player.TurnFazeEnum.UsingItem;
        // todo zmienić kolor animacji
        players[0].HighlightTargetedEnemyCardForItem((d, b) => UseItemToTargetCards());
    }

    private void TargetAllieCardToUseItemCard(CardBase card)
    {
        players[0].TargetedAlliesCards.Add(card);
        players[0].TurnFaze = Player.TurnFazeEnum.UsingItem;
        // todo zmienić kolor animacji
        players[0].HighlightTargetedAllieCardForItem((d, b) => UseItemToTargetCards());
    }

    private void TargetAllieCardsToUseItemCard(CardBase card)
    {

    }

    private void TargetAllAllieCardsToUseItemCard(List<CardBase> cards)
    {
        players[0].TargetedAlliesCards.AddRange(cards);
        players[0].TurnFaze = Player.TurnFazeEnum.UsingItem;
        // todo zmienić kolor animacji
        players[0].HighlightTargetedAllieCardForItem((d, b) => UseItemToTargetCards());
    }

    #endregion

    // Wybieranie karty:
    private void OnComputerCardClickedPlayerTargeted(CardBase card)
    {
        switch (players[0].TurnFaze)
        {
            case Player.TurnFazeEnum.TargetingEnemyCard:
                if (players[0].ChosenCard is CharacterCard)
                    TargetEnemyCardForChosenCharacter(card as CharacterCard);
                else if (players[0].ChosenCard is ItemCard)
                    TargetEnemyCardToUseItemCard(card as ItemCard);
                break;

            case Player.TurnFazeEnum.TargetingEnemyCards:
                if (players[0].ChosenCard is ItemCard)
                    TargetEnemyCardsToUseItemCard(card as ItemCard);
                break;

            default:
                return;
        }
    }

    private void AttackTargetCard()
    {
        if (players[0].TurnFaze != Player.TurnFazeEnum.Attacking)
            return;

        CharacterBase myCharacter = (players[0].ChosenCard.BindingContext as ICardViewModel).CardModel as CharacterBase;
        CharacterBase enemyCharacter = (players[0].TargetedEnemiesCards[0].BindingContext as ICardViewModel).CardModel as CharacterBase;

        // Normal attack
        if (players[0].AttackType == Player.AttackTypeEnum.Attack)
        {
            myCharacter.Attack(enemyCharacter);
            players[0].SpecialPoints++;
        }
        // Special attack
        else if (players[0].AttackType == Player.AttackTypeEnum.SpecialAttack)
        {
            List<CharacterBase> playerBoardCharacters = new();
            foreach (CardBase item in PlayerBoard.Cast<CardBase>())
            {
                if ((item.BindingContext as ICardViewModel).CardModel is CharacterBase)
                    playerBoardCharacters.Add((item.BindingContext as ICardViewModel).CardModel as CharacterBase);
            }

            List<CharacterBase> computerBoardCharacters = new();
            foreach (CardBase item in ComputerBoard.Cast<CardBase>())
            {
                if ((item.BindingContext as CharacterCardViewModel).CardModel is CharacterBase)
                    computerBoardCharacters.Add((item.BindingContext as CharacterCardViewModel).CardModel as CharacterBase);
            }

            myCharacter.SpecialAttack(computerBoardCharacters.ToArray(), playerBoardCharacters.ToArray(), enemyCharacter);
            players[0].SpecialPoints = 0;
        }

        PostPlayerAttackOrUseItem();
    }

    private void PostPlayerAttackOrUseItem()
    {
        #region Animation
        if (players[0].ChosenCard != null)
            (players[0].ChosenCard.BindingContext as ICardViewModel).CardModel.AuraBrush = Brush.Transparent;
        #endregion
        players[0].ChosenCard = null;

        #region Animation
        if (players[0].TargetedEnemiesCards?.Count > 0)
            players[0].TargetedEnemiesCards.ForEach((card) => (card.BindingContext as ICardViewModel).CardModel.AuraBrush = Brush.Transparent);
        #endregion

        #region Animation
        if (players[0].TargetedAlliesCards?.Count > 0)
            players[0].TargetedAlliesCards.ForEach((card) => (card.BindingContext as ICardViewModel).CardModel.AuraBrush = Brush.Transparent);
        #endregion

        players[0].TargetedEnemiesCards?.Clear();
        players[0].TargetedAlliesCards?.Clear();

        players[0].AttackType = Player.AttackTypeEnum.Attack;

        players[0].TurnFaze = Player.TurnFazeEnum.EnemyTure;

        PlayerCards.IsVisible = false;

        ComputerTurn();

        PlayerCards.IsVisible = true;
    }

    private void UseItemToTargetCards()
    {
        if (players[0].TurnFaze != Player.TurnFazeEnum.UsingItem)
            return;

        ItemBase myItem = (players[0].ChosenCard.BindingContext as ICardViewModel).CardModel as ItemBase;

        List<ICardModel> enemyCardModels = new();
        enemyCardModels.AddRange(from card in players[0].TargetedEnemiesCards
                                 select (card.BindingContext as ICardViewModel).CardModel);

        List<ICardModel> allieCardModels = new();
        allieCardModels.AddRange(from card in players[0].TargetedAlliesCards
                                 select (card.BindingContext as ICardViewModel).CardModel);

        List<ICardModel> playerBoardCardModels = new();
        playerBoardCardModels.AddRange(from CardBase item in PlayerBoard.Cast<CardBase>()
                                       select (item.BindingContext as ICardViewModel).CardModel);

        List<ICardModel> computerBoardCardModels = new();
        computerBoardCardModels.AddRange(from CardBase item in ComputerBoard.Cast<CardBase>()
                                         select (item.BindingContext as ICardViewModel).CardModel);

        myItem.ItemFunction(computerBoardCardModels.ToArray(), playerBoardCardModels.ToArray(), enemyCardModels.ToArray(), allieCardModels.ToArray());

        // todo czy naliczać punkty przy urzyciu itemu?
        players[0].SpecialPoints++;

        PostPlayerAttackOrUseItem();
    }

    #endregion

    #region Computer turn

    private void ComputerTurn()
    {
        if (ComputerBoard.Children.Count > 0)
        {
            switch (new Random().Next(0, 2))
            {
                case 0:
                    new Animation((v) => { }).Commit(ComputerBoard, "Animation", 16, 1500, finished: (d, b) =>
                    {
                        players[1].ChosenCard = ComputerBoard[new Random().Next(0, ComputerBoard.Children.Count)] as CardBase;
                        players[1].HighlightChosenCard((d, b) => ComputerTargetEnemyCard());
                    });
                    break;

                case 1:
                    ToComputerThrowNewCard();
                    break;
            }

        }
        else ToComputerThrowNewCard();
    }

    private void ToComputerThrowNewCard()
    {
        if (players[1].DeckOfCards.Cards.Count < 1)
        {

            ClearComputerTurnData();
            return;
        }

        // czekanie na ruch
        new Animation((v) => { return; }).Commit(this, "Animation", 16, 1500, Easing.Linear, finished: (d, b) => ComputerThrowNewCard());
    }

    private void ComputerThrowNewCard()
    {
        players[1].ChosenCard = players[1].DeckOfCards.Cards.First();

        for (int i = 0; i < players[1].DeckOfCards.Cards.Count; i++)
        {
            if (!ComputerBoard.Contains(players[1].DeckOfCards.Cards[i]))
            {
                players[1].ChosenCard = players[1].DeckOfCards.Cards[i];
                break;
            }
        }

        if (!ComputerBoard.Contains(players[1].ChosenCard))
        {
            ComputerBoard.Children.Add(players[1].ChosenCard);
            players[1].ChosenCard.ToDestroy += players[1].RemoveCardFromBoard;
            players[1].ChosenCard.OnCardTaped += OnComputerCardClickedPlayerTargeted;
        }

        players[1].HighlightChosenCard((d, b) => ComputerTargetEnemyCard());
    }

    private void ComputerTargetEnemyCard()
    {
        new Animation((v) => { }).Commit(ComputerBoard, "Animation", 16, 1500, finished: (d, b) =>
        {
            if (PlayerBoard.Children.Count == 0)
            {
                ClearComputerTurnData();
                return;
            }

            players[1].TargetedEnemiesCards.Add(PlayerBoard.Children[new Random().Next(0, PlayerBoard.Children.Count)] as CardBase);

            if (players[1].TargetedEnemiesCards[0] != null)
                new Animation(callback: v => (players[1].TargetedEnemiesCards[0].BindingContext as CharacterCardViewModel).CardModel.AuraBrush = Color.FromRgba(v, 0, 0, 0.75),
                    start: 0,
                    end: 1).Commit(players[1].TargetedEnemiesCards[0], "Animation", 16, highlightCardAnimationTime, finished: (d, b) => ComputerAttack());
        });
    }

    private void ComputerAttack()
    {
        // atak kończący turę
        new Animation((v) => { }).Commit(ComputerBoard, "Animation", 16, 1000, finished: (d, b) =>
        {
            CharacterBase computerCharacter = (CharacterBase)(players[1].ChosenCard.BindingContext as CharacterCardViewModel).CardModel;
            CharacterBase enemyCharacter = (CharacterBase)(players[1].TargetedEnemiesCards[0].BindingContext as CharacterCardViewModel).CardModel;

            // Normal attack
            if (players[1].AttackType == Player.AttackTypeEnum.Attack)
            {
                computerCharacter.Attack(enemyCharacter);
                players[1].SpecialPoints++;
            }
            // Special attack
            else if (players[1].AttackType == Player.AttackTypeEnum.SpecialAttack)
            {
                List<CharacterBase> computerBoardCharacters = new();
                foreach (CardBase item in ComputerBoard.Cast<CardBase>())
                {
                    computerBoardCharacters.Add((CharacterBase)(item.BindingContext as CharacterCardViewModel).CardModel);
                }
                List<CharacterBase> playerBoardCharacters = new();
                foreach (CardBase item in ComputerBoard.Cast<CardBase>())
                {
                    playerBoardCharacters.Add((CharacterBase)(item.BindingContext as CharacterCardViewModel).CardModel);
                }

                computerCharacter.SpecialAttack(playerBoardCharacters.ToArray(), computerBoardCharacters.ToArray(), enemyCharacter);
                players[1].SpecialPoints = 0;
            }

            ClearComputerTurnData();
        });
    }

    private void ClearComputerTurnData()
    {
        #region Animation
        if (players[1].ChosenCard != null)
            (players[1].ChosenCard.BindingContext as CharacterCardViewModel).CardModel.AuraBrush = Brush.Transparent;
        #endregion
        players[1].ChosenCard = null;

        #region Animation
        if (players[1].TargetedEnemiesCards.Count > 0 && players[1].TargetedEnemiesCards[0] != null)
            (players[1].TargetedEnemiesCards[0].BindingContext as CharacterCardViewModel).CardModel.AuraBrush = Brush.Transparent;
        #endregion
        players[1].TargetedEnemiesCards?.Clear();

        players[0].TurnFaze = Player.TurnFazeEnum.ChosePlayerCard;
    }

    #endregion

    #endregion

    // ---------------------------------------------- //

    #region Drag&Drop

    //private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
    //{
    //    var d1 = e.Data.Properties["Card"];
    //    if (d1 is Card)
    //    {
    //        var card = d1 as Card;

    //        // For player1:
    //        if (player1.DeckOfCards.Cards.Contains(card))
    //        {
    //            if (!PlayerBoard.Contains(card))
    //            {
    //                PlayerThrowCard(card);
    //                PlayerTurn(card);
    //            }
    //        }
    //    }

    //    // Cofnięcie animacji:
    //    PlayerBoardBorder.BackgroundColor = Color.FromHex("#66AAAAFF");
    //    PlayerBoardBorder.Stroke = Color.Parse("Blue");
    //}

    //#region Animations

    //private void DropGestureRecognizer_DragOver(object sender, DragEventArgs e)
    //{
    //    PlayerBoardBorder.BackgroundColor = Color.FromHex("#AAAAFF");
    //    PlayerBoardBorder.Stroke = Color.Parse("DarkBlue");
    //}

    //private void DropGestureRecognizer_DragLeave(object sender, DragEventArgs e)
    //{
    //    PlayerBoardBorder.BackgroundColor = Color.FromHex("#66AAAAFF");
    //    PlayerBoardBorder.Stroke = Color.Parse("Blue");
    //}

    //#endregion

    #endregion
}