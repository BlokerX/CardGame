using CardGame.CardModels.Characters;
using CardGame.ServiceObjects;

namespace CardGame.ViewModels
{
    internal class MagicCharacterCardViewModel : CharacterCardViewModel
    {
        public MagicCharacterCardViewModel(MagicCharacter character): base(character)
        {
            CardModel = character;
        }

        public new readonly MagicCharacter CardModel;

        public string MagicPoints
        {
            get => CardModel.MagicPoints.IntToThreeCharStringComparer();
        }

    }
}
