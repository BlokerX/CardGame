namespace CardGame.CardModels.Characters
{
    public class MagicCharacter : CharacterBase
    {
        public MagicCharacter(string name, int iD, string describe, string shortDescribe, SpeciesTypes species, CharacterTypeEnum characterType, int attackPoints, int healthPoints, int shieldPoints, bool isMagicResistant, string exampleImageSource, int magicPoints, Brush backgroundColor = null, Brush strokeColor = null) :
            base(name, iD, describe, shortDescribe, species, characterType, attackPoints, healthPoints, shieldPoints, isMagicResistant, exampleImageSource, backgroundColor, strokeColor)
        {
            _magicPoints = magicPoints;
        }

        protected int _magicPoints;
        /// <summary>
        /// Item's magic points.
        /// </summary>
        public int MagicPoints
        {
            get => _magicPoints;
            private set
            {
                _magicPoints = value;
                OnPropertyChanged(nameof(MagicPoints));
            }
        }

        private int MagicAttack()
        {
            int m;
            if (MagicPoints == 0)
                m = AttackPoints;
            else
                m = AttackPoints * (MagicPoints + 1) / 2;
            return m;
        }

        public override void Attack(CharacterBase selectedCharacter)
        {
            if (!selectedCharacter.IsMagicResistant)
                selectedCharacter.GetDamaged(MagicAttack());
            else base.Attack(selectedCharacter);
        }

    }
}
