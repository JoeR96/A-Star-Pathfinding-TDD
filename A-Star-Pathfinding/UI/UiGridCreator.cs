using A_Star_Pathfinding.Grid;

namespace A_Star_Pathfinding.UI
{
    internal static class UiGridCreator
    {
        public static Grid.Grid Grid;

        internal static void CreateGrid()
        {
            Console.WriteLine("Please enter the X Length of the grid");
            var xLength = InputLength();

            Console.WriteLine("Please enter the Y Length of the grid");
            var yLength = InputLength();

            Grid = GridGenerator.GenerateGrid((int)xLength, (int)yLength);
        }

        static int? InputLength( )
        {
            var num = StringToNullableInt(Console.ReadLine());

            if (num == null)
            {
                Console.WriteLine("Please enter a valid integer");
                return InputLength();
            };

            return num;
        }
        static int? StringToNullableInt(string value)
            => int.TryParse(value, out int val) ? (int?)val : null;

        internal static void FindPath()
        {
            AStar.AStar astar = new AStar.AStar(Grid);
            astar.Search();
        }

        internal static void SetRandomWalkable()
        {
            Random random = new Random();

            for (int i = 0; i < Grid.Nodes.LongLength / 10; i++)
            {
                //test this
                var randomX = random.Next(0, Grid.XLength);
                var randomY = random.Next(0, Grid.YLength);

                var node = Grid.Nodes[randomX,randomY];

                if (node.IsDestination || !node.IsWalkable || node.IsStartPoint || node.IsPath)
                    continue;

                node.SetWalkable();
            }
        }
    }
    public enum Direction
    {
        X,
        Y,
        Z
    }
}
