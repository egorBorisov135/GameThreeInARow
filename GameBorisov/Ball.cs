using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GameBorisov
{
    class Ball
    {
        public Image img;
        public int size;
        public List<string> balls = new List<string>();
        public string balltype;

        public Ball(Random rand, Canvas canvas, int row, int column, int size, Gameplay gameDeck)
        {
            balls.Add("football");
            balls.Add("basketball");
            balls.Add("valleyball");
            balls.Add("tennis");
            balls.Add("rugby");
            balls.Add("baseball");
            balltype = balls[rand.Next(0,6)];
            var path = @"pack://application:,,,/Resources/" + balltype + ".png";
            this.size = size;
            img = new Image
            {
                Width = size,
                Height = size,
                Margin = new Thickness(0),
                Source = new BitmapImage(new Uri(path)),
                Stretch = Stretch.Fill,
            };
            img.MouseLeftButtonDown += (s, e) => gameDeck.OnBallClicked(this);
            canvas.Children.Add(img);
            Canvas.SetLeft(img, size * column);
            Canvas.SetTop(img, size * row);
            Canvas.SetZIndex(img, 1);
        }
    }
}
