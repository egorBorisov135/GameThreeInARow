using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GameBorisov
{
    class Timer
    {
        DispatcherTimer dispatcherTimer;
        Gameplay gameplay;
        Label labelTime;
        public int Finish = 0;
        public int Start = 60;
        public Timer(Gameplay gp, Label labelTime)
        {
            this.gameplay = gp;
            this.labelTime = labelTime;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Start -= 1;
            labelTime.Content = Start;
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
            if (Start <= Finish)
            {
                dispatcherTimer.Stop();
                MessageBox.Show("GAME OVER");
                gameplay.ClearBalls();
                gameplay = new Gameplay(gameplay.GetCanvas(), gameplay.GetLabel());
            }
        }
    }
}
