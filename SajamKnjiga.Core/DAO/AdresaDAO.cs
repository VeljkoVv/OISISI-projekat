using SajamKnjiga.Model;
using SajamKnjiga.Storage;

// Preuzeto iz CRUDExample (Vezbe 3)

namespace SajamKnjiga.DAO
{
    public class AdresaDAO
    {
        private readonly List<Adresa> _adrese;
        private readonly FileStorage<Adresa> _storage;

        public AdresaDAO()
        {
            _storage = new FileStorage<Adresa>("adrese.csv");
            _adrese = _storage.Load();
        }

        private int GenerateId()
        {
            if (_adrese.Count == 0)
            {
                return 1;
            }
            return _adrese.Max(a => a.Id) + 1;
        }

        public List<Adresa> GetAll()
        {
            return _adrese;
        }

        public Adresa? GetById(int id)
        {
            return _adrese.Find(a => a.Id == id);
        }

        public Adresa Add(Adresa adresa)
        {
            adresa.Id = GenerateId();
            _adrese.Add(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public Adresa? Update(Adresa adresa)
        {
            Adresa? stara = GetById(adresa.Id);
            if (stara == null)
            {
                return null;
            }

            stara.Ulica = adresa.Ulica;
            stara.Broj = adresa.Broj;
            stara.Grad = adresa.Grad;
            stara.Drzava = adresa.Drzava;

            _storage.Save(_adrese);
            return stara;
        }

        public Adresa? Remove(int id)
        {
            Adresa? adresa = GetById(id);
            if (adresa == null)
            {
                return null;
            }

            _adrese.Remove(adresa);
            _storage.Save(_adrese);
            return adresa;
        }
    }
}