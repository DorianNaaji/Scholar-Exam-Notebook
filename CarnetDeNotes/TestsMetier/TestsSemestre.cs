using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using metier;

namespace TestsMetier
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestsSemestre
    {
        [TestMethod]
        public void TestSemestre()
        {
            Semestre s = new Semestre(0);
            Assert.AreEqual(s.NumeroSemestre, 0);
            Assert.AreEqual(s.ToString(), "Semestre 1");
        }

        [TestMethod]
        public void TestAjouterUEListerUE()
        {
            Semestre s = new Semestre(1);
            UE ue = new UE("INFO", 12);
            s.AjouterUE(ue);

            Assert.AreEqual(s.ListerUE()[0].Nom, ue.Nom);
            Assert.AreEqual(s.ListerUE()[0].Coefficient, ue.Coefficient);
            Assert.AreEqual(s.ListerUE()[0], ue); 
        }
    }
}
