using CardGame.Characters;
using CardGame.GameObjects;
using CardGame.ViewModels;

namespace CardGame.GameObjectsUI;

public partial class Board : ContentPage
{
    private readonly Player player1;
    private readonly Player computer;
    private readonly int cardPerPerson = 32;

    public Board()
    {
        InitializeComponent();
        player1 = new Player()
        {
            DeckOfCards = new()
            {
                Cards = Board.GetCards(cardPerPerson)
            }
        };
        player1.DeckOfCards.Cards.ForEach((card) =>
        {
            (card.BindingContext as CardViewModel).Character.OnHealthToZero += Player1CardDipose;
        });

        computer = new Player()
        {
            DeckOfCards = new()
            {
                Cards = Board.GetCards(cardPerPerson)
            }
        };
        computer.DeckOfCards.Cards.ForEach((card) =>
            {
                (card.BindingContext as CardViewModel).Character.OnHealthToZero += ComputerCardDipose;

                //foreach (var item in card.GestureRecognizers)
                //{
                //    if (item is DragGestureRecognizer)
                //    {
                //        (item as DragGestureRecognizer).CanDrag = false;
                //        break;
                //    }
                //}
            });

        // Show player's cards in lobby panel:
        foreach (Card card in player1.DeckOfCards.Cards)
        {
            PlayerCards.Children.Add(card);
        }

        AddClickEventToSelectForPlayer1Cards();

        ComputerTurn();

    }

    private void Player1CardDipose(Card card)
    {
        // naprawić zwalnianie objektu z pamięci
        if (player1.DeckOfCards.Cards.Contains(card))
        {
            player1.DeckOfCards.Cards.Remove(card);
        }
        if (PlayerCards.Children.Contains(card))
        {
            PlayerCards.Children.Remove(card);
        }
        if (PlayerBoard.Children.Contains(card))
        {
            PlayerBoard.Children.Remove(card);
        }
    }

    private void ComputerCardDipose(Card card)
    {
        // naprawiæ zwalnianie objektu z pamiêci
        if (computer.DeckOfCards.Cards.Contains(card))
        {
            computer.DeckOfCards.Cards.Remove(card);
        }
        if (ComputerBoard.Children.Contains(card))
        {
            ComputerBoard.Children.Remove(card);
        }
    }

    private void AddClickEventToSelectForPlayer1Cards()
    {
        foreach (Card card in PlayerCards.Children.Cast<Card>())
        {
            card.OnCardTaped += PlayerThrowCard;
        }
        foreach (Card card in player1.DeckOfCards.Cards)
        {
            card.OnCardTaped += PlayerTurn;
        }
    }

    private void RemoveClickEventToSelectForPlayer1Cards()
    {
        foreach (Card card in PlayerCards.Children.Cast<Card>())
        {
            card.OnCardTaped -= PlayerThrowCard;
        }
        foreach (Card card in player1.DeckOfCards.Cards)
        {
            card.OnCardTaped -= PlayerTurn;
        }
    }

    private static List<Card> GetCards(int count)
    {
        // Skalowanie kart:
        double scale = 0.3;
        double x = -(1 - scale) * 505 / 2, y = -(1 - scale) * 829 / 2;

        List<Card> cards = new();

        for (int i = 0; i < count; i++)
        {
            var c = Board.GetRandomCard();
            c.Scale = scale;
            c.Margin = new Thickness(x, y);
            cards.Add(c);
        }

        return cards;

        //return new List<Card>()
        //    {
        //        new(new FireDragon())
        //        {
        //            Scale = scale,
        //            Margin = new Thickness(x,y),
        //        },
        //        new(new Archer())
        //        {
        //            Scale = scale,
        //            Margin = new Thickness(x,y)
        //        },
        //        new(new SwordFighter())
        //        {
        //            Scale = scale,
        //            Margin = new Thickness(x,y)
        //        },
        //        new(new Zeus())
        //        {
        //            Scale = scale,
        //            Margin = new Thickness(x,y)
        //        },
        //        new(new Bandit())
        //        {
        //            Scale = scale,
        //            Margin = new Thickness(x,y)
        //        },
        //        new(new Fighter())
        //        {
        //            Scale = scale,
        //            Margin = new Thickness(x,y)
        //        },
        //        new(new FireDragon())
        //        {
        //            Scale = scale,
        //            Margin = new Thickness(x,y)
        //        },
        //    };
    }

    private void PlayerThrowCard(object sender)
    {
        player1.ChosenCard = sender as Card;
        player1.ChosenCard.OnCardTaped -= PlayerThrowCard;
        (player1.ChosenCard.Parent as HorizontalStackLayout).Remove(player1.ChosenCard);
        PlayerBoard.Children.Add(player1.ChosenCard);
    }

    // for all player cards
    private void PlayerTurn(object sender)
    {
        RemoveClickEventToSelectForPlayer1Cards();
        player1.ChosenCard = sender as Card;

        if (ComputerBoard.Children.Count == 0)
        {
            if (computer.DeckOfCards.Cards.Count != 0)
            {
                ComputerTurn();
            }
            return;
        }

        foreach (Card card in ComputerBoard.Children.Cast<Card>())
        {
            card.OnCardTaped += OnComputerCardClickedPlayerTargeted;
        }

        player1.TargetedCardSelected += OnPlayer1TargetedCardSelected;
        PlayerCards.IsVisible = false;
    }

    private void OnComputerCardClickedPlayerTargeted(object card)
    {
        var c = card as Card;
        foreach (Card item in ComputerBoard.Children.Cast<Card>())
        {
            item.OnCardTaped -= OnComputerCardClickedPlayerTargeted;
        }
        player1.TargetedCard = c;
        player1.TargetedCardSelected();
    }

    private void OnPlayer1TargetedCardSelected()
    {
        player1.TargetedCardSelected -= OnPlayer1TargetedCardSelected;
        var myCharacter = (player1.ChosenCard.BindingContext as CardViewModel).Character;
        CharacterBase enemyCharacter = (player1.TargetedCard.BindingContext as CardViewModel).Character;

        myCharacter.Attack(enemyCharacter);
        player1.ChosenCard = null;
        player1.TargetedCard = null;

        ComputerTurn();

        AddClickEventToSelectForPlayer1Cards();
        PlayerCards.IsVisible = true;
    }

    private void ComputerTurn()
    {
        Card card;
        if (ComputerBoard.Children.Count > 0)
        {
            card = computer.ChosenCard = (ComputerBoard[0] as Card);
        }
        else
        {
            if (0 >= computer.DeckOfCards.Cards.Count)
                return;
            card = computer.ChosenCard = computer.DeckOfCards.Cards[0];
            ComputerBoard.Children.Add(card);
        }

        if (PlayerBoard.Children.Count == 0)
            return;

        var myCharacter = (card.BindingContext as CardViewModel).Character;
        var playerCharacter = ((computer.TargetedCard = PlayerBoard.Children.First() as Card).BindingContext as CardViewModel).Character;

        myCharacter.Attack(playerCharacter);
        computer.ChosenCard = null;
        computer.TargetedCard = null;
    }

    // ---------------------------------------------- //

    private static Card GetRandomCard()
    {
        return new Card(GetCharacterTypesById(new Random().Next(1, 11)));
    }

    private static CharacterBase GetCharacterTypesById(int id)
    {
        return id switch
        {
            1 => new Fighter(),
            2 => new Archer(),
            3 => new Wizard(),
            4 => new SwordFighter(),
            5 => new Zeus(),
            6 => new FireDragon(),
            7 => new Bandit(),
            8 => new Witch(),
            9 => new RoyalSoldier(),
            10 => new Prince(),
            11 => new Knight(),
            _ => null,
        };
        //return CharacterTypesByID[id];
        //todo trzeba twożyć nowe instancje zamiast używania gotowych ze słownika
    }

    //private readonly Dictionary<int, CharacterBase> CharacterTypesByID = new()
    //{
    //    { 1, new Fighter() },
    //    { 2, new Archer() },
    //    { 3, new Wizard() },
    //    { 4, new SwordFighter() },
    //    { 5, new Zeus() },
    //    { 6, new FireDragon() },
    //    { 7, new Bandit() },
    //    { 8, new Witch() },
    //    { 9, new RoyalSoldier() },
    //    { 10, new Prince() },
    //    { 11, new Knight() }
    //};

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