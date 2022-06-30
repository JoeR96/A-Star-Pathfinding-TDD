using System.Numerics;

namespace A_Star_Pathfinding.Grid
{
    public static class GridGenerator
    {
        public static Grid GenerateGrid(int xLength, int yLength, int zLength = 0)
        {
            Grid grid = new Grid(xLength,yLength);

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    grid.Nodes[x, y] = new Node(zLength);
                    grid.Nodes[x, y].X = x;
                    grid.Nodes[x, y].Y = y;
                }
            }

            return grid;
        }
    }
}
