namespace CardGame.Characters
{
    internal class Wizard : MagicCharacter
    {
        public Wizard() : base("Wizard", 3, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Magic, 7, 5, 5, true, "img_source", 15, Color.Parse("MediumPurple")) { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            selectedCharacter.Attack(this);
            foreach (var enemy in enemies)
            {
                if (!enemy.IsMagicResistant)
                    enemy.DebuffAttack(2);
            }
        }
    }
}
