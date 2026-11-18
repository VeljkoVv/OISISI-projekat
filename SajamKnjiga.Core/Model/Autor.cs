using System.Globalization;

namespace SajamKnjiga.Model
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    public class Autor : ISerializable
    {
        private const string DateFormat = "dd.MM.yyyy.";

        public int Id { get; set; }
        public string Ime { get; set; } = "";
        public string Prezime { get; set; } = "";
        public DateTime DatumRodjenja { get; set; }
        public int AdresaId { get; set; }
        public string Telefon { get; set; } = "";
        public string Email { get; set; } = "";
        public string BrojLicneKarte { get; set; } = "";
        public int GodineIskustva { get; set; }

        public Autor()
        {
        }

        public Autor(string ime, string prezime, DateTime datumRodjenja, int adresaId,
                     string telefon, string email, string brojLicneKarte, int godineIskustva)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            AdresaId = adresaId;
            Telefon = telefon;
            Email = email;
            BrojLicneKarte = brojLicneKarte;
            GodineIskustva = godineIskustva;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Ime,
                Prezime,
                DatumRodjenja.ToString(DateFormat, CultureInfo.InvariantCulture),
                AdresaId.ToString(),
                Telefon,
                Email,
                BrojLicneKarte,
                GodineIskustva.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateTime.ParseExact(values[3], DateFormat, CultureInfo.InvariantCulture);
            AdresaId = int.Parse(values[4]);
            Telefon = values[5];
            Email = values[6];
            BrojLicneKarte = values[7];
            GodineIskustva = int.Parse(values[8]);
        }

        public override string ToString()
        {
            return $"ID: {Id,4} | {Ime} {Prezime} | LK: {BrojLicneKarte} | " +
                   $"{DatumRodjenja.ToString(DateFormat)} | {Email}";
        }
    }
}