using CardGame.CardModels.Characters;

namespace CardGame.CardModels.Items
{
    public class Excaliber : ItemBase
    {
        public Excaliber() : base("Excaliber", 12, "-", "-", "img_source", 1, ItemTypeEnum.ToOneAllie, 1, Color.Parse("Red")) { }

        public override void ItemFunction(ICardModel[] enemies, ICardModel[] allies, ICardModel[] selectedEnemies, ICardModel[] selectedAllies)
        {
            var charactersSelectedAllies = selectedAllies as CharacterBase[];

            // Wzmacnia atak wybranej karty (sojusznika o 50%).
            if (charactersSelectedAllies.Length == 1)
                charactersSelectedAllies[0].BoostAttack(charactersSelectedAllies[0].AttackPoints / 2);
        }
    }
}
