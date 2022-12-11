using CardGame.Characters;
using CardGame.ViewModels;

namespace CardGame.GameObjectsUI;

public partial class Card : ContentView
{
	private Card()
	{
		InitializeComponent();
	}

	public Card(MagicCharacter character):this()
	{
		BindingContext = new MagicCardViewModel(character);
	}
	
	public Card(CharacterBase character):this()
	{
		BindingContext = new CardViewModel(character);
	}

}