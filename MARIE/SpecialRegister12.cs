using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MARIE
{
    public class SpecialRegister12:Register12
    {
        public Label DisplayLabel;
        public override void input(bool[] incoming)
        {
            base.input(incoming);
            if (DisplayLabel != null)
            {
                DisplayLabel.Text = Form1.bitsToHex(incoming);
            }
        }

        public override void increment()
        {
            base.increment();
            DisplayLabel.Text = Form1.bitsToHex(this.output());
        }

        public SpecialRegister12(Label displayer):base()
        {
            this.DisplayLabel = displayer;
        }
    }
}
