using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using Image = System.Windows.Controls.Image;

namespace _2D_Minigame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<(CustomLabel, int, int)> labelList = new List<(CustomLabel, int, int)>();
        private Player player; // Spielerobjekt

        public MainWindow()
        {
            InitializeComponent();

            player = new Player(1, 1); // Setzt die Startposition auf (1,1)

            //start Generating
            CreateLabels();

            TestChangeOnCurrentFields();
            PlacePlayer(player.X, player.Y); // Spieler initial auf das Spielfeld setzen
            //GenerateMaze();



            this.KeyDown += new KeyEventHandler(Window_KeyDown);


        }

        // Methode zum Platzieren des Spielers auf dem Spielfeld
        private void PlacePlayer(int x, int y)
        {
            int index = y * 39 + x;
            labelList[index].Item1.Content = "X";
            labelList[index].Item1.Foreground = Brushes.Red;
            CustomLabel label = labelList[index].Item1;


            // Erstelle ein Image-Objekt
            Image playerImage = new Image();

            // Setze die Bildquelle (verwende den relativen Pfad oder eine Ressource)
            playerImage.Source = new BitmapImage(new Uri("C:\\Users\\pilic\\source\\repos\\2D Minigame\\2D Minigame\\iron_giant_2x.png"));

            // Optional: Passe die Größe des Bildes an
            playerImage.Width = 30; // z.B. 30x30 Pixel
            playerImage.Height = 30;

            // Setze das Image-Objekt als Content des Labels
            label.Content = playerImage;




            //int index = y * 39 + x;
            //CustomLabel label = labelList[index].Item1;

            // Setze den Spieler auf das neue Feld
            //label.Content = "X";
            //label.Foreground = Brushes.Black;

            // Stelle sicher, dass die Schriftgröße konsistent ist
            //label.FontSize = 30; // Hier kannst du die gewünschte Schriftgröße einstellen

            // Optional: Zentriere das "X"
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            //label.Padding = new Thickness(0, -10, 0, 10);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int newX = player.X;
            int newY = player.Y;

            switch (e.Key)
            {
                case Key.Up:
                    newY--;
                    break;
                case Key.Down:
                    newY++;
                    break;
                case Key.Left:
                    newX--;
                    break;
                case Key.Right:
                    newX++;
                    break;
            }

            // Stelle sicher, dass die neue Position innerhalb des Spielfelds liegt und ein weißes Feld ist
            if (IsMoveValid(newX, newY))
            {
                MovePlayer(newX, newY);
            }
        }

        private bool IsMoveValid(int x, int y)
        {
            if (x < 0 || y < 0 || x >= 39 || y >= 15)
                return false; // Überprüfe die Spielfeldgrenzen

            int index = y * 39 + x;
            return labelList[index].Item1.Background == Brushes.White; // Überprüfe, ob das Ziel ein weißes Feld ist
        }

        private void MovePlayer(int newX, int newY)
        {
            // Entferne das "X" vom aktuellen Feld
            int oldIndex = player.Y * 39 + player.X;
            labelList[oldIndex].Item1.Content = "";

            // Update Spielerposition
            player.X = newX;
            player.Y = newY;

            // Setze das "X" auf das neue Feld
            PlacePlayer(newX, newY);
        }





















        private void CreateLabels()
        {
            int Spacing = 20;

            // Erstellen eines 15x39 Rasters von Labels, die alle von CustomLabel erben
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 39; j++)
                {
                    CustomLabel label = new CustomLabel();
                    SetLabelPosition(i, j, label);
                    SetPropertiesOfLabel(label, i, j, Spacing);

                    PlaceQuestionMarksOnLabels(label, i, j);

                    //AddFieldStatusInSeedList(label);
                    AddLabelsInGrid(label, i, j);
                    AddLabelInList(label, i, j);

                }
            }
        }

        private void SetLabelPosition(int i, int j, CustomLabel label)
        {
            Grid.SetRow(label, i);
            Grid.SetColumn(label, j);
        }
        private void SetPropertiesOfLabel(CustomLabel label, int i, int j, int Spacing)
        {
            double topMargin = i * 42.5 + Spacing + 210;
            double leftMargin = j * 42.5 + Spacing + 110;

            label.Margin = new Thickness(leftMargin, topMargin, 0, 0);
            label.Name = "label_" + j + "_" + i;
        }
        private void PlaceQuestionMarksOnLabels(CustomLabel label, int i, int j)
        {
            Random random = new Random();
            if (i == j || i + j == 38 || random.NextDouble() < 0.75) // diagonal, Fragezeichen-Wahrscheinlichkeit: 75% guter wert
            {
                label.CustomAttribute = "Brushes.LightGray";

                // Fragezeichen als Content des Labels
                TextBlock textBlock = new TextBlock();
                textBlock.Text = " ?";

                // Zentrierte Ausrichtung des TextBlocks
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                // Content des Labels auf den TextBlock setzen
                label.Content = textBlock;
                label.FontSize = 30;
                label.Foreground = Brushes.White;
                label.Name = "label_" + j + "_" + i + "_questionmark";

                label.Background = Brushes.Pink;/*new ImageBrush(imageBrush.Source);*/
                label.isQuestionMarkField = true;
            }
            else
            {
                label.CustomAttribute = "Brushes.Gray";
                label.Content = "    ";
                label.IsEnabled = false;

                label.Foreground = Brushes.Purple;
                label.isQuestionMarkField = false;

            }
        }
        private void AddLabelsInGrid(CustomLabel label, int i, int j)
        {
            myGrid.Children.Add(label);
        }
        private void AddLabelInList(CustomLabel label, int i, int j)
        {
            labelList.Add((label, i, j));
        }

        private void TestChangeOnCurrentFields()
        {
            for (int i = 0; i < labelList.Count; i++)
            {
                labelList[i].Item1.Background = Brushes.White; // Erstmal alle wieder weiß machen
                labelList[i].Item1.isQuestionMarkField = false;
                labelList[i].Item1.Content = "";

                if (i < 39 || i % 39 == 0 || i % 39 == 38 || i > 544) // Grauer Rahmen erstellen
                {
                    labelList[i].Item1.Background = Brushes.Gray;
                }
            }

            labelList[80].Item1.Background = Brushes.Black; // Startfeld erstellen

            GenerateMaze(); // Labyrinth generieren
        }

        private void GenerateMaze()
        {
            // Fülle das Spielfeld mit Wänden (schwarze Felder)
            foreach (var label in labelList)
            {
                label.Item1.Background = Brushes.Black;
            }

            Random rand = new Random();
            Stack<int> stack = new Stack<int>();

            int startX = 1;
            int startY = 1;

            int index = startY * 39 + startX;
            labelList[index].Item1.Background = Brushes.White;
            stack.Push(index);

            while (stack.Count > 0)
            {
                index = stack.Pop();
                int x = index % 39;
                int y = index / 39;

                List<int> neighbors = new List<int>();

                // Finde alle Nachbarn, die noch nicht besucht wurden
                if (x >= 2 && labelList[index - 2].Item1.Background == Brushes.Black)
                    neighbors.Add(index - 2);
                if (x < 37 && labelList[index + 2].Item1.Background == Brushes.Black)
                    neighbors.Add(index + 2);
                if (y >= 2 && labelList[index - 78].Item1.Background == Brushes.Black)
                    neighbors.Add(index - 78);
                if (y < 13 && labelList[index + 78].Item1.Background == Brushes.Black)
                    neighbors.Add(index + 78);

                if (neighbors.Count > 0)
                {
                    // Wähle einen zufälligen Nachbarn
                    int nextIndex = neighbors[rand.Next(neighbors.Count)];
                    int nextX = nextIndex % 39;
                    int nextY = nextIndex / 39;

                    // Entferne die Wand zwischen den Zellen
                    labelList[nextY * 39 + nextX].Item1.Background = Brushes.White;

                    // Markiere die nächste Zelle als besucht und schiebe sie auf den Stack
                    labelList[nextIndex].Item1.Background = Brushes.White;
                    stack.Push(nextIndex);

                    // Markiere die Zelle zwischen den Zellen als besucht
                    if (nextX > x)
                        labelList[y * 39 + x + 1].Item1.Background = Brushes.White;
                    else if (nextX < x)
                        labelList[y * 39 + x - 1].Item1.Background = Brushes.White;
                    else if (nextY > y)
                        labelList[(y + 1) * 39 + x].Item1.Background = Brushes.White;
                    else if (nextY < y)
                        labelList[(y - 1) * 39 + x].Item1.Background = Brushes.White;

                    // Schiebe die nächste Zelle auf den Stack
                    stack.Push(nextIndex);
                }
            }
        }
    }
}
