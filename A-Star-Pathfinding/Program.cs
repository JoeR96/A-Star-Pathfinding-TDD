using A_Star_Pathfinding.UI;

try
{
    bool showMenu = true;
    var menu = new Menu();

    while (showMenu)
    {
        showMenu = menu.MainMenu();
    }
}
catch (Exception ex)
{
    throw ex;
}
