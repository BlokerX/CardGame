using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Characters
{
    internal class SwordFighter : CharacterBase
    {
        public SwordFighter() : base("Sword fighter", 4, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 15, 10, 7, false, "img_source", Color.Parse("Red")) { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            selectedCharacter.GetPearcingDamaged(AttackPoints * 2);
        }

    }
}
