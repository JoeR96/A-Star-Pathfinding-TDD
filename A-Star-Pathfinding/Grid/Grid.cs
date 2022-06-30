using System.Numerics;

namespace A_Star_Pathfinding.Grid
{


    public class Grid
    {
        public int XLength { get; }
        public int YLength { get; }
        public Grid(int xLength, int yLength)
        {
            XLength = xLength;
            YLength = yLength;
            Nodes = new Node[xLength, yLength];
        }

        public Node[,] Nodes { get; set; }


        public List<Node> GetNeighbours(Node node, bool excludeDiagionals = true)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if(excludeDiagionals)
                    {
                        if (x == 0 && y == 0 ||
                  x == -1 && y == -1 ||
                  x == 1 && y == -1 ||
                  x == -1 && y == 1 ||
                  x == 1 && y == 1)
                        {
                            continue;
                        }
                    }
                  

                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.X + x;
                    int checkY = node.Y + y;

                    if (checkX >= 0 && checkX < XLength && checkY >= 0 && checkY < YLength)
                    {
                        neighbours.Add(Nodes[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }
    }

    public class Node
    {
        public int FCost => GCost + HCost;
        public int GCost { get; set; }
        public int HCost { get; set; }

        public Node() { }
        public int X { get; set; }
        public int Y { get; set; }
        internal void SetStartPoint()
        {
            IsStartPoint = !IsStartPoint;
            if (IsStartPoint)
                PrintValue = 'X';
            else
                PrintValue = '*';
        }

        internal void SetFinishPoint()
        {
            IsDestination = !IsDestination;

            if (IsDestination)
                PrintValue = 'Y';
            else
                PrintValue = '*';
        }

        public char PrintValue { get; private set; } = '*';
        public Node(int zLength)
        {
            VerticalNodes = new Node[zLength];
            CreateVerticalNodes();
        }
        public Node[] VerticalNodes { get; set; }
        public bool IsStartPoint { get; set; }
        public bool IsDestination { get; set; }
        public bool IsWalkable { get; set; } = true;
        public bool Evaluated { get; set; } = false;
        public Node Parent { get; internal set; }
        public bool IsPath { get; private set; }

        private void CreateVerticalNodes()
        {
            for (int i = 0; i < VerticalNodes.Length; i++)
            {
                VerticalNodes[i] = new Node();
            }
        }

        internal void SetPath()
        {
            IsPath = !IsPath;

            if (IsPath)
                PrintValue = 'P';
            else
                PrintValue = '*';
        }

        internal void SetWalkable()
        {
            IsWalkable = !IsWalkable;

            if (!IsWalkable)
                PrintValue = 'G';
            else
                PrintValue = '*';
        }
    }
}