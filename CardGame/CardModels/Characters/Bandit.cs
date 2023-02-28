namespace CardGame.CardModels.Characters
{
    internal class Bandit : CharacterBase
    {
        public Bandit() : base("Bandit", 7, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Normal, 10, 6, 2, false, "img_source", Color.Parse("DarkRed")) { }
        public override void SpecialAttack(ICardModel[] enemies, ICardModel[] allies, ICardModel selectedCardModel)
        {
            var characterEnemies = enemies as CharacterBase[];

            var selectedEnemies = new int?[3];

            if (characterEnemies.Length < 3)
                selectedEnemies = new int?[characterEnemies.Length];

            Random random = new();
            for (int i = 0; i < selectedEnemies.Length; i++)
            {
                int x;
                do
                {
                    x = random.Next() % characterEnemies.Length;

                } while (selectedEnemies.Contains(x));
                selectedEnemies[i] = x;
                characterEnemies[x].GetPearcingDamaged(AttackPoints);
                characterEnemies[x].ReinforceShield(1);
            }
        }
    }
}
