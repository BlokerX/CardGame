using CardGame.GameObjectsUI;
using System.ComponentModel;
using System.Diagnostics;

namespace CardGame.Characters
{
    public class CharacterBase : INotifyPropertyChanged
    {
        protected string _name;
        /// <summary>
        /// Name of character.
        /// </summary>
        public string Name
        {
            get => _name;
        }

        protected readonly int _iD;
        /// <summary>
        /// ID of character.
        /// </summary>
        public int ID
        {
            get => _iD;
        }

        protected readonly string _describe;
        /// <summary>
        /// Character describe.
        /// </summary>
        public string Describe
        {
            get => _describe;
        }

        protected readonly string _shortDescribe;
        /// <summary>
        /// Character short describe.
        /// </summary>
        public string ShortDescribe
        {
            get => _shortDescribe;
        }

        protected readonly SpeciesTypes _species;
        /// <summary>
        /// Species of character.
        /// </summary>
        public SpeciesTypes Species
        {
            get => _species;
        }

        protected readonly CharacterTypeEnum _characterType;
        /// <summary>
        /// Type of character.
        /// </summary>
        public CharacterTypeEnum CharacterType
        {
            get => _characterType;
        }

        protected int _attackPoints;
        /// <summary>
        /// Character's attack points.
        /// </summary>
        public int AttackPoints
        {
            get => _attackPoints;
            private set
            {
                _attackPoints = value;
                OnPropertyChanged("AttackPoints");
            }
        }

        protected int _healthPoints;
        /// <summary>
        /// Character's health points.
        /// </summary>
        public int HealthPoints
        {
            get => _healthPoints;
            private set
            {
                _healthPoints = value;
                OnPropertyChanged("HealthPoints");
            }
        }

        protected int _shieldPoints;
        /// <summary>
        /// Character's shield points.
        /// </summary>
        public int ShieldPoints
        {
            get => _shieldPoints;
            private set
            {
                _shieldPoints = value;
                OnPropertyChanged("ShieldPoints");
            }
        }

        protected readonly bool _isMagicResistant;
        /// <summary>
        /// Return true if character is magic resistant.
        /// </summary>
        public bool IsMagicResistant
        {
            get => _isMagicResistant;
        }

        protected readonly string _exampleImageSource;

        /// <summary>
        /// 
        /// </summary>
        public string ExampleImageSource
        {
            get => _exampleImageSource;
        }

        private Brush _backgroundColor;

        public Brush BakckgroundColor
        {
            get => _backgroundColor;
            set => _backgroundColor = value;
        }

        private Brush _strokeColor;

        public Brush StrokeColor
        {
            get => _strokeColor;
            set => _strokeColor = value;
        }

        public enum SpeciesTypes
        {
            Human,
            Dragon,
            Beast,
            God
        }

        public enum CharacterTypeEnum
        {
            Normal,
            Melee,
            Distance,
            Magic,
            Tank,
            God
        }

        public void Attack(CharacterBase selectedCharacter)
        {
            selectedCharacter.GetDamaged(AttackPoints);
        }

        public virtual void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter) { }

        public void GetDamaged(int damage)
        {
            if (damage <= 0)
                return;

            // todo get damages for shield
            else if (ShieldPoints >= damage)
            {
                ShieldPoints -= damage;
            }
            else if (ShieldPoints < damage)
            {
                damage -= ShieldPoints;
                ShieldPoints = 0;
                GetPearcingDamaged(damage);
            }
        }

        public void GetPearcingDamaged(int damage)
        {
            if (damage <= 0)
                return;

            HealthPoints -= damage;
            if (HealthPoints <= 0)
            {
                HealthPoints = 0;
                OnHealthToZero?.Invoke(CardOvner);
            }

        }

        public void Heal(int healthPoints)
        {
            if (healthPoints <= 0)
                return;

            HealthPoints += healthPoints;
        }

        public void BoostAttack(int attackPoints)
        {
            if (attackPoints <= 0)
                return;

            AttackPoints += attackPoints;
        }

        public void DebuffAttack(int attackPoints)
        {
            if (attackPoints <= 0)
                return;

            AttackPoints -= attackPoints;
        }

        public void ReinforceShield(int shieldPoints)
        {
            if (shieldPoints <= 0)
                return;

            ShieldPoints += shieldPoints;
        }

        public void BreakShield()
        {
            ShieldPoints = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Card CardOvner;
        public Action<Card> OnHealthToZero;

        public CharacterBase(string name, int iD, string describe, string shortDescribe, SpeciesTypes species, CharacterTypeEnum characterType, int attackPoints, int healthPoints, int shieldPoints, bool isMagicResistant, string exampleImageSource, Brush backgroundColor = null, Brush strokeColor = null)
        {
            _name = name;
            _iD = iD;
            _describe = describe;
            _shortDescribe = shortDescribe;
            _species = species;
            _characterType = characterType;
            _attackPoints = attackPoints;
            _healthPoints = healthPoints;
            _shieldPoints = shieldPoints;
            _isMagicResistant = isMagicResistant;
            _exampleImageSource = exampleImageSource;
            if (backgroundColor == null)
                _backgroundColor = Brush.Orange;
            else _backgroundColor = backgroundColor;
            if (strokeColor == null)
                _strokeColor = Brush.White;
            else _strokeColor = strokeColor;
        }

        ~CharacterBase()
        {
#if DEBUG
            Debug.WriteLine("Character has been destroyed.");
#endif
        }

    }
}
