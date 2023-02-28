namespace CardGame.ViewModels
{
    internal interface ICardViewModel
    {
        public ICardModel CardModel { get; }

        public string ID { get; }

        public string Name { get; }

        //private readonly Image _exampleImage/* = new() { Source = "https://i.pinimg.com/280x280_RS/2e/51/23/2e51230e3d557acde4744f7848308da0.jpg" }*/;
        public Image ExampleImage { get; }

        public Brush BackgroundColor { get; }

    }
}
