using System;
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
            Zone.CreateGridForm(GRID, MinesCountLabel);
            Zone.CreateBombs(GRID);
        }

        private void NewPlayer(object sender, RoutedEventArgs e)
        {
            this.Height = 375;
            this.Width = 300;
            Zone.row = 10; Zone.column = 10; Zone.bombs = 10;
            LovePl.IsChecked = false;
            ProfPl.IsChecked = false;
            NewPl.IsChecked = true;
            Zone.CreateGridForm(GRID, MinesCountLabel);
            Zone.CreateBombs(GRID);
        }

        private void LovePlayer(object sender, RoutedEventArgs e)
        {
            this.Height = 562.5;
            this.Width = 450;
            Zone.row = 15; Zone.column = 15; Zone.bombs = 35;
            LovePl.IsChecked = true;
            ProfPl.IsChecked = false;
            NewPl.IsChecked = false;
            Zone.CreateGridForm(GRID, MinesCountLabel);
            Zone.CreateBombs(GRID);
        }

        private void ProfPlayer(object sender, RoutedEventArgs e)
        {
            this.Height = 750;
            this.Width = 600;
            Zone.row = 20; Zone.column = 20; Zone.bombs = 60;
            LovePl.IsChecked = false;
            ProfPl.IsChecked = true;
            NewPl.IsChecked = false;
            Zone.CreateGridForm(GRID, MinesCountLabel);
            Zone.CreateBombs(GRID);
        }
    }
}
