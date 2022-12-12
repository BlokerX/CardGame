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
            Cards = GetCards()
        };

        computer = new Player()
        {
            Cards = GetCards()
        };

        foreach (Card card in player1.Cards)
        {
            PlayerCards.Children.Add(card);
            card.OnSomeButtonClicked += PlayerTurn;
        }

        ComputerTurn();

    }

    private static List<Card> GetCards()
    {
        return new List<Card>()
            {
                new(new FireDragon())
                {
                    HeightRequest = 829,
                    WidthRequest = 505,
                    Scale = 0.2,
                    Margin = new Thickness(-200,-50),
                },
                new(new Archer())
                {
                    HeightRequest = 829,
                    WidthRequest = 505,
                    Scale = 0.2,
                    Margin = new Thickness(-200,-50)
                },
                new(new SwordFighter())
                {
                    HeightRequest = 829,
                    WidthRequest = 505,
                    Scale = 0.2,
                    Margin = new Thickness(-200,-50)
                },
                new(new Zeus())
                {
                    HeightRequest = 829,
                    WidthRequest = 505,
                    Scale = 0.2,
                    Margin = new Thickness(-200,-50)
                },
                new(new Bandit())
                {
                    HeightRequest = 829,
                    WidthRequest = 505,
                    Scale = 0.2,
                    Margin = new Thickness(-200,-50)
                },
                new(new Fighter())
                {
                    HeightRequest = 829,
                    WidthRequest = 505,
                    Scale = 0.2,
                    Margin = new Thickness(-200,-50)
                },
                new(new FireDragon())
                {
                    HeightRequest = 829,
                    WidthRequest = 505,
                    Scale = 0.2,
                    Margin = new Thickness(-200,-50)
                },
            };
    }

    private void PlayerTurn(object sender)
    {
        player1.ChosenCard = sender as Card;
        player1.ChosenCard.OnSomeButtonClicked -= PlayerTurn;
        (player1.ChosenCard.Parent as HorizontalStackLayout).Remove(player1.ChosenCard);
        PlayerBoard.Children.Add(player1.ChosenCard);

        if (ComputerBoard.Children.Count == 0)
        {
            if(computer.Cards.Count != 0) 
            {
                ComputerTurn();
            }
            return;
        }


        foreach (Card card in ComputerBoard.Children)
        {
            card.OnSomeButtonClicked += OnComputerCardClickedPlayerTargeted;
        }

        player1.TargetedCardSelected += OnPlayer1TargetedCardSelected;
        PlayerCards.IsVisible = false;
    }

    private void OnComputerCardClickedPlayerTargeted(object card)
    {
        var c = card as Card;
        foreach (Card item in ComputerBoard.Children)
        {
            item.OnSomeButtonClicked -= OnComputerCardClickedPlayerTargeted;
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

        Thread.Sleep(2000);

        ComputerTurn();
        PlayerCards.IsVisible = true;
    }

    int ComputerCardIndex = 0;
    private void ComputerTurn()
    {
        if (ComputerCardIndex >= computer.Cards.Count)
            return;
        var card = computer.Cards[ComputerCardIndex++];
        ComputerBoard.Children.Add(card);

        if (PlayerBoard.Children.Count == 0)
            return;

        var myCharacter = (card.BindingContext as CardViewModel).Character;
        var playerCharacter = ((PlayerBoard.Children.First() as Card).BindingContext as CardViewModel).Character;

        myCharacter.Attack(playerCharacter);

    }

}