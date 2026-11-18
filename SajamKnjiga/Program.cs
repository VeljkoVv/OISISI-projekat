using SajamKnjiga.ConsoleView;
using SajamKnjiga.DAO;

namespace SajamKnjiga
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    class Program
    {
        static void Main()
        {
            AutorDAO autorDao = new AutorDAO();
            AutorConsoleView autorView = new AutorConsoleView(autorDao);
            autorView.RunMenu();
        }
    }
}