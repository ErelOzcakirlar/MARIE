using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MARIE
{
    public class Clock
    {
        private Timer timer;
        private byte _momentSignal;
        private Computer Current;

        public byte MomentSignal { 
            get {
                return this._momentSignal;
                } 
        }

        public Clock()
        {
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += new System.EventHandler(this.timer_Tick);
            _momentSignal = 0;
        }

        public void start(Computer current)
        {
            _momentSignal = 0;
            timer.Start();
            this.Current = current;
        }

        public void stop()
        {
            timer.Stop();
            _momentSignal = 0;
        }

        public void clear()
        {
            _momentSignal = 0;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _momentSignal++;
            if (_momentSignal > 9)
            {
                _momentSignal = 0;
            }
            Current.step();
        }
    }
}
