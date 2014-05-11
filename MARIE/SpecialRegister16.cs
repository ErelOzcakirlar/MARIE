using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MARIE
{
    public class SpecialRegister16:Register16
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

        public SpecialRegister16(Label displayer):base()
        {
            this.DisplayLabel = displayer;
        }
    }
}
