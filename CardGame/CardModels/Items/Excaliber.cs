﻿using CardGame.CardModels.Characters;
using System.Linq;

namespace CardGame.CardModels.Items
{
    public class Excaliber : ItemBase
    {
        public Excaliber() : base("Excaliber", 12, "-", "-", "excaliber.png", 1, ItemTypeEnum.ToOneAllie, 1, Color.Parse("Red")) { }

        public override void ItemFunction(ICardModel[] enemies, ICardModel[] allies, ICardModel[] selectedEnemies, ICardModel[] selectedAllies)
        {
            var charactersSelectedAllies = (from allie in selectedAllies
                                            where allie is CharacterBase
                                            select allie as CharacterBase).ToArray();

            // Wzmacnia atak wybranej karty (sojusznika o 50%).
            if (charactersSelectedAllies?.Length == 1)
                charactersSelectedAllies[0].BoostAttack(charactersSelectedAllies[0].AttackPoints / 2);
        }
    }
}
