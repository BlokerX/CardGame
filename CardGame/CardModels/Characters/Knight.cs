namespace CardGame.CardModels.Characters
{
    internal class Knight : CharacterBase
    {
        public Knight() : base("Knight", 11, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 18, 10, 15, false, "img_source", Color.Parse("Red")) { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var selectedCharacter = selectedCardModel as CharacterBase;

            Attack(selectedCharacter);
            this.ReinforceShield(5);
        }

    }
}
