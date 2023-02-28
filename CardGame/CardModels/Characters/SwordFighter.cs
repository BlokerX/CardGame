namespace CardGame.CardModels.Characters
{
    internal class SwordFighter : CharacterBase
    {
        public SwordFighter() : base("Sword fighter", 4, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 15, 10, 7, false, "img_source", Color.Parse("Red")) { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var selectedCharacter = selectedCardModel as CharacterBase;

            selectedCharacter.GetPearcingDamaged(AttackPoints * 2);
        }

    }
}
