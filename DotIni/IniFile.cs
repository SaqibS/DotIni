﻿namespace DotIni
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;

    public class IniFile
    {
        private const int BufferSize = 32 * 1024;

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
            IntPtr buffer = Marshal.AllocCoTaskMem(BufferSize);
            try
            {
                uint bytesReturned = NativeMethods.GetPrivateProfileSectionNames(buffer, BufferSize, filename);
                if (bytesReturned == 0)
                {
                    return null;
                }

                string[] sections = SplitNullDelimitedBuffer(buffer, (int)bytesReturned);
                return sections;
            }
            finally
            {
                Marshal.FreeCoTaskMem(buffer);
            }
        }

        public bool HasSection(string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentNullException("section");
            }

            string[] sections = Sections();
            if (sections == null)
            {
                return false;
            }

            foreach (string s in sections)
            {
                if (s.Equals(section, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public string[] Options(string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentNullException("section");
            }

            IntPtr buffer = Marshal.AllocCoTaskMem(BufferSize);
            try
            {
                uint bytesReturned = NativeMethods.GetPrivateProfileString(section, null, null, buffer, BufferSize, filename);
                if (bytesReturned == 0)
                {
                    return null;
                }

                string[] options = SplitNullDelimitedBuffer(buffer, (int)bytesReturned);
                return options;
            }
            finally
            {
                Marshal.FreeCoTaskMem(buffer);
            }
        }

        public bool HasOption(string section, string option)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentNullException("section");
            }

            if (string.IsNullOrEmpty(option))
            {
                throw new ArgumentNullException("option");
            }

            string[] options = Options(section);
            if (options == null)
            {
                return false;
            }

            foreach (string o in options)
            {
                if (o.Equals(option, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public string Get(string section, string option, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentNullException("section");
            }

            if (string.IsNullOrEmpty("option"))
            {
                throw new ArgumentNullException("option");
            }

            IntPtr buffer = Marshal.AllocCoTaskMem(BufferSize);
            try
            {
                uint bytesReturned = NativeMethods.GetPrivateProfileString(section, option, defaultValue, buffer, BufferSize, filename);
                if (bytesReturned == 0)
                {
                    return defaultValue;
                }

                string value = Marshal.PtrToStringAuto(buffer, (int)bytesReturned).ToString();
                return value;
            }
            finally
            {
                Marshal.FreeCoTaskMem(buffer);
            }
        }

        public int GetInt(string section, string option, int defaultValue = 0)
        {
            string stringValue = Get(section, option, defaultValue.ToString());
            int value;
            if (!int.TryParse(stringValue, out value))
            {
                value = defaultValue;
            }

            return value;
        }

        public long GetLong(string section, string option, long defaultValue = 0)
        {
            string stringValue = Get(section, option, defaultValue.ToString());
            long value;
            if (!long.TryParse(stringValue, out value))
            {
                value = defaultValue;
            }

            return value;
        }

        public float GetFloat(string section, string option, float defaultValue = 0)
        {
            string stringValue = Get(section, option, defaultValue.ToString());
            float value;
            if (!float.TryParse(stringValue, out value))
            {
                value = defaultValue;
            }

            return value;
        }

        public double GetDouble(string section, string option, double defaultValue = 0)
        {
            string stringValue = Get(section, option, defaultValue.ToString());
            double value;
            if (!double.TryParse(stringValue, out value))
            {
                value = defaultValue;
            }

            return value;
        }

        public bool GetBoolean(string section, string option, bool defaultValue = false)
        {
            string stringValue = Get(section, option, defaultValue.ToString()).Trim().ToLower();
            if (stringValue == "true" || stringValue == "1" || stringValue == "yes")
            {
                return true;
            }
            else if (stringValue == "false" || stringValue == "0" || stringValue == "no")
            {
                return false;
            }
            else
            {
                return defaultValue;
            }
        }

        public KeyValuePair<string, string>[] Items(string section)
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(BufferSize);
            try
            {
                uint bytesReturned = NativeMethods.GetPrivateProfileSection(section, buffer, BufferSize, filename);
                if (bytesReturned == 0)
                {
                    return null;
                }

                string[] tokens = SplitNullDelimitedBuffer(buffer, (int)bytesReturned);
                KeyValuePair<string, string>[] items = new KeyValuePair<string, string>[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                {
                    int equalsPos = tokens[i].IndexOf('=');
                    string option = tokens[i].Substring(0, equalsPos).Trim();
                    string value = tokens[i].Substring(equalsPos + 1).Trim();
                    items[i] = new KeyValuePair<string, string>(option, value);
                }

                return items;
            }
            finally
            {
                Marshal.FreeCoTaskMem(buffer);
            }
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

        private static string[] SplitNullDelimitedBuffer(IntPtr buffer, int length)
        {
            string s = Marshal.PtrToStringAuto(buffer, length - 1).ToString();
            string[] parts = s.Split('\0');
            return parts;
        }
    }
}
