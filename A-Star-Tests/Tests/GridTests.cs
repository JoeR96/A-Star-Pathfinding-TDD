using A_Star_Pathfinding.Grid;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace A_Star_Tests.Tests
{
    public class GridTests
    {
        private int _xLength = 8;
        private int _yLength = 8;
        private int _zLength = 8;
        private Grid _grid;

        [Test,Order(1)]
        public void GeneratesGrid()
        {
            _grid = GridGenerator.GenerateGrid(_xLength,_yLength,_zLength);

            for (int x = 0; x < _xLength; x++)
            {
                for (int y = 0; y < _yLength; y++)
                {
                    for (int z = 0; z < _zLength; z++)
                    {
                        _grid.Nodes[x, y].VerticalNodes[z].Should().NotBeNull();
                    }
                }
            }

            _grid.Nodes.LongLength.Should().Be(8 * 8);

        }

        [Test, Order(2)]
        public void PathHasRandomStartAndEndPoint()
        {

            var pathfinder = new Pathfinder(_grid);
            var path = pathfinder.FindPath();

            bool hasStartPoint = false;
            bool hasEndPoint = false;

            for (int x = 0; x < _xLength; x++)
            {
                for (int y = 0; y < _yLength; y++)
                {
                    if (_grid.Nodes[x, y].IsStartPoint)
                        hasStartPoint = true;

                    if (_grid.Nodes[x, y].IsStartPoint)
                        hasEndPoint = true;
                }
            }

            hasStartPoint.Should().BeTrue();
            hasEndPoint.Should().BeTrue();

        }

        [Test]
        public void ReturnsNeighbourNodes()
        {
            var result = _grid.GetNeighbours(_grid.Nodes[4, 4],false);
            result.Count().Should().Be(8);
        }
        [Test]
        public void ReturnsAdjacentNeighbourNodes()
        {
            var result = _grid.GetNeighbours(_grid.Nodes[4, 4]);
            result.Count().Should().Be(4);
        }
        [Test]
        public void ReturnsNeighbourNodesAtEdge()
        {
            var result = _grid.GetNeighbours(_grid.Nodes[0, 0], false);
            result.Count().Should().Be(3);
        }      
    }
}
