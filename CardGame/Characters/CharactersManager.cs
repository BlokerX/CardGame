using CardGame.GameObjectsUI;

namespace CardGame.Characters
{
    internal class CharactersManager
    {
        public const int CharactersID = 11;

        public static CharacterBase GetCharacterTypesById(int id)
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

        public static CharacterBase GetRandomCharacterType()
        {
            return GetCharacterTypesById(new Random().Next(1, CharactersID));
        }

        public static List<CharacterBase> GetAllCharacters()
        {
            var tmp = new List<CharacterBase>();
            for (int i = 1; i >= CharactersID; i++)
            {
                tmp.Add(GetCharacterTypesById(i));
            }
            return tmp;
        }

        public static CardBase GetCardByCharacter(CharacterBase character)
        {
            return new CharacterCard(character);
        }

        public static CardBase GetCardById(int id)
        {
            return new CharacterCard(GetCharacterTypesById(id));
        }

        public static CardBase GetRandomCard()
        {
            return new CharacterCard(GetRandomCharacterType());
        }

        public static List<CardBase> GetAllCards()
        {
            var tmp = new List<CardBase>();
            foreach (var character in GetAllCharacters())
            {
                tmp.Add(new CharacterCard(character));
            }
            return tmp;
        }

    }
}
