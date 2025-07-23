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
using System.Runtime.InteropServices;//for debugging

namespace _2D_Minigame
{

    public partial class MainWindow : Window
    {
        private List<(CustomLabel, int, int)> labelList = new List<(CustomLabel, int, int)>();
        private Player player;
        private MediaPlayer mediaPlayer_BackgroundSound; //backgroundSound
        private ImageBrush fieldImageBrush;
        private MediaPlayer coinSoundPlayer; //CoinSound
        private RadialGradientBrush fogBrush;

        private int coinCount = 0;
        private int fieldWidth = 41;
        private int fieldHeight = 19;

        //for debugging
        //[DllImport("kernel32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();


        public MainWindow()
        {
            InitializeComponent();
            Initialize_MediaPlayer_BackgroundSound();
            ShowMainMenu();
            //AllocConsole();
        }

        private void Initialize_FieldImageBrush()
        {
            fieldImageBrush = new ImageBrush();
            fieldImageBrush.ImageSource = new BitmapImage(new Uri("C:\\Users\\pilic\\source\\repos\\2D Minigame\\2D Minigame\\05muronero.jpg", UriKind.Absolute));
        }
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
        private void Initialize_MediaPlayer_BackgroundSound()
        {
            if (isSoundOn && GameGrid.Visibility == Visibility.Visible)
            {
                mediaPlayer_BackgroundSound.Play();
            }
            else
            {

                mediaPlayer_BackgroundSound = new MediaPlayer();
                mediaPlayer_BackgroundSound.Open(new Uri("C:\\Users\\pilic\\source\\repos\\2D Minigame\\2D Minigame\\Sounds\\Coole Background Musik Spannung\\cannontube_loop_medium.mp3"));
                mediaPlayer_BackgroundSound.Volume = 0.05;
                mediaPlayer_BackgroundSound.MediaEnded += MediaPlayer_MediaEnded;
            }

        }
        private void Initialize_CoinSound()
        {
            try
            {
                coinSoundPlayer = new MediaPlayer();
                coinSoundPlayer.Open(new Uri("C:\\Users\\pilic\\source\\repos\\2D Minigame\\2D Minigame\\coinsplash.mp3"));
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error starting the sound: " + ex.Message);
            }
        }

        //Sounds
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
                Console.WriteLine("Error starting the sound: " + ex.Message);
            }
        }
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer_BackgroundSound.Position = TimeSpan.Zero;
            mediaPlayer_BackgroundSound.Play();
        }


        //Add collectable coins
        private void CollectCoin(int x, int y)
        {
            coinCount++;
            CoinCounter.Text = coinCount.ToString();
            if (isSoundOn)
            {
                PlayCoinSound();
            }
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
                Console.WriteLine("Bug - to few coins placed");
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
                else
                {
                }
            }

            foreach (var field in selectedFields)
            {
                PlaceCoinOnField(field);
            }

            int coinFieldCount = selectedFields.Count(field => field.isCoinField);
        }
        private void PlaceCoinOnField(CustomLabel label)
        {
            if (label.isCoinField)
            {
                return;
            }

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
            StartButton.Visibility = Visibility.Collapsed;
            ContinueButton.Visibility = Visibility.Visible;
        }
        private void StartGame()
        {
            InitializeFogBrush();
            Initialize_FieldImageBrush();
            Initialize_CoinSound();

            MenuGrid.Visibility = Visibility.Collapsed;
            GameGrid.Visibility = Visibility.Visible;

            player = new Player(1, 1);

            CreateLabels();
            TestChangeOnCurrentFields();
            PlacePlayer(player.X, player.Y);

            SpawnCoins();

            this.KeyDown += new KeyEventHandler(Window_KeyDown);


            Initialize_MediaPlayer_BackgroundSound();

        }
        private void ShowMainMenu()
        {
            MenuGrid.Visibility = Visibility.Visible;
            GameGrid.Visibility = Visibility.Collapsed;

            Window_Loaded();
        }

        //Moving
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
            if (x < 0 || y < 0 || x >= fieldWidth || y >= fieldHeight)
                return false;

            int index = y * fieldWidth + x;
            var background = labelList[index].Item1.Background;

            return background == fogBrush || background == Brushes.LightGray;
        }
        private void MovePlayer(int newX, int newY)
        {
            int oldIndex = player.Y * fieldWidth + player.X;
            var oldLabel = labelList[oldIndex].Item1;
            oldLabel.Content = "";

            player.X = newX;
            player.Y = newY;

            PlacePlayer(newX, newY);

            MakeAdjacentFieldsVisible(newX, newY);
        }
        private void PlacePlayer(int x, int y)
        {
            int index = y * fieldWidth + x;
            var label = labelList[index].Item1;


            if (label is CustomLabel customLabel && customLabel.isCoinField)
            {
                Console.WriteLine("customlabel.isCoinField?: " + customLabel.isCoinField);
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


                    if (newX >= 0 && newY >= 0 && newX < fieldWidth && newY < fieldHeight)
                    {
                        int index = newY * fieldWidth + newX;
                        var label = labelList[index].Item1;


                        if (label.Background == fogBrush)
                        {
                            label.Background = Brushes.LightGray;
                        }
                    }
                }
            }
        }

        //Field creation
        private void CreateLabels()
        {
            int Spacing = 20;

            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
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
            if (i == j || i + j == fieldWidth - 1 || random.NextDouble() < 0.75) // diagonal?, questionmark-probability: 75% good value || -> old generation
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

                /** removed random invisible generated coins bug **/
                //if (random.NextDouble() < 0.05) // Wahrscheinlichkeit für Münzen: 5%
                //{
                //    label.isCoinField = true;
                //}
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
        private void TestChangeOnCurrentFields()//TODO Überarbeiten
        {
            //Hier muss überarbeitet werden und kontrolliert werden ob diese Methode einen Einfluss auf das Spiel hat oder ein überpleibsel der V2.0 des Spiels war.

            for (int i = 0; i < labelList.Count; i++)
            {
                labelList[i].Item1.Background = fogBrush;
                labelList[i].Item1.isQuestionMarkField = false;
                labelList[i].Item1.Content = "";

                if (i < fieldWidth || i % fieldWidth == 0 || i % fieldWidth == fieldWidth - 1 || i > 544) // !!! warum 544, woher kommt diese zahl??
                {
                    labelList[i].Item1.Background = Brushes.Gray; //weiß nicht mehr was hier gemeint ist!
                }
            }

            labelList[80].Item1.Background = Brushes.Black; // warum item 80??? - was macht diese Zeile?!

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

            int index = startY * fieldWidth + startX;
            labelList[index].Item1.Background = fogBrush;//starting field?
            stack.Push(index);

            while (stack.Count > 0)
            {
                index = stack.Pop();
                int x = index % fieldWidth;
                int y = index / fieldWidth;

                List<int> neighbors = new List<int>();

                if (x >= 2 && labelList[index - 2].Item1.Background == Brushes.Black)
                    neighbors.Add(index - 2);
                if (x < fieldWidth - 2 && labelList[index + 2].Item1.Background == Brushes.Black)
                    neighbors.Add(index + 2);
                if (y >= 2 && labelList[index - fieldWidth * 2].Item1.Background == Brushes.Black)
                    neighbors.Add(index - fieldWidth * 2);
                if (y < fieldHeight - 2 && labelList[index + fieldWidth * 2].Item1.Background == Brushes.Black)
                    neighbors.Add(index + fieldWidth * 2);

                if (neighbors.Count > 0)
                {
                    int nextIndex = neighbors[rand.Next(neighbors.Count)];
                    int nextX = nextIndex % fieldWidth;
                    int nextY = nextIndex / fieldWidth;

                    labelList[nextY * fieldWidth + nextX].Item1.Background = fogBrush;

                    labelList[nextIndex].Item1.Background = fogBrush;
                    stack.Push(nextIndex);

                    if (nextX > x)
                        labelList[y * fieldWidth + x + 1].Item1.Background = fogBrush;
                    else if (nextX < x)
                        labelList[y * fieldWidth + x - 1].Item1.Background = fogBrush;
                    else if (nextY > y)
                        labelList[(y + 1) * fieldWidth + x].Item1.Background = fogBrush;
                    else if (nextY < y)
                        labelList[(y - 1) * fieldWidth + x].Item1.Background = fogBrush;

                    stack.Push(nextIndex);
                }
            }
        }
        
        //New Buttons in Menu 
        private bool isSoundOn = true;
        private void Window_Loaded()
        {
            ConfigureButton(StartButton, "Start", 1451, 323, StartGameButton_Click);
            ConfigureButton(LoadButton, "Load", 1451, 423, LoadGameButton_Click);
            ConfigureButton(OptionsButton, "Options", 1451, 523, OptionsButton_Click);
            ConfigureButton(QuitButton, "Quit", 1451, 623, QuitGameButton_Click);
        }
        private void ConfigureButton(Button button, string content, double left, double top, RoutedEventHandler clickHandler)
        {
            button.Content = content;
            button.Width = 250;
            button.Height = 75;
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Top;
            button.Margin = new Thickness(left, top, 0, 0);
            button.Click += clickHandler;
        }
        //private void StartGameButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Start-Logik
        //}
        private void LoadGameButton_Click(object sender, RoutedEventArgs e)
        {
            //Old Saves
        }
        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            //options
        }
        private void QuitGameButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void SoundToggleButton_Click(object sender, RoutedEventArgs e)
        {
            isSoundOn = !isSoundOn;

            SoundToggleButton.Content = isSoundOn ? "🔊" : "🔇";

            if (isSoundOn && GameGrid.Visibility == Visibility.Visible)
            {
                mediaPlayer_BackgroundSound.Play();
            }
            if (!isSoundOn)
            {
                mediaPlayer_BackgroundSound.Pause();
            }
        }
        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            GameGrid.Visibility = Visibility.Collapsed;
            MenuGrid.Visibility = Visibility.Visible;

            StartButton.Visibility = Visibility.Collapsed;
            ContinueButton.Visibility = Visibility.Visible;

            if (isSoundOn && GameGrid.Visibility == Visibility.Visible)
            {
                mediaPlayer_BackgroundSound.Play();
            }
            if (!isSoundOn)
            {
                mediaPlayer_BackgroundSound.Pause();
            }
            if (GameGrid.Visibility == Visibility.Collapsed)
            {
                mediaPlayer_BackgroundSound.Pause();
            }
        }
        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            GameGrid.Visibility = Visibility.Visible;

            if (isSoundOn && GameGrid.Visibility == Visibility.Visible)
            {
                mediaPlayer_BackgroundSound.Play();
            }
            if (!isSoundOn)
            {
                mediaPlayer_BackgroundSound.Pause();
            }
            if (GameGrid.Visibility == Visibility.Collapsed)
            {
                mediaPlayer_BackgroundSound.Pause();
            }
        }
    }
}
