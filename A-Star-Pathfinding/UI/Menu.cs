
using A_Star_Pathfinding.Grid;

namespace A_Star_Pathfinding.UI
{
    public class Menu
    {
        public bool MainMenu()
        {
            PrintMenu();

            switch (Console.ReadLine())
            {
                case "1":
                    UiGridCreator.CreateGrid();
                    return true;
                case "2": GridPrinter.Print2DArray<Node>();
                    return true;

                case "3":
                    UiGridCreator.FindPath();
                    return true;
                case "4":
                    UiGridCreator.SetRandomWalkable();
                    return true;
                default:
                    return true;
            }
        }

        private void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");       
            Console.WriteLine("1) Create Map");
            Console.WriteLine("2) Print Map");
            Console.WriteLine("3) Find Path");
            Console.WriteLine("4) Set UnwalkableLocation");
            Console.Write("\r\nSelect an option: ");
        }
    }
}
