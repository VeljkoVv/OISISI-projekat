using SajamKnjiga.DAO;
using SajamKnjiga.Model;

namespace SajamKnjiga.ConsoleView
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    public class AutorConsoleView
    {
        private readonly AutorDAO _autorDao;

        public AutorConsoleView(AutorDAO autorDao)
        {
            _autorDao = autorDao;
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
            Console.WriteLine("\nAutori - izaberite opciju:");
            Console.WriteLine("1: Dodaj autora");
            Console.WriteLine("2: Prikazi sve autore");
            Console.WriteLine("3: Izmeni autora");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    AddAutor();
                    break;
                case "2":
                    ShowAllAutori();
                    break;
                case "3":
                    UpdateAutor();
                    break;
            }
        }

        private void AddAutor()
        {
            Autor autor = InputAutor();
            _autorDao.Add(autor);
            Console.WriteLine("Autor dodat.");
        }

        private Autor InputAutor()
        {
            Console.WriteLine("Unesite ime: ");
            string ime = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite prezime: ");
            string prezime = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite datum rodjenja (dd.MM.yyyy.): ");
            DateTime datumRodjenja = ConsoleViewUtils.SafeInputDate();

            Console.WriteLine("Unesite ID adrese: ");
            int adresaId = ConsoleViewUtils.SafeInputInt();

            Console.WriteLine("Unesite telefon: ");
            string telefon = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite e-mail: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite broj licne karte: ");
            string brojLicneKarte = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite godine iskustva: ");
            int godineIskustva = ConsoleViewUtils.SafeInputInt();

            return new Autor(ime, prezime, datumRodjenja, adresaId, telefon, email, brojLicneKarte, godineIskustva);
        }

        private void ShowAllAutori()
        {
            PrintAutori(_autorDao.GetAll());
        }

        private void PrintAutori(List<Autor> autori)
        {
            Console.WriteLine("\nAutori:");
            if (autori.Count == 0)
            {
                Console.WriteLine("Nema unetih autora.");
                return;
            }
            foreach (Autor a in autori)
            {
                Console.WriteLine(a);
            }
        }

        private void UpdateAutor()
        {
            int id = InputId();
            Autor autor = InputAutor();
            autor.Id = id;
            Autor? updatedAutor = _autorDao.Update(autor);
            if (updatedAutor == null)
            {
                Console.WriteLine("Autor nije pronadjen.");
                return;
            }

            Console.WriteLine("Autor izmenjen.");
        }

        private int InputId()
        {
            Console.WriteLine("Unesite ID autora: ");
            return ConsoleViewUtils.SafeInputInt();
        }
    }
}