using SajamKnjiga.ConsoleView;
using SajamKnjiga.DAO;

namespace SajamKnjiga
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    class Program
    {
        static void Main()
        {
            AdresaDAO adresaDao = new AdresaDAO();
            AdresaConsoleView adresaView = new AdresaConsoleView(adresaDao);
            adresaView.RunMenu();
        }
    }
}