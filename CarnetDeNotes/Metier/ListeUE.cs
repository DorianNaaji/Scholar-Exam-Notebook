using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// La partie métier du code
/// </summary>
namespace metier
{
     /// <summary>
     /// Classe ListeUE étant un conteneur qui permet de regrouper plusieurs UE.
     /// </summary>
    [DataContract]
    public class ListeUE
    {
        [DataMember]
        private List<UE> listeDesUE = null;

        /// <summary>
        /// Constructeur sans paramètres
        /// </summary>
        public ListeUE()
        {
            this.listeDesUE = new List<UE>();
        }

        #region Gestion de la ListeUE
        /// <summary>
        /// Méthode permettant d'ajouter une UE à la liste des UE
        /// </summary>
        /// <param name="ue">L'UE à ajouter</param>
        public void AjouterUE(UE ue)
        {
            this.listeDesUE.Add(ue);
        }

        /// <summary>
        /// Permet de lister les différentes UE
        /// </summary>
        /// <returns>Une copie de l'attribut listeDesUE</returns>
        public List<UE> ListerUE()
        {
            List<UE> listeUE = this.listeDesUE;
            return listeUE;
        }

        /// <summary>
        /// Méthode permettant de lister les modules des UE contenues dans la liste des UE
        /// </summary>
        /// <returns>la liste de tous les modules des UE</returns>
        public List<Module> ListerModules()
        {
            List<Module> listeMod = new List<Module>();
            // pour chaque UE
            foreach (UE ue in this.listeDesUE)
            {
                // pour chaque module
                foreach (Module m in ue.ListerModulesUE())
                {
                    // on l'ajoute à la liste des modules que l'on va retourner
                    listeMod.Add(m);
                }
            }
            return listeMod;
        }
        #endregion

        #region Fonctionnalité 4 : calcul moyenne générale

        /// <summary>
        /// Fonction permettant de calculer la moyenne générale
        /// </summary>
        /// <returns>La moyenne des notes obtenues aux différentes UE</returns>
        public float CalculerMoyenneGenerale()
        {
            float sommePondérée = 0;
            float sommeCoefficient = 0;
            float moyenne = 0;
            foreach (UE ue in this.ListerUE())
            {
                sommePondérée += ue.CalculerMoyenneUE() * ue.Coefficient;
                sommeCoefficient += ue.Coefficient;
            }
            if (sommePondérée == 0 || sommeCoefficient == 0)
            {
                moyenne = 0;
            }
            else
            {
                moyenne = sommePondérée / sommeCoefficient;
                moyenne = (float)(Math.Round((double)moyenne, 2));
            }
            return moyenne;
        }

        #endregion
    }
}
