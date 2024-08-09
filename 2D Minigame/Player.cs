using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Minigame
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; }

        public Player(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Health = 100;
        }
    }

}
