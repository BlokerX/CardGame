using CardGame.GameObjectsUI;

namespace CardGame;
public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
		MainPage = new Board();
	}
}
