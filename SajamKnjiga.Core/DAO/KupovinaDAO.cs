using SajamKnjiga.Model;
using SajamKnjiga.Storage;

namespace SajamKnjiga.DAO
{
    public class KupovinaDAO
    {
        private readonly List<Kupovina> _kupovine;
        private readonly FileStorage<Kupovina> _storage;

        public KupovinaDAO()
        {
            _storage = new FileStorage<Kupovina>("kupovine.csv");
            _kupovine = _storage.Load();
        }

        private int GenerateId()
        {
            if (_kupovine.Count == 0)
            {
                return 1;
            }
            return _kupovine.Max(k => k.Id) + 1;
        }

        public List<Kupovina> GetAll()
        {
            return _kupovine;
        }

        public Kupovina? GetById(int id)
        {
            return _kupovine.Find(k => k.Id == id);
        }

        public Kupovina Add(Kupovina kupovina)
        {
            kupovina.Id = GenerateId();
            _kupovine.Add(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        public Kupovina? Update(Kupovina kupovina)
        {
            Kupovina? stara = GetById(kupovina.Id);
            if (stara == null)
            {
                return null;
            }

            stara.PosetilacId = kupovina.PosetilacId;
            stara.KnjigaId = kupovina.KnjigaId;
            stara.DatumKupovine = kupovina.DatumKupovine;
            stara.Ocena = kupovina.Ocena;
            stara.Komentar = kupovina.Komentar;

            _storage.Save(_kupovine);
            return stara;
        }

        public Kupovina? Remove(int id)
        {
            Kupovina? kupovina = GetById(id);
            if (kupovina == null)
            {
                return null;
            }

            _kupovine.Remove(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        // Brise sve kupovine jednog posetioca, vraca broj obrisanih kupovina
        public int RemoveByPosetilacId(int posetilacId)
        {
            int brojObrisanih = _kupovine.RemoveAll(k => k.PosetilacId == posetilacId);
            if (brojObrisanih > 0)
            {
                _storage.Save(_kupovine);
            }
            return brojObrisanih;
        }
    }
}