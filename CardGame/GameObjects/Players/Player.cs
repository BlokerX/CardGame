using CardGame.CardModels;
using CardGame.GameObjectsUI;
using CardGame.ViewModels;

namespace CardGame.GameObjects
{
    /// <summary>
    /// Player object.
    /// </summary>
    internal class Player
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

        private Score _score;
        /// <summary>
        /// Player score.
        /// </summary>
        public Score Score
        {
            get => _score;
            set => _score = value;
        }

        #region Cards

        /// <summary>
        /// Player's deck of cards.
        /// </summary>
        public Deck DeckOfCards;

        /// <summary>
        /// Chosen card (ovn card).
        /// </summary>
        public CardBase ChosenCard;

        /// <summary>
        /// Targeted card (enemy card).
        /// </summary>
        public CardBase TargetedCard;

        #endregion

        #region On board

        /// <summary>
        /// Player's lobby.
        /// </summary>
        private Layout Lobby;

        public void SetLobby(Layout lobby)
        {
            Lobby = lobby;
            this.DeckOfCards.Cards.ForEach((card) =>
            {
                this.Lobby.Children.Add(card);
                card.ToDestroy += RemoveCardFromLobby;
            });
        }

        /// <summary>
        /// Player's board.
        /// </summary>
        private Layout Board;

        public void SetBoard(Layout board)
        {
            Board = board;
            this.DeckOfCards.Cards.ForEach((card) =>
            {
                card.ToDestroy += RemoveCardFromBoard;
            });
        }

        #endregion

        private byte _specialPoints;
        /// <summary>
        /// Special point to use speial attacks.
        /// </summary>
        public byte SpecialPoints
        {
            get => _specialPoints;
            set
            {
                if (value > 3)
                    return;

                //todo stała liczba punktów
                _specialPoints = value;
            }
        }

        /// <summary>
        /// Turn faze for player.
        /// </summary>
        public TurnFazeEnum TurnFaze;

        /// <summary>
        /// Selected attack type 
        /// (eg. normal attack or special attack).
        /// </summary>
        public AttackTypeEnum AttackType;

        /// <summary>
        /// 
        /// </summary>
        public enum TurnFazeEnum
        {
            // Ememy turn:
            EnemyTure,

            // Player moves:
            SelectingPlayerCard,
            SelectingEnemyCard,
            Attacking,

            // On animating:
            Animating,

            // End game:
            EndGame
        }

        /// <summary>
        /// 
        /// </summary>
        public enum AttackTypeEnum
        {
            Attack,
            SpecialAttack
        }

        /// <summary>
        /// It gets random cards list.
        /// </summary>
        /// <param name="count">Count of cards.</param>
        /// <returns>Random cards list.</returns>
        public static List<CardBase> GetCards(int count)
        {
            // Skalowanie kart:
            double scale = 0.3;
            double x = -(1 - scale) * 505 / 2, y = -(1 - scale) * 829 / 2;

            List<CardBase> cards = new();

            for (int i = 0; i < count; i++)
            {
                var c = CardManager.GetRandomCard();
                c.Scale = scale;
                c.Margin = new Thickness(x, y);
                cards.Add(c);
            }

            return cards;
        }

        public void Build(Deck deck)
        {
            DeckOfCards = deck;
            DeckOfCards.Cards.ForEach((card) =>
            {
                card.ToDestroy += RemoveCardFromDeck;
            });
        }

        public void RemoveCardFromDeck(CardBase card)
        {
            if (this.DeckOfCards.Cards.Contains(card))
                this.DeckOfCards.Cards.Remove(card);

            card.ToDestroy -= RemoveCardFromDeck;
        }

        private void RemoveCardFromLobby(CardBase card)
        {
            if (Lobby.Children.Contains(card))
                Lobby.Children.Remove(card);

            card.ToDestroy -= RemoveCardFromLobby;
        }

        public void RemoveCardFromBoard(CardBase card)
        {
            if (Board.Children.Contains(card))
                Board.Children.Remove(card);

            card.ToDestroy -= RemoveCardFromBoard;
        }

        public void ChoseCard(CardBase card)
        {
            if (this.TurnFaze is not Player.TurnFazeEnum.SelectingPlayerCard)
                return;

            this.ChosenCard = card;

            this.HighlightChosenCard();

            // todo chowanie lobby
            Lobby.IsVisible = false;

            this.TurnFaze = Player.TurnFazeEnum.SelectingEnemyCard;
        }

        public void ThrowNewCard(CardBase card)
        {
            if (this.TurnFaze is not Player.TurnFazeEnum.SelectingPlayerCard)
                return;

            card.OnCardTaped -= ThrowNewCard;
            card.OnCardTaped += this.ChangeAttackMode;    // 1
            card.OnCardTaped += this.ChoseCard;           // 2

            RemoveCardFromLobby(card);

            Board.Children.Add(card);
            card.ToDestroy += RemoveCardFromBoard;

            this.ChoseCard(card);
        }

        public void ChangeAttackMode(CardBase card)
        {
            if (card != this.ChosenCard)
                return;

            if (this.ChosenCard != card && this.TurnFaze != Player.TurnFazeEnum.SelectingEnemyCard)
                return;

            switch (this.AttackType)
            {
                case Player.AttackTypeEnum.Attack:
                    if (this.SpecialPoints < 3)
                        return;
                    this.AttackType = Player.AttackTypeEnum.SpecialAttack;
                    break;

                case Player.AttackTypeEnum.SpecialAttack:
                    this.AttackType = Player.AttackTypeEnum.Attack;
                    break;
            }

            this.HighlightChosenCard();
        }

        #region Animations

        /// <summary>
        /// Highlight card animation time.
        /// </summary>
        protected const int highlightCardAnimationTime = 500;

        public virtual void HighlightChosenCard(Action<double, bool> finished = null)
        {
            if (this.ChosenCard != null)
                switch (this.AttackType)
                {
                    case Player.AttackTypeEnum.Attack:
                        new Animation(callback: v => (this.ChosenCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Color.FromRgba(0, 0, v, 0.5),
                            start: 0,
                            end: 1).Commit(this.ChosenCard, "Animation", 16, highlightCardAnimationTime, finished: finished);
                        break;

                    case Player.AttackTypeEnum.SpecialAttack:
                        new Animation(callback: v => (this.ChosenCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Color.FromRgba(v, v, 0, 0.73),
                            start: 0,
                            end: 1).Commit(this.ChosenCard, "Animation", 16, highlightCardAnimationTime, finished: finished);
                        break;
                }
        }

        public void HighlightTargetedCard(Action<double, bool> finished = null)
        {
            if (this.TargetedCard != null)
                new Animation(callback: v => (this.TargetedCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Color.FromRgba(v, 0, 0, 0.5),
                        start: 0,
                        end: 1).Commit(this.TargetedCard, "Animation", 16, highlightCardAnimationTime, finished: finished);
        }

        #endregion
    }
}
