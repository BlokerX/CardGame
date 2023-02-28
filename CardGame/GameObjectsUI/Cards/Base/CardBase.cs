using CardGame.CardModels.Items;
using CardGame.GameObjectsUI;
using System.Diagnostics;
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

    protected void TapGestureRecognizer_Tapped(object sender, EventArgs e) => OnCardTaped?.Invoke(this);

    #endregion

    protected void ContentView_SizeChanged(object sender, EventArgs e)
    {
        if (this.Height / 2.5 != this.Width)
            this.SizeAllocated(this.Height / 2.5, this.Height);
        //ImgBorder.StrokeShape = new RoundRectangle() { CornerRadius = 10 };
    }
}