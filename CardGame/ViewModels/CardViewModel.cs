using CardGame.Characters;
using Color = Microsoft.Maui.Graphics.Color;

namespace CardGame.ViewModels
{
    public class CardViewModel
    {
        private readonly bool _isMagicCard = false;
        public bool IsMagicCard
        {
            get => _isMagicCard;
        }

        public CardViewModel(CharacterBase character)
        {
            _isMagicCard = false;
            Character = character;
        }

        protected CardViewModel(MagicCharacter character)
        {
            _isMagicCard = true;
            Character = character;
        }

        public readonly CharacterBase Character;

        private readonly Brush _backgroundColor = Color.Parse("orange");
        public Brush BackgroundColor
        {
            get => _backgroundColor;
        }

        private readonly Brush _strokeColor = Color.Parse("white");
        public Brush StrokeColor
        {
            get => _strokeColor;
        }

        private readonly Image _exampleImage = new() { Source = "D:\\Moje dokumenty\\Obrazy\\Animacje\\AE\\Snapinsta.app_1080_318581844_198805902630374_2066124822979055544_n.jpg" };
        public Image ExampleImage
        {
            get => _exampleImage;
        }

        public string Name
        {
            get => Character.Name;
        }

        public string ID
        {
            get
            {
                string s = Character.ID.ToString();
                char[] c = new char[3] { '0', '0', '0' };
                for (int i = 0; i < 3; i++)
                {
                    if (s.Length < i + 1)
                        continue;
                    c[i] = s[i];
                }
                s = string.Empty;
                for (int i = 2; i >= 0; i--)
                {
                    s += c[i];
                }
                return s;
            }
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

        public int AttackPoints
        {
            get => Character.AttackPoints;
        }

        public int HealthPoints
        {
            get => Character.HealthPoints;
        }

        public int ShieldPoints
        {
            get => Character.ShieldPoints;
        }

    }
}
