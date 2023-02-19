using CardGame.CardModels.Items;
using System.Diagnostics;

namespace CardGame.GameObjectsUI;

public partial class ItemCard : CardBase
{
    private ItemCard()
    {
        InitializeComponent();
    }

    ~ItemCard()
    {
#if DEBUG
        // todo destroy it doesnt works
        Debug.WriteLine("Card has been destroyed successfull!");
#endif
    }

    public ItemCard(ItemBase character) : this()
    {
        //BindingContext = new CharacterCardViewModel(character);
        //(BindingContext as CharacterCardViewModel).Item.CardOvner = this;
    }

    private void ContentView_SizeChanged(object sender, EventArgs e)
    {
        if (this.Height / 2.5 != this.Width)
            this.SizeAllocated(this.Height / 2.5, this.Height);
        //ImgBorder.StrokeShape = new RoundRectangle() { CornerRadius = 10 };
    }


    private void TapGestureRecognizer_Tapped(object sender, EventArgs e) => OnCardTaped?.Invoke(this);

}