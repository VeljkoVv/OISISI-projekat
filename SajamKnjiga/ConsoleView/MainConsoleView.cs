using SajamKnjiga.DAO;

namespace SajamKnjiga.ConsoleView
{
    // Glavni meni koji usmerava na menije entiteta
    public class MainConsoleView
    {
        private readonly AdresaConsoleView _adresaView;
        private readonly AutorConsoleView _autorView;
        private readonly KnjigaConsoleView _knjigaView;

        public MainConsoleView()
        {
            _adresaView = new AdresaConsoleView(new AdresaDAO());
            _autorView = new AutorConsoleView(new AutorDAO());
            _knjigaView = new KnjigaConsoleView(new KnjigaDAO());
        }

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string userInput = Console.ReadLine() ?? "0";
                if (userInput == "0")
                {
                    break;
                }
                HandleMenuInput(userInput);
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n===== SAJAM KNJIGA =====");
            Console.WriteLine("1: Autori");
            Console.WriteLine("2: Knjige");
            Console.WriteLine("3: Adrese");
            Console.WriteLine("0: Izlaz");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    _autorView.RunMenu();
                    break;
                case "2":
                    _knjigaView.RunMenu();
                    break;
                case "3":
                    _adresaView.RunMenu();
                    break;
            }
        }
    }
}