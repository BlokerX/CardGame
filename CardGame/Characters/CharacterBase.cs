namespace CardGame.Characters
{
    public class CharacterBase
    {
        protected string _name;
        /// <summary>
        /// Name of character.
        /// </summary>
        public string Name
        {
            get => _name;
        }

        protected readonly uint _iD;
        /// <summary>
        /// ID of character.
        /// </summary>
        public uint ID
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
        }

        protected int _healthPoints;
        /// <summary>
        /// Character's health points.
        /// </summary>
        public int HealthPoints
        {
            get => _healthPoints;
        }

        protected int _shieldPoints;
        /// <summary>
        /// Character's shield points.
        /// </summary>
        public int ShieldPoints
        {
            get => _shieldPoints;
        }

        protected readonly bool _isMagicResistant;
        /// <summary>
        /// Return true if character is magic resistant.
        /// </summary>
        public bool IsMagicResistant
        {
            get => _isMagicResistant;
        }

        public CharacterBase(string name, uint iD, string describe, string shortDescribe, SpeciesTypes species, CharacterTypeEnum characterType, int attackPoints, int healthPoints, int shieldPoints, bool isMagicResistant)
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
    }
}
