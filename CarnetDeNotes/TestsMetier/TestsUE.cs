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
    /// Tests de la classe UE
    /// </summary>
    [TestClass]
    public class TestsUE
    {
        /// <summary>
        /// Test des constructeur.. pour être sur
        /// </summary>
        [TestMethod]
        public void TestUE()
        {
            try
            {
                UE ue1 = new UE("", 12.0F);
            }
            catch(ArgumentException)
            {
            }

            try
            {
                UE ue2 = new UE("INFO", -45.75F);
            }
            catch(ArgumentException)
            {
            }

            UE ue3 = new UE("INFO", 12.0F);
            String expecteValue1 = "INFO";
            float expectedValue2 = 12.0F;

            Assert.AreEqual(ue3.Nom, expecteValue1);
            Assert.AreEqual(ue3.Coefficient, expectedValue2);
        }

        /// <summary>
        /// Test de la fonction permettant de lister les modules d'une UE
        /// </summary>
        [TestMethod]
        public void TestListerModulesUE()
        {
            UE ue1 = new UE("INFO", 12.0F);
            List<Module> expResult = new List<Module>();

            Module m1 = new Module("SYSTEMES", 2.5F);

            ue1.InsererNouveauModule(m1);
            expResult.Add(m1);

            Assert.AreEqual(ue1.ListerModulesUE().ElementAt(0), expResult.ElementAt(0));
        }

        /// <summary>
        /// Test de la fonction permettant d'ajouter un module à une UE
        /// </summary>
        [TestMethod]
        public void TestInsererNouveauModule()
        {
            UE ue1 = new UE("INFO", 12.0F);
            ue1.InsererNouveauModule("SYSTEMES", 2.5F);
            ue1.InsererNouveauModule("PROG WEB", 2.0F);
            ue1.InsererNouveauModule("PROG OBJET", 2.5F);

            List<Module> expectedList = new List<Module>();
            Module m1 = new Module("SYSTEMES", 2.5F);
            Module m2 = new Module("PROG WEB", 2.0F);
            Module m3 = new Module("PROG OBJET", 2.5F);
            expectedList.Add(m1);
            expectedList.Add(m2);
            expectedList.Add(m3);

            Assert.AreEqual(ue1.ListerModulesUE().Count, expectedList.Count);
            Assert.AreEqual(ue1.ListerModulesUE().ElementAt(0).Nom, expectedList.ElementAt(0).Nom);

            Assert.AreEqual(ue1.ListerModulesUE().ElementAt(1).Nom, expectedList.ElementAt(1).Nom);
            Assert.AreEqual(ue1.ListerModulesUE().ElementAt(2).Nom, expectedList.ElementAt(2).Nom);
            try
            {
                Assert.AreEqual(ue1.ListerModulesUE().ElementAt(3), expectedList.ElementAt(3));
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e);
            }

            Module m4 = ue1.ListerModulesUE().ElementAt(0);
            UE ue2 = new UE("INFO", 12.0F);
            ue2.InsererNouveauModule(m4);
            Assert.AreEqual(ue1.ListerModulesUE().ElementAt(0), ue2.ListerModulesUE().ElementAt(0));
        }

        /// <summary>
        /// Test du calcul de la moyenne d'une UE
        /// </summary>
        [TestMethod]
        public void TestCalculerMoyenneUE()
        {
            UE ue1 = new UE("CULTURE", 5.5F);
            Module m1 = new Module("DROIT", 3);
            Module m2 = new Module("MATHS", 2.5F);
            Examen e1 = new Examen(15);
            Examen e2 = new Examen(12);

            m1.AjouterExamenModule(e1);
            m2.AjouterExamenModule(e2);

            ue1.InsererNouveauModule(m1);
            ue1.InsererNouveauModule(m2);
            // la moyenne vaut ((15*3)+(12*2.5)) / (3+2.5)
            // soit 13.636 qui arrondit donne 13.64

            Assert.AreEqual(ue1.CalculerMoyenneUE(), 13.64F);
        }


    }
}