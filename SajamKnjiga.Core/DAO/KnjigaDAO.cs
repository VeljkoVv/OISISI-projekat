using SajamKnjiga.Model;
using SajamKnjiga.Storage;

namespace SajamKnjiga.DAO
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    public class KnjigaDAO
    {
        private readonly List<Knjiga> _knjige;
        private readonly FileStorage<Knjiga> _storage;

        public KnjigaDAO()
        {
            _storage = new FileStorage<Knjiga>("knjige.csv");
            _knjige = _storage.Load();
        }

        private int GenerateId()
        {
            if (_knjige.Count == 0)
            {
                return 1;
            }
            return _knjige.Max(k => k.Id) + 1;
        }

        private Knjiga? GetById(int id)
        {
            return _knjige.Find(k => k.Id == id);
        }

        public List<Knjiga> GetAll()
        {
            return _knjige;
        }

        public Knjiga Add(Knjiga knjiga)
        {
            knjiga.Id = GenerateId();
            _knjige.Add(knjiga);
            _storage.Save(_knjige);
            return knjiga;
        }

        public Knjiga? Update(Knjiga knjiga)
        {
            Knjiga? stara = GetById(knjiga.Id);
            if (stara == null)
            {
                return null;
            }

            stara.Isbn = knjiga.Isbn;
            stara.Naziv = knjiga.Naziv;
            stara.Zanr = knjiga.Zanr;
            stara.GodinaIzdanja = knjiga.GodinaIzdanja;
            stara.Cena = knjiga.Cena;
            stara.BrojStrana = knjiga.BrojStrana;
            stara.IzdavacId = knjiga.IzdavacId;

            _storage.Save(_knjige);
            return stara;
        }

        public Knjiga? Remove(int id)
        {
            Knjiga? knjiga = GetById(id);
            if (knjiga == null)
            {
                return null;
            }

            _knjige.Remove(knjiga);
            _storage.Save(_knjige);
            return knjiga;
        }
    }
}