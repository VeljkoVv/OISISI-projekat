using SajamKnjiga.Model;
using SajamKnjiga.Storage;

namespace SajamKnjiga.DAO
{
    public class IzdavacDAO
    {
        private readonly List<Izdavac> _izdavaci;
        private readonly FileStorage<Izdavac> _storage;

        public IzdavacDAO()
        {
            _storage = new FileStorage<Izdavac>("izdavaci.csv");
            _izdavaci = _storage.Load();
        }

        private int GenerateId()
        {
            if (_izdavaci.Count == 0)
            {
                return 1;
            }
            return _izdavaci.Max(i => i.Id) + 1;
        }

        public List<Izdavac> GetAll()
        {
            return _izdavaci;
        }

        public Izdavac? GetById(int id)
        {
            return _izdavaci.Find(i => i.Id == id);
        }

        public Izdavac Add(Izdavac izdavac)
        {
            izdavac.Id = GenerateId();
            _izdavaci.Add(izdavac);
            _storage.Save(_izdavaci);
            return izdavac;
        }

        public Izdavac? Update(Izdavac izdavac)
        {
            Izdavac? stari = GetById(izdavac.Id);
            if (stari == null)
            {
                return null;
            }

            stari.Sifra = izdavac.Sifra;
            stari.Naziv = izdavac.Naziv;
            stari.SefId = izdavac.SefId;

            _storage.Save(_izdavaci);
            return stari;
        }

        public Izdavac? Remove(int id)
        {
            Izdavac? izdavac = GetById(id);
            if (izdavac == null)
            {
                return null;
            }

            _izdavaci.Remove(izdavac);
            _storage.Save(_izdavaci);
            return izdavac;
        }
    }
}