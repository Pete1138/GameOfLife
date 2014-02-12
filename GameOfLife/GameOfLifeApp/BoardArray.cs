using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeApp
{
    public class BoardArray : Dictionary<int, Dictionary<int, bool>>
    {
        public BoardArray()
        {
            // For serialization
        }

        public BoardArray(int size)
        {
            for (int i = 0; i < size; i++)
            {
                this[i] = new Dictionary<int, bool>();
                
                for (int j = 0; j < size; j++)
                {
                    this[i][j] = false;
                }
            }
        }
    }
}
