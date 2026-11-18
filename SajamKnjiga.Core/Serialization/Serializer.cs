using SajamKnjiga.Model;

namespace SajamKnjiga.Serialization
{
    public static class Csv
    {
        public const char Delimiter = '|';
    }

    public class Serializer<T> where T : ISerializable, new()
    {
        public List<string> ToCSV(List<T> entities)
        {
            List<string> lines = new List<string>();
            foreach (T entity in entities)
            {
                lines.Add(string.Join(Csv.Delimiter, entity.ToCSV()));
            }
            return lines;
        }

        public List<T> FromCSV(IEnumerable<string> lines)
        {
            List<T> entities = new List<T>();
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                T entity = new T();
                entity.FromCSV(line.Split(Csv.Delimiter));
                entities.Add(entity);
            }
            return entities;
        }
    }
}