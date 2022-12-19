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
    }

    // Player's card lobby:
    private void RemoveCardFrom_PlayerCards(Card card)
    {
        if (PlayerCards.Children.Contains(card))
            PlayerCards.Children.Remove(card);
    }

    // Player's board:
    private void RemoveCardFrom_PlayerBoard(Card card)
    {
        if (PlayerBoard.Children.Contains(card))
            PlayerBoard.Children.Remove(card);
    }

    // - - - - - //

    // All computer cards:
    private void RemoveCardFrom_Computer(Card card)
    {
        if (players[1].DeckOfCards.Cards.Contains(card))
            players[1].DeckOfCards.Cards.Remove(card);
    }

    // Computer's board:
    private void RemoveCardFrom_ComputerBoard(Card card)
    {
        if (ComputerBoard.Children.Contains(card))
            ComputerBoard.Children.Remove(card);
    }

    // ----------------------------- //

    private void DestroyCard(Card card)
    {
        card.Destroy();
    }

    #endregion

    private void AddClickEventToSelect_PlayerCards()
    {
        foreach (Card card in PlayerCards.Children.Cast<Card>())
        {
            card.OnCardTaped += ThrowCard_Player;
        }
        foreach (Card card in players[0].DeckOfCards.Cards)
        {
            card.OnCardTaped += PlayerTurn;
        }
    }

    private void RemoveClickEventToSelect_PlayerCards()
    {
        foreach (Card card in PlayerCards.Children.Cast<Card>())
        {
            card.OnCardTaped -= ThrowCard_Player;
        }
        foreach (Card card in players[0].DeckOfCards.Cards)
        {
            card.OnCardTaped -= PlayerTurn;
        }
    }

    #region Game logic

    private void ThrowCard_Player(object sender)
    {
        players[0].ChosenCard = sender as Card;
        players[0].ChosenCard.OnCardTaped -= ThrowCard_Player;

        RemoveCardFrom_PlayerCards(players[0].ChosenCard);
        players[0].ChosenCard.ToDestroy -= RemoveCardFrom_PlayerCards;

        PlayerBoard.Children.Add(players[0].ChosenCard);
        players[0].ChosenCard.ToDestroy += RemoveCardFrom_PlayerBoard;
    }

    // for all player cards
    private void PlayerTurn(object sender)
    {
        RemoveClickEventToSelect_PlayerCards();

        players[0].ChosenCard = sender as Card;

        if (ComputerBoard.Children.Count == 0)
        {
            if (players[1].DeckOfCards.Cards.Count != 0)
            {
                ComputerTurn();
            }
            return;
        }

        foreach (Card card in ComputerBoard.Children.Cast<Card>())
        {
            card.OnCardTaped += OnComputerCardClickedPlayerTargeted;
        }

        players[0].TargetedCardSelected += OnPlayerTargetedCardSelected;

        PlayerCards.IsVisible = false;
    }

    private void OnComputerCardClickedPlayerTargeted(object card)
    {
        var c = card as Card;
        foreach (Card item in ComputerBoard.Children.Cast<Card>())
        {
            item.OnCardTaped -= OnComputerCardClickedPlayerTargeted;
        }
        players[0].TargetedCard = c;
        players[0].TargetedCardSelected();
    }

    private void OnPlayerTargetedCardSelected()
    {
        players[0].TargetedCardSelected -= OnPlayerTargetedCardSelected;

        CharacterBase myCharacter = (players[0].ChosenCard.BindingContext as CardViewModel).Character;
        CharacterBase enemyCharacter = (players[0].TargetedCard.BindingContext as CardViewModel).Character;

        myCharacter.Attack(enemyCharacter);

        players[0].ChosenCard = null;
        players[0].TargetedCard = null;

        ComputerTurn();

        AddClickEventToSelect_PlayerCards();
        PlayerCards.IsVisible = true;
    }

    private void ComputerTurn()
    {
        if (ComputerBoard.Children.Count > 0)
        {
            players[1].ChosenCard = ComputerBoard[0] as Card;
        }
        else
        {
            if (players[1].DeckOfCards.Cards.Count < 1)
                return;

            players[1].ChosenCard = players[1].DeckOfCards.Cards[0];

            ComputerBoard.Children.Add(players[1].ChosenCard);
            players[1].ChosenCard.ToDestroy += RemoveCardFrom_ComputerBoard;
        }

        if (PlayerBoard.Children.Count == 0)
            return;

        var myCharacter = (players[1].ChosenCard.BindingContext as CardViewModel).Character;
        var playerCharacter = ((players[1].TargetedCard = PlayerBoard.Children.First() as Card).BindingContext as CardViewModel).Character;

        myCharacter.Attack(playerCharacter);
        players[1].ChosenCard = null;
        players[1].TargetedCard = null;
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