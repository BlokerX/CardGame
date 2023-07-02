namespace CardGame.CardModels.Characters
{
    internal class Archer : CharacterBase
    {
        public Archer() : base("Archer", 2, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Distance, 5, 10, 5, false, "archer.png") { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var selectedCharacter = selectedCardModel as CharacterBase;

            selectedCharacter.BreakShield();
            selectedCharacter.GetDamaged(AttackPoints);
        }

    }
}
