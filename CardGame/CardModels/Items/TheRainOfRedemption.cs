using CardGame.CardModels.Characters;

namespace CardGame.CardModels.Items
{
    public class TheRainOfRedemption : ItemBase
    {
        public TheRainOfRedemption() : base("The rain of redemption", 15, "-", "-", "the_rain_of_redemption.png", 1, ItemTypeEnum.ToAllAllies, 1, Color.Parse("Green")) { }

        public override void ItemFunction(ICardModel[] enemies, ICardModel[] allies, ICardModel[] selectedEnemies, ICardModel[] selectedAllies)
        {
            var charactersAllies = (from allie in allies
                                            where allie is CharacterBase
                                            select allie as CharacterBase).ToArray();

            if (charactersAllies.Length > 0)
                foreach(var allie in charactersAllies)
                {
                    allie.HealToMax();
                }
        }
    }
}
