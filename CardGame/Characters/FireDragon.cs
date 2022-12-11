using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class FireDragon : CharacterBase
    {
        public FireDragon() : base("Fire dragon", 6, "-", "-", SpeciesTypes.Dragon, CharacterTypeEnum.Tank, 15, 15, 5, true) { }
    }
}
