using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class Zeus : CharacterBase
    {
        public Zeus() : base("Zeus", 5, "-", "-", SpeciesTypes.God, CharacterTypeEnum.God, 20, 10, 0, false, "img_source") { }
    }
}
