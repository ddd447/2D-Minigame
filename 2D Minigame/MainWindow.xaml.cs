using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

namespace _2D_Minigame
{

    public partial class MainWindow : Window
    {
        private List<(CustomLabel, int, int)> labelList = new List<(CustomLabel, int, int)>();
        private Player player;
        private MediaPlayer mediaPlayer; //backgroundSound
        private ImageBrush fieldImageBrush; 
        private int coinCount = 0;
        private MediaPlayer coinSoundPlayer; //CoinSound
        private RadialGradientBrush fogBrush;

        public MainWindow()
        {
            InitializeComponent();
            ShowMainMenu();//Sobald StartGame  aufgerufen wird Start!
        }

        private void InitializeCoinSound()
        {
            try
            {
                coinSoundPlayer = new MediaPlayer();
                coinSoundPlayer.Open(new Uri("C:\\Users\\pilic\\source\\repos\\2D Minigame\\2D Minigame\\coinsplash.mp3"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden des Sounds: " + ex.Message);
            }
        }
        private void InitializeFieldImageBrush()
        {
            fieldImageBrush = new ImageBrush();
            fieldImageBrush.ImageSource = new BitmapImage(new Uri("C:\\Users\\pilic\\source\\repos\\2D Minigame\\2D Minigame\\05muronero.jpg", UriKind.Absolute));
        }
        private void InitializeMediaPlayer()
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("C:\\Users\\pilic\\source\\repos\\2D Minigame\\2D Minigame\\Sounds\\Coole Background Musik Spannung\\cannontube_loop_medium.mp3")); 
            mediaPlayer.Volume = 0.1; 
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            mediaPlayer.Play(); 
        }//background sound startet hier
        private void InitializeFogBrush()
        {
            fogBrush = new RadialGradientBrush();
            fogBrush.GradientOrigin = new Point(0.5, 0.5);
            fogBrush.Center = new Point(0.5, 0.5);
            fogBrush.RadiusX = 1.0;
            fogBrush.RadiusY = 1.0;

            fogBrush.GradientStops.Add(new GradientStop(Colors.DarkGray, 0.0));
            fogBrush.GradientStops.Add(new GradientStop(Colors.Black, 1.0));
        }

        //Sounds?
        private void PlayCoinSound()
        {
            try
            {
                coinSoundPlayer.Stop(); 
                coinSoundPlayer.Position = TimeSpan.Zero; 
                coinSoundPlayer.Play(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Abspielen des Sounds: " + ex.Message);
            }
        }
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero; 
            mediaPlayer.Play(); 
        }


        //ergänzung Coins
        private void CollectCoin(int x, int y)
        {
            coinCount++;
            CoinCounter.Text = coinCount.ToString(); 
            PlayCoinSound();
        }
        private void SpawnCoins()
        {
           
            var walkableFields = labelList
                .Where(label => label.Item1.Background == fogBrush || label.Item1.Background == Brushes.LightGray)
                .Select(label => label.Item1)
                .OfType<CustomLabel>() 
                .ToList();

            if (walkableFields.Count < 15)
            {
                MessageBox.Show("Problem: Mind. 15 Münzen sollten platziert werden. Ist nicht Möglich da zu wenig betretbare Felder existieren");
                return;
            }

            Random rand = new Random();
            List<CustomLabel> selectedFields = new List<CustomLabel>();

            while (selectedFields.Count < 15)
            {
                int index = rand.Next(walkableFields.Count);
                var selectedField = walkableFields[index];

                if (!selectedFields.Contains(selectedField) && !selectedField.isCoinField)
                {
                    selectedFields.Add(selectedField);
                }
            }

            foreach (var field in selectedFields)
            {
                PlaceCoinOnField(field);
            }
        }
        private void PlaceCoinOnField(CustomLabel label)
        {
            Image coinImage = new Image
            {
                Source = new BitmapImage(new Uri("C:\\Users\\pilic\\source\\repos\\2D Minigame\\2D Minigame\\coin 2.png")),
                Width = 30,
                Height = 30
            };

            label.Content = coinImage;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.isCoinField = true;
        }

        //Start Navigation
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
        private void StartGame()
        {
            InitializeFogBrush();
            InitializeFieldImageBrush();
            InitializeCoinSound();

            MenuGrid.Visibility = Visibility.Collapsed; 
            GameGrid.Visibility = Visibility.Visible; 

            player = new Player(1, 1);

            CreateLabels();
            TestChangeOnCurrentFields();
            PlacePlayer(player.X, player.Y);

            SpawnCoins();

            this.KeyDown += new KeyEventHandler(Window_KeyDown);
            InitializeMediaPlayer();

        }
        private void ShowMainMenu()
        {
            MenuGrid.Visibility = Visibility.Visible; 
            GameGrid.Visibility = Visibility.Collapsed; 
        }

        //Bewegen
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

            if (IsMoveValid(newX, newY))
            {
                MovePlayer(newX, newY);
            }
        }
        private bool IsMoveValid(int x, int y)
        {
            if (x < 0 || y < 0 || x >= 39 || y >= 15)
                return false;

            int index = y * 39 + x;
            var background = labelList[index].Item1.Background;

            return background == fogBrush || background == Brushes.LightGray;
        }
        private void MovePlayer(int newX, int newY)
        {
            int oldIndex = player.Y * 39 + player.X;
            var oldLabel = labelList[oldIndex].Item1;
            oldLabel.Content = "";

            player.X = newX;
            player.Y = newY;

            PlacePlayer(newX, newY);

            MakeAdjacentFieldsVisible(newX, newY);
        }
        private void PlacePlayer(int x, int y)
        {
            int index = y * 39 + x;
            var label = labelList[index].Item1;

            
            if (label is CustomLabel customLabel && customLabel.isCoinField)
            {
                CollectCoin(x, y);
                customLabel.isCoinField = false; 
                label.Content = "";
            }

            label.Content = ""; 

            Image playerImage = new Image
            {
                Source = new BitmapImage(new Uri("C:\\Users\\pilic\\source\\repos\\2D Minigame\\2D Minigame\\iron_giant_2x.png")),
                Width = 30,
                Height = 30
            };
            label.Content = playerImage;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
        }
        private void MakeAdjacentFieldsVisible(int x, int y)
        {
            int radius = 1;

            for (int dy = -radius; dy <= radius; dy++)
            {
                for (int dx = -radius; dx <= radius; dx++)
                {
                    int newX = x + dx;
                    int newY = y + dy;

                    
                    if (newX >= 0 && newY >= 0 && newX < 39 && newY < 15)
                    {
                        int index = newY * 39 + newX;
                        var label = labelList[index].Item1;

                       
                        if (label.Background == fogBrush)
                        {
                            label.Background = Brushes.LightGray;
                        }
                    }
                }
            }
        }

        //Spielfeld erstellung
        private void CreateLabels()
        {
            int Spacing = 20;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 39; j++)
                {
                    CustomLabel label = new CustomLabel();
                    SetLabelPosition(i, j, label);
                    SetPropertiesOfLabel(label, i, j, Spacing);

                    PlaceQuestionMarksOnLabels(label, i, j);

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

                TextBlock textBlock = new TextBlock();
                textBlock.Text = " ?";

                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                label.Content = textBlock;
                label.FontSize = 30;
                label.Foreground = fogBrush;
                label.Name = "label_" + j + "_" + i + "_questionmark";

                label.Background = Brushes.Pink;
                label.isQuestionMarkField = true;
            }
            else
            {
                label.CustomAttribute = "Brushes.Gray";
                label.Content = "    ";
                label.IsEnabled = false;

                label.Foreground = Brushes.Purple;
                label.isQuestionMarkField = false;


                // Füge hier Code hinzu, um Münzen in einigen der begehbaren Felder zu platzieren
                if (random.NextDouble() < 0.05) // Wahrscheinlichkeit für Münzen: 5%
                {
                    label.isCoinField = true;
                }
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
                labelList[i].Item1.Background = fogBrush;
                labelList[i].Item1.isQuestionMarkField = false;
                labelList[i].Item1.Content = "";

                if (i < 39 || i % 39 == 0 || i % 39 == 38 || i > 544)
                {
                    labelList[i].Item1.Background = Brushes.Gray;
                }
            }

            labelList[80].Item1.Background = Brushes.Black;

            GenerateMaze();
        }
        private void GenerateMaze()
        {
            foreach (var label in labelList)
            {
                label.Item1.Background = Brushes.Black;
            }

            Random rand = new Random();
            Stack<int> stack = new Stack<int>();

            int startX = 1;
            int startY = 1;

            int index = startY * 39 + startX;
            labelList[index].Item1.Background = fogBrush;//Startfeld?
            stack.Push(index);

            while (stack.Count > 0)
            {
                index = stack.Pop();
                int x = index % 39;
                int y = index / 39;

                List<int> neighbors = new List<int>();

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
                    int nextIndex = neighbors[rand.Next(neighbors.Count)];
                    int nextX = nextIndex % 39;
                    int nextY = nextIndex / 39;

                    labelList[nextY * 39 + nextX].Item1.Background = fogBrush;

                    labelList[nextIndex].Item1.Background = fogBrush;
                    stack.Push(nextIndex);

                    if (nextX > x)
                        labelList[y * 39 + x + 1].Item1.Background = fogBrush;
                    else if (nextX < x)
                        labelList[y * 39 + x - 1].Item1.Background = fogBrush;
                    else if (nextY > y)
                        labelList[(y + 1) * 39 + x].Item1.Background = fogBrush;
                    else if (nextY < y)
                        labelList[(y - 1) * 39 + x].Item1.Background = fogBrush;

                    stack.Push(nextIndex);
                }
            }
        }
    }
}
