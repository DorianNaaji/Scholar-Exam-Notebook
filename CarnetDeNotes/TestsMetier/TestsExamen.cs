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
    /// Test de la classe Examen
    /// </summary>
    [TestClass]
    public class TestsExamen
    {
        /// <summary>
        /// Tests liés au constructeur
        /// </summary>
        [TestMethod]
        public void TestExamen()
        {
            Examen e1 = new Examen(15.75F);
            e1.NomProf = "M. DOUSSOT";
            e1.DateExam = "17/11/2017";
            e1.Pondération = 1;
            Module m1 = new Module("SYSTEMES", 2.5F);
            m1.AjouterExamenModule(e1);

            Examen e2 = e1;

            Examen e3 = new Examen(13.25F);
            e2.NomProf = "M. DOUSSOT";
            e2.DateExam = "20/11/2017";
            e2.Pondération = 2;
            m1.AjouterExamenModule(e3);

            Assert.AreEqual(e1, e2);

            Note n11 = e1.Note;
            Note n33 = e3.Note;
            Assert.AreNotEqual(n11, n33);
        }
    }
}