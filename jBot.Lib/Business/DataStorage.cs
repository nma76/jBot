using System;
using System.IO;

namespace jBot.Lib.Business
{
    public class DataStorage
    {
        //private variables
        private readonly string _filePrefix;
        private readonly string _folder;

        public DataStorage(string Folder, string FilePrefix)
        {
            _folder = Folder;
            _filePrefix = FilePrefix;
        }

        public void Save(string Identifier, long SinceId)
        {
            var dataFile = Path.Combine(_folder, $"{_filePrefix}_{Identifier}.dat");
            File.WriteAllText(dataFile, SinceId.ToString());
        }

        public long Load(string Identifier)
        {
            var dataFile = Path.Combine(_folder, $"{_filePrefix}_{Identifier}.dat");

            try
            {
                if (File.Exists(dataFile))
                {
                    long.TryParse(File.ReadAllText(dataFile), out long SinceId);
                    return SinceId;
                }
                else
                {
                    Save(Identifier, 0);
                }
            }
            catch { }

            return 0;
        }

        public void Reset(string Identifier)
        {
            var dataFile = Path.Combine(_folder, $"{_filePrefix}_{Identifier}.dat");

            if (File.Exists(dataFile))
            {
                Save(Identifier, 0);
            }
        }

        public string Folder
        {
            get
            {
                return _folder;
            }
        }
    }
}
