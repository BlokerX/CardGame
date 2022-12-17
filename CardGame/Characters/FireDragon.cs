using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class FireDragon : CharacterBase
    {
        public FireDragon() : base("Fire dragon", 6, "-", "-", SpeciesTypes.Dragon, CharacterTypeEnum.Tank, 15, 15, 5, true, "img_source", Color.Parse("OrangeRed")) { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            foreach(var character in enemies)
            {
                character.GetDamaged(20);
            }
            foreach(var character in allies)
            {
                character.ReinforceShield(2);
            }
        }

    }
}
