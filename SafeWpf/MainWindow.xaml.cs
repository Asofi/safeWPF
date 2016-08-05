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

        private void MouseCordinateMethod(object sender, MouseEventArgs e)
        {
            var relativePosition = e.GetPosition(this);
            var point = PointToScreen(relativePosition);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
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

            GetRandomField();

            for (int i = 0; i < fieldSize; i++)
            {

                for (int j = 0; j < fieldSize; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Margin = new Thickness(i*50, j*50, 0, 0);
                    buttons[i, j].Width = 50;
                    buttons[i, j].Height = 50;
                    buttons[i, j].Background = new SolidColorBrush(col[ID[i,j]]);
                    buttons[i, j].Content =ID[i, j];
                    buttons[i, j].VerticalAlignment = VerticalAlignment.Top;
                    buttons[i, j].HorizontalAlignment = HorizontalAlignment.Left;

                    buttons[i, j].Click += new RoutedEventHandler(Clicks);
                    MainGrid.Children.Add(buttons[i, j]);
                    Console.Write("{0} ", ID[j, i]);

                }
                Console.WriteLine();
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

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
