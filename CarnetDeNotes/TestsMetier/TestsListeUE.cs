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
    /// Test de la classe ListeUE
    /// </summary>
    [TestClass]
    public class TestsListeUE
    {

        /// <summary>
        /// Test de la méthode permettant d'ajouter une UE à la liste des UE
        /// </summary>
        [TestMethod]
        public void TestAjouterUE()
        {
            UE ue1 = new UE("INFO", 12.00F);

            ListeUE listeUE = new ListeUE();
            listeUE.AjouterUE(ue1);
            Assert.AreEqual(listeUE.ListerUE().ElementAt(0), ue1);

        }

        /// <summary>
        /// Test de la méthode permettant de lister les UE contenues dans la listes des UE
        /// </summary>
        [TestMethod]
        public void TestListerUE()
        {
            UE ue1 = new UE("INFO", 12.00F);
            UE ue2 = new UE("CULTURE", 12.00F);
            UE ue3 = new UE("METHODOLOGIE ET PROJETS", 6.00F);

            ListeUE listeUE = new ListeUE();
            listeUE.AjouterUE(ue1);
            listeUE.AjouterUE(ue2);
            listeUE.AjouterUE(ue3);

            Assert.AreEqual(listeUE.ListerUE().ElementAt(0), ue1);
            Assert.AreEqual(listeUE.ListerUE().ElementAt(1), ue2);
            Assert.AreEqual(listeUE.ListerUE().ElementAt(2), ue3);

            try
            {
                Assert.AreEqual(listeUE.ListerUE().ElementAt(3), ue3);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        /// <summary>
        /// Test de la méthode permettant de lister les modules
        /// </summary>
        [TestMethod]
        public void TestListerModules()
        {
            UE ue1 = new UE("INFO", 12.00F);
            Module m11 = new Module("SYSTEMES", 2.5F);
            Module m12 = new Module("PROG WEB", 2.0F);
            Module m13 = new Module("PROG OBJET", 2.5F);
            ue1.InsererNouveauModule(m11);
            ue1.InsererNouveauModule(m12);
            ue1.InsererNouveauModule(m13);
            Assert.AreEqual(m11, ue1.ListerModulesUE().ElementAt(0));
            Assert.AreEqual(m12, ue1.ListerModulesUE().ElementAt(1));
            Assert.AreEqual(m13, ue1.ListerModulesUE().ElementAt(2));

            UE ue2 = new UE("CULTURE", 12.00F);
            Module m21 = new Module("GESTION DES SI", 2.5F);
            Module m22 = new Module("EXPRESSION COMMUNICATION", 1.5F);
            Module m23 = new Module("PROBAS/STATS", 2.5F);
            ue2.InsererNouveauModule(m21);
            ue2.InsererNouveauModule(m22);
            ue2.InsererNouveauModule(m23);
            Assert.AreEqual(m21, ue2.ListerModulesUE().ElementAt(0));
            Assert.AreEqual(m22, ue2.ListerModulesUE().ElementAt(1));
            Assert.AreEqual(m23, ue2.ListerModulesUE().ElementAt(2));


        }

        /// <summary>
        /// Test du calcul de la moyenne générale
        /// </summary>
        [TestMethod]
        public void TestCalculerMoyenneGenerale()
        {
            ListeUE mesUE = new ListeUE();
            UE ue1 = new UE("CULTURE", 5.5F);
            Module m1 = new Module("DROIT", 3);
            Module m2 = new Module("MATHS", 2.5F);
            Examen e1 = new Examen(15);
            Examen e2 = new Examen(12);

            m1.AjouterExamenModule(e1);
            m2.AjouterExamenModule(e2);

            ue1.InsererNouveauModule(m1);
            ue1.InsererNouveauModule(m2);
            // la moyenne de l'UE ((15*3)+(12*2.5)) / (3+2.5)
            // soit 13.636 qui arrondit donne 13.64

            UE ue2 = new UE("INFO", 2);
            Module m3 = new Module("PROG", 2);
            Examen e3 = new Examen(15);
            m3.AjouterExamenModule(e3);
            ue2.InsererNouveauModule(m3);
            mesUE.AjouterUE(ue1);
            mesUE.AjouterUE(ue2);

            // une seule note : la moyenne vaut 15


            // la moyenne générale est donc normalement :
            // (15*2 + 13,64 * 5.5) / 7.5
            // Soit 14,002 et 14 une fois arrondi
            Assert.AreEqual(mesUE.CalculerMoyenneGenerale(), 14);

        }
    }
}