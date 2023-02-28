namespace CardGame.CardModels.Characters
{
    internal class RoyalSoldier : CharacterBase
    {
        public RoyalSoldier() : base("Royal soldier", 9, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 16, 15, 18, false, "img_source", Color.Parse("Brown")) { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var characterEnemies = enemies as CharacterBase[];
            var characterAllies = allies as CharacterBase[];
            var selectedCharacter = selectedCardModel as CharacterBase;

            // przejmowanie obrażeń (odejmowanie od nich 3)
        }

    }
}
