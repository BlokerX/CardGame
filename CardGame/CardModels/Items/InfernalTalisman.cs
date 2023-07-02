using CardGame.CardModels.Characters;

namespace CardGame.CardModels.Items
{
    public class InfernalTalisman : ItemBase
    {
        public InfernalTalisman() : base("Infernal talisman", 14, "-", "-", "infernal_talisman.png", 1, ItemTypeEnum.ToOneAllie, 1, Color.Parse("Purple")) { }

        public override void ItemFunction(ICardModel[] enemies, ICardModel[] allies, ICardModel[] selectedEnemies, ICardModel[] selectedAllies)
        {
            var charactersSelectedAllies = (from allie in selectedAllies
                                            where allie is CharacterBase
                                            select allie as CharacterBase).ToArray();

            if (charactersSelectedAllies.Length == 1)
                charactersSelectedAllies[0].AddMagicResistant();
        }
    }
}
