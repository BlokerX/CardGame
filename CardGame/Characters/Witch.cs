using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class Witch : MagicCharacter
    {
        public Witch() : base("Witch", 8, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Magic, 12, 5, 5, true, 10) { }
    }
}
