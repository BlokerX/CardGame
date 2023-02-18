using CardGame.Characters;

namespace CardGame.ViewModels
{
    public class MagicCharacterCardViewModel : CharacterCardViewModel
    {
        public MagicCharacterCardViewModel(MagicCharacter character): base(character)
        {
            Character = character;
        }

        public new readonly MagicCharacter Character;

        public string MagicPoints
        {
            get => IntToThreeCharStringComparer(Character.MagicPoints);
        }

    }
}
