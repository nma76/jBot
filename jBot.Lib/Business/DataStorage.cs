using System;
using System.IO;

namespace jBot.Lib.Business
{
    public class DataStorage
    {
        //private variables
        private readonly string _filePrefix;
        private readonly string _datafolder;
        private readonly string _overlayFolder;

        public DataStorage(string DataFolder, string DataFilePrefix, string OverlayFolder)
        {
            _datafolder = DataFolder;
            _filePrefix = DataFilePrefix;
            _overlayFolder = OverlayFolder;
        }

        public void Save(string Identifier, long SinceId)
        {
            var dataFile = Path.Combine(_datafolder, $"{_filePrefix}_{Identifier}.dat");
            File.WriteAllText(dataFile, SinceId.ToString());
        }

        public long Load(string Identifier)
        {
            var dataFile = Path.Combine(_datafolder, $"{_filePrefix}_{Identifier}.dat");

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
            var dataFile = Path.Combine(_datafolder, $"{_filePrefix}_{Identifier}.dat");

            if (File.Exists(dataFile))
            {
                Save(Identifier, 0);
            }
        }

        public string DataFolder
        {
            get
            {
                return _datafolder;
            }
        }

        public string OverleyFolder
        {
            get
            {
                return _overlayFolder;
            }
        }
    }
}
