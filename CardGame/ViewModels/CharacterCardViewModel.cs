using CardGame.CardModels.Characters;
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
            _character = character;
            Character.HealthPointsChanged += (i) => OnPropertyChanged(nameof(HealthPoints));
            Character.AttackPointsChanged += (i) => OnPropertyChanged(nameof(AttackPoints));
            Character.ShieldPointsChanged += (i) => OnPropertyChanged(nameof(ShieldPoints));
        }

        public CharacterCardViewModel(MagicCharacter character) : this((CharacterBase)character)
        {
            _isMagicCard = true;
        }

        public readonly CharacterBase _character;

        public CharacterBase Character
        {
            get => _character;
        }

        public string ID
        {
            get => Character.ID.IntToThreeCharStringComparer();
        }

        public string Name
        {
            get => Character.Name;
        }

        //private readonly Image _exampleImage/* = new() { Source = "https://i.pinimg.com/280x280_RS/2e/51/23/2e51230e3d557acde4744f7848308da0.jpg" }*/;
        public Image ExampleImage
        {
            get => new() { Source = Character.ExampleImageSource };
        }

        public Brush BackgroundColor
        {
            get => Character.BackgroundColor;
        }

        public string ShortDescribe
        {
            get => Character.ShortDescribe;
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
            get => Character.IsMagicResistant;
        }

        public string AttackPoints
        {
            get => Character.AttackPoints.IntToThreeCharStringComparer();
        }

        public string HealthPoints
        {
            get => Character.HealthPoints.IntToThreeCharStringComparer();
        }

        public string ShieldPoints
        {
            get => Character.ShieldPoints.IntToThreeCharStringComparer();
        }

    }
}
