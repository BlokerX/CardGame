using CardGame.GameObjectsUI;
using CardGame.ServiceObjects;
using CardGame.ViewModels;
using System.Diagnostics;

namespace CardGame.Characters
{
    public class CharacterBase : PropertyChangeObject
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
                OnPropertyChanged(nameof(AttackPoints));
                AttackPointsChanged?.Invoke(_attackPoints);
            }
        }
        public Action<int> AttackPointsChanged;

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
                OnPropertyChanged(nameof(HealthPoints));
                HealthPointsChanged?.Invoke(_healthPoints);
            }
        }
        public Action<int> HealthPointsChanged;

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
                OnPropertyChanged(nameof(ShieldPoints));
                ShieldPointsChanged?.Invoke(_shieldPoints);
            }
        }
        public Action<int> ShieldPointsChanged;

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

        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        private Brush _strokeColor;

        public Brush StrokeColor
        {
            get => _strokeColor;
            set
            {
                _strokeColor = value;
                OnPropertyChanged(nameof(StrokeColor));
            }
        }

        private Brush _auraBrush;

        public Brush AuraBrush
        {
            get => _auraBrush;
            set
            {
                _auraBrush = value;
                OnPropertyChanged(nameof(AuraBrush));
            }
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

        public virtual void Attack(CharacterBase selectedCharacter)
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

            HealthPoints = HealthPoints - damage > 0 ? HealthPoints - damage : 0;

            if (HealthPoints <= 0)
                OnHealthToZero?.Invoke(CardOvner);

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

            AttackPoints = AttackPoints - attackPoints > 0 ? AttackPoints - attackPoints : 0;
        }

        public void ReinforceShield(int shieldPoints)
        {
            if (shieldPoints <= 0)
                return;

            ShieldPoints += shieldPoints;
        }

        public void BreakShield()
        {
            if (ShieldPoints != 0) ShieldPoints = 0;
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

            // Śmierć karty:
            OnHealthToZero += DestroyCard;

        }

        private static void DestroyCard(Card card)
        {
            card.Destroy();
        }

        ~CharacterBase()
        {
#if DEBUG
            Debug.WriteLine("Character has been destroyed.");
            // todo destroy it doesn't works
#endif
        }

        public static CharacterBase GetCharacterTypesById(int id)
        {
            return id switch
            {
                1 => new Fighter(),
                2 => new Archer(),
                3 => new Wizard(),
                4 => new SwordFighter(),
                5 => new Zeus(),
                6 => new FireDragon(),
                7 => new Bandit(),
                8 => new Witch(),
                9 => new RoyalSoldier(),
                10 => new Prince(),
                11 => new Knight(),
                _ => null,
            };
        }

    }
}
