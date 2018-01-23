using System;
using System.Runtime.Serialization;

/// <summary>
/// La partie métier du code
/// </summary>
namespace metier
{
    /// <summary>
    /// Classe Examen permettant de gérer les examens.
    /// Un examen est donné par un enseignant (chaîne de caractères, non vide), à une certaine date (chaîne
    /// de caractères), avec une certaine pondération(flottant >0)
    /// </summary>
    [DataContract]
    public class Examen
    {
        #region Attributs et propriétés
        [DataMember]
        private String nomProf;
        [DataMember]
        private String dateExam;
        [DataMember]
        private float pondération;
        [DataMember]
        private Note note;

        /// <summary>
        /// Property r/w
        /// </summary>
        public Note Note
        {
            get
            {
                return this.note;
            }
            set
            {
                this.note = value;
            }
        }
        
        /// <summary>
        /// Property r/w
        /// <exception cref="ArgumentException">Nom du professeur doit être non vide</exception>
        /// </summary>
        public String NomProf
        {
            get
            {
                return this.nomProf;
            }

            set
            {
                if (value != "")
                {
                    this.nomProf = value;
                }
                else
                {
                    throw new ArgumentException("Le nom du professeur doit être saisi");
                }
            }
        }

        /// <summary>
        /// Property r/w
        /// <exception cref="ArgumentException">Date doit être non nul</exception>
        /// </summary>
        public String DateExam
        {
            get
            {
                return this.dateExam;
            }

            set
            {
                if (value != "")
                {
                    this.dateExam = value;
                }
                else
                {
                    throw new ArgumentException("L'examen doit posséder une date");
                }
            }
        }
        
        /// <summary>
        /// Property r/w
        /// <exception cref="ArgumentException">Pondération doit être > 0</exception>
        /// </summary>
        public float Pondération
        {
            get
            {
                return this.pondération;
            }
            set
            {
                if (value > 0)
                {
                    this.pondération = value;
                }
                else
                {
                    throw new ArgumentException("La pondération de l'examen doit être positive");
                }
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// constructeur avec la valeur d'une note
        /// </summary>
        /// <param name="valeur"> La valeur de la note</param>
        public Examen(float valeur)
        {
            Note n = new Note(valeur);
            this.note = n;
            this.note.Absent = false;
            // par défaut la pondération est à 1
            this.pondération = 1;
        }

        /// <summary>
        /// Constructeur d'un Examen avec un booléen
        /// </summary>
        /// <param name="abs">True : étudiant absent. False : étudiant présent</param>
        public Examen(bool abs)
        {
            Note n = new Note(abs);
            this.note = n;
            // par défaut la pondération est à 1
            this.pondération = 1;
        }
        #endregion

        public override string ToString()
        {
            String text = "Note : " + this.note.Valeur + " (coeff : " + this.pondération + ")";
            return text;
        }
    }
}
