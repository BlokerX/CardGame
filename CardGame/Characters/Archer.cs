namespace CardGame.Characters
{
    internal class Archer : CharacterBase
    {
        public Archer() : base("Archer", 2, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Distance, 5, 10, 5, false, "img_source") { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            selectedCharacter.BreakShield();
            selectedCharacter.GetDamaged(AttackPoints);
        }

    }
}
