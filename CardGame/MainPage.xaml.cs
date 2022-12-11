using CardGame.Characters;
using CardGame.GameObjectsUI;

namespace CardGame;

public partial class MainPage : ContentPage
{
    //int count = 0;

    public MainPage()
    {
        InitializeComponent();
        Content.Add(new Card(new Wizard())
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Margin = 20,
            Scale = 0.8
        });
        Content.Add(new Card(new Zeus())
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Margin = 20,
            Scale = 0.8
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

