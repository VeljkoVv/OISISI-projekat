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
            Console.WriteLine("2: Prikazi sve adrese");
            Console.WriteLine("3: Izmeni adresu");
            Console.WriteLine("4: Obrisi adresu");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    AddAdresa();
                    break;
                case "2":
                    ShowAllAdrese();
                    break;
                case "3":
                    UpdateAdresa();
                    break;
                case "4":
                    RemoveAdresa();
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

        private void ShowAllAdrese()
        {
            PrintAdrese(_adresaDao.GetAll());
        }

        private void PrintAdrese(List<Adresa> adrese)
        {
            Console.WriteLine("\nAdrese:");
            if (adrese.Count == 0)
            {
                Console.WriteLine("Nema unetih adresa.");
                return;
            }
            foreach (Adresa a in adrese)
            {
                Console.WriteLine(a);
            }
        }

        private void UpdateAdresa()
        {
            int id = InputId();
            Adresa adresa = InputAdresa();
            adresa.Id = id;
            Adresa? updatedAdresa = _adresaDao.Update(adresa);
            if (updatedAdresa == null)
            {
                Console.WriteLine("Adresa nije pronadjena.");
                return;
            }

            Console.WriteLine("Adresa izmenjena.");
        }

        private int InputId()
        {
            Console.WriteLine("Unesite ID adrese: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void RemoveAdresa()
        {
            int id = InputId();
            Adresa? removedAdresa = _adresaDao.Remove(id);
            if (removedAdresa == null)
            {
                Console.WriteLine("Adresa nije pronadjena.");
                return;
            }

            Console.WriteLine("Adresa obrisana.");
        }
    }
}