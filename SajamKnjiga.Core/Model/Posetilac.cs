using System.Globalization;

namespace SajamKnjiga.Model
{
    public class Posetilac : ISerializable
    {
        private const string DateFormat = "dd.MM.yyyy.";

        public int Id { get; set; }
        public string Ime { get; set; } = "";
        public string Prezime { get; set; } = "";
        public DateTime DatumRodjenja { get; set; }
        public int AdresaId { get; set; }
        public string Telefon { get; set; } = "";
        public string Email { get; set; } = "";
        public string BrojClanskeKarte { get; set; } = "";
        public int GodinaClanstva { get; set; }
        public StatusPosetioca Status { get; set; }

        // Racuna se iz ocena u kupovinama posetioca
        public double ProsecnaOcena { get; set; }


        // Kupovine i lista zelja posetioca, ne upisuju se u posetioci.csv
        public List<Kupovina> Kupovine { get; set; } = new List<Kupovina>();
        public List<Knjiga> ListaZelja { get; set; } = new List<Knjiga>();

        public Posetilac()
        {
        }

        public Posetilac(string ime, string prezime, DateTime datumRodjenja, int adresaId,
                         string telefon, string email, string brojClanskeKarte,
                         int godinaClanstva, StatusPosetioca status)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            AdresaId = adresaId;
            Telefon = telefon;
            Email = email;
            BrojClanskeKarte = brojClanskeKarte;
            GodinaClanstva = godinaClanstva;
            Status = status;
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
                BrojClanskeKarte,
                GodinaClanstva.ToString(),
                Status.ToString(),
                ProsecnaOcena.ToString(CultureInfo.InvariantCulture)
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
            BrojClanskeKarte = values[7];
            GodinaClanstva = int.Parse(values[8]);
            Status = Enum.Parse<StatusPosetioca>(values[9]);
            ProsecnaOcena = double.Parse(values[10], CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            return $"ID: {Id,4} | {BrojClanskeKarte} | {Ime} {Prezime} | " +
                   $"Adresa ID: {AdresaId} | {Status} | Prosecna ocena: {ProsecnaOcena:0.00}";
        }
    }
}