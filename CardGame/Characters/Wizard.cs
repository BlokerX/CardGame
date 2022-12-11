using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class Wizard : MagicCharacter
    {
        public Wizard() : base("Wizard", 3, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Magic, 7, 5, 5, true, 15) { }
    }
}
