using System;
using System.IO;

namespace jBot.Lib
{
    //TODO: Add support for multiple sections in one file
    public class DataStorage
    {
        private long _lastSinceId = -1;
        private string _fileName;

        public DataStorage(string FileName)
        {
            _fileName = FileName;
        }

        public long LastSinceID
        {
            get
            {
                if (_lastSinceId == -1)
                {
                    if (File.Exists(_fileName))
                    {
                        long.TryParse(File.ReadAllText(_fileName), out _lastSinceId);
                    }
                    else
                    {
                        LastSinceID = 0;
                    }
                }
                return _lastSinceId;
            }
            set
            {
                _lastSinceId = value;
                File.WriteAllText(_fileName, _lastSinceId.ToString());
            }
        }
    }
}
