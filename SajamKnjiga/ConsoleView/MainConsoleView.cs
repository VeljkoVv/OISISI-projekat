using SajamKnjiga.DAO;

namespace SajamKnjiga.ConsoleView
{
    // Glavni meni koji usmerava na menije entiteta
    public class MainConsoleView
    {
        private readonly AdresaConsoleView _adresaView;
        private readonly AutorConsoleView _autorView;
        private readonly KnjigaConsoleView _knjigaView;
        private readonly PosetilacConsoleView _posetilacView;
        private readonly KupovinaConsoleView _kupovinaView;
        private readonly IzdavacConsoleView _izdavacView;

        public MainConsoleView()
        {
            // Meniji koji koriste podatke drugih entiteta dele isti DAO objekat,
            // jer svaki DAO drzi svoju listu u memoriji
            AutorDAO autorDao = new AutorDAO();
            KnjigaDAO knjigaDao = new KnjigaDAO();
            PosetilacDAO posetilacDao = new PosetilacDAO();
            KupovinaDAO kupovinaDao = new KupovinaDAO();
            IzdavacDAO izdavacDao = new IzdavacDAO();

            _adresaView = new AdresaConsoleView(new AdresaDAO());
            _autorView = new AutorConsoleView(autorDao);
            _knjigaView = new KnjigaConsoleView(knjigaDao);
            _posetilacView = new PosetilacConsoleView(posetilacDao, kupovinaDao);
            _kupovinaView = new KupovinaConsoleView(kupovinaDao, posetilacDao, knjigaDao);
            _izdavacView = new IzdavacConsoleView(izdavacDao, autorDao, knjigaDao);
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
            Console.WriteLine("4: Posetioci");
            Console.WriteLine("5: Kupovine");
            Console.WriteLine("6: Izdavaci");
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
                case "4":
                    _posetilacView.RunMenu();
                    break;
                case "5":
                    _kupovinaView.RunMenu();
                    break;
                case "6":
                    _izdavacView.RunMenu();
                    break;
            }
        }
    }
}