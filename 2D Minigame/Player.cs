using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Minigame
{
    public class Player
    {
        public int X { get; set; } // X-Position auf dem Spielfeld
        public int Y { get; set; } // Y-Position auf dem Spielfeld
        public int Health { get; set; }

        public Player(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Health = 100; // Beispiel: Standard-HP
        }
    }

}
