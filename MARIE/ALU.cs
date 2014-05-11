using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARIE
{
    public class ALU
    {
        public Register16 AC;
        public Register16 MBR;
        private bool[] Command;
        private FullAdder16 FullAdder;
        public bool E;

        public ALU()
        {
            this.Command = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                Command[i] = false;
            }
            this.FullAdder = new FullAdder16();
        }

        public void setCommand(bool[] value)
        {
            for (int i = 0; i < 4; i++)
            {
                Command[i] = value[i];
            }
            executeCommand();
        }

        private void executeCommand()
        {
            if (Command[0])
            {
                if (Command[1])
                {
                    if (Command[2])
                    {
                        if (Command[3])//1111 Opcode
                        {

                        }
                        else//1110 Opcode
                        {

                        }
                    }
                    else
                    {
                        if (Command[3])//1101 Opcode
                        {

                        }
                        else//1100 Opcode
                        {
                            
                        }
                    }
                }
                else
                {
                    if (Command[2])
                    {
                        if (Command[3])//1011 Opcode
                        {
                            
                        }
                        else//1010 Opcode
                        {

                        }
                    }
                    else
                    {
                        if (Command[3])//1001 Opcode
                        {

                        }
                        else//1000 Opcode
                        {

                        }
                    }
                }
            }
            else
            {
                if (Command[1])
                {
                    if (Command[2])
                    {
                        if (Command[3])//0111 Opcode
                        {

                        }
                        else//0110 Opcode
                        {

                        }
                    }
                    else
                    {
                        if (Command[3])//0101 Opcode
                        {

                        }
                        else//0100 Opcode Subt X
                        {
                            FullAdder.setA(AC.output());
                            FullAdder.setB(complement(MBR.output()));
                            FullAdder.setCin(true);
                            AC.Enable = true;
                            AC.input(FullAdder.Output);
                            E = FullAdder.Cout;
                            AC.Enable = false;
                        }
                    }
                }
                else
                {
                    if (Command[2])
                    {
                        if (Command[3])//0011 Opcode Add X
                        {
                            FullAdder.setCin(false);
                            FullAdder.setA(AC.output());
                            FullAdder.setB(MBR.output());
                            AC.Enable = true;
                            AC.input(FullAdder.Output);
                            E = FullAdder.Cout;
                            AC.Enable = false;
                        }
                        else//0010 Opcode
                        {

                        }
                    }
                    else
                    {
                        if (Command[3])//0001 Opcode
                        {

                        }
                        else//0000 Opcode
                        {

                        }
                    }
                }
            }
            
        }

        private bool[] complement(bool[] bits)
        {
            bool[] value = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                value[i] = !bits[i];
            }
            return value;
        }
    }
}
