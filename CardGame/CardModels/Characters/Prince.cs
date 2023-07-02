namespace CardGame.CardModels.Characters
{
    internal class Prince : CharacterBase
    {
        public Prince() : base("Prince", 10, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 25, 15, 10, false, "prince.png", Color.Parse("Gold")) { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var characterAllies = allies as CharacterBase[];
            var selectedCharacter = selectedCardModel as CharacterBase;

            foreach (var allie in characterAllies)
            {
                allie.Attack(selectedCharacter);
                if (selectedCharacter is null)
                    break;
            }
        }
    }
}
