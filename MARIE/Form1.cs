using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MARIE
{
    public partial class Form1 : Form
    {
        Computer marie;
        List<KeyValuePair<String, String>> memory;
        List<KeyValuePair<String, String>> Labels;
        int origin;
        bool loaded;

        public Form1()
        {
            InitializeComponent();
            marie = new Computer(MARvalue,PCvalue,ACvalue,MBRvalue,IRvalue,InRegValue,OutRegValue);
            Labels = new List<KeyValuePair<String, String>>();
            origin = 0;
            loaded = false;
            printMarie();
            MemoryView.DataSource = memory;
            LabelView.DataSource = Labels;
            
        }

        private void printMarie()
        {
            ACvalue.Text = bitsToHex(marie.AC.output());
            MARvalue.Text = bitsToHex(marie.MAR.output());
            MBRvalue.Text = bitsToHex(marie.MBR.output());
            IRvalue.Text = bitsToHex(marie.IR.output());
            PCvalue.Text = bitsToHex(marie.PC.output());
            InRegValue.Text = bitsToHex(marie.InReg.output());
            OutRegValue.Text = bitsToHex(marie.OutReg.output());
            memory = new List<KeyValuePair<String, String>>();
            marie.printMemory(memory);
            MemoryView.DataSource = memory;
        }

        public static String bitsToHex(bool[] bits)
        {
            String value = "";
            for (int i = 0; i < bits.Length; i = i + 4)
            {
                bool[] CurrentFour = new bool[4];
                CurrentFour[0] = bits[i];
                CurrentFour[1] = bits[i + 1];
                CurrentFour[2] = bits[i + 2];
                CurrentFour[3] = bits[i + 3];
                byte hex = decode4_16(CurrentFour);
                if (hex < 10)
                {
                    value += hex.ToString();
                }
                else
                {
                    value += (char)(hex + 55);
                }
            }
            return value;
        }

        public static byte decode4_16(bool[] bits)
        {
            byte rvalue = (byte)((bits[0] ? 1 : 0) * 8);
            rvalue += (byte)((bits[1] ? 1 : 0) * 4);
            rvalue += (byte)((bits[2] ? 1 : 0) * 2);
            rvalue += (byte)(bits[3] ? 1 : 0);
            return rvalue;
        }

        public static bool[] encode16_4(byte value)
        {
            bool[] bits = new bool[4];
            bits[0] = (value / 8) == 1;
            bits[1] = ((value % 8) / 4) == 1;
            bits[2] = ((value % 4) / 2) == 1;
            bits[3] = (value % 2) == 1;
            return bits;
        }

        public static bool[] hexToBits(String value)
        {
            bool[] bits = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                bits[i] = false;
            }
            if (!String.IsNullOrEmpty(value))
            {
                List<byte> byteValues = new List<byte>();
                for (int i = 0; i < value.Length; i++)
                {
                    int AsciiValue = value[i];
                    if (AsciiValue >= 65)
                    {
                        AsciiValue -= 55;
                    }
                    else
                    {
                        AsciiValue -= 48;
                    }
                    byteValues.Add((byte)AsciiValue);
                }
                byteValues.Reverse();
                int j = 15;
                foreach (byte item in byteValues)
                {
                    bool[] CurrentFour = encode16_4(item);
                    for (int i = 3; i >= 0; i--)
                    {
                        bits[j] = CurrentFour[i]; 
                        j--;
                    }
                }
            }
            return bits;
        }

        public static bool[] decToBits(String value)
        {
            bool[] bits = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                bits[i] = false;
            }
            if (!String.IsNullOrEmpty(value))
            {
                int DecValue = int.Parse(value);
                DecValue %= (int)Math.Pow(2, 16);
                int counter = 15;
                while (DecValue != 0)
                {
                    bits[counter] = (DecValue % 2) == 1 ? true : false;
                    DecValue /= 2;
                    counter--;
                }
            }
            
            return bits;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            marie = new Computer(MARvalue, PCvalue, ACvalue, MBRvalue, IRvalue, InRegValue, OutRegValue);
            printMarie();
            origin = 0;
            loaded = false;
            CodeBox.Clear();
            Labels = new List<KeyValuePair<String, String>>();
            LabelView.DataSource = Labels;
        }

        private void Load_Click(object sender, EventArgs e)
        {
            String Code = CodeBox.Text;
            if (!String.IsNullOrEmpty(Code))
            {
               
                bool accepted = true;

                List<bool[]> Program = new List<bool[]>();
                List<String> LabelList = new List<String>();
                Labels = new List<KeyValuePair<String, String>>();

                char[] Splitter = { '\n' };
                String[] Lines = Code.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);
                Splitter = new char[]{ ' ' };
                String[] FirstWords = Lines[0].Split(Splitter,StringSplitOptions.RemoveEmptyEntries);
                if (FirstWords[0].ToUpper().Equals("ORG"))
                {
                        if (FirstWords.Length == 2)
                        {
                            try
                            {
                                origin = byte.Parse(FirstWords[1]);
                                List<String>LinesList = Lines.ToList();
                                LinesList.RemoveAt(0);
                                Lines = LinesList.ToArray();
                            }
                            catch (Exception ex)
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:0\n" + ex.Message);
                                
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:0\nEksik komut");
                        }
                }
                for (int i = 0; i < Lines.Length; i++)//First Compile
                {
                    String[] Words = Lines[i].Split(Splitter,StringSplitOptions.RemoveEmptyEntries);
                    if (Words[0].ToUpper().Equals("JNS"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = false;
                            Word[1] = false;
                            Word[2] = false;
                            Word[3] = false;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if (Words[0].ToUpper().Equals("LOAD"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = false;
                            Word[1] = false;
                            Word[2] = false;
                            Word[3] = true;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if (Words[0].ToUpper().Equals("STORE"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = false;
                            Word[1] = false;
                            Word[2] = true;
                            Word[3] = false;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if (Words[0].ToUpper().Equals("ADD"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = false;
                            Word[1] = false;
                            Word[2] = true;
                            Word[3] = true;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if (Words[0].ToUpper().Equals("SUBT"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = false;
                            Word[1] = true;
                            Word[2] = false;
                            Word[3] = false;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if(Words[0].ToUpper().Equals("INPUT")){
                        bool[] Word = new bool[16];
                        Word[0] = false;
                        Word[1] = true;
                        Word[2] = false;
                        Word[3] = true;
                        for (int j = 0; j < 12; j++)
                        {
                            Word[i + 4] = false;
                        }
                        Program.Add(Word);
                    }
                    else if (Words[0].ToUpper().Equals("OUTPUT"))
                    {
                        bool[] Word = new bool[16];
                        Word[0] = false;
                        Word[1] = true;
                        Word[2] = true;
                        Word[3] = false;
                        for (int j = 0; j < 12; j++)
                        {
                            Word[i + 4] = false;
                        }
                        Program.Add(Word);
                    }
                    else if (Words[0].ToUpper().Equals("HALT"))
                    {
                        bool[] Word = new bool[16];
                        Word[0] = false;
                        Word[1] = true;
                        Word[2] = true;
                        Word[3] = true;
                        for (int j = 0; j < 12; j++)
                        {
                            Word[i + 4] = false;
                        }
                        Program.Add(Word);
                    }
                    else if (Words[0].ToUpper().Equals("JUMP"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = true;
                            Word[1] = false;
                            Word[2] = false;
                            Word[3] = false;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if (Words[0].ToUpper().Equals("CLEAR"))
                    {
                        bool[] Word = new bool[16];
                        Word[0] = true;
                        Word[1] = false;
                        Word[2] = false;
                        Word[3] = true;
                        for (int j = 0; j < 12; j++)
                        {
                            Word[i + 4] = false;
                        }
                        Program.Add(Word);
                    }
                    else if (Words[0].ToUpper().Equals("ADDI"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = true;
                            Word[1] = false;
                            Word[2] = true;
                            Word[3] = true;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if (Words[0].ToUpper().Equals("JUMPI"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = true;
                            Word[1] = true;
                            Word[2] = false;
                            Word[3] = false;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if (Words[0].ToUpper().Equals("STOREI"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = true;
                            Word[1] = true;
                            Word[2] = false;
                            Word[3] = true;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if (Words[0].ToUpper().Equals("LOADI"))
                    {
                        if (Words.Length == 2)
                        {
                            bool[] Word = new bool[16];
                            Word[0] = true;
                            Word[1] = true;
                            Word[2] = true;
                            Word[3] = false;
                            Program.Add(Word);
                            if (Char.IsLetter(Words[1][0]))
                            {
                                if (!LabelList.Contains(Words[1]))
                                {
                                    LabelList.Add(Words[1]);
                                }
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nRakam ile başlayan etiket");
                                break;
                            }
                        }
                        else
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Satır:" + i.ToString() + "\nEksik komut");
                            break;
                        }
                    }
                    else if (Words[0].ToUpper().Equals("END"))
                    {
                        break;
                    }
                    else if (Char.IsLetter(Words[0][0]))
                    {
                        Words[0] = Words[0].Replace(",","");
                        if (Words.Length == 3)
                        {
                            if (Words[1].ToUpper().Equals("DEC"))
                            {
                                Program.Add(decToBits(Words[2]));
                                String Key = Words[0];
                                String Value = bitsToHex(decToBits((i + origin).ToString()));
                                KeyValuePair<String, String> Pair = new KeyValuePair<String, String>(Key, Value);
                                Labels.Add(Pair);
                            }
                            else if(Words[1].ToUpper().Equals("HEX"))
                            {
                                Program.Add(hexToBits(Words[2]));
                                String Key = Words[0];
                                String Value = bitsToHex(decToBits((i + origin).ToString()));
                                KeyValuePair<String, String> Pair = new KeyValuePair<String, String>(Key, Value);
                                Labels.Add(Pair);
                            }
                            else
                            {
                                accepted = false;
                                MessageBox.Show("Hata! Satır:" + i.ToString() + "\nLütfen etiketten sonra gelen komutu bir satır aşağı yazın");
                                break;
                            }
                        }
                        else if (!LabelList.Contains(Words[0]))
                        {
                            LabelList.Add(Words[0]);
                            String Key = Words[0];
                            String Value = bitsToHex(decToBits((i + origin).ToString()));
                            KeyValuePair<String, String> Pair = new KeyValuePair<String, String>(Key, Value);
                            Labels.Add(Pair);
                        }
                    }
                    else
                    {
                        accepted = false;
                        MessageBox.Show("Hata! Satır:" + i.ToString() + "\nAnlaşılmayan komut");
                        break;
                    }
                }
                if (accepted)//Check Labels
                {
                    foreach (String item in LabelList)
                    {
                        bool found = false;
                        foreach (KeyValuePair<String, String> pair in Labels)
                        {
                            if (pair.Key.Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            accepted = false;
                            MessageBox.Show("Hata! Tanımlanmayan etiket");
                            break;
                        }
                    }
                }
                if (accepted)
                {
                    for(int i = 0; i < Lines.Length; i++)//Second Compile
                    {
                        String[] Words = Lines[i].Split(Splitter, StringSplitOptions.RemoveEmptyEntries);
                        if (Words.Length > 1)
                        {
                            if (!Words[1].ToUpper().Equals("DEC") && !Words[1].ToUpper().Equals("HEX"))
                            {
                                foreach (KeyValuePair<String, String> pair in Labels)
                                {
                                    if (pair.Key.Equals(Words[1]))
                                    {
                                        bool[] Address = hexToBits(pair.Value);
                                        for (int j = 0; j < 12; j++)
                                        {
                                            Program.ElementAt(i)[j + 4] = Address[j + 4];
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (accepted)
                {
                    loaded = true;
                    LabelView.DataSource = Labels;
                    marie.load(Program, origin);
                    printMarie();
                }
                
            }
            else
            {
                MessageBox.Show("Lütfen bir program yazın");
            }
        }

        private void Run_Click(object sender, EventArgs e)
        {
            if (loaded)
            {
                marie.run(origin,MemoryView);
                printMarie();
            }
            else
            {
                MessageBox.Show("Henüz bir program yüklemediniz!");
            }
        }
    }
}
