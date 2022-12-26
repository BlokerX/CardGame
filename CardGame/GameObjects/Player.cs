using CardGame.GameObjectsUI;

namespace CardGame.GameObjects
{
    /// <summary>
    /// Player object.
    /// </summary>
    public class Player
    {
        //todo do zmiany 
        private string _name = "William";
        /// <summary>
        /// Player's name.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Player's deck of cards.
        /// </summary>
        public Deck DeckOfCards;

        /// <summary>
        /// Chosen card (ovn card).
        /// </summary>
        public Card ChosenCard;

        /// <summary>
        /// Targeted card (enemy card).
        /// </summary>
        public Card TargetedCard;

        /// <summary>
        /// Special point to use speial attacks.
        /// </summary>
        public byte SpecialPoints;

        // - - - //



        // - - - //

        /// <summary>
        /// It gets random cards list.
        /// </summary>
        /// <param name="count">Count of cards.</param>
        /// <returns>Random cards list.</returns>
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

        public TurnFazeEnum TurnFaze;

        public enum TurnFazeEnum
        {
            // Ememy turn:
            EnemyTure,

            // Player moves:
            SelectingPlayerCard,
            SelectingEnemyCard,
            UsingSpecialAttack,
            
            // End game:
            EndGame
        }

    }
}
