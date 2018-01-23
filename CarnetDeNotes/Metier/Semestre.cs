using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using metier;
using System.Runtime.Serialization;

/// <summary>
/// La partie métier du code
/// </summary>
namespace metier
{
    /// <summary>
    /// Classe permettant de gérer les semestres contenus dans une liste de
    /// semestres
    /// (fonctionnalité 6)
    /// </summary>
    [DataContract]
    public class Semestre
    {
        private List<UE> lesUE = new List<UE>();
        private String nom;
        [DataMember]
        private int numéro;

        /// <summary>
        /// Constructeur d'un semestre. Permet de créer un nouveau semestre à partir d'un numéro de semestre.
        /// En raison de l'indicage des collections (à partir de 0), le numéro de semestre prend ensuite
        /// la valeur num+1
        /// </summary>
        /// <param name="num"></param>
        public Semestre(int num)
        {
            int n = num + 1;
            this.nom = "Semestre " + n;
            this.numéro = num;
        }

        /// <summary>
        /// Méthode permettant d'ajouter une UE à la liste d'UE du semestre.
        /// </summary>
        /// <param name="ue"></param>
        public void AjouterUE(UE ue)
        {
            this.lesUE.Add(ue);
        }

        /// <summary>
        /// Méthode permettant de lister les UE contenues dans un semestre
        /// </summary>
        /// <returns></returns>
        public List<UE> ListerUE()
        {
            List<UE> listeRet = this.lesUE;
            return listeRet;
        }

        /// <summary>
        /// Getter permettant de récupérer le numéro d'un semestre
        /// <return>Le numéro d'un semestre</return>
        /// </summary>
        public int NumeroSemestre
        {
            get
            {
                return this.numéro;
            }
        }

        /// <summary>
        /// Méthode permettant de renvoyer le nom d'un semestre.
        /// </summary>
        /// <returns>L'objet semestre sous forme d'une chaîne de caratères</returns>
        public override string ToString()
        {
            return this.nom;
        }
    }
}
