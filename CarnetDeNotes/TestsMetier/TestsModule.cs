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
    /// Test de la classe Module
    /// </summary>
    [TestClass]
    public class TestsModule
    {
        /// <summary>
        /// Test liés aux constructeurs.. pour être sur
        /// </summary>
        [TestMethod]
        public void TestModule()
        {
            Module m1 = new Module("M3104 - Prog Web", 2.5F);
            Module m2 = new Module("M3104 - Prog Web", 2.5F);
            Module m3 = new Module("M3104 - Prog Web", 2.6F);
            Module m4 = m1;

            Assert.AreEqual(m1, m1);
            Assert.AreEqual(m1, m4);
            Assert.AreNotEqual(m1, m2);
            Assert.AreNotEqual(m1, m3);        
        }

        /// <summary>
        /// Test constructeur 2
        /// </summary>
        [TestMethod]
        public void TestModule2()
        {
            try
            {
                Module m1 = new Module("M3104 - Prog Web", -1F);
            }
            catch(ArgumentException)
            {
            }
        }

        /// <summary>
        /// Test e la fonction ListerNotes();
        /// </summary>
        [TestMethod]
        public void TestListerNotes()
        {
            Module m1 = new Module("SYSTEMES", 2.5F);
            Examen e1 = new Examen(15);
            Examen e2 = new Examen(true);
            Examen e3 = new Examen(false);
            e3.Note.Valeur = 11.5F;
            m1.AjouterExamenModule(e1);
            m1.AjouterExamenModule(e2);
            m1.AjouterExamenModule(e3);

            Assert.AreEqual(m1.ListerExamens().ElementAt(0), e1);
            Assert.AreEqual(m1.ListerExamens().ElementAt(0).Note, e1.Note);
            Assert.AreEqual(m1.ListerExamens().ElementAt(1).Note.Valeur, 0);
            Assert.AreEqual(m1.ListerExamens().ElementAt(2).Note.Valeur, 11.5F);

        }

        /// <summary>
        /// Test 1 du calcul de la moyenne d'un module
        /// </summary>
        [TestMethod]
        public void TestCalculerMoyenneModule()
        {
            Module m1 = new Module("Maths", 5);
            Examen ex1 = new Examen(11)
            {
                Pondération = 4
            };
            Examen ex2 = new Examen(6)
            {
                Pondération = 3
            };

            Examen ex3 = new Examen(6)
            {
                Pondération = 2
            };

            Examen ex4 = new Examen(10)
            {
                Pondération = 1
            };
            m1.AjouterExamenModule(ex1);
            m1.AjouterExamenModule(ex2);
            m1.AjouterExamenModule(ex3);
            m1.AjouterExamenModule(ex4);

            // cette moyenne devrait renvoyer 8,4
            Assert.AreEqual(m1.CalculerMoyenneModule(), 8.4F);
        }

        /// <summary>
        /// Test 2 de la calcul de la moyenne d'un module
        /// </summary>
        [TestMethod]
        public void TestCalculerMoyenneModule2()
        {
            Module m1 = new Module("Maths", 5);
            Examen ex1 = new Examen(10);
            Examen ex2 = new Examen(14);
            Examen ex3 = new Examen(18);
            m1.AjouterExamenModule(ex1);
            m1.AjouterExamenModule(ex2);
            m1.AjouterExamenModule(ex3);

            Console.WriteLine(m1.CalculerMoyenneModule());
            Assert.AreEqual(m1.CalculerMoyenneModule(), 14F);

            Module m2 = new Module("Maths", 2);
            Examen ex4 = new Examen(true);
            Assert.AreEqual(m2.CalculerMoyenneModule(), 0);

            Module m3 = new Module("Mod", 4);
            Examen ex5 = new Examen(0);
            Assert.AreEqual(m3.CalculerMoyenneModule(), 0);
        }
    }
}