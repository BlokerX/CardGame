using CardGame.CardModels.Characters;

namespace CardGame.CardModels.Items
{
    public class WoodenShield : ItemBase
    {
        public WoodenShield() : base("Wooden shield", 13, "-", "-", "wooden_shield.png", 1, ItemTypeEnum.ToOneAllie, 1, Color.Parse("LightBlue")) { }

        public override void ItemFunction(ICardModel[] enemies, ICardModel[] allies, ICardModel[] selectedEnemies, ICardModel[] selectedAllies)
        {
            var charactersSelectedAllies = (from allie in selectedAllies
                                            where allie is CharacterBase
                                            select allie as CharacterBase).ToArray();

            if (charactersSelectedAllies.Length == 1)
                charactersSelectedAllies[0].ReinforceShield(10);
        }
    }
}
