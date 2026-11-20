using SajamKnjiga.DAO;
using SajamKnjiga.Model;

namespace SajamKnjiga.ConsoleView
{
    public class KupovinaConsoleView
    {
        private readonly KupovinaDAO _kupovinaDao;
        private readonly PosetilacDAO _posetilacDao;
        private readonly KnjigaDAO _knjigaDao;

        public KupovinaConsoleView(KupovinaDAO kupovinaDao, PosetilacDAO posetilacDao, KnjigaDAO knjigaDao)
        {
            _kupovinaDao = kupovinaDao;
            _posetilacDao = posetilacDao;
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
            Console.WriteLine("\nKupovine - izaberite opciju:");
            Console.WriteLine("1: Dodaj kupovinu");
            Console.WriteLine("2: Prikazi sve kupovine");
            Console.WriteLine("3: Izmeni kupovinu");
            Console.WriteLine("4: Obrisi kupovinu");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    AddKupovina();
                    break;
                case "2":
                    ShowAllKupovine();
                    break;
                case "3":
                    UpdateKupovina();
                    break;
                case "4":
                    RemoveKupovina();
                    break;
            }
        }

        private void AddKupovina()
        {
            Kupovina kupovina = InputKupovina();
            if (!IsKupovinaValid(kupovina))
            {
                Console.WriteLine("Kupovina nije dodata.");
                return;
            }

            _kupovinaDao.Add(kupovina);
            UpdateProsecnaOcena(kupovina.PosetilacId);
            Console.WriteLine("Kupovina dodata.");
        }

        private Kupovina InputKupovina()
        {
            Console.WriteLine("Unesite ID posetioca: ");
            int posetilacId = ConsoleViewUtils.SafeInputInt();

            Console.WriteLine("Unesite ID knjige: ");
            int knjigaId = ConsoleViewUtils.SafeInputInt();

            Console.WriteLine("Unesite datum kupovine (dd.MM.yyyy.): ");
            DateTime datumKupovine = ConsoleViewUtils.SafeInputDate();

            int ocena = InputOcena();

            Console.WriteLine("Unesite komentar: ");
            string komentar = Console.ReadLine() ?? string.Empty;

            return new Kupovina(posetilacId, knjigaId, datumKupovine, ocena, komentar);
        }

        // Po uzoru na ConsoleViewUtils.SafeInputInt
        private int InputOcena()
        {
            Console.WriteLine("Unesite ocenu (" + Kupovina.MinOcena + "-" + Kupovina.MaxOcena + "): ");
            int ocena = ConsoleViewUtils.SafeInputInt();

            while (ocena < Kupovina.MinOcena || ocena > Kupovina.MaxOcena)
            {
                Console.WriteLine("Ocena mora biti izmedju " + Kupovina.MinOcena + " i " +
                                  Kupovina.MaxOcena + ", pokusajte ponovo: ");

                ocena = ConsoleViewUtils.SafeInputInt();
            }

            return ocena;
        }

        // Posetilac i knjiga moraju da postoje, a posetilac moze da kupi istu knjigu samo jednom
        private bool IsKupovinaValid(Kupovina kupovina)
        {
            if (_posetilacDao.GetById(kupovina.PosetilacId) == null)
            {
                Console.WriteLine("Posetilac sa tim ID-jem ne postoji.");
                return false;
            }

            if (_knjigaDao.GetAll().Find(k => k.Id == kupovina.KnjigaId) == null)
            {
                Console.WriteLine("Knjiga sa tim ID-jem ne postoji.");
                return false;
            }

            bool vecKupljena = _kupovinaDao.GetAll().Exists(k => k.PosetilacId == kupovina.PosetilacId
                                                                 && k.KnjigaId == kupovina.KnjigaId
                                                                 && k.Id != kupovina.Id);
            if (vecKupljena)
            {
                Console.WriteLine("Posetilac je vec kupio tu knjigu.");
                return false;
            }

            return true;
        }

        // Prosecna ocena posetioca se ne unosi rucno, nego se racuna iz njegovih kupovina
        private void UpdateProsecnaOcena(int posetilacId)
        {
            List<Kupovina> kupovine = _kupovinaDao.GetAll().FindAll(k => k.PosetilacId == posetilacId);

            double prosecnaOcena = 0;
            if (kupovine.Count > 0)
            {
                prosecnaOcena = Math.Round(kupovine.Average(k => k.Ocena), 2);
            }

            _posetilacDao.UpdateProsecnaOcena(posetilacId, prosecnaOcena);
        }

        private void ShowAllKupovine()
        {
            PrintKupovine(_kupovinaDao.GetAll());
        }

        private void PrintKupovine(List<Kupovina> kupovine)
        {
            Console.WriteLine("\nKupovine:");
            if (kupovine.Count == 0)
            {
                Console.WriteLine("Nema unetih kupovina.");
                return;
            }
            foreach (Kupovina k in kupovine)
            {
                Console.WriteLine(k);
                Console.WriteLine("      " + DescribeKupovina(k));
            }
        }

        // Ime posetioca i naziv knjige, da prikaz bude citljiviji
        private string DescribeKupovina(Kupovina kupovina)
        {
            string posetilacTekst = "nepoznat posetilac";
            Posetilac? posetilac = _posetilacDao.GetById(kupovina.PosetilacId);
            if (posetilac != null)
            {
                posetilacTekst = posetilac.BrojClanskeKarte + " " + posetilac.Ime + " " + posetilac.Prezime;
            }

            string knjigaTekst = "nepoznata knjiga";
            Knjiga? knjiga = _knjigaDao.GetAll().Find(k => k.Id == kupovina.KnjigaId);
            if (knjiga != null)
            {
                knjigaTekst = knjiga.Naziv + " (ISBN " + knjiga.Isbn + ")";
            }

            return "Posetilac: " + posetilacTekst + " | Knjiga: " + knjigaTekst;
        }

        private void UpdateKupovina()
        {
            int id = InputId();
            Kupovina kupovina = InputKupovina();
            kupovina.Id = id;
            if (!IsKupovinaValid(kupovina))
            {
                Console.WriteLine("Kupovina nije izmenjena.");
                return;
            }

            // Stari posetilac je potreban da bi mu se prosecna ocena ponovo izracunala
            Kupovina? stara = _kupovinaDao.GetById(id);
            if (stara == null)
            {
                Console.WriteLine("Kupovina nije pronadjena.");
                return;
            }
            int stariPosetilacId = stara.PosetilacId;

            _kupovinaDao.Update(kupovina);
            UpdateProsecnaOcena(kupovina.PosetilacId);
            if (stariPosetilacId != kupovina.PosetilacId)
            {
                UpdateProsecnaOcena(stariPosetilacId);
            }

            Console.WriteLine("Kupovina izmenjena.");
        }

        private int InputId()
        {
            Console.WriteLine("Unesite ID kupovine: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void RemoveKupovina()
        {
            int id = InputId();
            Kupovina? removedKupovina = _kupovinaDao.Remove(id);
            if (removedKupovina == null)
            {
                Console.WriteLine("Kupovina nije pronadjena.");
                return;
            }

            UpdateProsecnaOcena(removedKupovina.PosetilacId);
            Console.WriteLine("Kupovina obrisana.");
        }
    }
}