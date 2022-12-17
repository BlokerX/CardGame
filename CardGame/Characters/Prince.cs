using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class Prince : CharacterBase
    {
        public Prince() : base("Prince", 10, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 25, 15, 10, false, "img_source", Color.Parse("Gold")) { }
    }
}
