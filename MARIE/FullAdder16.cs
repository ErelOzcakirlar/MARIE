using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARIE
{
    public class FullAdder16
    {
        private bool Cin;
        private bool[] B;
        private bool[] A;
        public bool[] Output;
        public bool Cout;

        public void setCin(bool value){
            if (Cin != value)
            {
                Cin = value;
                add();
            }
        }

        public void setA(bool[] value)
        {
            for (int i = 0; i < 16; i++)
            {
                A[i] = value[i];
            }
            add();
        }

        public void setB(bool[] value)
        {
            for (int i = 0; i < 16; i++)
            {
                B[i] = value[i];
            }
            add();
        }

        private void add()
        {
            bool AuxCarry = Cin;
            for (int i = 15; i >= 0; i--)
            {
                bool[] result = addBits(A[i], B[i], AuxCarry);
                Output[i] = result[0];
                AuxCarry = result[1];
            }
            Cout = AuxCarry;
        }

        private bool[] addBits(bool a, bool b, bool carryIn)
        {
            bool sum,carryOut;
            sum = xor(xor(a, b), carryIn);
            carryOut = (a && b) || ( xor(a,b) && carryIn );
            bool[] rvalue = new bool[2];
            rvalue[0] = sum;
            rvalue[1] = carryOut;
            return rvalue;
        }

        private bool xor(bool a, bool b)
        {
            return ((!a) && b) || (a && (!b));
        }

        public FullAdder16()
        {
            Cin = false;
            Cout = false;
            A = new bool[16];
            B = new bool[16];
            Output = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                A[i] = false;
                B[i] = false;
                Output[i] = false;
            }
        }
    }
}
