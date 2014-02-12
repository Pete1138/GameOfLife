using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class CellStatusTests
    {
        [TestMethod]
        public void GivenALiveCell_WhenLessThanTwoLiveNeighbours_ThenCellDies()
        {
            var liveCell = new Cell { IsAlive = true };

            var result = liveCell.IsAliveNextGeneration();

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void GivenALiveCell_WhenHasTwoAliveNeighbours_ThenCellStaysAlive()
        {
            var liveCell = new Cell { IsAlive = true };

            var neighbour1 = new Cell {IsAlive = true};

            var neighbour2 = new Cell {IsAlive = true};

            liveCell.Neighbours.Add(neighbour1);
            liveCell.Neighbours.Add(neighbour2);

            var result = liveCell.IsAliveNextGeneration();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenALiveCell_WhenHasThreeAliveNeighbours_ThenCellStaysAlive()
        {
            var liveCell = new Cell { IsAlive = true };

            var neighbour1 = new Cell { IsAlive = true };

            var neighbour2 = new Cell { IsAlive = true };
            
            var neighbour3 = new Cell { IsAlive = true };

            liveCell.Neighbours.Add(neighbour1);
            liveCell.Neighbours.Add(neighbour2);
            liveCell.Neighbours.Add(neighbour3);

            var result = liveCell.IsAliveNextGeneration();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenALiveCell_WhenHasFourAliveNeighbours_ThenCellDies()
        {
            var liveCell = new Cell { IsAlive = true };

            var neighbour1 = new Cell { IsAlive = true };

            var neighbour2 = new Cell { IsAlive = true };

            var neighbour3 = new Cell { IsAlive = true };

            var neighbour4 = new Cell { IsAlive = true };

            liveCell.Neighbours.Add(neighbour1);
            liveCell.Neighbours.Add(neighbour2);
            liveCell.Neighbours.Add(neighbour3);
            liveCell.Neighbours.Add(neighbour4);

            var result = liveCell.IsAliveNextGeneration();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenADeadCell_WhenHasThreeAliveNeighbours_ThenCellBecomesAlive()
        {
            var deadCell = new Cell { IsAlive = false };

            var neighbour1 = new Cell { IsAlive = true };

            var neighbour2 = new Cell { IsAlive = true };

            var neighbour3 = new Cell { IsAlive = true };

            deadCell.Neighbours.Add(neighbour1);
            deadCell.Neighbours.Add(neighbour2);
            deadCell.Neighbours.Add(neighbour3);

            var result = deadCell.IsAliveNextGeneration();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenADeadCell_WhenHasNoAliveNeighbours_ThenCellStaysDead()
        {
            var deadCell = new Cell { IsAlive = false };

            var result = deadCell.IsAliveNextGeneration();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenADeadCell_WhenHasTwoAliveNeighbours_ThenCellStaysDead()
        {
            var deadCell = new Cell { IsAlive = false };

            var neighbour1 = new Cell { IsAlive = true };

            var neighbour2 = new Cell { IsAlive = true };

            deadCell.Neighbours.Add(neighbour1);
            deadCell.Neighbours.Add(neighbour2);

            var result = deadCell.IsAliveNextGeneration();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenADeadCell_WhenHasFourAliveNeighbours_ThenCellStaysDead()
        {
            var deadCell = new Cell { IsAlive = false };

            var neighbour1 = new Cell { IsAlive = true };

            var neighbour2 = new Cell { IsAlive = true };

            var neighbour3 = new Cell { IsAlive = true };
            
            var neighbour4 = new Cell { IsAlive = true };

            deadCell.Neighbours.Add(neighbour1);
            deadCell.Neighbours.Add(neighbour2);
            deadCell.Neighbours.Add(neighbour3);
            deadCell.Neighbours.Add(neighbour4);

            var result = deadCell.IsAliveNextGeneration();

            Assert.IsFalse(result);
        }
    }
}
