namespace CardGame.CardModels.Characters
{
    internal class Wizard : MagicCharacter
    {
        public Wizard() : base("Wizard", 3, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Magic, 7, 5, 5, true, "wizard.png", 15, Color.Parse("MediumPurple")) { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var characterEnemies = enemies as CharacterBase[];
            var selectedCharacter = selectedCardModel as CharacterBase;

            selectedCharacter.Attack(this);
            foreach (var enemy in characterEnemies)
            {
                if (!enemy.IsMagicResistant)
                    enemy.DebuffAttack(2);
            }
        }
    }
}
