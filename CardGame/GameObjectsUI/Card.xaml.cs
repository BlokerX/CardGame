using CardGame.Characters;
using CardGame.ViewModels;
using Microsoft.Maui.Controls.Shapes;

namespace CardGame.GameObjectsUI;

public partial class Card : ContentView
{
    private Card()
    {
        InitializeComponent();
    }

    public Card(MagicCharacter character) : this()
    {
        BindingContext = new MagicCardViewModel(character);
    }

    public Card(CharacterBase character) : this()
    {
        BindingContext = new CardViewModel(character);
    }

    private void ContentView_SizeChanged(object sender, EventArgs e)
    {
        if (this.Height / 2.5 != this.Width)
            this.SizeAllocated(this.Height / 2.5, this.Height);
        //ImgBorder.StrokeShape = new RoundRectangle() { CornerRadius = 10 };
    }
}