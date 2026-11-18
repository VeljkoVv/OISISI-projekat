using SajamKnjiga.ConsoleView;
using SajamKnjiga.DAO;

namespace SajamKnjiga
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    class Program
    {
        static void Main()
        {
            KnjigaDAO knjigaDao = new KnjigaDAO();
            KnjigaConsoleView knjigaView = new KnjigaConsoleView(knjigaDao);
            knjigaView.RunMenu();
        }
    }
}