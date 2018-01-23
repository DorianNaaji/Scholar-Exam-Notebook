using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// La partie métier du code
/// </summary>
namespace metier
{
    /// <summary>
    /// Classe singleton permettant d'instancier 4 semestres. Si l'on souhaite
    /// en instancier plus, il suffit de changer la boucle du consructeur.
    /// (fonctionnalité 6)
    /// </summary>
    public sealed class ListeSemestres
    {
        private List<Semestre> semestres = new List<Semestre>();
        private static ListeSemestres instance = null;

        /// <summary>
        /// Constructeur privé permettant de créer 4 semestres indicés de 0 à 3 dans
        /// une Liste de semestres.
        /// </summary>
        private ListeSemestres()
        {
            for (int i = 0; i < 4; i++)
            {
                Semestre s = new Semestre(i);
                this.semestres.Add(s);
            }

        }

        /// <summary>
        /// Méthode permettant de lister les semestre de l'instance de la liste
        /// de semestres
        /// </summary>
        /// <returns></returns>
        public List<Semestre> ListerSemestres()
        {
            List<Semestre> toReturn = this.semestres;
            return toReturn;
        }

        /// <summary>
        /// Getter permettant de récupérer l'instance unique (singleton) de la
        /// liste des semestres
        /// </summary>
        public static ListeSemestres Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ListeSemestres();
                }
                return instance;
            }
        }
    }
}
