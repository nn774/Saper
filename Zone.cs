using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Security.Permissions;
using System.Windows.Automation.Provider;

namespace Saper
{
    public static class Zone
    {
        public static int row = 15;
        public static int column = 15;
        public static int bombs = 10;

        static pole[,]? zona;

        public static Grid CreateGridForm(Grid grid)
        {
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
            BitmapImage buttonImage = new BitmapImage(new Uri(@"Resourse/base.png"));
            for (int i = 0; i < row; i++)
            {
                for (int l = 0; l < column; l++)
                {
                    Button button = new Button();
                    button.Name = $"Btn{i}_{l}";
                    button.Margin = new Thickness(0, 0, 0, 0);
                    
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
            return grid;
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
            return grid;
        }
    }
}
