using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Minigame
{

    //aktuell noch nicht in verwendung?
    public class InventoryItem
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Level { get; set; }
        public int LevelPoints { get; set; }
        public string rarity { get; set; }
        public int ItemIdentNumber { get; set; }
        public int Durability { get; set; }
        public int Damage { get; set; }
        public int Healing { get; set; }
        public int IncreaseHitPoints { get; set; }
        public float SellingPriceInGold { get; set; }


        public InventoryItem()
        {
            Level = 0;
            LevelPoints = 0;
            rarity = "common";
            Durability = 0;
            Damage = 0;
            Healing = 0;
            IncreaseHitPoints = 0;
            SellingPriceInGold = 0f;
        }

    }

}
