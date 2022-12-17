namespace CardGame.Characters
{
    internal class Fighter : CharacterBase
    {
        public Fighter() : base("Fighter", 1, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Normal, 10, 15, 5, false, "img_source") { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            selectedCharacter.GetDamaged(AttackPoints * 2);
            if (selectedCharacter.HealthPoints == 0)
                this.BoostAttack(2);
        }
    }
}
