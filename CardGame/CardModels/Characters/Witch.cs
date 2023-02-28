namespace CardGame.CardModels.Characters
{
    internal class Witch : MagicCharacter
    {
        public Witch() : base("Witch", 8, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Magic, 12, 5, 5, true, "img_source", 10, Color.Parse("MediumPurple")) { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var characterAllies = allies as CharacterBase[];

            this.ReinforceShield(5);
            this.Heal(5);
            foreach (var allie in characterAllies)
            {
                if (allie != this)
                    allie.Heal(5);
            }
        }
    }
}
