﻿using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Security.Permissions;

namespace Saper
{
    public static class Zone
    {
        public static int row = 15;
        public static int column = 15;
        public static int bombs = 35;
        public static Label CounMines;
        public static int countMines = 0;
        public static int CountOfNotMines = 0;
        public static bool lose = false;

        static pole[,]? zona;

        public static void CreateGridForm(Grid grid, Label la)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            la.Content = bombs;
            CounMines = la;
            CountOfNotMines = row * column - bombs;

            zona = new pole[row, column];
            for (int i = 0; i < row; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                grid.RowDefinitions.Add(rowDef);
            }

            for (int i = 0; i < column; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                grid.ColumnDefinitions.Add(colDef);
            }
            BitmapImage buttonImage = new BitmapImage(new Uri("pack://application:,,,/Resources/base.png"));
            for (int i = 0; i < row; i++)
            {
                for (int l = 0; l < column; l++)
                {
                    Button button = new Button {Name = $"_{i}_{l}", Margin = new Thickness(0, 0, 0, 0)};
                    button.Click += BtnCLick;
                    button.MouseRightButtonDown += BtnRightCLick;
                    button.Content = new Image
                    {
                        Source = buttonImage,
                        Stretch = Stretch.Fill

                    };
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, l);
                    grid.Children.Add(button);
                    zona[i, l] = new pole{ button = button};
                }
            }
            GC.Collect();
        }

        public static Grid CreateBombs(Grid grid)
        {
            Random rnd = new Random();
            for (int i = 0; i < bombs; i++)
            {
                int x = rnd.Next(0, row);
                int y = rnd.Next(0, column);
                if (zona[x, y].isBomb == true)
                {
                    i--;
                    continue;
                }
                zona[x, y].isBomb = true;
            }
            CalculateBombsNear();
            return grid;
        }

        public static void CalculateBombsNear()
        {
            for (int i = 0; i < zona.GetLength(0); i++)
                for (int l = 0; l < zona.GetLength(1); l++)
                    if (!zona[i, l].isBomb)
                        zona[i,l].bombsNear = HowManyBombsNear(i,l);
        }

        public static int HowManyBombsNear(int x, int y)
        {
            int count = 0;
            if (x != row-1 && y != column-1)
                if (zona[x + 1 , y + 1].isBomb)
                    count++;

            if (x != 0 && y != 0)
                if (zona[x - 1, y - 1].isBomb)
                    count++;

            if (x != row- 1 && y != 0)
                if (zona[x + 1, y - 1].isBomb)
                    count++;

            if (x != 0 && y != column - 1)
                if (zona[x - 1, y + 1].isBomb)
                    count++;

            if (x != row - 1)
                if (zona[x + 1, y].isBomb)
                    count++;

            if (x != 0)
                if (zona[x - 1, y].isBomb)
                    count++;

            if (y != column - 1)
                if (zona[x, y + 1].isBomb)
                    count++;

            if (y != 0)
                if (zona[x, y - 1].isBomb)
                    count++;
            return count;
        }

        private static void BtnCLick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Move(btn);
            if (CountOfNotMines == 0 && !lose)
                Win();
        }

        private static void BtnRightCLick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string[] line = btn.Name.Split('_');
            int x = int.Parse(line[1]);
            int y = int.Parse(line[2]);
            if (!zona[x, y].isOpened)
            {
                string uri = "pack://application:,,,/Resources/";
                if (zona[x, y].isChecked)
                {
                    uri += "base.png";
                    zona[x, y].isChecked = false;
                    CounMines.Content = int.Parse(CounMines.Content.ToString()) + 1;
                }
                else
                {
                    uri += "flag.png";
                    zona[x,y].isChecked = true;
                    CounMines.Content = int.Parse(CounMines.Content.ToString()) - 1;
                }
                btn.Content = new Image
                {
                    Source = new BitmapImage(new Uri(uri)),
                    Stretch = Stretch.Fill

                };
                if (countMines == 0 && !lose)
                    checkWinWithFlags();
            }
        }

        private static void checkWinWithFlags()
        {
            bool iswin = true;
            for (int i = 0; i < zona.GetLength(0); i++)
            {
                for (int l = 0; l < zona.GetLength(1); l++)
                {
                    if (zona[i, l].isBomb && !zona[i, l].isChecked)
                    {
                        iswin = false;
                        break;
                    }
                }
            }
            if (iswin)
                Win();
        }

        private static void Move(object sender)
        {
            Button btn = sender as Button;
            string[] line = btn.Name.Split('_');
            int x = int.Parse(line[1]);
            int y = int.Parse(line[2]);
            if (!zona[x, y].isOpened)
            {
                if (zona[x, y].isBomb)
                    GameOver(btn);

                else if (!zona[x, y].isChecked)
                {
                    btn.Content = new Image
                    {
                        Source = new BitmapImage(new Uri($"pack://application:,,,/Resources/{zona[x, y].bombsNear}.png")),
                        Stretch = Stretch.Fill
                    };
                    btn.IsEnabled = false;
                    zona[x, y].isOpened = true;
                    if (zona[x, y].bombsNear == 0)
                        OpenNear0(x, y);
                }
                CountOfNotMines--;
            }
        }

        private static void OpenNear0(int x, int y)
        {
            if (x != row - 1 && y != column - 1)
                if (!zona[x + 1, y + 1].isOpened)
                    Move(zona[x + 1,y + 1].button);

            if (x != 0 && y != 0)
                if (!zona[x - 1, y - 1].isOpened)
                    Move(zona[x - 1, y - 1].button);

            if (x != row - 1 && y != 0)
                if (!zona[x + 1, y - 1].isOpened)
                    Move(zona[x + 1, y - 1].button);

            if (x != 0 && y != column - 1)
                if (!zona[x - 1, y + 1].isOpened)
                    Move(zona[x - 1, y + 1].button);

            if (x != row - 1)
                if (!zona[x + 1, y].isOpened)
                    Move(zona[x + 1, y].button);

            if (x != 0)
                if (!zona[x - 1, y].isOpened)
                    Move(zona[x - 1, y].button);

            if (y != column - 1)
                if (!zona[x, y + 1].isOpened)
                    Move(zona[x, y + 1].button);

            if (y != 0)
                if (!zona[x, y - 1].isOpened)
                    Move(zona[x, y - 1].button);
        }

        private static void GameOver(Button btn)
        { 
            for (int i = 0; i < zona.GetLength(0); i++)
            {
                for (int l = 0; l < zona.GetLength(1); l++)
                {
                    if (!zona[i, l].isOpened)
                    {
                        string url = "pack://application:,,,/Resources/";

                        if (zona[i, l].isBomb && !zona[i, l].isChecked)
                            url += "bomb.png";

                        else if (!zona[i, l].isBomb && zona[i, l].isChecked)
                            url += "xflag.png";

                        else if (zona[i, l].isChecked && zona[i, l].isBomb)
                            url += "flag.png";

                        else
                            url += "base.png";
                        zona[i, l].button.Content = new Image
                        {
                            Source = new BitmapImage(new Uri(url)),
                            Stretch = Stretch.Fill
                        };
                        zona[i, l].isOpened = true;
                    }
                }
            }
            btn.Content = new Image
            {
                Source = new BitmapImage(new Uri($"pack://application:,,,/Resources/xbomb.png")),
                Stretch = Stretch.Fill
            };
            lose = false;
        }

        private static void Win()
        {
            MessageBox.Show("U win!!");
        }
    }
}
