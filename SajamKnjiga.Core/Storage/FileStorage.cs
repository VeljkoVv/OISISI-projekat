using SajamKnjiga.Model;
using SajamKnjiga.Serialization;

namespace SajamKnjiga.Storage
{
    public class FileStorage<T> where T : ISerializable, new()
    {
        private const string DataFolder = "Data";

        private readonly string _filePath;
        private readonly Serializer<T> _serializer = new Serializer<T>();

        public FileStorage(string fileName)
        {
            _filePath = DataFolder + Path.DirectorySeparatorChar + fileName;
        }

        public List<T> Load()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            return _serializer.FromCSV(File.ReadAllLines(_filePath));
        }

        public void Save(List<T> entities)
        {
            Directory.CreateDirectory(DataFolder);
            File.WriteAllLines(_filePath, _serializer.ToCSV(entities));
        }
    }
}