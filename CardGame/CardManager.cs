using CardGame.CardModels.Characters;
using CardGame.GameObjectsUI;
using CardGame.CardModels.Items;

namespace CardGame
{
    internal class CardManager
    {
        public const int CardsID = 11;

        public static ICardModel GetCardTypesById(int id)
        {
            return id switch
            {
                1 => new Fighter(),
                2 => new Archer(),
                3 => new Wizard(),
                4 => new SwordFighter(),
                5 => new Zeus(),
                6 => new FireDragon(),
                7 => new Bandit(),
                8 => new Witch(),
                9 => new RoyalSoldier(),
                10 => new Prince(),
                11 => new Knight(),

                _ => null,
            };
        }

        public static ICardModel GetRandomCharacterType()
        {
            return GetCardTypesById(new Random().Next(1, CardsID));
        }

        public static List<ICardModel> GetAllCharacters()
        {
            var tmp = new List<ICardModel>();
            for (int i = 1; i >= CardsID; i++)
            {
                tmp.Add(GetCardTypesById(i));
            }
            return tmp;
        }

        public static CardBase GetCardByCardModel(ICardModel cardModel)
        {
            if (cardModel is CharacterBase)
            {
                if (cardModel is MagicCharacter)
                    return new CharacterCard(cardModel as MagicCharacter);
                return new CharacterCard(cardModel as CharacterBase);
            }
            else if (cardModel is ItemBase)
            {
                return new ItemCard(cardModel as ItemBase);
            }
            else
                return null;
        }

        public static CardBase GetCardById(int id)
        {
            return GetCardByCardModel(GetCardTypesById(id));
        }

        public static CardBase GetRandomCard()
        {
            return GetCardByCardModel(GetRandomCharacterType());
        }

        public static List<CardBase> GetAllCards()
        {
            var tmp = new List<CardBase>();
            foreach (var cardModel in GetAllCharacters())
            {
                tmp.Add(GetCardByCardModel(cardModel));
            }
            return tmp;
        }

    }
}
