using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _2D_Minigame
{
    public class CustomLabel : Label
    {
       
        public string CustomAttribute { get; set; }


        public bool isQuestionMarkField { get; set; }
        public bool IsItemAvailable { get; set; }
        public string Item { get; set; }
        public int ItemIdentNumber { get; set; }

        
        public bool IsEnemyField { get; set; }
        public int MaxEnemyHealth { get; set; }
        public int CurrentEnemyHealth { get; set; }
        public int CurrentEnemyDamage { get; set; }

        public int MaxPlayerHealth { get; set; }
        public int CurrentPlayerHealth { get; set; }
        public int CurrentPlayerDamage { get; set; }

        public int BackgroundFieldNumber { get; set; }

        public InventoryItem CustomItem { get; set; }

        
        public CustomLabel()
        {
            IsEnemyField = false; 
            IsItemAvailable = false; 

            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;

            Height = 42.5;
            Width = 42.5;
            CustomAttribute = "Custon value";
            BorderBrush = Brushes.Black;
            BorderThickness = new Thickness(1);
            IsItemAvailable = false;

        }
    }
}
