using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameBorisov
{
    class Gameplay
    {
        Label ScoreLabel;
        int score;
        int rows = 7;
        int columns = 7;
        Ball[,] balls;
        public int size;
        Canvas gameField;
        Random rand;
        Ball selectedBall = null;

        public Gameplay(Canvas field, Label scoreLab)
        {
            score = 0;
            ScoreLabel = scoreLab;
            ScoreLabel.Content = score;
            this.gameField = field;
            field.Height = 350;
            this.size = (int)350 / rows;
            balls = new Ball[rows, columns];
            rand = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    balls[i, j] = new Ball(rand, field, i, j, size, this);
                }
            }
        }
        public void ClearBalls()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    gameField.Children.Remove(balls[i, j].img);
                }
            }
            score = 0;
            ScoreLabel.Content = score;
        }
        public Canvas GetCanvas()
        {
            return gameField;
        }
        public Label GetLabel() 
        {
            return ScoreLabel;
        }
        private int GetRow(Image im)
        {
            return (int)Canvas.GetTop(im) / size;
        }

        private int GetColumn(Image im)
        {
            return (int)Canvas.GetLeft(im) / size;
        }
        public void SwapBalls(int row1, int col1, int row2, int col2)
        {
            Ball temp = balls[row1, col1];
            balls[row1, col1] = balls[row2, col2];
            balls[row2, col2] = temp;
            Canvas.SetLeft(balls[row1, col1].img, col1 * size);
            Canvas.SetTop(balls[row1, col1].img, row1 * size);
            Canvas.SetLeft(balls[row2, col2].img, col2 * size);
            Canvas.SetTop(balls[row2, col2].img, row2 * size);
        }

        public HashSet<Tuple<int, int>> FindMatches()
        {
            HashSet<Tuple<int, int>> matches = new HashSet<Tuple<int, int>>();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns - 2; col++)
                {
                    if (balls[row, col].balltype == balls[row, col + 1].balltype && balls[row, col].balltype == balls[row, col + 2].balltype)
                    {
                        matches.Add(new Tuple<int, int>(row, col));
                        matches.Add(new Tuple<int, int>(row, col + 1));
                        matches.Add(new Tuple<int, int>(row, col + 2));
                    }
                }
            }

            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows - 2; row++)
                {
                    if (balls[row, col].balltype == balls[row + 1, col].balltype && balls[row, col].balltype == balls[row + 2, col].balltype)
                    {
                        matches.Add(new Tuple<int, int>(row, col));
                        matches.Add(new Tuple<int, int>(row + 1, col));
                        matches.Add(new Tuple<int, int>(row + 2, col));
                    }
                }
            }

            return matches;
        }

        public void OnBallClicked(Ball ball)
        {
            if (selectedBall == null)
            {
                selectedBall = ball;
            }
            else
            {
                if (AreAdjacent(selectedBall, ball) && TrySwapBalls(GetRow(selectedBall.img), GetColumn(selectedBall.img), GetRow(ball.img), GetColumn(ball.img)))
                {
                    selectedBall = null;
                }
                else
                {
                    selectedBall = ball;
                }
            }
        }

        private bool AreAdjacent(Ball ball1, Ball ball2)
        {
            int row1 = GetRow(ball1.img);
            int col1 = GetColumn(ball1.img);
            int row2 = GetRow(ball2.img);
            int col2 = GetColumn(ball2.img);
            return (row1 == row2 && Math.Abs(col1 - col2) == 1) || (col1 == col2 && Math.Abs(row1 - row2) == 1);
        }

        public void RemoveMatches(HashSet<Tuple<int, int>> matches)
        {
            foreach (var match in matches)
            {
                gameField.Children.Remove(balls[match.Item1, match.Item2].img);
                balls[match.Item1, match.Item2] = new Ball(rand, gameField, match.Item1, match.Item2, size, this);
            }
        }

        public bool TrySwapBalls(int row1, int col1, int row2, int col2)
        {
            SwapBalls(row1, col1, row2, col2);
            HashSet<Tuple<int, int>> matches = FindMatches();
            if (matches.Count > 0)
            {
                RemoveMatches(matches);
                score += matches.Count;
                ScoreLabel.Content = score;
                return true;
            }
            else
            {
                SwapBalls(row1, col1, row2, col2);
                return false;
            }
        }
    }
}
