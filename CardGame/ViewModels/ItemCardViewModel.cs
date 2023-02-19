using CardGame.CardModels.Items;
using CardGame.ServiceObjects;

namespace CardGame.ViewModels
{
    public class ItemCardViewModel : ICardViewModel
    {
        public readonly ItemBase _item;

        public ItemBase Item
        {
            get => _item;
        }

        public string ID
        {
            get => Item.ID.IntToThreeCharStringComparer();
        }

        public string Name
        {
            get => Item.Name;
        }

        //private readonly Image _exampleImage/* = new() { Source = "https://i.pinimg.com/280x280_RS/2e/51/23/2e51230e3d557acde4744f7848308da0.jpg" }*/;
        public Image ExampleImage
        {
            get => new() { Source = Item.ExampleImageSource };
        }

        public Brush BackgroundColor
        {
            get => Item.BackgroundColor;
        }
    }
}
