using System.Windows;
using System.Windows.Input;

namespace Saper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static RoutedCommand NewGameCommand { get; } = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(NewGameCommand, New_Game));
            
        }

        private void New_Game(object sender, RoutedEventArgs e)
        {
            GRID = Zone.CreateGridForm(GRID, MinesCountLabel);
            Zone.CreateBombs(GRID);

        }

        private void NewPlayer(object sender, RoutedEventArgs e)
        {
            this.Height = 350;
            this.Width = 280;
            Zone.row = 10; Zone.column = 10; Zone.bombs = 20;
            LovePl.IsChecked = false;
            ProfPl.IsChecked = false;
            NewPl.IsChecked = true;
            GRID = Zone.CreateGridForm(GRID, MinesCountLabel);
            Zone.CreateBombs(GRID);
        }

        private void LovePlayer(object sender, RoutedEventArgs e)
        {
            this.Height = 525;
            this.Width = 420;
            Zone.row = 15; Zone.column = 15; Zone.bombs = 45;
            LovePl.IsChecked = true;
            ProfPl.IsChecked = false;
            NewPl.IsChecked = false;
            GRID = Zone.CreateGridForm(GRID, MinesCountLabel);
            Zone.CreateBombs(GRID);
        }

        private void ProfPlayer(object sender, RoutedEventArgs e)
        {
            this.Height = 700;
            this.Width = 560;
            Zone.row = 20; Zone.column = 20; Zone.bombs = 80;
            LovePl.IsChecked = false;
            ProfPl.IsChecked = true;
            NewPl.IsChecked = false;
            GRID = Zone.CreateGridForm(GRID, MinesCountLabel);
            Zone.CreateBombs(GRID);
        }
    }
}
