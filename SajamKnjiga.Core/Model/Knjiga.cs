using System.Globalization;

namespace SajamKnjiga.Model
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    public class Knjiga : ISerializable
    {
        public int Id { get; set; }
        public string Isbn { get; set; } = "";
        public string Naziv { get; set; } = "";
        public string Zanr { get; set; } = "";
        public int GodinaIzdanja { get; set; }
        public decimal Cena { get; set; }
        public int BrojStrana { get; set; }
        public int IzdavacId { get; set; }

        public Knjiga()
        {
        }

        public Knjiga(string isbn, string naziv, string zanr, int godinaIzdanja,
                      decimal cena, int brojStrana, int izdavacId)
        {
            Isbn = isbn;
            Naziv = naziv;
            Zanr = zanr;
            GodinaIzdanja = godinaIzdanja;
            Cena = cena;
            BrojStrana = brojStrana;
            IzdavacId = izdavacId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Isbn,
                Naziv,
                Zanr,
                GodinaIzdanja.ToString(),
                Cena.ToString(CultureInfo.InvariantCulture),
                BrojStrana.ToString(),
                IzdavacId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Isbn = values[1];
            Naziv = values[2];
            Zanr = values[3];
            GodinaIzdanja = int.Parse(values[4]);
            Cena = decimal.Parse(values[5], CultureInfo.InvariantCulture);
            BrojStrana = int.Parse(values[6]);
            IzdavacId = int.Parse(values[7]);
        }

        public override string ToString()
        {
            return $"ID: {Id,4} | ISBN: {Isbn} | {Naziv} | {Cena} | {GodinaIzdanja} | {Zanr}";
        }
    }
}