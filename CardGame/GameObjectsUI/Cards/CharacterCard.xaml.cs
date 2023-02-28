using CardGame.CardModels.Characters;
using CardGame.ViewModels;
using System.Diagnostics;

namespace CardGame.GameObjectsUI;

public partial class CharacterCard : CardBase
{
    private CharacterCard()
    {
        InitializeComponent();
    }

    ~CharacterCard()
    {
#if DEBUG
        // todo destroy it doesnt works
        Debug.WriteLine("Card has been destroyed successfull!");
#endif
    }

    public CharacterCard(MagicCharacter character) : this()
    {
        BindingContext = new MagicCharacterCardViewModel(character);
        ((BindingContext as CharacterCardViewModel).CardModel as CharacterBase).CardOvner = this;
    }

    public CharacterCard(CharacterBase character) : this()
    {
        BindingContext = new CharacterCardViewModel(character);
        ((BindingContext as CharacterCardViewModel).CardModel as CharacterBase).CardOvner = this;
    }

}