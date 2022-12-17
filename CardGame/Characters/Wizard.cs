namespace CardGame.Characters
{
    internal class Wizard : MagicCharacter
    {
        public Wizard() : base("Wizard", 3, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Magic, 7, 5, 5, true, "img_source", 15, Color.Parse("MediumPurple")) { }
    }
}
