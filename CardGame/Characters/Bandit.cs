namespace CardGame.Characters
{
    internal class Bandit : CharacterBase
    {
        public Bandit() : base("Bandit", 7, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Normal, 10, 6, 2, false, "img_source") { }
        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            var selectedEnemies = new int[3];

            Random random = new();
            for (int i = 0; i < selectedEnemies.Length; i++)
            {
                int x;
                do
                {
                    x = random.Next() % enemies.Length;

                } while (selectedEnemies.Contains(x));
                selectedEnemies[i] = x;
                enemies[x].GetPearcingDamaged(AttackPoints);
                enemies[x].ReinforceShield(1);
            }
        }
    }
}
