using CardGame.CardModels.Characters;
using CardGame.GameObjectsUI;
using CardGame.ServiceObjects;

namespace CardGame.ViewModels
{
    internal class CharacterCardViewModel : PropertyChangeObject, ICardViewModel
    {
        private readonly bool _isMagicCard = false;
        public bool IsMagicCard
        {
            get => _isMagicCard;
        }

        public CharacterCardViewModel(CharacterBase character)
        {
            _cardModel = character;
            (CardModel as CharacterBase).HealthPointsChanged += (i) => OnPropertyChanged(nameof(HealthPoints));
            (CardModel as CharacterBase).AttackPointsChanged += (i) => OnPropertyChanged(nameof(AttackPoints));
            (CardModel as CharacterBase).ShieldPointsChanged += (i) => OnPropertyChanged(nameof(ShieldPoints));
        }

        public CharacterCardViewModel(MagicCharacter character) : this((CharacterBase)character)
        {
            _isMagicCard = true;
        }

        private readonly CharacterBase _cardModel;

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

        public string ShortDescribe
        {
            get => CardModel.ShortDescribe;
        }

        private readonly string _speciesImage;
        public string SpeciesImage
        {
            get => _speciesImage;
        }

        private readonly string _typeImage;
        public string TypeImage
        {
            get => _typeImage;
        }

        public bool IsMagicResistant
        {
            get => (CardModel as CharacterBase).IsMagicResistant;
        }

        public string AttackPoints
        {
            get => (CardModel as CharacterBase).AttackPoints.IntToThreeCharStringComparer();
        }

        public string HealthPoints
        {
            get => (CardModel as CharacterBase).HealthPoints.IntToThreeCharStringComparer();
        }

        public string ShieldPoints
        {
            get => (CardModel as CharacterBase).ShieldPoints.IntToThreeCharStringComparer();
        }
    }
}
