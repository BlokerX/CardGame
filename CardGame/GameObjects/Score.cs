using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.GameObjects
{
    public class Score
    {
		private int _points;
        /// <summary>
        /// 
        /// </summary>
		public int Points
        {
            get => _points;
            set => _points = value;
        }

        private int _wons;
        /// <summary>
        /// 
        /// </summary>
        public int Wons
        {
            get => _wons;
            set => _wons = value;
        }

        private int _losts;
        /// <summary>
        /// 
        /// </summary>
        public int Losts
        {
            get => _losts;
            set => _losts = value;
        }

        // todo dodać historię rozgrywki

    }
}
