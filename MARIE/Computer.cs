using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MARIE
{
    public class Computer
    {
        private byte InstructionSignal;
        private bool[] Instruction;

        public RAM Ram;
        public ALU Alu;
        public Register12 MAR;
        public Register12 PC;
        public Register16 AC;
        public Register16 MBR;
        public Register16 IR;
        public Register16 InReg;
        public Register16 OutReg;
        private DataGridView MemoryDisplayer;
        private Clock CLK;

        public Computer(Label MARLabel, Label PCLabel, Label ACLabel, Label MBRLabel, Label IRLabel, Label InRegLabel, Label OutRegLabel)
        {
            Instruction = new bool[4];
            Ram = new RAM();
            Alu = new ALU();
            MAR = new SpecialRegister12(MARLabel);
            PC = new SpecialRegister12(PCLabel);
            AC = new SpecialRegister16(ACLabel);
            MBR = new SpecialRegister16(MBRLabel);
            IR = new SpecialRegister16(IRLabel);
            InReg = new SpecialRegister16(InRegLabel);
            OutReg = new SpecialRegister16(OutRegLabel);
            CLK = new Clock();
            Alu.AC = this.AC;
            Alu.MBR = this.MBR;
        }

        public void run(int origin, DataGridView displayer)
        {
            PC.Enable = true;
            PC.input(getLast12(Form1.decToBits(origin.ToString())));
            PC.Enable = false;
            this.MemoryDisplayer = displayer;
            CLK.start(this);
        }

        public void step()
        {
            switch (CLK.MomentSignal)
            {
                case 1://FETCH
                    this.MAR.Enable = true; //MAR <-- PC
                    this.MAR.input(this.PC.output());
                    this.MAR.Enable = false;

                    this.IR.Enable = true; //IR <-- M[MAR]
                    this.IR.input(this.Ram.Read(this.MAR.output()));
                    this.IR.Enable = false;

                    this.PC.increment();// PC <-- PC + 1

                    break;

                case 2://DECODE
                    bool[] CurrentIR = IR.output();
                    bool[] OperandAddress = new bool[12];
                    for (int i = 0; i < 16; i++)
                    {
                        if (i < 4)
                        {
                            Instruction[i] = CurrentIR[i];//Decode IR(15-12)
                            
                        }
                        else
                        {
                            OperandAddress[i - 4] = CurrentIR[i];
                        }
                    }
                    InstructionSignal = Form1.decode4_16(Instruction);
                    this.MAR.Enable = true;
                    this.MAR.input(OperandAddress);//MAR <-- IR(11-0)
                    this.MAR.Enable = false;

                    break;

                case 3://GET OPERAND
                    this.MBR.Enable = true;
                    this.MBR.input(this.Ram.Read(this.MAR.output()));//MBR <-- M[MAR]
                    this.MBR.Enable = false;
                    break;

                case 4:
                    switch (this.InstructionSignal)
                    {
                        case 0: //JnS X T4
                            this.MBR.Enable = true;
                            this.MBR.input(this.PC.output()); //MBR <-- PC
                            this.MBR.Enable = false;
                            break;

                        case 1: //Load X T4
                            this.AC.Enable = true;
                            this.AC.input(this.MBR.output());//AC <-- MBR
                            this.AC.Enable = false;
                            this.CLK.clear();//next clock cycle
                            break;

                        case 2://Store X T4
                            this.MBR.Enable = true;
                            this.MBR.input(this.AC.output());//MBR <-- AC
                            this.MBR.Enable = false;
                            break;

                        case 3://Add X T4
                            this.Alu.setCommand(this.Instruction);//AC <-- AC + MBR
                            this.CLK.clear();//next clock cycle
                            break;

                        case 4://Subt X T4
                            this.Alu.setCommand(this.Instruction);//AC <-- AC - MBR
                            this.CLK.clear();//next clock cycle
                            break;

                        case 5://Input T4
                            this.AC.Enable = true;
                            this.AC.input(this.InReg.output());//AC <-- InReg
                            this.AC.Enable = false;
                            this.CLK.clear();//next clock cycle
                            break;

                        case 6://Output T4
                            this.OutReg.Enable = true;
                            this.OutReg.input(this.AC.output());//OutReg --> AC
                            this.OutReg.Enable = false;
                            this.CLK.clear();//next clock cycle
                            break;

                        case 7://Halt T4
                            this.CLK.stop();
                            break;

                        case 8://Skipcond T4
                            bool[] ContentMBR = this.MBR.output();
                            bool[] ContentAC = this.AC.output();
                            if (!ContentMBR[4] && !ContentMBR[5])//MBR(11-10)=00 
                            {
                                if (ContentAC[0])
                                {
                                    this.PC.increment();// AC < 0
                                }
                            }
                            else if (!ContentMBR[4] && ContentMBR[5])//MBR(11-10)=01
                            {
                                bool value = true;
                                for (int i = 0; i < 16; i++)
                                {
                                    if (ContentAC[i])
                                    {
                                        value = false;
                                        break;
                                    }
                                }
                                if (value)
                                {
                                    this.PC.increment();// AC = 0
                                }
                            }
                            else if (ContentMBR[4] && !ContentMBR[5])//MBR(11-10)=10
                            {
                                if (!ContentAC[0])
                                {
                                    bool value = false;
                                    for (int i = 1; i < 16; i++)
                                    {
                                        if (ContentAC[i])
                                        {
                                            value = true;
                                            break;
                                        }
                                    }
                                    if (value)
                                    {
                                        this.PC.increment();// AC > 0
                                    }
                                }
                            }
                            this.CLK.clear();//next clock cycle
                            break;

                        case 9://Jump X T4
                            this.PC.Enable = true;
                            this.PC.input(this.MAR.output()); // PC <-- IR(11-0) (MAR)
                            this.PC.Enable = false;
                            this.CLK.clear();//next clock cycle
                            break;

                        case 10://Clear T4
                            bool[] FullZero = new bool[16];
                            for (int i = 0; i < 16; i++)
                            {
                                FullZero[i] = false;
                            }
                            this.AC.Enable = true;
                            this.AC.input(FullZero);
                            this.AC.Enable = false;
                            this.CLK.clear();//next clock cycle
                            break;

                        case 11://AddI X T4
                            this.MAR.Enable = true;
                            this.MAR.input(getLast12(this.MBR.output()));//MAR <-- MBR
                            this.MAR.Enable = false;
                            break;

                        case 12://JumpI X T4
                            this.PC.Enable = true;
                            this.PC.input(getLast12(this.MBR.output()));//PC <-- MBR
                            this.PC.Enable = false;
                            this.CLK.clear();//next clock cycle
                            break;

                        case 13://StoreI X T4
                            this.MAR.Enable = true;
                            this.MAR.input(getLast12(this.MBR.output()));//MAR <-- MBR
                            this.MAR.Enable = false;
                            break;

                        case 14://LoadI X T4
                            this.MAR.Enable = true;
                            this.MAR.input(getLast12(this.MBR.output()));//MAR <-- MBR
                            this.MAR.Enable = false;
                            break;
                    }
                    break;
                case 5:
                    switch (this.InstructionSignal)
                    {
                        case 0: //JnS X T5
                            this.Ram.EnableWrite(this.MAR.output());
                            this.Ram.Write(this.MBR.output()); //M[MAR] <-- MBR
                            //t6 mbr <-- mar
                            break;
                        
                        case 2://Store X T5
                            this.Ram.EnableWrite(this.MAR.output());
                            this.Ram.Write(this.MBR.output()); //M[MAR] <-- MBR
                            this.CLK.clear();//next clock cycle
                            break;
                        
                        case 11://AddI X T5
                            this.MBR.Enable = true;
                            this.MBR.input(this.Ram.Read(this.MAR.output()));//MBR <-- M[MAR]
                            this.MBR.Enable = false;
                            break;
                        
                        case 13://StoreI X T5
                            this.MBR.Enable = true;
                            this.MBR.input(this.AC.output());//MBR <-- AC
                            this.MBR.Enable = false;
                            break;

                        case 14://LoadI X T5
                            this.MBR.Enable = true;
                            this.MBR.input(this.Ram.Read(this.MAR.output()));//MBR <-- M[MAR]
                            this.MBR.Enable = false;
                            break;
                    }
                    break;
                case 6:
                    switch (this.InstructionSignal)
                    {
                        case 0: //JnS X T6
                            bool[] Extended = new bool[16];
                            bool[] ContentMAR = this.MAR.output();
                            for (int i = 0; i < 12; i++)
                            {
                                if (i < 4)
                                {
                                    Extended[i] = false;
                                }
                                Extended[i + 4] = ContentMAR[i];
                            }
                            this.MBR.Enable = true;
                            this.MBR.input(Extended);//MBR <-- MAR (X)
                            this.MBR.Enable = false;
                            break;

                        case 11://AddI X T6
                            this.Alu.setCommand(this.Instruction);//AC <-- AC + MBR
                            this.CLK.clear();//next clock cycle
                            break;

                        case 13://StoreI X T6
                            this.Ram.EnableWrite(this.MAR.output());
                            this.Ram.Write(this.MBR.output()); //M[MAR] <-- MBR
                            this.CLK.clear();//next clock cycle
                            break;

                        case 14://LoadI X T6
                            this.AC.Enable = true;
                            this.AC.input(this.MBR.output());//AC <-- MBR
                            this.AC.Enable = false;
                            this.CLK.clear();//next clock cycle
                            break;
                    }
                    break;
                case 7:
                    if (this.InstructionSignal == 0)// JnS X T7
                    {
                        bool[] FullZero = new bool[16];
                        for (int i = 0; i < 16; i++)
                        {
                            FullZero[i] = false;
                        }
                        this.AC.Enable = true;
                        this.AC.input(FullZero);
                        this.AC.Enable = false;
                        this.AC.increment(); // AC <-- 1
                    }
                    break;
                case 8:
                    if (this.InstructionSignal == 0)// JnS X T8
                    {
                        this.Alu.setCommand(this.Instruction);//AC <-- AC + MBR
                    }
                    break;
                case 9:
                    if (this.InstructionSignal == 0)// JnS X T9
                    {
                        this.PC.Enable = true;
                        this.PC.input(this.AC.output());//PC <-- AC
                        this.PC.Enable = false;
                    }
                    break;
            }
            List<KeyValuePair<String, String>> memory = new List<KeyValuePair<String, String>>();
            this.printMemory(memory);
            MemoryDisplayer.DataSource = memory;
        }

        public void load(List<bool[]> program, int origin)
        {
            for (int i = 0; i < program.Count; i++)
            {
                this.Ram.EnableWrite(getLast12(Form1.decToBits((origin + i).ToString())));
                this.Ram.Write(program[i]);
            }
        }

        public void printMemory(List<KeyValuePair<String,String>> Map)
        {
            
            for (byte i = 0; i < 16; i++)
            {
                for (byte j = 0; j < 16; j++)
                {
                    for (byte k = 0; k < 16; k++)
                    {
                        bool[] Address = new bool[12];
                        bool[] high = Form1.encode16_4(i);
                        bool[] middle = Form1.encode16_4(j);
                        bool[] low = Form1.encode16_4(k);
                        for (int ii=0; ii < 12; ii++)
                        {
                            if (ii < 4)
                            {
                                Address[ii] = high[ii];
                            }
                            else if (ii < 8)
                            {
                                Address[ii] = middle[ii - 4];
                            }
                            else
                            {
                                Address[ii] = low[ii - 8];
                            }
                        }
                        String Key = Form1.bitsToHex(Address);
                        String Value = Form1.bitsToHex(this.Ram.Read(Address));
                        KeyValuePair<String, String> Pair = new KeyValuePair<String, String>(Key, Value);
                        Map.Add(Pair);
                    }
                }
                
            }
        }

        private bool[] getLast12(bool[] incoming)
        {
            bool[] Last12 = new bool[12];
            for (int i = 0; i < 12; i++)
            {
                Last12[i] = incoming[i + 4];
            }
            return Last12;
        }

    }
}
