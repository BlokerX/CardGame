namespace CardGame.Characters
{
    internal class Prince : CharacterBase
    {
        public Prince() : base("Prince", 10, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Melee, 25, 15, 10, false, "img_source", Color.Parse("Gold")) { }

        public override void SpecialAttack(CharacterBase[] enemies, CharacterBase[] allies, CharacterBase selectedCharacter)
        {
            foreach (var allie in allies)
            {
                allie.Attack(selectedCharacter);
                if (selectedCharacter is null)
                    break;
            }
        }
    }
}
