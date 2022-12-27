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

    // Wystawia karty z lobby na planszę.
    private void ThrowCard_Player(Card card)
    {
        if (players[0].TurnFaze is not Player.TurnFazeEnum.SelectingPlayerCard)
            return;

        card.OnCardTaped -= ThrowCard_Player;
        card.OnCardTaped += SelectOwnCard;

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
            (players[0].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromArgb("#880000FF");
        #endregion

        PlayerCards.IsVisible = false;

        players[0].TurnFaze = Player.TurnFazeEnum.SelectingEnemyCard;
    }

    // Wybieranie karty atakowanej:
    private void OnComputerCardClickedPlayerTargeted(Card card)
    {
        if (players[0].TurnFaze != Player.TurnFazeEnum.SelectingEnemyCard &&
            players[0].TurnFaze != Player.TurnFazeEnum.UsingSpecialAttack)
            return;

        players[0].TargetedCard = card;
        #region Animation
        if (players[0].TargetedCard != null)
            (players[0].TargetedCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromArgb("#88FF0000");
        #endregion

        CharacterBase myCharacter = (players[0].ChosenCard.BindingContext as CardViewModel).Character;
        CharacterBase enemyCharacter = (players[0].TargetedCard.BindingContext as CardViewModel).Character;

        // Normal attack
        if (players[0].TurnFaze == Player.TurnFazeEnum.SelectingPlayerCard)
            myCharacter.Attack(enemyCharacter);

        // Special attack
        else if (players[0].TurnFaze == Player.TurnFazeEnum.UsingSpecialAttack)
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

    //testowe
    private void LoadSpecialAttackButton_Clicked(object sender, EventArgs e)
    {
        switch (players[0].TurnFaze)
        {
            case Player.TurnFazeEnum.SelectingEnemyCard:
                players[0].TurnFaze = Player.TurnFazeEnum.UsingSpecialAttack;

                #region Animation
                if (players[0].ChosenCard != null)
                    (players[0].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromArgb("#BBFFFF00");
                #endregion

                (sender as Button).Text = "Load normal attack";
                break;
            case Player.TurnFazeEnum.UsingSpecialAttack:
                players[0].TurnFaze = Player.TurnFazeEnum.SelectingEnemyCard;

                #region Animation
                if (players[0].ChosenCard != null)
                    (players[0].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromArgb("#880000FF");
                #endregion

                (sender as Button).Text = "Load special attack";
                break;
        }
    }

    //testowe
    //private void TargetedCardsToSpecialAttack(Card card)
    //{
    //    if (players[0].TurnFaze is not Player.TurnFazeEnum.UsingSpecialAttack)
    //        return;

    //    players[0].TargetedCard = card;
    //    #region Animation
    //    if (players[0].TargetedCard != null)
    //        (players[0].TargetedCard.BindingContext as CardViewModel).Character.AuraBrush = Color.FromArgb("#88FF0000");
    //    #endregion

    //    CharacterBase myCharacter = (players[0].ChosenCard.BindingContext as CardViewModel).Character;
    //    CharacterBase enemyCharacter = (players[0].TargetedCard.BindingContext as CardViewModel).Character;

    //    #region other code

    //    List<CharacterBase> playerBoardCharacters = new();
    //    foreach (Card item in PlayerBoard.Cast<Card>())
    //    {
    //        playerBoardCharacters.Add((item.BindingContext as CardViewModel).Character);
    //    }
    //    List<CharacterBase> computerBoardCharacters = new();
    //    foreach (Card item in ComputerBoard.Cast<Card>())
    //    {
    //        computerBoardCharacters.Add((item.BindingContext as CardViewModel).Character);
    //    }

    //    // Special attack
    //    myCharacter.SpecialAttack(computerBoardCharacters.ToArray(), playerBoardCharacters.ToArray(), enemyCharacter);
    //    #endregion

    //    #region Animation
    //    if (players[0].ChosenCard != null)
    //        (players[0].ChosenCard.BindingContext as CardViewModel).Character.AuraBrush = Brush.Transparent;
    //    #endregion
    //    players[0].ChosenCard = null;

    //    #region Animation
    //    if (players[0].TargetedCard != null)
    //        (players[0].TargetedCard.BindingContext as CardViewModel).Character.AuraBrush = Brush.Transparent;
    //    #endregion
    //    players[0].TargetedCard = null;

    //    players[0].TurnFaze = Player.TurnFazeEnum.EnemyTure;

    //    ComputerTurn();

    //    PlayerCards.IsVisible = true;
    //}

    private void ComputerTurn()
    {
        if (ComputerBoard.Children.Count > 0)
        {
            players[1].ChosenCard = ComputerBoard[0] as Card;
        }
        else
        {
            if (players[1].DeckOfCards.Cards.Count < 1)
                goto END;

            players[1].ChosenCard = players[1].DeckOfCards.Cards[0];

            ComputerBoard.Children.Add(players[1].ChosenCard);
            players[1].ChosenCard.ToDestroy += RemoveCardFrom_ComputerBoard;

            players[1].ChosenCard.OnCardTaped += OnComputerCardClickedPlayerTargeted;
            //todo pauza timerem
        }

        if (PlayerBoard.Children.Count == 0)
            goto END;

        var myCharacter = (players[1].ChosenCard.BindingContext as CardViewModel).Character;
        var playerCharacter = ((players[1].TargetedCard = PlayerBoard.Children.First() as Card).BindingContext as CardViewModel).Character;

        myCharacter.Attack(playerCharacter);

    END:
        players[1].ChosenCard = null;
        players[1].TargetedCard = null;

        players[0].TurnFaze = Player.TurnFazeEnum.SelectingPlayerCard;
    }

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