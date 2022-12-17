using CardGame.Characters;
using CardGame.GameObjects;
using CardGame.ViewModels;

namespace CardGame.GameObjectsUI;

public partial class Board : ContentPage
{
    private Player player1;
    private Player computer;

    public Board()
    {
        InitializeComponent();
        player1 = new Player()
        {
            DeckOfCards = new()
            {
                Cards = GetCards()
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
                Cards = GetCards()
            }
        };
        computer.DeckOfCards.Cards.ForEach((card) =>
        {
            (card.BindingContext as CardViewModel).Character.OnHealthToZero += ComputerCardDipose;
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
        foreach (Card card in PlayerCards.Children)
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
        foreach (Card card in PlayerCards.Children)
        {
            card.OnCardTaped -= PlayerThrowCard;
        }
        foreach (Card card in player1.DeckOfCards.Cards)
        {
            card.OnCardTaped -= PlayerTurn;
        }
    }

    private static List<Card> GetCards()
    {
        // Skalowanie kart:
        double scale = 0.3;
        double x = -(1 - scale) * 505 / 2, y = -(1 - scale) * 829 / 2;
        return new List<Card>()
            {
                new(new FireDragon())
                {
                    Scale = scale,
                    Margin = new Thickness(x,y),
                },
                new(new Archer())
                {
                    Scale = scale,
                    Margin = new Thickness(x,y)
                },
                new(new SwordFighter())
                {
                    Scale = scale,
                    Margin = new Thickness(x,y)
                },
                new(new Zeus())
                {
                    Scale = scale,
                    Margin = new Thickness(x,y)
                },
                new(new Bandit())
                {
                    Scale = scale,
                    Margin = new Thickness(x,y)
                },
                new(new Fighter())
                {
                    Scale = scale,
                    Margin = new Thickness(x,y)
                },
                new(new FireDragon())
                {
                    Scale = scale,
                    Margin = new Thickness(x,y)
                },
            };
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


        foreach (Card card in ComputerBoard.Children)
        {
            card.OnCardTaped += OnComputerCardClickedPlayerTargeted;
        }

        player1.TargetedCardSelected += OnPlayer1TargetedCardSelected;
        PlayerCards.IsVisible = false;
    }

    private void OnComputerCardClickedPlayerTargeted(object card)
    {
        var c = card as Card;
        foreach (Card item in ComputerBoard.Children)
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

}