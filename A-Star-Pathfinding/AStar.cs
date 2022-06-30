using A_Star_Pathfinding.Grid;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("A Star Tests")]

namespace A_Star_Pathfinding.AStar
{
    public class AStar
    {
        public List<Node> openNodes = new List<Node>();
        public List<Node> closedNodes = new List<Node>();
        public Node node;
        public Grid.Grid _grid;

        public AStar(Grid.Grid grid)
        {
            _grid = grid;
        }

        public Node SelectNextNode() => node = openNodes
            .OrderBy(x => x.FCost)
            .OrderBy(x => x.HCost)
            .Where(x => x.Evaluated != true)
            .First();

        public bool NodeIsDestination(Node node)
            => node.IsDestination;
        public void Search()
        {
            var start = CreateRandomPoint();
            var end = CreateRandomPoint();
            
            Search(start, end);
        }

        private Point CreateRandomPoint()
        {
            Random random = new Random();

            var x = random.Next(0, _grid.XLength - 1);
            var y = random.Next(0, _grid.YLength - 1);

            return new Point(x, y);
        }
        public List<Node> Search(Point startPosition, Point endPosition)
        {
            Node startNode = _grid.Nodes[startPosition.X, startPosition.Y];
            Node targetNode = _grid.Nodes[endPosition.X, endPosition.Y];
            targetNode.SetFinishPoint();
            startNode.SetStartPoint();

            node = startNode;
            openNodes.Add(node);

            while (openNodes.Count > 0)
            {
                SelectNextNode();
                CloseCurrentNode();

                if (node.IsDestination)
                    return RetracePath(startNode, targetNode);

                var neighbourNodes = _grid.GetNeighbours(node);

                foreach (var neighbour in neighbourNodes)
                {
                    if (!NodeIsSearchable(neighbour))
                        continue;
                    int newCostToNeighbour = CalculateNewNeighbourCost(neighbour);

                    if (newCostToNeighbour < neighbour.GCost || !closedNodes.Contains(neighbour))
                    {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.Parent = node;

                        if (!openNodes.Contains(neighbour))
                            openNodes.Add(neighbour);
                    }
                }
            }

            return null;
        }

        private List<Node> RetracePath(Node startNode, Node targetNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = targetNode;      

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            for (int i = 1; i < path.Count; i++)
            {
                path[i].SetPath();
            }
            path.Reverse();

            return path;
        }

        public int CalculateNewNeighbourCost(Node neighbour)
        {
            return node.GCost + GetDistance(node, neighbour);
        }

        public bool NodeIsSearchable(Node neighbour)
        {
            if (!neighbour.IsWalkable || closedNodes.Contains(neighbour) 
                || neighbour.Evaluated == true )
                return false;

            return true;
        }

        public bool CloseCurrentNode()
        {
            if(node.IsDestination)
                return true;
            

            closedNodes.Add(node);
            openNodes.Remove(node);
            node.Evaluated = true;

            return false;

        }

        public int GetDistance(Node neighbour, Node targetNode)
        {
            //calculate how many nodes away on 
            int dstX = Math.Abs(neighbour.X - targetNode.X);
            //calculate how many nodes away on y
            int dstY = Math.Abs(neighbour.Y - targetNode.Y);
            //take the lowest value - that is our diagonl movements to get inline
            //subtract the lower number from the higher number - that is how many horizontal movements 
            //one diagonal movement costs 14y
            //one horizontal movement costs 10y

            //if X is Greater return X - y;
            //if Y is Greater return Y - x;
            return GetHorizontalMovements(dstX, dstY);
        }

        public int GetHorizontalMovements(int dstX, int dstY)
        {
            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }

      

        public static class Heuristics
    {
        public static int ManhattanDistance(int xStart, int xFinish, int yStart, int yFinish)
        {
            return Math.Abs(xStart - xFinish) + Math.Abs(yStart - yFinish);
        }
    }

    
}
