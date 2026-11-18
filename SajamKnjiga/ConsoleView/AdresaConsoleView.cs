using SajamKnjiga.DAO;
using SajamKnjiga.Model;

namespace SajamKnjiga.ConsoleView
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    public class AdresaConsoleView
    {
        private readonly AdresaDAO _adresaDao;

        public AdresaConsoleView(AdresaDAO adresaDao)
        {
            _adresaDao = adresaDao;
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
            Console.WriteLine("\nAdrese - izaberite opciju:");
            Console.WriteLine("1: Dodaj adresu");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    AddAdresa();
                    break;
            }
        }

        private void AddAdresa()
        {
            Adresa adresa = InputAdresa();
            _adresaDao.Add(adresa);
            Console.WriteLine("Adresa dodata.");
        }

        private Adresa InputAdresa()
        {
            Console.WriteLine("Unesite ulicu: ");
            string ulica = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite broj: ");
            string broj = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite grad: ");
            string grad = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite drzavu: ");
            string drzava = Console.ReadLine() ?? string.Empty;

            return new Adresa(ulica, broj, grad, drzava);
        }
    }
}