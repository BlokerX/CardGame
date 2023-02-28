using CardGame.CardModels.Items;
using CardGame.ViewModels;
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

    public ItemCard(ItemBase item) : this()
    {
        BindingContext = new ItemCardViewModel(item);
        ((BindingContext as ItemCardViewModel).CardModel as ItemBase).CardOvner = this;
    }

}