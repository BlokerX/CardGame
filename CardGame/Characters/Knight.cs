using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class Knight : CharacterBase
    {
        public Knight() : base("Knight", 11, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 18, 10, 15, false, "img_source", Color.Parse("Red")) { }

    }
}
