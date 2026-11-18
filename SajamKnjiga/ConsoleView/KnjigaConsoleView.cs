using SajamKnjiga.DAO;
using SajamKnjiga.Model;

namespace SajamKnjiga.ConsoleView
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    public class KnjigaConsoleView
    {
        private readonly KnjigaDAO _knjigaDao;

        public KnjigaConsoleView(KnjigaDAO knjigaDao)
        {
            _knjigaDao = knjigaDao;
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
            Console.WriteLine("\nKnjige - izaberite opciju:");
            Console.WriteLine("1: Dodaj knjigu");
            Console.WriteLine("2: Prikazi sve knjige");
            Console.WriteLine("3: Izmeni knjigu");
            Console.WriteLine("4: Obrisi knjigu");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    AddKnjiga();
                    break;
                case "2":
                    ShowAllKnjige();
                    break;
                case "3":
                    UpdateKnjiga();
                    break;
                case "4":
                    RemoveKnjiga();
                    break;
            }
        }

        private void AddKnjiga()
        {
            Knjiga knjiga = InputKnjiga();
            _knjigaDao.Add(knjiga);
            Console.WriteLine("Knjiga dodata.");
        }

        private Knjiga InputKnjiga()
        {
            Console.WriteLine("Unesite ISBN: ");
            string isbn = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite naziv: ");
            string naziv = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite zanr: ");
            string zanr = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite godinu izdanja: ");
            int godinaIzdanja = ConsoleViewUtils.SafeInputInt();

            Console.WriteLine("Unesite cenu: ");
            decimal cena = ConsoleViewUtils.SafeInputDecimal();

            Console.WriteLine("Unesite broj strana: ");
            int brojStrana = ConsoleViewUtils.SafeInputInt();

            Console.WriteLine("Unesite ID izdavaca: ");
            int izdavacId = ConsoleViewUtils.SafeInputInt();

            return new Knjiga(isbn, naziv, zanr, godinaIzdanja, cena, brojStrana, izdavacId);
        }

        private void ShowAllKnjige()
        {
            PrintKnjige(_knjigaDao.GetAll());
        }

        private void PrintKnjige(List<Knjiga> knjige)
        {
            Console.WriteLine("\nKnjige:");
            if (knjige.Count == 0)
            {
                Console.WriteLine("Nema unetih knjiga.");
                return;
            }
            foreach (Knjiga k in knjige)
            {
                Console.WriteLine(k);
            }
        }

        private void UpdateKnjiga()
        {
            int id = InputId();
            Knjiga knjiga = InputKnjiga();
            knjiga.Id = id;
            Knjiga? updatedKnjiga = _knjigaDao.Update(knjiga);
            if (updatedKnjiga == null)
            {
                Console.WriteLine("Knjiga nije pronadjena.");
                return;
            }

            Console.WriteLine("Knjiga izmenjena.");
        }

        private int InputId()
        {
            Console.WriteLine("Unesite ID knjige: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void RemoveKnjiga()
        {
            int id = InputId();
            Knjiga? removedKnjiga = _knjigaDao.Remove(id);
            if (removedKnjiga == null)
            {
                Console.WriteLine("Knjiga nije pronadjena.");
                return;
            }

            Console.WriteLine("Knjiga obrisana.");
        }
    }
}