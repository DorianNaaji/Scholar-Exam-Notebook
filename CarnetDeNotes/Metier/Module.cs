using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// La partie métier du code
/// </summary>
namespace metier
{

    /// <summary>
    /// Classe permettant de gérer les modules.
    /// Un module possède un certain nom (chaîne de caractères, doit être non vide), est placé dans une unité
    /// d’enseignement, possède un certain coefficient (flottant, >0)
    /// </summary>
    [DataContract]
    public class Module
    {
        #region Attributs et propriétés

        [DataMember]
        private String nom;
        [DataMember]
        private float coefficient;
        [DataMember]
        private List<Examen> examens = new List<Examen>();

        /// <summary>
        /// Property r/w sur l'attribut nom
        /// <exception cref="ArgumentException">Coff doit être >0 </exception>
        /// </summary>
        public String Nom
        {
            get
            {
                return this.nom;
            }
            set
            {
                if ((value != ""))
                {
                    this.nom = value;
                }
                else
                {
                    // exception
                    throw new ArgumentException("Le nom du module ne peut être vide");
                }       
            }
        }

        /// <summary>
        /// Property r/w sur l'attribut coefficient
        /// <exception cref="ArgumentException">Coff doit être >0 </exception>
        /// </summary>
        public float Coefficient
        {
            get
            {
                return this.coefficient;
            }
            set
            {
                if (value > 0)
                {
                    this.coefficient = value;
                }
                else
                {
                    // exception
                    throw new ArgumentException("Le coefficient du module doit être supérieur à 0");
                }
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur d'un module avec nom et coefficient
        /// </summary>
        /// <param name="nom">Le nom du module</param>
        /// <param name="coeff">Le coefficient du module</param>
        public Module(String nom, float coeff)
        {
            // si le nom n'est pas nul
            if ((nom != ""))
            {
                this.nom = nom;
            }
            else
            {
                // exception
                throw new ArgumentException("Le nom du module ne peut être vide");
            }
            // si le coeff est supérieur à 0
            if (coeff > 0)
            {
                this.coefficient = coeff;
            }
            else
            {
                // exception
                throw new ArgumentException("Le coefficient du module doit être supérieur à 0");
            }
        }
        #endregion

        #region Gestion du module
        /// <summary>
        /// Permet d'ajouter un examen à la liste des examens du module
        /// </summary>
        /// <param name="e">l'examen à ajouter</param>
        public void AjouterExamenModule(Examen e)
        {
            this.examens.Add(e);
        }

        /// <summary>
        /// Permet de gérer l'affichage des modules dans l'IHM
        /// </summary>
        /// <returns>Une chaine décrivant le nom et le coefficient du module</returns>
        public override string ToString()
        {
            String text = this.nom + " (coeff : " + this.coefficient + ")";
            return text;
        }

        /// <summary>
        /// Méthode permettant de lister les examens d'un module
        /// </summary>
        /// <returns></returns>
        public List<Examen> ListerExamens()
        {
            List<Examen> listeExamens = this.examens;
            return listeExamens;
        }
        #endregion

        #region Fonctionnalité 4 : calcul moyenne Module
        /// <summary>
        /// Fonction permettant de calculer la moyenne obtenue dans un module
        /// </summary>
        /// <returns>La moyenne des notes obtenues aux différents examens</returns>
        public float CalculerMoyenneModule()
        {
            float sommeNotesPondérées = 0;
            float sommeCoefficient = 0;
            float moyenne = 0;
            foreach (Examen ex in this.ListerExamens())
            {
                sommeNotesPondérées += ex.Note.Valeur * ex.Pondération;
                sommeCoefficient += ex.Pondération;           
            }

            if ((sommeNotesPondérées == 0) || (sommeCoefficient == 0))
            {
                moyenne = 0;
            }
            else
            {
                moyenne = sommeNotesPondérées / sommeCoefficient;
                moyenne = (float)(Math.Round((double)moyenne, 2));
            }
            return moyenne;
        }
        #endregion
    }
}
