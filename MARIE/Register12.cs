using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARIE
{
    public abstract class Register12
    {
        public bool Enable;
        private bool[] bits;

        public virtual void input(bool[] incoming){
            if(this.Enable){
                for(int i=0;i < 12;i++){
                    bits[i] = incoming[i];
                }
            }
        }

        public virtual bool[] output()
        {
            bool[] outgoing = new bool[12];
            for (int i = 0; i < 12; i++)
            {
                outgoing[i] = bits[i];
            }
            return outgoing;
        }

        public virtual void increment()
        {
            bool AuxCarry = true;
            for (int i = 11; i >= 0; i--)
            {
                bool[] result = addBits(bits[i], false, AuxCarry);
                bits[i] = result[0];
                AuxCarry = result[1];
            }
        }

        private bool[] addBits(bool a, bool b, bool carryIn)
        {
            bool sum, carryOut;
            sum = xor(xor(a, b), carryIn);
            carryOut = (a && b) || (xor(a, b) && carryIn);
            bool[] rvalue = new bool[2];
            rvalue[0] = sum;
            rvalue[1] = carryOut;
            return rvalue;
        }

        private bool xor(bool a, bool b)
        {
            return ((!a) && b) || (a && (!b));
        }

        public Register12()
        {
            Enable = false;
            bits = new bool[12];
            for (int i = 0; i < 12; i++)
            {
                bits[i] = false;
            }
        }
    }
}
