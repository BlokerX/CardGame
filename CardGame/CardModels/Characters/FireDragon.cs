namespace CardGame.CardModels.Characters
{
    internal class FireDragon : CharacterBase
    {
        public FireDragon() : base("Fire dragon", 6, "-", "-", SpeciesTypes.Dragon, CharacterTypeEnum.Tank, 15, 15, 5, true, "img_source", Color.Parse("OrangeRed")) { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var characterEnemies = enemies as CharacterBase[];
            var characterAllies = allies as CharacterBase[];

            foreach (var character in characterEnemies)
            {
                character.GetDamaged(20);
            }
            foreach (var character in characterAllies)
            {
                character.ReinforceShield(2);
            }
        }

    }
}
