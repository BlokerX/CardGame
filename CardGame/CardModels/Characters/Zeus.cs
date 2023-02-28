namespace CardGame.CardModels.Characters
{
    internal class Zeus : CharacterBase
    {
        public Zeus() : base("Zeus", 5, "-", "-", SpeciesTypes.God, CharacterTypeEnum.God, 20, 10, 0, false, "img_source", Color.Parse("LightBlue")) { }

        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var characterEnemies = enemies as CharacterBase[];
            var selectedCharacter = selectedCardModel as CharacterBase;

            // Atak główny:
            selectedCharacter.GetDamaged(AttackPoints * 3);

            // Ataki poboczne:
            if (characterEnemies.Length < 2)
                return;
            Random random = new();
            var selectedEnemies = new int[random.Next() % (characterEnemies.Length - 1)];
            for (int i = 0; i < selectedEnemies.Length; i++)
            {
                int x;
                do
                {
                    x = random.Next() % characterEnemies.Length;

                } while (selectedEnemies.Contains(x) || characterEnemies[x] == selectedCharacter);
                selectedEnemies[i] = x;

                // Działanie na wybranych, przypadkowych przeciwnikach:
                GetDamaged(AttackPoints / 3);

            }
        }

    }
}
