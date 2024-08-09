using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Minigame
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Level { get; set; }//angedacht als level der waffe um die stärke darzustellen
        public int LevelPoints { get; set; }//angedacht mit evtl gesammelter waffenep upgrades freizuschalten
        public string rarity { get; set; }
        public int ItemIdentNumber { get; set; }//angedacht um eine unverwechselbarkeit für das programm zu schaffen und so den aktuellen zustand jeder waffe anhand der nummer zu identifizieren
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
