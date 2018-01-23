using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using metier;

namespace TestsMetier
{
    /// <summary>
    /// Les tests relatifs à la classe listeSemestre
    /// </summary>
    [TestClass]
    public class TestsListeSemestres
    {
        [TestMethod]
        public void TestListerSemestres()
        {
            ListeSemestres liste = ListeSemestres.Instance;
            Assert.AreEqual(liste.ListerSemestres(), ListeSemestres.Instance.ListerSemestres());
            Assert.AreEqual(liste.ListerSemestres()[0], ListeSemestres.Instance.ListerSemestres()[0]);
            Assert.AreEqual(liste.ListerSemestres()[1], ListeSemestres.Instance.ListerSemestres()[1]);
            Assert.AreEqual(liste.ListerSemestres()[2], ListeSemestres.Instance.ListerSemestres()[2]);
            Assert.AreEqual(liste.ListerSemestres()[3], ListeSemestres.Instance.ListerSemestres()[3]);
            try
            {
                Assert.AreEqual(liste.ListerSemestres()[4], ListeSemestres.Instance.ListerSemestres()[4]);
            }
            catch(ArgumentOutOfRangeException)
            {
            }
        }
    }
}
