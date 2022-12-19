using CardGame.GameObjectsUI;

namespace CardGame.GameObjects
{
    public class Player
    {
        //todo do zmiany
        private string _name = "William";

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public Deck DeckOfCards;


        public Card ChosenCard;

        public Action ChosenCardSelected;


        public Card TargetedCard;

        public Action TargetedCardSelected;

        // - - - //



        // - - - //

        public static List<Card> GetCards(int count)
        {
            // Skalowanie kart:
            double scale = 0.3;
            double x = -(1 - scale) * 505 / 2, y = -(1 - scale) * 829 / 2;

            List<Card> cards = new();

            for (int i = 0; i < count; i++)
            {
                var c = Card.GetRandomCard();
                c.Scale = scale;
                c.Margin = new Thickness(x, y);
                cards.Add(c);
            }

            return cards;
        }

    }
}
