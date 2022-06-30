using A_Star_Pathfinding.Grid;
using A_Star_Pathfinding.Helpers;

namespace A_Star_Pathfinding.UI
{
    internal class GridPrinter
    {
        internal static void Print2DArray<T>()
        {
            for (int i = 0; i < UiGridCreator.Grid.Nodes.GetLength(0); i++)
            {
                for (int j = 0; j < UiGridCreator.Grid.Nodes.GetLength(1); j++)
                {
                    var n = UiGridCreator.Grid.Nodes[i, j];
                  

                    if (n.IsStartPoint)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;

                    }
                    else if(n.IsDestination)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                    }
                    else if(!n.IsWalkable)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;

                    }
                    else if (n.IsPath)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write(n.PrintValue + " ");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                Console.WriteLine();
            }
            UIHelper.ReturnToMenu();
        }     
    }    
}
