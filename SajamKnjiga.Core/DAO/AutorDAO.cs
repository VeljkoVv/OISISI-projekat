using SajamKnjiga.Model;
using SajamKnjiga.Storage;

namespace SajamKnjiga.DAO
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    public class AutorDAO
    {
        private readonly List<Autor> _autori;
        private readonly FileStorage<Autor> _storage;

        public AutorDAO()
        {
            _storage = new FileStorage<Autor>("autori.csv");
            _autori = _storage.Load();
        }

        private int GenerateId()
        {
            if (_autori.Count == 0)
            {
                return 1;
            }
            return _autori.Max(a => a.Id) + 1;
        }

        private Autor? GetById(int id)
        {
            return _autori.Find(a => a.Id == id);
        }

        public List<Autor> GetAll()
        {
            return _autori;
        }

        public Autor Add(Autor autor)
        {
            autor.Id = GenerateId();
            _autori.Add(autor);
            _storage.Save(_autori);
            return autor;
        }

        public Autor? Update(Autor autor)
        {
            Autor? stari = GetById(autor.Id);
            if (stari == null)
            {
                return null;
            }

            stari.Ime = autor.Ime;
            stari.Prezime = autor.Prezime;
            stari.DatumRodjenja = autor.DatumRodjenja;
            stari.AdresaId = autor.AdresaId;
            stari.Telefon = autor.Telefon;
            stari.Email = autor.Email;
            stari.BrojLicneKarte = autor.BrojLicneKarte;
            stari.GodineIskustva = autor.GodineIskustva;

            _storage.Save(_autori);
            return stari;
        }

        public Autor? Remove(int id)
        {
            Autor? autor = GetById(id);
            if (autor == null)
            {
                return null;
            }

            _autori.Remove(autor);
            _storage.Save(_autori);
            return autor;
        }
    }
}