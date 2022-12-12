using CardGame.Characters;
using CardGame.GameObjectsUI;

namespace CardGame;

public partial class MainPage : ContentPage
{
    //int count = 0;

    public MainPage()
    {
        InitializeComponent();
        GContent.Add(new Card(new Wizard())
        {
            Scale = 0.5
        });
        Content.Add(new Card(new Zeus())
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        });
        Content.Add(new Card(new Knight())
        {
            HeightRequest = 829,
            WidthRequest = 505
        });
        Content.Add(new Card(new Archer())
        {
            HeightRequest = 829/2,
            WidthRequest = 505/2
        });
        Content.Add(new Card(new RoyalSoldier())
        {
            HeightRequest = 829,
            WidthRequest = 505,
            Scale = 0.8
        });
        Content.Add(new Card(new SwordFighter())
        {
            HeightRequest = 829,
            WidthRequest = 505,
            Scale = 0.25
        });
        Content.Add(new Card(new Witch()));
        Content.Add(new Card(new Bandit())
        {
            HeightRequest = 829,
            WidthRequest = 505,
            Scale = 0.8
        });
        
        Content.Add(new Card(new Bandit())
        {
            HeightRequest = 829,
            WidthRequest = 505,
            Scale = 1.5
        });
    }

    //private void OnCounterClicked(object sender, EventArgs e)
    //{
    //	count++;

    //	if (count == 1)
    //		CounterBtn.Text = $"Clicked {count} time";
    //	else
    //		CounterBtn.Text = $"Clicked {count} times";

    //	SemanticScreenReader.Announce(CounterBtn.Text);
    //}
}

