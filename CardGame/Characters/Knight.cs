namespace CardGame.Characters
{
    internal class Knight : CharacterBase
    {
        public Knight() : base("Knight", 11, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 18, 10, 15, false, "img_source", Color.Parse("Red")) { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            Attack(selectedCharacter);
            this.ReinforceShield(5);
        }

    }
}
