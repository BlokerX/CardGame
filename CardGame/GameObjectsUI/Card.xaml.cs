using CardGame.Characters;
using CardGame.ViewModels;

namespace CardGame.GameObjectsUI;

public partial class Card : ContentView
{
    private Card()
    {
        InitializeComponent();

        // Click action implement:
        this.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command((o) => OnSomeButtonClicked(this))
        });
        OnSomeButtonClicked += (a) => { };
    }

    public Card(MagicCharacter character) : this()
    {
        BindingContext = new MagicCardViewModel(character);
        (BindingContext as MagicCardViewModel).Character.CardOvner = this;
    }

    public Card(CharacterBase character) : this()
    {
        BindingContext = new CardViewModel(character);
        (BindingContext as CardViewModel).Character.CardOvner = this;
    }

    private void ContentView_SizeChanged(object sender, EventArgs e)
    {
        if (this.Height / 2.5 != this.Width)
            this.SizeAllocated(this.Height / 2.5, this.Height);
        //ImgBorder.StrokeShape = new RoundRectangle() { CornerRadius = 10 };
    }

    #region click event

    public delegate void OnSomeButtonClickedDelegate(object sender);

    public OnSomeButtonClickedDelegate OnSomeButtonClicked { get; set; }

    // ================================================================= //

    #endregion

}