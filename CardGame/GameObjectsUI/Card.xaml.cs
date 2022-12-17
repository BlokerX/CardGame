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

    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        e.Data.Properties.Add("Card", this);
    }

    private void DragGestureRecognizer_DropCompleted(object sender, DropCompletedEventArgs e)
    {

    }

    private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
    {

    }

    private void DropGestureRecognizer_DragOver(object sender, DragEventArgs e)
    {
        //var dgr = sender is DropGestureRecognizer;
        //var d1 = e.Data.Properties["Card"];
        //if (d1 is Card)
        //{
        //    e.AcceptedOperation = DataPackageOperation.None;
        //    var card = d1 as Card;
        //    if (card == this)
        //    {
        //        // Pokaż kartę w trybie pełnoekranowym
        //    }
        //    else if((card.BindingContext as CardViewModel).Character.CardOvner != (BindingContext as CardViewModel).Character.CardOvner)
        //    {
        //        // todo dodać opcję ataku na przeciwnika
        //    }
        //}
    }

    private void DropGestureRecognizer_DragLeave(object sender, DragEventArgs e)
    {

    }
}