namespace CardGame.Characters
{
    internal class Witch : MagicCharacter
    {
        public Witch() : base("Witch", 8, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Magic, 12, 5, 5, true, "img_source", 10, Color.Parse("MediumPurple")) { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            this.ReinforceShield(5);
            this.Heal(5);
            foreach (var allie in allies)
            {
                if (allie != this)
                    allie.Heal(5);
            }
        }
    }
}
