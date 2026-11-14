namespace SajamKnjiga.Model
{
    public interface ISerializable
    {
        int Id { get; set; }

        string[] ToCSV();

        void FromCSV(string[] values);
    }
}
