using System;
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

namespace SafeWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[,] buttons;
        private Color[] col = new Color[2];

        private int fieldSize;
        Random rnd = new Random();

        int countI;
        int countJ;
        int[,] ID;

        public MainWindow()
        {
            InitializeComponent();
            col[0] = Colors.Red;
            col[1] = Colors.Green;

            this.textBox.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);

        }
        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
                if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (buttons != null)
                for (int i = 0; i < fieldSize; i++)
                    for (int j = 0; j < fieldSize; j++)
                    {
                        MainGrid.Children.Remove(buttons[i, j]);
                    }

                        CreateButtons();
        }

        private void CreateButtons()
        {
            fieldSize = Convert.ToInt32(textBox.Text);
            buttons = new Button[fieldSize, fieldSize];
            ID = new int[fieldSize, fieldSize];
            int columnCount = 0;
            bool isEven = false;

            //Creating field of game

            //GetRandomField();

            for (int i = 0; i < fieldSize; i++)
                for (int j = 0; j < fieldSize; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Margin = new Thickness(i * 50, j * 50, 0, 0);
                    buttons[i, j].Width = 50;
                    buttons[i, j].Height = 50;
                    buttons[i, j].Background = new SolidColorBrush(col[ID[i, j]]);
                    buttons[i, j].Content = ID[i, j];
                    buttons[i, j].VerticalAlignment = VerticalAlignment.Top;
                    buttons[i, j].HorizontalAlignment = HorizontalAlignment.Left;

                    buttons[i, j].Click += new RoutedEventHandler(Clicks);
                    MainGrid.Children.Add(buttons[i, j]);
                }
                    ///////////////////////////////////////////////////////////////////

                    if (fieldSize % 2 != 0)
                    {
                        for (int i = 0; i < fieldSize - 1; i++)
                            for (int j = 0; j < fieldSize - 1; j++)
                            {
                                ID[i, j] = rnd.Next(0, 2);
                                buttons[i, j].Background = new SolidColorBrush(col[ID[i, j]]);
                                buttons[i, j].Content = ID[i, j];

                                if (i == 0 && ID[i, j] == 1)
                                    columnCount += 1;

                                if (columnCount % 2 == 0)
                                    isEven = true;
                                else
                                    isEven = false;
                            }

                        //Создается последняя строка на основании четности каждого столба
                        for (int i = 0; i < fieldSize; i++)
                        {
                            columnCount = 0;
                            for (int j = 0; j < fieldSize - 1; j++)
                            {
                                if (ID[i, j] == 1)
                                    columnCount += 1;
                            }
                            if (columnCount % 2 == 0)
                            {
                                if (isEven)
                                    ID[i, fieldSize - 1] = 0;
                                else
                                    ID[i, fieldSize - 1] = 1;
                            }
                            else
                            {
                                if (isEven)
                                    ID[i, fieldSize - 1] = 1;
                                else
                                    ID[i, fieldSize - 1] = 0;
                            }
                            buttons[i, fieldSize - 1].Background = new SolidColorBrush(col[ID[i, fieldSize - 1]]);
                            buttons[i, fieldSize - 1].Content = ID[i, fieldSize - 1];
                    }

                        ////Создается последний столбец на основании четности каждой строки
                        for (int j = 0; j < fieldSize; j++)
                        {
                            columnCount = 0;
                            for (int i = 0; i < fieldSize - 1; i++)
                            {
                                if (ID[i, j] == 1)
                                    columnCount += 1;
                            }
                            if (columnCount % 2 == 0)
                            {
                                if (isEven)
                                    ID[fieldSize - 1, j] = 0;
                                else
                                    ID[fieldSize - 1, j] = 1;
                            }
                            else
                            {
                                if (isEven)
                                    ID[fieldSize - 1, j] = 1;
                                else
                                    ID[fieldSize - 1, j] = 0;
                            }
                            buttons[fieldSize - 1, j].Background = new SolidColorBrush(col[ID[fieldSize - 1, j]]);
                            buttons[fieldSize - 1, j].Content = ID[fieldSize - 1, j];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < fieldSize; i++)
                            for (int j = 0; j < fieldSize; j++)
                            {
                                ID[i, j] = rnd.Next(0, 2);
                                buttons[i, j].Background = new SolidColorBrush(col[ID[i, j]]);
                                buttons[i, j].Content = ID[i, j];
                            }
                    }

                }

        void Clicks (object sender, RoutedEventArgs e)
        {
            int j = (int)((Button)sender).Margin.Top / (int)((Button)sender).Height;
            int i = (int)((Button)sender).Margin.Left / (int)((Button)sender).Width;
            //flip(i, j);

            for (countI = 0; countI < fieldSize; countI++)
            {
                flip(countI, j);
            }
            for (countJ = 0; countJ < fieldSize; countJ++)
            {
                if (countJ != j)
                    flip(i, countJ);
            } 

            Console.WriteLine();
            for (i = 0; i < fieldSize; i++)
            {

                for (j = 0; j < fieldSize; j++)
                {
                    Console.Write("{0} ", ID[j, i]);

                }
                Console.WriteLine();
            }

            checkWin();
        }

        //Функция смены ручки
        void flip(int i, int j)
        {
            
            int newID = 1 - ID[i, j];
            ID[i, j] = newID;
            buttons[i, j].Background = new SolidColorBrush(col[ID[i, j]]);
            buttons[i, j].Content = ID[i, j];

        }

        void GetRandomField()
        {
            for (int i = 0; i < fieldSize; i++)
                for (int j = 0; j < fieldSize; j++)
                {
                    ID[i, j] = rnd.Next(0, 2);
                }
        }

        private void btn_MouseClick (object sender, MouseEventArgs e)
        {
            var relativePosition = e.GetPosition(this);
            var point = PointToScreen(relativePosition);
            int j = (int)point.Y / 50;
            int i = (int)point.X / 50;
            Console.WriteLine("Button" + i + j);
        }

        private bool checkWin()
        {
            int prevI = 0;
            int prevJ = 0;
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 1; j < fieldSize; j++)
                {
                    if (ID[i, j] != ID[prevI, prevJ])
                        return false;
                    prevI = i;
                    prevJ = j;
                }
            }

            MessageBox.Show("WON!");
            return true;

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
