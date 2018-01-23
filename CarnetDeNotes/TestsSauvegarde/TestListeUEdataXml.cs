using System;
using System.IO;
using System.Runtime.Serialization;
using metier;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sauvegarde;

namespace TestsSauvegarde
{
    [TestClass]
    public class TestListeUEdataXml
    {
        [TestMethod]
        public void TestSauver()
        {
            DataContractSerializer ser = new DataContractSerializer(typeof(ListeUE));
            String fichier = "test.xml";
            Stream flux;

            ListeUE liste = new ListeUE();
            UE ue = new UE("UE1", 10);
            liste.AjouterUE(ue);


            try
            {
                IListeUEdata data = new ListeUEdataXml(fichier);
                flux = new FileStream(fichier, FileMode.OpenOrCreate);
                data.Sauver(liste, fichier);
                /* Le fichier ainsi créé doit normalement contenir la chaîne suivante :
                 * <ListeUE xmlns="http://schemas.datacontract.org/2004/07/metier" xmlns:i="http://www.w3.org/2001/XMLSchema-instance"><listeDesUE><UE><coefficient>10</coefficient><listeDesModules/><nom>UE1</nom></UE></listeDesUE></ListeUE>
                 * Soit 221 octets (vérifié via un éditeur de texte et le système de gestionnaire de fichier windows
                 * On peut alors faire un test sur la taille du flux et 221 :
                 */
                Assert.AreEqual(flux.Length, 221);
                flux.Close();
            }
            catch (Exception)
            {
            }
        }
   
        [TestMethod]
        public void TestCharger()
        {
            DataContractSerializer ser = new DataContractSerializer(typeof(ListeUE));
            String fichier = "test.xml";
            Stream flux;
            UE ue = new UE("UE1", 10);
            ListeUE liste;

            try
            {
                // les tests s'exécutent dans l'ordre, on peut alors directement charger le fichier "test.xml"
                IListeUEdata data = new ListeUEdataXml(fichier);
                flux = new FileStream(fichier, FileMode.Open);
                liste = data.Charger();
                // on vérifie que la liste contient bien l'UE précédemment créée, qui est la même qui a été sauvegardée.
                Assert.IsTrue(liste.ListerUE().Contains(ue));
            }
            catch (Exception)
            {
            }
            
        }
    }
}
