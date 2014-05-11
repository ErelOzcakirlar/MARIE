using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARIE
{
    public class Register16:Register12
    {
        private bool[] LastFour;

        public virtual void input(bool[] incoming)
        {
            if (this.Enable)
            {
                bool[] First12 = new bool[12];
                for (int i = 0; i < 12; i++)
                {
                    First12[i] = incoming[i];
                }
                base.input(First12);
                for (int i = 0; i < 4; i++)
                {
                    LastFour[i] = incoming[i + 12];
                }
            }
        }

        public override bool[] output()
        {
            bool[] outgoing = new bool[16];
            bool[] first12 = base.output();
            for (int i = 0; i < 12; i++)
            {
                outgoing[i] = first12[i];
            }
            for (int i = 0; i < 4; i++)
            {
                outgoing[i + 12] = LastFour[i];
            }
            return outgoing;
        }

        public Register16():base()
        {
            this.LastFour = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                LastFour[i] = false;
            }
        }
    }
}
