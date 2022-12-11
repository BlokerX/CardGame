using CardGame.Characters;

namespace CardGame.ViewModels
{
    public class MagicCardViewModel : CardViewModel
    {
        public MagicCardViewModel(MagicCharacter character): base(character)
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
