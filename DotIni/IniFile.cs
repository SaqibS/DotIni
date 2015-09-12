namespace DotIni
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;

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

            this.filename = Path.GetFullPath(filename);
        }

        public string[] Sections()
        {
            const int BufferSize = 32 * 1024;
            IntPtr buffer = Marshal.AllocCoTaskMem(BufferSize);
            uint bytesReturned = NativeMethods.GetPrivateProfileSectionNames(buffer, BufferSize, filename);
            if (bytesReturned == 0)
            {
                Marshal.FreeCoTaskMem(buffer);
                return null;
            }

            string returnedString = Marshal.PtrToStringAuto(buffer, (int)bytesReturned).ToString();
            Marshal.FreeCoTaskMem(buffer);

            string[] sections = returnedString.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            return sections;
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
