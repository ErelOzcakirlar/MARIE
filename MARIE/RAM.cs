using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARIE
{
    public class RAM
    {
        private Register16[,,] Cells;

        public void EnableWrite(bool[] CellIndex)
        {
            bool[] FirstIndexer = new bool[4];
            bool[] SecondIndexer = new bool[4];
            bool[] ThirdIndexer = new bool[4];
            for (int i = 0; i < 12; i++)
            {
                if (i < 4)
                {
                    FirstIndexer[i] = CellIndex[i];
                }
                else if (i < 8)
                {
                    SecondIndexer[i - 4] = CellIndex[i];
                }
                else
                {
                    ThirdIndexer[i - 8] = CellIndex[i];
                }
            }
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    for (int k = 0; k < 16; k++)
                    {
                        if (i == Form1.decode4_16(FirstIndexer) && j == Form1.decode4_16(SecondIndexer) && k == Form1.decode4_16(ThirdIndexer))
                        {
                            Cells[i,j,k].Enable = true;
                        }
                        else
                        {
                            Cells[i, j, k].Enable = false;
                        }
                    }
                }
            }
        }

        public void Write(bool[] incoming)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    for (int k = 0; k < 16; k++)
                    {
                        Cells[i, j, k].input(incoming);
                    }
                }
            }
        }

        public bool[] Read(bool[] CellIndex)
        {
            bool[] FirstIndexer = new bool[4];
            bool[] SecondIndexer = new bool[4];
            bool[] ThirdIndexer = new bool[4];
            for (int i = 0; i < 12; i++)
            {
                if (i < 4)
                {
                    FirstIndexer[i] = CellIndex[i];
                }
                else if (i < 8)
                {
                    SecondIndexer[i - 4] = CellIndex[i];
                }
                else
                {
                    ThirdIndexer[i - 8] = CellIndex[i];
                }
            }
            return Cells[Form1.decode4_16(FirstIndexer),Form1.decode4_16(SecondIndexer),Form1.decode4_16(ThirdIndexer)].output();
        }

        public RAM()
        {
            Cells = new Register16[16,16,16];
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    for (int k = 0; k < 16; k++)
                    {
                        Cells[i,j,k] = new Register16();
                    }
                }
            }
        }
    }
}
