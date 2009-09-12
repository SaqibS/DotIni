namespace DotIni
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class IniFile
    {
        private string filename;

        public IniFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("The specified file was not found.", filename);
            }

            this.filename = filename;
        }

        public string[] Sections()
        {
            throw new NotImplementedException();
        }

        public bool HasSection(string section)
        {
            throw new NotImplementedException();
        }

        public string[] Options(string section)
        {
            throw new NotImplementedException();
        }

        public bool HasOption(string section, string option)
        {
            throw new NotImplementedException();
        }

        public string Get(string section, string option, string defaultValue = "")
        {
            throw new NotImplementedException();
        }

        public int GetInt(string section, string option, int defaultValue = 0)
        {
            throw new NotImplementedException();
        }

        public long GetLong(string section, string option, long defaultValue = 0)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(string section, string option, float defaultValue = 0)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(string section, string option, double defaultValue = 0)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(string section, string option, bool defaultValue = false)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<string, string>[] Items(string section)
        {
            throw new NotImplementedException();
        }

        public void Set(string section, string option, object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveOption(string section, string option)
        {
            throw new NotImplementedException();
        }

        public void RemoveSection(string section)
        {
            throw new NotImplementedException();
        }
    }
}
