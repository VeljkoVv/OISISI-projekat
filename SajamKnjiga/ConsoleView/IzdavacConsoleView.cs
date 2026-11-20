using SajamKnjiga.DAO;
using SajamKnjiga.Model;

namespace SajamKnjiga.ConsoleView
{
    public class IzdavacConsoleView
    {
        private readonly IzdavacDAO _izdavacDao;
        private readonly AutorDAO _autorDao;
        private readonly KnjigaDAO _knjigaDao;

        public IzdavacConsoleView(IzdavacDAO izdavacDao, AutorDAO autorDao, KnjigaDAO knjigaDao)
        {
            _izdavacDao = izdavacDao;
            _autorDao = autorDao;
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
            Console.WriteLine("\nIzdavaci - izaberite opciju:");
            Console.WriteLine("1: Dodaj izdavaca");
            Console.WriteLine("2: Prikazi sve izdavace");
            Console.WriteLine("3: Izmeni izdavaca");
            Console.WriteLine("4: Obrisi izdavaca");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    AddIzdavac();
                    break;
                case "2":
                    ShowAllIzdavaci();
                    break;
                case "3":
                    UpdateIzdavac();
                    break;
                case "4":
                    RemoveIzdavac();
                    break;
            }
        }

        private void AddIzdavac()
        {
            Izdavac izdavac = InputIzdavac();
            if (!IsSefValid(izdavac.SefId))
            {
                Console.WriteLine("Izdavac nije dodat.");
                return;
            }

            _izdavacDao.Add(izdavac);
            Console.WriteLine("Izdavac dodat.");
        }

        private Izdavac InputIzdavac()
        {
            Console.WriteLine("Unesite sifru: ");
            string sifra = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite naziv: ");
            string naziv = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite ID sefa izdavaca (0 ako izdavac nema sefa): ");
            int sefId = ConsoleViewUtils.SafeInputInt();

            return new Izdavac(sifra, naziv, sefId);
        }

        // Sef mora biti postojeci autor sa dovoljno godina iskustva (0 znaci da nema sefa)
        private bool IsSefValid(int sefId)
        {
            if (sefId == 0)
            {
                return true;
            }

            Autor? sef = _autorDao.GetAll().Find(a => a.Id == sefId);
            if (sef == null)
            {
                Console.WriteLine("Autor sa tim ID-jem ne postoji.");
                return false;
            }

            if (sef.GodineIskustva < Izdavac.MinGodineIskustvaSefa)
            {
                Console.WriteLine("Sef izdavaca mora imati najmanje " + Izdavac.MinGodineIskustvaSefa +
                                  " godina iskustva.");
                return false;
            }

            return true;
        }

        private void ShowAllIzdavaci()
        {
            PrintIzdavaci(_izdavacDao.GetAll());
        }

        private void PrintIzdavaci(List<Izdavac> izdavaci)
        {
            Console.WriteLine("\nIzdavaci:");
            if (izdavaci.Count == 0)
            {
                Console.WriteLine("Nema unetih izdavaca.");
                return;
            }
            foreach (Izdavac i in izdavaci)
            {
                Console.WriteLine(i);
                Console.WriteLine("      " + DescribeIzdavac(i));
            }
        }

        // Ime sefa i broj knjiga izdavaca, da prikaz bude citljiviji
        private string DescribeIzdavac(Izdavac izdavac)
        {
            string sefTekst = "nema";
            if (izdavac.SefId != 0)
            {
                sefTekst = "nepoznat (ID " + izdavac.SefId + ")";
                Autor? sef = _autorDao.GetAll().Find(a => a.Id == izdavac.SefId);
                if (sef != null)
                {
                    sefTekst = sef.Ime + " " + sef.Prezime;
                }
            }

            int brojKnjiga = _knjigaDao.GetAll().Count(k => k.IzdavacId == izdavac.Id);

            return "Sef: " + sefTekst + " | Broj knjiga: " + brojKnjiga;
        }

        private void UpdateIzdavac()
        {
            int id = InputId();
            Izdavac izdavac = InputIzdavac();
            izdavac.Id = id;
            if (!IsSefValid(izdavac.SefId))
            {
                Console.WriteLine("Izdavac nije izmenjen.");
                return;
            }

            Izdavac? updatedIzdavac = _izdavacDao.Update(izdavac);
            if (updatedIzdavac == null)
            {
                Console.WriteLine("Izdavac nije pronadjen.");
                return;
            }

            Console.WriteLine("Izdavac izmenjen.");
        }

        private int InputId()
        {
            Console.WriteLine("Unesite ID izdavaca: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void RemoveIzdavac()
        {
            int id = InputId();
            Izdavac? izdavac = _izdavacDao.GetById(id);
            if (izdavac == null)
            {
                Console.WriteLine("Izdavac nije pronadjen.");
                return;
            }

            // Knjige referenciraju izdavaca preko IzdavacId, pa izdavac sa knjigama ne moze da se obrise
            int brojKnjiga = _knjigaDao.GetAll().Count(k => k.IzdavacId == id);
            if (brojKnjiga > 0)
            {
                Console.WriteLine("Izdavac ima knjige (" + brojKnjiga + "), pa ne moze biti obrisan.");
                return;
            }

            _izdavacDao.Remove(id);
            Console.WriteLine("Izdavac obrisan.");
        }
    }
}