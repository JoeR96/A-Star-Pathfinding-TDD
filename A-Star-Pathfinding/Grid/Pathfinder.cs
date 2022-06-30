using A_Star_Pathfinding.UI;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("A Star Tests")]

namespace A_Star_Pathfinding.Grid
{
    public class Pathfinder
    {
        private Grid _grid = UiGridCreator.Grid;
        private Node startPosition;
        private Node endPosition;
        private Random _random = new Random();

        public Pathfinder(Grid grid)
        {
            _grid = grid;
            _random = new Random();

        }

        public List<Node> FindPath()
        {
            startPosition = ReturnRandomPosition();
            startPosition.SetStartPoint();
            endPosition = ReturnRandomPosition();
            endPosition.SetFinishPoint();
            return new List<Node> { startPosition,
            endPosition};

        }

        private Node ReturnRandomPosition() => _grid.Nodes[_random.Next(0, _grid.XLength), _random.Next(0, _grid.XLength)];

    }

}