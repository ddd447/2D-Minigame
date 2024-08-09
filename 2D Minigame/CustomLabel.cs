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
        //erkennung welches Feld welchen status hat 
        /// <summary>
        /// Brushes.LightGray = Weg/Floor
        /// Brushes.Gray = Mauer/Wand
        /// Brushes.Blue = zwischenschritt um doppelte ansteuerung der Gegner bewegungsmethode zu vermeiden
        /// Brushes.Red = Gegner Feld - spawn bei betreten einen Gegner
        /// </summary>
        public string CustomAttribute { get; set; }

        //wenn Item auf einem Feld liegt
        /// <summary>
        /// 
        /// </summary>
        /// 

        public bool isQuestionMarkField { get; set; }
        public bool IsItemAvailable { get; set; }//
        public string Item { get; set; }//welche Art von Item
        public int ItemIdentNumber { get; set; }//welches Item genau //

        //Aktuelle Position aller Gegner erkennbar
        public bool IsEnemyField { get; set; }
        public int MaxEnemyHealth { get; set; }
        public int CurrentEnemyHealth { get; set; }
        public int CurrentEnemyDamage { get; set; }

        public int MaxPlayerHealth { get; set; }
        public int CurrentPlayerHealth { get; set; }
        public int CurrentPlayerDamage { get; set; }

        public int BackgroundFieldNumber { get; set; }

        public InventoryItem CustomItem { get; set; }

        // Weitere benutzerdefinierte Attribute können hier hinzugefügt werden
        public CustomLabel()
        {
            IsEnemyField = false; // Standardmäßig auf false setzen
            IsItemAvailable = false; // Standardmäßig auf false setzen

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
