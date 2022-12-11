using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class RoyalSoldier : CharacterBase
    {
        public RoyalSoldier() : base("Royal soldier", 9, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 16, 15, 18, false) { }
    }
}
