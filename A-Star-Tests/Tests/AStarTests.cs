using A_Star_Pathfinding.Grid;
using NUnit.Framework;
using GenFu;
using System.Collections.Generic;
using System.Linq;
using A_Star_Pathfinding;
using A_Star_Pathfinding.AStar;
using FluentAssertions;
using System.Drawing;

namespace A_Star_Tests.Tests
{
    internal class AStarTests
    {
        Grid grid;
        AStar aStar;

        [SetUp]
        public void Setup()
        {
            grid = GridGenerator.GenerateGrid(8, 8);
            aStar = new AStar(grid);
        }

        [Test]
        public void FScoreCalculates()
        {
         
            Node node = new Node();
            node.GCost = 1;
            node.HCost = 2;

            node.FCost.Should().Be(3);

        }

        [Test]
        public void LowestFScoreIsChosenBeforeHScore()
        {
            var nodeOne = new Node();
            var nodeTwo = new Node();
            var nodeThree = new Node();

            List<Node> nodes = new()
            {
                nodeOne,
                nodeTwo,
                nodeThree
            };

            nodes.First().GCost = 1;
            nodes.First().HCost = 2;

            nodes[1].HCost = 1;
            nodes[1].GCost = 2;

            nodes.Last().GCost = 3;
            nodes.Last().HCost = 3;

            aStar.openNodes = nodes;

            var result = aStar.SelectNextNode();

            nodes.First().FCost.Should().Be(3);
            nodes.Last().FCost.Should().Be(6);

            result.FCost.Should().Be(3);
            result.HCost.Should().Be(1);
        }

        [Test]
        public void ReturnsTrueIfDestinationNode()
        {
            Node notDestination = new Node();
            Node node = new Node();

            node.IsDestination = true;

            bool notDestinationResult = aStar.NodeIsDestination(notDestination);
            bool destinationResult = aStar.NodeIsDestination(node);

            notDestinationResult.Should().Be(false);
            destinationResult.Should().Be(true);
        }
        [Test]
        public void ReturnsFinishedIfCurrentNodeIsDestination()
        {
            var node = new Node();
            aStar.node = node;

            node.IsDestination = true;

            var result = aStar.CloseCurrentNode();

            result.Should().Be(true);

        }
        [Test]
        public void CurrentNodeClosesIfNotDestination()
        {
            var node = new Node();
            aStar.node = node;

            aStar.closedNodes.Contains(node);
            aStar.openNodes.Contains(node).Should().Be(false);

            aStar.CloseCurrentNode();
            aStar.node.Evaluated.Should().Be(true);
        }

        [Test]
        public void QueryNeighbourNodesWithoutDiagonal()
        {
            var neighbours = grid.GetNeighbours(grid.Nodes[4, 4]);
            neighbours.Count().Should().Be(4);
        }

        [Test]
        public void QueryNeighbourNodes()
        {
            var neighbours = grid.GetNeighbours(grid.Nodes[4, 4],false);
            neighbours.Count().Should().Be(8);
        }

        [Test]
        public void NotWalkableNodeReturnsFalse()
        {
            Node notWalkableNode = new Node();
            notWalkableNode.IsWalkable = false;
            aStar.NodeIsSearchable(notWalkableNode).Should().BeFalse();

        }

        [Test]
        public void WalkableNodeReturnsTrue()
        {
            Node walkableNode = new Node();
            walkableNode.IsWalkable = true;

            aStar.NodeIsSearchable(walkableNode).Should().BeTrue();
        }
        [Test]
        public void WalkableTileAlreadyVisitedReturnsFalse()
        {
            Node visitedNode = new Node();
            visitedNode.Evaluated = true;
            visitedNode.IsWalkable = true;

            aStar.NodeIsSearchable(visitedNode).Should().BeFalse();
        }

        [Test]
        public void WalkableTileNotVisitedReturnsTrue()
        {
            Node visitedNode = new Node();
            visitedNode.IsWalkable = true;
            visitedNode.Evaluated = false;

            aStar.NodeIsSearchable(visitedNode).Should().BeTrue();
        }
        //The lower value is equal to the number of diagonal movements
        //Subtracting the lower number from the higher number gives us our horizontal movement value
        [Test]
        public void LowerNumberSubtractsFromHigherNumber()
        {
            int x = 10;
            int y = 25;

            var returnValue = aStar.GetHorizontalMovements(x, y);

            returnValue.Should().Be(290);
        }

        [Test]
        public void NeighbourCostIsGCostAddDistance()
        {
            var node = grid.Nodes[4, 4];
            var neighbour = grid.Nodes[4, 3];
            var neighbourDiagonal = grid.Nodes[5, 5];
            aStar.node = node;
            node.GCost = 1;

            //11 is 1 tile + x axis ratio of 10
            //15 is 1 tile + y axis ratio of 14
            var newCostToNeighbour = aStar.CalculateNewNeighbourCost(neighbour);
            newCostToNeighbour.Should().Be(11);

            var newCostToNeighbourDiagonal = aStar.CalculateNewNeighbourCost(neighbourDiagonal);
            newCostToNeighbourDiagonal.Should().Be(15);

        }

        [Test]
        public void SearchReturnsPath()
        {
            var startPosition = new Point(0, 0);
            var endPosition = new Point(7, 7);
            var path = aStar.Search(startPosition, endPosition);

            path.Should().NotBeNull();
        }

       
    }
}
