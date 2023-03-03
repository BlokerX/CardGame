using CardGame.GameObjectsUI;
using CardGame.ServiceObjects;
using System.Diagnostics;

namespace CardGame.CardModels.Characters
{
    public class CharacterBase : PropertyChangeObject, ICardModel
    {
        protected readonly int _iD;
        /// <summary>
        /// ID of character.
        /// </summary>
        public int ID
        {
            get => _iD;
        }

        protected string _name;
        /// <summary>
        /// Name of character.
        /// </summary>
        public string Name
        {
            get => _name;
        }

        protected readonly string _describe;
        /// <summary>
        /// CardModel describe.
        /// </summary>
        public string Describe
        {
            get => _describe;
        }

        protected readonly string _shortDescribe;
        /// <summary>
        /// CardModel short describe.
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

        private readonly int _maxAttackPoints;
        /// <summary>
        /// Max attack points.
        /// </summary>
        public int MaxAttackPoints => _maxAttackPoints;

        protected int _attackPoints;
        /// <summary>
        /// CardModel's attack points.
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

        private readonly int _maxHealthPoints;
        /// <summary>
        /// Max health points.
        /// </summary>
        public int MaxHealthPoints => _maxHealthPoints;

        protected int _healthPoints;
        /// <summary>
        /// CardModel's health points.
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

        private readonly int _maxShieldPoints;
        /// <summary>
        /// Max shield points.
        /// </summary>
        public int MaxShieldPoints => _maxShieldPoints;

        protected int _shieldPoints;
        /// <summary>
        /// CardModel's shield points.
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

        protected bool _isMagicResistant;
        /// <summary>
        /// Return true if character is magic resistant.
        /// </summary>
        public bool IsMagicResistant => _isMagicResistant;

        protected readonly string _exampleImageSource;

        /// <summary>
        /// 
        /// </summary>
        public string ExampleImageSource => _exampleImageSource;

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

        public virtual void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel) { }

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

        public void HealToMax()
        {
            if (HealthPoints < MaxHealthPoints)
                HealthPoints = MaxHealthPoints;
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

        public void RestoreAttackToMax()
        {
            if (AttackPoints < MaxAttackPoints)
                AttackPoints = MaxAttackPoints;
        }

        public void ReinforceShield(int shieldPoints)
        {
            if (shieldPoints <= 0)
                return;

            ShieldPoints += shieldPoints;
        }

        public void AddMagicResistant()
        {
            _isMagicResistant = true;
            OnPropertyChanged(nameof(IsMagicResistant));
        }

        public void RemoveMagicResistant()
        {
            _isMagicResistant = false;
            OnPropertyChanged(nameof(IsMagicResistant));
        }

        public void BreakShield()
        {
            if (ShieldPoints != 0) ShieldPoints = 0;
        }

        public void ReinforceShieldToMax()
        {
            if (ShieldPoints < MaxShieldPoints)
                ShieldPoints = MaxShieldPoints;
        }

        public CharacterCard CardOvner;

        public Action<CharacterCard> OnHealthToZero;

        public CharacterBase(string name, int iD, string describe, string shortDescribe, SpeciesTypes species, CharacterTypeEnum characterType, int attackPoints, int healthPoints, int shieldPoints, bool isMagicResistant, string exampleImageSource, Brush backgroundColor = null, Brush strokeColor = null)
        {
            _name = name;
            _iD = iD;
            _describe = describe;
            _shortDescribe = shortDescribe;
            _species = species;
            _characterType = characterType;
            _maxAttackPoints = _attackPoints = attackPoints;
            _maxHealthPoints = _healthPoints = healthPoints;
            _maxShieldPoints = _shieldPoints = shieldPoints;
            _isMagicResistant = isMagicResistant;
            _exampleImageSource = exampleImageSource;
            _backgroundColor = backgroundColor == null ? Brush.Orange : backgroundColor;
            _strokeColor = strokeColor == null ? Brush.White : strokeColor;

            // Śmierć karty:
            OnHealthToZero += DestroyCard;

        }

        private static void DestroyCard(CardBase card)
        {
            card.Destroy();
        }

        ~CharacterBase()
        {
#if DEBUG
            Debug.WriteLine("CardModel has been destroyed.");
            // todo destroy it doesn't works
#endif
        }

    }
}
