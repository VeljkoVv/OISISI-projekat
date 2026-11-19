using System.Globalization;

namespace SajamKnjiga.Model
{
    public class Kupovina : ISerializable
    {
        private const string DateFormat = "dd.MM.yyyy.";

        // Dozvoljen opseg ocene
        public const int MinOcena = 1;
        public const int MaxOcena = 5;

        public int Id { get; set; }
        public int PosetilacId { get; set; }
        public int KnjigaId { get; set; }
        public DateTime DatumKupovine { get; set; }
        public int Ocena { get; set; }
        public string Komentar { get; set; } = "";

        public Kupovina()
        {
        }

        public Kupovina(int posetilacId, int knjigaId, DateTime datumKupovine, int ocena, string komentar)
        {
            PosetilacId = posetilacId;
            KnjigaId = knjigaId;
            DatumKupovine = datumKupovine;
            Ocena = ocena;
            Komentar = komentar;
        }

        public string[] ToCSV()
        {
            // Komentar je slobodan tekst, zato je poslednji u liniji
            string[] csvValues = {
                Id.ToString(),
                PosetilacId.ToString(),
                KnjigaId.ToString(),
                DatumKupovine.ToString(DateFormat, CultureInfo.InvariantCulture),
                Ocena.ToString(),
                Komentar
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            PosetilacId = int.Parse(values[1]);
            KnjigaId = int.Parse(values[2]);
            DatumKupovine = DateTime.ParseExact(values[3], DateFormat, CultureInfo.InvariantCulture);
            Ocena = int.Parse(values[4]);
            Komentar = values[5];
        }

        public override string ToString()
        {
            return $"ID: {Id,4} | Posetilac ID: {PosetilacId} | Knjiga ID: {KnjigaId} | " +
                   $"{DatumKupovine.ToString(DateFormat, CultureInfo.InvariantCulture)} | " +
                   $"Ocena: {Ocena} | Komentar: {Komentar}";
        }
    }
}