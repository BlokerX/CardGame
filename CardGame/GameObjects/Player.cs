using CardGame.GameObjectsUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		private List<Card> _cards;

		public List<Card> Cards
		{
			get => _cards;
			set => _cards = value;
		}

	}
}
