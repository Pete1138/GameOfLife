using System.Collections.Generic;
using System.Linq;

namespace GameOfLifeTests
{
    public class Cell
    {
        public Cell()
        {
            Neighbours = new List<Cell>();
        }

        public bool IsAlive { get; set; }

        public List<Cell> Neighbours { get; set; }

        public bool IsAliveNextGeneration()
        {
            var aliveNeighbours = Neighbours.Count(x => x.IsAlive);

            if (IsAlive)
            {
                return aliveNeighbours == 2 || aliveNeighbours == 3;
            }
            else
            {
                return aliveNeighbours == 3;
            }
        }
    }
}