using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// La partie métier du code
/// </summary>
namespace metier
{
    /// <summary>
    /// Classe permettant de gérer les unités d'enseignement.
    /// Une unité d’enseignement regroupe un certain nombre de modules, 
    /// et possède un certain coefficient (flottant >0)
    /// </summary>
    [DataContract]
    public class UE
    {
        #region Attributs et propriétés

        [DataMember]
        private List<Module> listeDesModules = new List<Module>();
        [DataMember]
        private String nom; 
        [DataMember]
        private float coefficient;
        [DataMember]
        private Semestre semestre;

        public Semestre Semestre
        {
            get
            {
                return this.semestre;
            }
            set
            {
                this.semestre = value;
            }
        }

        /// <summary>
        /// Nom de l'UE :
        /// Property r/w
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
                    throw new ArgumentException("Le nom de l'UE ne peut être vide");
                }             
            }
        }

        /// <summary>
        /// Coefficient de l'UE :
        /// property r/w
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
                    throw new ArgumentException("Le coefficient de l'UE doit être supérieur à 0");
                }             
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur d'une UE via deux paramètres
        /// </summary>
        /// <param name="nom"> Le nom de l'UE</param>
        /// <param name="coeff">Le coefficient de l'UE</param>
        /// <exception cref="ArgumentException"> Le nom de l'UE doit être différent de la chaîne vide
        /// et l'UE doit posséder un coefficient strictement positif</exception>
        public UE(String nom, float coeff)
        {
            if ((nom != ""))
            {
                this.nom = nom;
            }
            else
            {
                throw new ArgumentException("Le nom de l'UE ne peut être vide");
            }
            if (coeff > 0)
            {
                this.coefficient = coeff;
            }
            else
            {
                throw new ArgumentException("Le coefficient de l'UE doit être supérieur à 0");
            }
        }
        #endregion

        #region Gestion de l'UE
        /// <summary>
        /// Permet d'insérer un nouveau module dans l'UE
        /// </summary>
        /// <param name="nomModule">Le nom du module à insérer</param>
        /// <param name="coeffModule">Le coefficient du module à insérer</param>
        public void InsererNouveauModule(String nomModule, float coeffModule)
        {
            Module mod = new Module(nomModule, coeffModule);
            this.listeDesModules.Add(mod);
        }

        /// <summary>
        /// Permet d'insérer un nouveau module dans l'UE
        /// </summary>
        /// <param name="m">un objet de type Module</param>
        public void InsererNouveauModule(Module m)
        {
            this.listeDesModules.Add(m);
        }

        /// <summary>
        /// Permet de lister les modules de l'UE
        /// </summary>
        /// <returns>Une copie de l'attribut contenant la liste des modules de l'UE</returns>
        public List<Module> ListerModulesUE()
        {
            List<Module> modulesUE = this.listeDesModules;
            return modulesUE;
        }

        /// <summary>
        /// Permet de gérer l'IHM
        /// </summary>
        /// <returns>Une chaîne décrivant l'UE</returns>
        public override string ToString()
        {
            String text = this.nom + " (coeff : " + this.coefficient + ")";
            return text;
        }
        #endregion

        #region Fonctionnalité 4 : calcul moyenne UE

        /// <summary>
        /// Fonction permettant de calculer la moyenne obtenue dans une UE
        /// </summary>
        /// <returns>La moyenne des notes obtenues aux différents modules</returns>
        public float CalculerMoyenneUE()
        {
            float sommePondérée = 0;
            float sommeCoefficient = 0;
            float moyenne = 0;
            foreach(Module m in this.ListerModulesUE())
            {
                sommePondérée += m.CalculerMoyenneModule() * m.Coefficient;
                sommeCoefficient += m.Coefficient;
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
