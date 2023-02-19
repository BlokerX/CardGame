using CardGame.CardModels.Characters;
using CardGame.ServiceObjects;

namespace CardGame.ViewModels
{
    internal class MagicCharacterCardViewModel : CharacterCardViewModel
    {
        public MagicCharacterCardViewModel(MagicCharacter character): base(character)
        {
            Character = character;
        }

        public new readonly MagicCharacter Character;

        public string MagicPoints
        {
            get => Character.MagicPoints.IntToThreeCharStringComparer();
        }

    }
}
