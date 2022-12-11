using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class Fighter : CharacterBase
    {
        public Fighter() : base("Fighter", 1, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Normal, 10, 15, 5, false) { }
    }
}
