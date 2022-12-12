namespace CardGame.Characters
{
    internal class Zeus : CharacterBase
    {
        public Zeus() : base("Zeus", 5, "-", "-", SpeciesTypes.God, CharacterTypeEnum.God, 20, 10, 0, false, "img_source") { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            // Atak główny:
            selectedCharacter.GetDamaged(AttackPoints * 3);

            // Ataki poboczne:
            Random random = new();
            var selectedEnemies = new int[random.Next() % (enemies.Length-1)];
            for (int i = 0; i < selectedEnemies.Length; i++)
            {
                int x;
                do
                {
                    x = random.Next() % enemies.Length;

                } while (selectedEnemies.Contains(x) || enemies[x] == selectedCharacter);
                selectedEnemies[i] = x;

                // Działanie na wybranych, przypadkowych przeciwnikach:
                GetDamaged(AttackPoints / 3);

            }
        }

    }
}
