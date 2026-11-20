using SajamKnjiga.DAO;
using SajamKnjiga.Model;

namespace SajamKnjiga.ConsoleView
{
    public class PosetilacConsoleView
    {
        private readonly PosetilacDAO _posetilacDao;
        private readonly KupovinaDAO _kupovinaDao;

        public PosetilacConsoleView(PosetilacDAO posetilacDao, KupovinaDAO kupovinaDao)
        {
            _posetilacDao = posetilacDao;
            _kupovinaDao = kupovinaDao;
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
            Console.WriteLine("\nPosetioci - izaberite opciju:");
            Console.WriteLine("1: Dodaj posetioca");
            Console.WriteLine("2: Prikazi sve posetioce");
            Console.WriteLine("3: Izmeni posetioca");
            Console.WriteLine("4: Obrisi posetioca");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    AddPosetilac();
                    break;
                case "2":
                    ShowAllPosetioci();
                    break;
                case "3":
                    UpdatePosetilac();
                    break;
                case "4":
                    RemovePosetilac();
                    break;
            }
        }

        private void AddPosetilac()
        {
            Posetilac posetilac = InputPosetilac();
            _posetilacDao.Add(posetilac);
            Console.WriteLine("Posetilac dodat.");
        }

        private Posetilac InputPosetilac()
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

            Console.WriteLine("Unesite broj clanske karte (npr. SK-1-2023): ");
            string brojClanskeKarte = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Unesite tekucu godinu clanstva: ");
            int godinaClanstva = ConsoleViewUtils.SafeInputInt();

            StatusPosetioca status = InputStatus();

            return new Posetilac(ime, prezime, datumRodjenja, adresaId, telefon, email,
                                 brojClanskeKarte, godinaClanstva, status);
        }

        // Po uzoru na ConsoleViewUtils.SafeInputInt
        private StatusPosetioca InputStatus()
        {
            Console.WriteLine("Unesite status (R - redovan, V - VIP): ");
            string rawInput = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();

            while (rawInput != "R" && rawInput != "V")
            {
                Console.WriteLine("Nije validan status (R ili V), pokusajte ponovo: ");

                rawInput = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
            }

            if (rawInput == "V")
            {
                return StatusPosetioca.Vip;
            }
            return StatusPosetioca.Redovan;
        }

        private void ShowAllPosetioci()
        {
            PrintPosetioci(_posetilacDao.GetAll());
        }

        private void PrintPosetioci(List<Posetilac> posetioci)
        {
            Console.WriteLine("\nPosetioci:");
            if (posetioci.Count == 0)
            {
                Console.WriteLine("Nema unetih posetilaca.");
                return;
            }
            foreach (Posetilac p in posetioci)
            {
                Console.WriteLine(p);
            }
        }

        private void UpdatePosetilac()
        {
            int id = InputId();
            Posetilac posetilac = InputPosetilac();
            posetilac.Id = id;
            Posetilac? updatedPosetilac = _posetilacDao.Update(posetilac);
            if (updatedPosetilac == null)
            {
                Console.WriteLine("Posetilac nije pronadjen.");
                return;
            }

            Console.WriteLine("Posetilac izmenjen.");
        }

        private int InputId()
        {
            Console.WriteLine("Unesite ID posetioca: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void RemovePosetilac()
        {
            int id = InputId();
            Posetilac? removedPosetilac = _posetilacDao.Remove(id);
            if (removedPosetilac == null)
            {
                Console.WriteLine("Posetilac nije pronadjen.");
                return;
            }

            // Kupovine obrisanog posetioca se brisu zajedno sa njim
            int brojKupovina = _kupovinaDao.RemoveByPosetilacId(id);

            Console.WriteLine("Posetilac obrisan.");
            if (brojKupovina > 0)
            {
                Console.WriteLine("Obrisane kupovine posetioca: " + brojKupovina);
            }
        }
    }
}