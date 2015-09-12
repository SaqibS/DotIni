namespace DotIni.Test
{
    using System;
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
            Assert.AreEqual(3, sections.Length);
            for (int i = 0; i < sections.Length; i++)
            {
                Assert.AreEqual("Section" + (i + 1), sections[i]);
            }
        }
    }
}
