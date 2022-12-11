using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class Archer : CharacterBase
    {
        public Archer() : base("Archer", 2, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Distance, 5, 10, 5, false, "img_source") { }
    }
}
