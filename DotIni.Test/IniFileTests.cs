namespace DotIni.Test
{
    using System;
    using System.Collections.Generic;
    using DotIni;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class IniFileTests
    {
        [TestMethod]
        public void SectionsAreReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            string[] sections = file.Sections();
            Assert.IsNotNull(sections);
            Assert.AreEqual(4, sections.Length);
            for (int i = 0; i < sections.Length; i++)
            {
                Assert.AreEqual("Section" + (i + 1), sections[i]);
            }
        }

        [TestMethod]
        public void HasSectionFindsSection()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(true, file.HasSection("section1"));
        }

        [TestMethod]
        public void HasSectionDoesntFindNonexistentSection()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(false, file.HasSection("section0"));
        }

        [TestMethod]
        public void ItemsAreReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            KeyValuePair<string, string>[] items = file.Items("section1");
            Assert.IsNotNull(items);
            Assert.AreEqual(3, items.Length);
            for (int i = 0; i < items.Length; i++)
            {
                Assert.AreEqual("Option" + (i + 1), items[i].Key);
                Assert.AreEqual("Value" + (i + 1), items[i].Value);
            }
        }

        [TestMethod]
        public void ItemsAreNotReadForNonexistentSection()
        {
            IniFile file = new IniFile("test.ini");
            KeyValuePair<string, string>[] items = file.Items("section0");
            Assert.IsNull(items);
        }

        [TestMethod]
        public void OptionsAreReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            string[] options = file.Options("section1");
            Assert.IsNotNull(options);
            Assert.AreEqual(3, options.Length);
            for (int i = 0; i < options.Length; i++)
            {
                Assert.AreEqual("Option" + (i + 1), options[i]);
            }
        }

        [TestMethod]
        public void OptionsAreNotReadForNonexistentSection()
        {
            IniFile file = new IniFile("test.ini");
            bool threwException = false;
            try
            {
                string[] options = file.Options("section0");
            }
            catch (ArgumentException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException);
        }

        [TestMethod]
        public void HasOptionFindsOption()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(true, file.HasOption("Section2", "Option4"));
        }

        [TestMethod]
        public void HasOptionDoesntFindNonexistentOption()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(false, file.HasOption("Section2", "Option2"));
        }

        [TestMethod]
        public void HasOptionDoesntFindOptionInNonexistentSection()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(false, file.HasOption("Section0", "Option0"));
        }

        [TestMethod]
        public void StringIsReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual("Hello", file.Get("Section4", "Text"));
        }

        [TestMethod]
        public void IntIsReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(7, file.GetInt("Section4", "Number"));
        }

        [TestMethod]
        public void LongIsReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(7L, file.GetLong("Section4", "Number"));
        }

        [TestMethod]
        public void FloatIsReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(3.14F, file.GetFloat("Section4", "Pi"));
        }

        [TestMethod]
        public void DoubleIsReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(3.14D, file.GetDouble("Section4", "Pi"));
        }

        [TestMethod]
        public void BoolIsReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(true, file.GetBoolean("Section4", "Positive"));
        }

        [TestMethod]
        public void DefaultStringIsReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
            Assert.AreEqual(string.Empty, file.Get("Section4", "Doesn't exist"));
        }

        [TestMethod]
        public void OptionIsSavedCorrectly()
        {
            string filename = Path.GetTempFileName();
            var file = new IniFile(filename);
            file.Set("Section1", "Option1", "Value1");
            Assert.AreEqual("Value1", file.Get("Section1", "Option1"));
            File.Delete(filename);
        }

        [TestMethod]
        public void OptionIsChangedCorrectly()
        {
            string filename = Path.GetTempFileName();
            var file = new IniFile(filename);
            file.Set("Section1", "Option1", "Value1");
            file.Set("Section1", "Option1", "Value2");
            Assert.AreEqual("Value2", file.Get("Section1", "Option1"));
            File.Delete(filename);
        }

        [TestMethod]
        public void NumberIsSavedCorrectly()
        {
            string filename = Path.GetTempFileName();
            var file = new IniFile(filename);
            file.Set("Section1", "Option1", 3.14);
            Assert.AreEqual(3.14, file.GetDouble("Section1", "Option1"));
            File.Delete(filename);
        }

        [TestMethod]
        public void OptionCanBeRemoved()
        {
            string filename = Path.GetTempFileName();
            var file = new IniFile(filename);
            file.Set("Section1", "Option1", "Value1");
            Assert.AreEqual(1, file.Options("Section1").Length);
            file.RemoveOption("Section1", "Option1");
            Assert.AreEqual(0, file.Options("Section1").Length);
            File.Delete(filename);
        }

        [TestMethod]
        public void SectionCanBeRemoved()
        {
            string filename = Path.GetTempFileName();
            var file = new IniFile(filename);
            file.Set("Section1", "Option1", "Value1");
            Assert.AreEqual(1, file.Sections().Length);
            file.RemoveSection("Section1");
            Assert.AreEqual(0, file.Sections().Length);
            File.Delete(filename);
        }
    }
}
