using Microsoft.VisualStudio.TestTools.UnitTesting;
using metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metier.Tests
{
    /// <summary>
    /// Test de la classe Note
    /// </summary>
    [TestClass]
    public class TestsNotes
    {
        /// <summary>
        /// Test du constructeur.. pour être sur
        /// </summary>
        [TestMethod]
        public void TestNote()
        {
            Note n1 = new Note(13F, true);
            float expectedResult = 0;

            Assert.AreEqual(n1.Valeur, expectedResult);

            Note n2 = n1;

            Assert.AreEqual(n1, n2);
            Assert.AreEqual(n1.Valeur, n2.Valeur);

            Note n3 = new Note(13F, false);

            Assert.AreNotEqual(n1, n3);

            try
            {
                Note n4 = new Note(-5F, false);
            }
            catch (ArgumentException)
            {
            }

            try
            {
                Note n5 = new Note(-5F, true);
            }
            catch (ArgumentException)
            {
            } 
        }
    }
}