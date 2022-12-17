namespace CardGame.Characters
{
    internal class Witch : MagicCharacter
    {
        public Witch() : base("Witch", 8, "-", "-", SpeciesTypes.Human, CharacterTypeEnum.Magic, 12, 5, 5, true, "img_source", 10, Color.Parse("MediumPurple")) { }
    }
}
