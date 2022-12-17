using CardGame.Characters;
using CardGame.ServiceObjects;

namespace CardGame.ViewModels
{
    public class CardViewModel : PropertyChangeObject
    {
        private readonly bool _isMagicCard = false;
        public bool IsMagicCard
        {
            get => _isMagicCard;
        }

        public CardViewModel(CharacterBase character)
        {
            _character = character;
            Character.HealthPointsChanged += (i) => OnPropertyChanged(nameof(HealthPoints));
            Character.AttackPointsChanged += (i) => OnPropertyChanged(nameof(AttackPoints));
            Character.ShieldPointsChanged += (i) => OnPropertyChanged(nameof(ShieldPoints));
        }

        public CardViewModel(MagicCharacter character) : this((CharacterBase)character)
        {
            _isMagicCard = true;
        }

        public readonly CharacterBase _character;

        public CharacterBase Character
        {
            get => _character;
        }

        public Brush BackgroundColor
        {
            get => Character.BakckgroundColor;
        }

        public Brush StrokeColor
        {
            get => Character.StrokeColor;
        }

        //private readonly Image _exampleImage/* = new() { Source = "https://i.pinimg.com/280x280_RS/2e/51/23/2e51230e3d557acde4744f7848308da0.jpg" }*/;
        public Image ExampleImage
        {
            get => new() { Source = Character.ExampleImageSource };
        }

        public string Name
        {
            get => Character.Name;
        }

        public string ID
        {
            get => IntToThreeCharStringComparer(Character.ID);
        }

        protected static string IntToThreeCharStringComparer(int value, int charPlacesCount = 3)
        {
            char[] c = new char[charPlacesCount];

            int d = 1;
            for (int i = charPlacesCount - 1; i >= 0; i--)
            {
                c[i] = char.Parse((value / d % 10).ToString());
                d *= 10;
            }
            return new string(c);
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
            get => IntToThreeCharStringComparer(Character.AttackPoints);
        }

        public string HealthPoints
        {
            get => IntToThreeCharStringComparer(Character.HealthPoints);
        }

        public string ShieldPoints
        {
            get => IntToThreeCharStringComparer(Character.ShieldPoints);
        }

    }
}
