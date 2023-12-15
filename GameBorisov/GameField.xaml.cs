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
using System.Windows.Shapes;

namespace GameBorisov
{
    /// <summary>
    /// Логика взаимодействия для GameField.xaml
    /// </summary>
    public partial class GameField : Window
    {
        private Gameplay game;
        private Timer timer;
        public GameField()
        {
            InitializeComponent();
            game = new Gameplay(Canvas, score);
            timer = new Timer(game, timerLabel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(game != null) 
            { 
                game.ClearBalls();
                game = null;
                game = new Gameplay(Canvas, score);
            }
        }
    }
}
