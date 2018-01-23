using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text;
using metier;

namespace Sauvegarde
{
    /// <summary>
    /// Classe gérant la sérialisation.
    /// Gère la transformation d'une listeUE en XML
    /// (Persistance des données - fonctionnalité 5)
    /// </summary>
    public class ListeUEdataXml : IListeUEdata
    {
        private Stream flux;
        
        /// <summary>
        /// Constructeur à partir d'un flux
        /// </summary>
        /// <param name="flux"></param>
        public ListeUEdataXml(Stream flux)
        {
            this.flux = flux;
        }

        /// <summary>
        /// Constructeur à partir d'un chemin
        /// </summary>
        /// <param name="fichier"></param>
        public ListeUEdataXml(String fichier)
        {
            try
            {
                this.flux = new FileStream(fichier, FileMode.OpenOrCreate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex);
            }
        }

        /// <summary>
        /// Fonction permettant de charger les données d'une listeUE
        /// </summary>
        /// <returns>La ListeUE désérialisée (xml to listeUE)</returns>
        public ListeUE Charger()
        {
            try
            {
                if (this.FichierVide())
                {
                    return null;
                }
                else
                {
                    DataContractSerializer ser = new DataContractSerializer(typeof(ListeUE));
                    return ser.ReadObject(this.flux) as ListeUE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex);
                return null;
            }
        }

        /// <summary>
        /// Fonction permettant de sauvegarder les données d'une listeUE
        /// </summary>
        /// <param name="liste">La liste à sauvegarder</param>
        /// <param name="nomFichier">Le nom du fichier dans lequel on veut sauvegarder la liste</param>
        public void Sauver(ListeUE liste, String nomFichier)
        {

            try
            {
                if (!this.FichierVide())
                {
                    this.flux.Close();
                    this.flux = new FileStream(nomFichier, FileMode.Create);
                }
                DataContractSerializer ser = new DataContractSerializer(typeof(ListeUE));
                ser.WriteObject(this.flux, liste);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception : " + ex);
            }
        }

        /// <summary>
        /// Fonction permattant de vérifier si le fichier où les données sont stockées est vide ou non
        /// </summary>
        /// <returns>True si le fichier est vide</returns>
        public bool FichierVide()
        {
            try
            {
                if (this.flux.Length == 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex);
                return false;
            }
        }
    }
}