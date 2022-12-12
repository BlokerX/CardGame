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
        var card = sender as Card;
        card.OnSomeButtonClicked -= PlayerTurn;
        (card.Parent as HorizontalStackLayout).Remove(card);
        PlayerBoard.Children.Add(card);

        if (computer.Cards.Count == 0)
            return;

        var myCharacter = (card.BindingContext as CardViewModel).Character;
        CharacterBase computerCharacter = (computer.Cards.First().BindingContext as CardViewModel).Character;

        myCharacter.Attack(computerCharacter);

        Thread.Sleep(2000);

        ComputerTurn();
    }

    int ComputerCardIndex = 0;
    private void ComputerTurn()
    {
        if (ComputerCardIndex >= computer.Cards.Count)
            return;
        var card = computer.Cards[ComputerCardIndex++];
        ComputerBoard.Children.Add(card);

        if (player1.Cards.Count == 0)
            return;

        var myCharacter = (card.BindingContext as CardViewModel).Character;
        var playerCharacter = (player1.Cards.First().BindingContext as CardViewModel).Character;

        myCharacter.Attack(playerCharacter);

    }

}