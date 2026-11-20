using SajamKnjiga.Model;
using SajamKnjiga.Storage;

namespace SajamKnjiga.DAO
{
    public class PosetilacDAO
    {
        private readonly List<Posetilac> _posetioci;
        private readonly FileStorage<Posetilac> _storage;

        public PosetilacDAO()
        {
            _storage = new FileStorage<Posetilac>("posetioci.csv");
            _posetioci = _storage.Load();
        }

        private int GenerateId()
        {
            if (_posetioci.Count == 0)
            {
                return 1;
            }
            return _posetioci.Max(p => p.Id) + 1;
        }

        public List<Posetilac> GetAll()
        {
            return _posetioci;
        }

        public Posetilac? GetById(int id)
        {
            return _posetioci.Find(p => p.Id == id);
        }

        public Posetilac Add(Posetilac posetilac)
        {
            posetilac.Id = GenerateId();
            _posetioci.Add(posetilac);
            _storage.Save(_posetioci);
            return posetilac;
        }

        public Posetilac? Update(Posetilac posetilac)
        {
            Posetilac? stari = GetById(posetilac.Id);
            if (stari == null)
            {
                return null;
            }

            // Prosecna ocena i liste se ne menjaju kroz konzolu, pa se ovde ne prepisuju
            stari.Ime = posetilac.Ime;
            stari.Prezime = posetilac.Prezime;
            stari.DatumRodjenja = posetilac.DatumRodjenja;
            stari.AdresaId = posetilac.AdresaId;
            stari.Telefon = posetilac.Telefon;
            stari.Email = posetilac.Email;
            stari.BrojClanskeKarte = posetilac.BrojClanskeKarte;
            stari.GodinaClanstva = posetilac.GodinaClanstva;
            stari.Status = posetilac.Status;

            _storage.Save(_posetioci);
            return stari;
        }

        // Prosecnu ocenu racunaju kupovine posetioca (vidi KupovinaConsoleView)
        public Posetilac? UpdateProsecnaOcena(int id, double prosecnaOcena)
        {
            Posetilac? posetilac = GetById(id);
            if (posetilac == null)
            {
                return null;
            }

            posetilac.ProsecnaOcena = prosecnaOcena;

            _storage.Save(_posetioci);
            return posetilac;
        }

        public Posetilac? Remove(int id)
        {
            Posetilac? posetilac = GetById(id);
            if (posetilac == null)
            {
                return null;
            }

            _posetioci.Remove(posetilac);
            _storage.Save(_posetioci);
            return posetilac;
        }
    }
}