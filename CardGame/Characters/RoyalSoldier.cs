namespace CardGame.Characters
{
    internal class RoyalSoldier : CharacterBase
    {
        public RoyalSoldier() : base("Royal soldier", 9, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 16, 15, 18, false, "img_source", Color.Parse("Brown")) { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            // przejmowanie obrażeń (odejmowanie od nich 3)
        }

    }
}
