using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using metier;

namespace Sauvegarde
{
    public interface IListeUEdata
    {
        /// <summary>
        /// Fonction permettant de charger les données d'une listeUE
        /// </summary>
        /// <returns>La ListeUE désérialisée (xml to listeUE)</returns>
        ListeUE Charger();

        /// <summary>
        /// Fonction permettant de sauvegarder les données d'une listeUE
        /// </summary>
        /// <param name="liste">La liste à sauvegarder</param>
        /// <param name="nomFichier">Le nom du fichier dans lequel on veut sauvegarder la liste</param>
        void Sauver(ListeUE liste, String nomFichier);

        /// <summary>
        /// Fonction permattant de vérifier si le fichier où les données sont stockées est vide ou non
        /// </summary>
        /// <returns>True si le fichier est vide</returns>
        bool FichierVide();
    }
}
