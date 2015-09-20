namespace DotIni.Test
{
    using System;
    using System.Collections.Generic;
    using DotIni;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            string[] options = file.Options("section0");
            Assert.IsNull(options);
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
Assert.AreEqual(7l, file.GetLong("Section4", "Number"));
        }

        [TestMethod]
        public void FloatIsReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
Assert.AreEqual(3.14f, file.GetFloat("Section4", "Pi"));
        }

        [TestMethod]
        public void DoubleIsReadCorrectly()
        {
            IniFile file = new IniFile("test.ini");
Assert.AreEqual(3.14d, file.GetDouble("Section4", "Pi"));
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
    }
}
