namespace CardGame.GameObjectsUI;

public class CardBase : ContentView
{
	public CardBase()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}

    #region Destroy

    public Action<CardBase> ToDestroy;
    public void Destroy()
    {
        ToDestroy?.Invoke(this);
    }

    #endregion

    #region click event

    public delegate void OnSomeButtonClickedDelegate(CardBase sender);

    public OnSomeButtonClickedDelegate OnCardTaped { get; set; }

    // ================================================================= //


    #endregion

    #region Drag&Drop

    //private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    //{
    //    e.Data.Properties.Add("Card", this);
    //}

    #endregion
}