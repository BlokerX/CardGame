using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class Bandit : CharacterBase
    {
        public Bandit() : base("Bandit", 7, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Normal, 10, 6, 2, false, "img_source") { }
    }
}
