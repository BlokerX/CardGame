using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    public class MagicCharacter : CharacterBase
    {
        protected int _magicPoints;

        public MagicCharacter(string name, int iD, string describe, string shortDescribe, SpeciesTypes species, CharacterTypeEnum characterType, int attackPoints, int healthPoints, int shieldPoints, bool isMagicResistant, string exampleImageSource, int magicPoints) : 
            base(name, iD, describe, shortDescribe, species, characterType, attackPoints, healthPoints, shieldPoints, isMagicResistant, exampleImageSource)
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
    }
}
