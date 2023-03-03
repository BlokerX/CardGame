using CardGame.CardModels.Characters;

namespace CardGame.CardModels.Items
{
    public class InfernalTalisman : ItemBase
    {
        public InfernalTalisman() : base("Infernal talisman", 14, "-", "-", "img_source", 1, ItemTypeEnum.ToOneAllie, 1, Color.Parse("Purple")) { }

        public override void ItemFunction(ICardModel[] enemies, ICardModel[] allies, ICardModel[] selectedEnemies, ICardModel[] selectedAllies)
        {
            var charactersSelectedAllies = selectedAllies as CharacterBase[];

            if (charactersSelectedAllies.Length == 1)
                charactersSelectedAllies[0].AddMagicResistant();
        }
    }
}
