namespace CardGame.CardModels.Characters
{
    internal class Fighter : CharacterBase
    {
        public Fighter() : base("Fighter", 1, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Normal, 10, 15, 5, false, "fighter.png") { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var selectedCharacter = selectedCardModel as CharacterBase;

            selectedCharacter.GetDamaged(AttackPoints * 2);
            if (selectedCharacter.HealthPoints == 0)
                this.BoostAttack(2);
        }
    }
}
