namespace CardGame.Characters
{
    public class MagicCharacter : CharacterBase
    {
        protected int _magicPoints;

        public MagicCharacter(string name, int iD, string describe, string shortDescribe, SpeciesTypes species, CharacterTypeEnum characterType, int attackPoints, int healthPoints, int shieldPoints, bool isMagicResistant, string exampleImageSource, int magicPoints, Brush backgroundColor = null, Brush strokeColor = null) :
            base(name, iD, describe, shortDescribe, species, characterType, attackPoints, healthPoints, shieldPoints, isMagicResistant, exampleImageSource, backgroundColor, strokeColor)
        {
            _magicPoints = magicPoints;
        }

        /// <summary>
        /// Character's magic points.
        /// </summary>
        public int MagicPoints
        {
            get => _magicPoints;
        }

        public int MagicAttack()
        {
            int m;
            if (MagicPoints == 0)
                m = AttackPoints;
            else
                m = AttackPoints * (MagicPoints + 1) / 2;
            return m;
        }

    }
}
