using CardGame.Characters;
using CardGame.ViewModels;
using System.Diagnostics;

namespace CardGame.GameObjectsUI;

public partial class Card : ContentView
{
    private Card()
    {
        InitializeComponent();
    }

    ~Card()
    {
#if DEBUG
        Debug.WriteLine("Card has been destroyed successfull!");
#endif
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

    public OnSomeButtonClickedDelegate OnCardTaped { get; set; }

    // ================================================================= //

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e) => OnCardTaped?.Invoke(this);

    #endregion

    #region Drag&Drop

    //private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    //{
    //    e.Data.Properties.Add("Card", this);
    //}

    #endregion
}