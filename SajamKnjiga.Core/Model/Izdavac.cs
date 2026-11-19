namespace SajamKnjiga.Model
{
    public class Izdavac : ISerializable
    {
        // Sef izdavaca mora biti autor sa najmanje toliko godina iskustva
        public const int MinGodineIskustvaSefa = 5;

        public int Id { get; set; }
        public string Sifra { get; set; } = "";
        public string Naziv { get; set; } = "";

        // ID autora koji je sef izdavaca, 0 znaci da izdavac nema sefa
        public int SefId { get; set; }


        // Autori i knjige izdavaca, ne upisuju se u izdavaci.csv
        public List<Autor> Autori { get; set; } = new List<Autor>();
        public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();

        public Izdavac()
        {
        }

        public Izdavac(string sifra, string naziv, int sefId)
        {
            Sifra = sifra;
            Naziv = naziv;
            SefId = sefId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Sifra,
                Naziv,
                SefId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Sifra = values[1];
            Naziv = values[2];
            SefId = int.Parse(values[3]);
        }

        public override string ToString()
        {
            return $"ID: {Id,4} | Sifra: {Sifra} | {Naziv} | Sef ID: {SefId}";
        }
    }
}