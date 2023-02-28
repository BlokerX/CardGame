using CardGame.CardModels.Items;
using CardGame.ServiceObjects;

namespace CardGame.ViewModels
{
    internal class ItemCardViewModel : ICardViewModel
    {
        public ItemCardViewModel(ItemBase item)
        {
            _cardModel = item;
        }

        private readonly ItemBase _cardModel;

        public ICardModel CardModel
        {
            get => _cardModel;
        }

        public string ID
        {
            get => CardModel.ID.IntToThreeCharStringComparer();
        }

        public string Name
        {
            get => CardModel.Name;
        }

        //private readonly Image _exampleImage/* = new() { Source = "https://i.pinimg.com/280x280_RS/2e/51/23/2e51230e3d557acde4744f7848308da0.jpg" }*/;
        public Image ExampleImage
        {
            get => new() { Source = CardModel.ExampleImageSource };
        }

        public Brush BackgroundColor
        {
            get => CardModel.BackgroundColor;
        }

        public string NumberOfUses
        {
            get => (CardModel as ItemBase).NumberOfUses.IntToThreeCharStringComparer();
        }
    }
}
