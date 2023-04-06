using CardGame.GameObjects;

namespace CardGame.ViewModels
{
    internal class BoardViewModel
    {
        private Player _player1;

        public Player Player1
        {
            get { return _player1; }
            set { _player1 = value; }
        }

        private Player _player2;

        public Player Player2
        {
            get { return _player2; }
            set { _player2 = value; }
        }

        //todo połączyć view model ze szkieletem strony
    }
}
