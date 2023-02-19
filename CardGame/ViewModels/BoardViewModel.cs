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

        private Player _computer;

        public Player Computer
        {
            get { return _computer; }
            set { _computer = value; }
        }

        //todo połączyć view model ze szkieletem strony
    }
}
