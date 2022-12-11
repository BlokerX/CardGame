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
        /// <summary>
        /// Character's magic points.
        /// </summary>
        public int MagicPoints
        {
            get => _magicPoints;
        }

        public MagicCharacter(string name, int iD, string describe, string shortDescribe, SpeciesTypes species, CharacterTypeEnum type, int attackPoints, int healthPoints, int shieldPoints, bool isMagicResistant, int magicPoints) : 
            base(name, iD, describe, shortDescribe, species, type, attackPoints, healthPoints, shieldPoints, isMagicResistant)
        {
            _magicPoints = magicPoints;
        }
    }
}
