using CardGame.Characters;
using CardGame.GameObjects;
using CardGame.ViewModels;

namespace CardGame.GameObjectsUI;

public partial class Board : ContentPage
{
    private readonly List<Player> players;
    private readonly int cardPerPerson = 32;

    public Board()
    {
        InitializeComponent();

        players = new List<Player>() { null, null };

        for (int i = 0; i < players.Count; i++)
        {
            players[i] = new Player()
            {
                DeckOfCards = new()
                {
                    Cards = Player.GetCards(cardPerPerson)
                }
            };
            players[i].DeckOfCards.Cards.ForEach((card) =>
            {
                (card.BindingContext as CardViewModel).Character.OnHealthToZero += DestroyCard;

                card.ToDestroy += RemoveCardFrom_Player; // dać to
                card.ToDestroy += RemoveCardFrom_Computer; // dla każdego osobno
            });
        }

        // Show player's cards in lobby panel:
        foreach (Card card in players[0].DeckOfCards.Cards)
        {
            PlayerCards.Children.Add(card);
            card.ToDestroy += RemoveCardFrom_PlayerCards;
        }

        AddClickEventToSelect_PlayerCards();

        this.Loaded += (s, e) =>
        ComputerTurn();

    }

    #region Events methods

    // All player cards:
    private void RemoveCardFrom_Player(Card card)
    {
        if (players[0].DeckOfCards.Cards.Contains(card))
            players[0].DeckOfCards.Cards.Remove(card);

        card.ToDestroy -= RemoveCardFrom_Player;
    }

    // Player's card lobby:
    private void RemoveCardFrom_PlayerCards(Card card)
    {
        if (PlayerCards.Children.Contains(card))
            PlayerCards.Children.Remove(card);

        card.ToDestroy -= RemoveCardFrom_PlayerCards;
    }

    // Player's board:
    private void RemoveCardFrom_PlayerBoard(Card card)
    {
        if (PlayerBoard.Children.Contains(card))
            PlayerBoard.Children.Remove(card);

        card.ToDestroy -= RemoveCardFrom_PlayerBoard;
    }

    // - - - - - //

    // All computer cards:
    private void RemoveCardFrom_Computer(Card card)
    {
        if (players[1].DeckOfCards.Cards.Contains(card))
            players[1].DeckOfCards.Cards.Remove(card);

        card.ToDestroy -= RemoveCardFrom_Computer;
    }

    // Computer's board:
    private void RemoveCardFrom_ComputerBoard(Card card)
    {
        if (ComputerBoard.Children.Contains(card))
            ComputerBoard.Children.Remove(card);

        card.ToDestroy -= RemoveCardFrom_ComputerBoard;
    }

    // ----------------------------- //

    private void DestroyCard(Card card)
    {
        card.Destroy();
    }

    #endregion

    // domyślna akcja kliknięcia karty gracza
    private void AddClickEventToSelect_PlayerCards()
    {
        foreach (Card card in PlayerCards.Children.Cast<Card>())
        {
            card.OnCardTaped += ThrowCard_Player;
        }
    }

    // przestarzałe
    private void RemoveClickEventToSelect_PlayerCards()
    {
        foreach (Card card in PlayerCards.Children.Cast<Card>())
        {
            card.OnCardTaped -= ThrowCard_Player;
        }
        foreach (Card card in players[0].DeckOfCards.Cards)
        {
            card.OnCardTaped -= SelectOwnCard;
        }
    }

    #region Game logic

    #region Player turn

    // Wystawia karty z lobby na planszę.
    private void ThrowCard_Player(Card card)
    {
        if (players[0].TurnFaze is not Player.TurnFazeEnum.SelectingPlayerCard)
            return;

        card.OnCardTaped -= ThrowCard_Player;
        card.OnCardTaped += ChangeAttackMode;   // 1
        card.OnCardTaped += SelectOwnCard;      // 2

        RemoveCardFrom_PlayerCards(card);

        PlayerBoard.Children.Add(card);
        card.ToDestroy += RemoveCardFrom_PlayerBoard;

        SelectOwnCard(card);
    }

    // Wybieranie karty.
    private void SelectOwnCard(Card card)
    {
        if (players[0].TurnFaze is not Player.TurnFazeEnum.SelectingPlayerCard)
            return;

        players[0].ChosenCard = card;

        #region Animation
        if (players[0].ChosenCard != null)
            new Animation(callback: v => (players[0].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromRgba(0, 0, v, 0.5),
                start: 0,
                end: 1).Commit(players[0].ChosenCard, "Animation", 16, 500);
        #endregion

        PlayerCards.IsVisible = false;

        players[0].TurnFaze = Player.TurnFazeEnum.SelectingEnemyCard;
    }

    // Wybieranie karty atakowanej:
    private void OnComputerCardClickedPlayerTargeted(Card card)
    {
        if (players[0].TurnFaze != Player.TurnFazeEnum.SelectingEnemyCard)
            return;

        players[0].TargetedCard = card;

        players[0].TurnFaze = Player.TurnFazeEnum.Attacking;

        if (players[0].TargetedCard != null)
            new Animation(callback: v => (players[0].TargetedCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromRgba(v, 0, 0, 0.5),
                    start: 0,
                    end: 1).Commit(players[0].TargetedCard, "Animation", 16, 500, finished: (d, b) => AttackTargetCard());
    }

    private void ChangeAttackMode(Card card)
    {
        if (players[0].ChosenCard != card && players[0].TurnFaze != Player.TurnFazeEnum.SelectingEnemyCard)
            return;

        switch (players[0].AttackType)
        {
            case Player.AttackTypeEnum.Attack:

                if (players[0].SpecialPoints < 3)
                    return;

                players[0].AttackType = Player.AttackTypeEnum.SpecialAttack;

                #region Animation
                if (players[0].ChosenCard != null)
                    new Animation(callback: v => (players[0].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromRgba(v, v, 0, 0.73),
                        start: 0,
                        end: 1).Commit(players[0].ChosenCard, "Animation", 16, 500);
                #endregion

                break;

            case Player.AttackTypeEnum.SpecialAttack:

                players[0].AttackType = Player.AttackTypeEnum.Attack;

                #region Animation
                if (players[0].ChosenCard != null)
                    new Animation(callback: v => (players[0].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromRgba(0, 0, v, 0.5),
                        start: 0,
                        end: 1).Commit(players[0].ChosenCard, "Animation", 16, 500);
                #endregion

                break;
        }
    }

    private void AttackTargetCard()
    {
        CharacterBase myCharacter = (players[0].ChosenCard.BindingContext as CardViewModel).Character;
        CharacterBase enemyCharacter = (players[0].TargetedCard.BindingContext as CardViewModel).Character;

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
            foreach (Card item in PlayerBoard.Cast<Card>())
            {
                playerBoardCharacters.Add((item.BindingContext as CardViewModel).Character);
            }
            List<CharacterBase> computerBoardCharacters = new();
            foreach (Card item in ComputerBoard.Cast<Card>())
            {
                computerBoardCharacters.Add((item.BindingContext as CardViewModel).Character);
            }

            myCharacter.SpecialAttack(computerBoardCharacters.ToArray(), playerBoardCharacters.ToArray(), enemyCharacter);
            players[0].SpecialPoints = 0;
        }

        #region Animation
        if (players[0].ChosenCard != null)
            (players[0].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Brush.Transparent;
        #endregion
        players[0].ChosenCard = null;

        #region Animation
        if (players[0].TargetedCard != null)
            (players[0].TargetedCard.BindingContext as CardViewModel).Character.AuraBrush = Brush.Transparent;
        #endregion
        players[0].TargetedCard = null;

        players[0].TurnFaze = Player.TurnFazeEnum.EnemyTure;

        ComputerTurn();

        PlayerCards.IsVisible = true;
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
                        players[1].ChosenCard = ComputerBoard[new Random().Next(0, ComputerBoard.Children.Count)] as Card;

                        if (players[1].ChosenCard != null)
                            new Animation(callback: v => (players[1].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromRgba(0, 0, 0, v),
                                start: 0,
                                end: 0.75).Commit(players[1].ChosenCard, "Animation", 16, 500, finished: (d, b) => ComputerTargetEnemyCard());
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
            players[1].ChosenCard.ToDestroy += RemoveCardFrom_ComputerBoard;
            players[1].ChosenCard.OnCardTaped += OnComputerCardClickedPlayerTargeted;
        }

        if (players[1].ChosenCard != null)
            new Animation(callback: v => (players[1].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromRgba(0, 0, 0, v),
                start: 0,
                end: 0.75).Commit(players[1].ChosenCard, "Animation", 16, 500, finished: (d, b) => ComputerTargetEnemyCard());
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

            players[1].TargetedCard = PlayerBoard.Children[new Random().Next(0, PlayerBoard.Children.Count)] as Card;

            if (players[1].TargetedCard != null)
                new Animation(callback: v => (players[1].TargetedCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromRgba(v, 0, 0, 0.75),
                    start: 0,
                    end: 1).Commit(players[1].TargetedCard, "Animation", 16, 500, finished: (d, b) => ComputerAttack());
        });
    }

    private void ComputerAttack()
    {
        // atak kończący turę
        new Animation((v) => { }).Commit(ComputerBoard, "Animation", 16, 1000, finished: (d, b) =>
        {
            CharacterBase computerCharacter = (players[1].ChosenCard.BindingContext as CardViewModel).Character;
            CharacterBase enemyCharacter = (players[1].TargetedCard.BindingContext as CardViewModel).Character;

            computerCharacter.Attack(enemyCharacter);

            ClearComputerTurnData();
        });
    }

    private void ClearComputerTurnData()
    {
        #region Animation
        if (players[1].ChosenCard != null)
            (players[1].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Brush.Transparent;
        #endregion
        players[1].ChosenCard = null;

        #region Animation
        if (players[1].TargetedCard != null)
            (players[1].TargetedCard.BindingContext as CardViewModel).Character.AuraBrush = Brush.Transparent;
        #endregion
        players[1].TargetedCard = null;

        players[0].TurnFaze = Player.TurnFazeEnum.SelectingPlayerCard;
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