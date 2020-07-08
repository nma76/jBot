using System;
using System.IO;
using jBot.Lib.Models;

namespace jBot.Lib.Business
{
    public class DataStorage
    {
        //private variables
        private StorageSettings _storageSettings;

        public DataStorage(StorageSettings storageSettings)
        {
            _storageSettings = storageSettings;
        }

        public void Save(string Identifier, long SinceId)
        {
            var dataFile = Path.Combine(_storageSettings.Datafolder, $"{_storageSettings.FilePrefix}_{Identifier}.dat");
            File.WriteAllText(dataFile, SinceId.ToString());
        }

        public long Load(string Identifier)
        {
            var dataFile = Path.Combine(_storageSettings.Datafolder, $"{_storageSettings.FilePrefix}_{Identifier}.dat");

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
            var dataFile = Path.Combine(_storageSettings.Datafolder, $"{_storageSettings.FilePrefix}_{Identifier}.dat");

            if (File.Exists(dataFile))
            {
                Save(Identifier, 0);
            }
        }

        public string DataFolder
        {
            get
            {
                return _storageSettings.Datafolder;
            }
        }

        public string OverleyFolder
        {
            get
            {
                return _storageSettings.OverlayFolder;
            }
        }
    }
}
