using System;
using System.Runtime.Serialization;

/// <summary>
/// La partie métier du code
/// </summary>
namespace metier
{
    /// <summary>
    /// Classe permettant de gérer les notes.
    /// Un étudiant est noté à chaque examen par une note qui peut être une valeur (flottante) entre 0 et 20, ou
    /// être absent à l’examen(ce qui compte pour une note de 0).
    /// </summary>
    [DataContract]
    public class Note
    {
        #region Attributs et propriétés

        [DataMember]
        private float valeur;
        [DataMember]
        private bool absent;

        /// <summary>
        /// Property r/w sur l'attribut valeur
        /// </summary>
        public float Valeur
        {
            get
            {
                return this.valeur;
            }
            set
            {
                if ((value >= 0) && (value <= 20))
                {
                    this.valeur = value;
                }    
                else
                {
                    throw new ArgumentException("La note doit être comprise entre 0 et 20");
                }
            }
        }

        /// <summary>
        /// Property r/w sur l'attribut absent
        /// </summary>
        public bool Absent
        {
            get
            {
                return this.absent;
            }
            set
            {
                if(value == true)
                {
                    this.valeur = 0;
                    this.absent = value;
                }
                else if(value == false)
                {
                    this.absent = false;
                }
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur sans Examen
        /// </summary>
        /// <param name="val">La valeur de la note</param>
        /// <param name="abs">Booléen précisant si l'étudiant est absent ou non.</param>
        /// <exception cref="ArgumentException">val doit être compris entre 0 et 20</exception>
        public Note(float val, bool abs)
        {
            if ((val >= 0) && (val <= 20))
            {
                this.valeur = val;
                this.absent = false;
            }
            else
            {
                throw new ArgumentException("La note doit être comprise entre 0 et 20");
            }
            if (abs == true)
            {
                this.valeur = 0;
                this.absent = true;
            }
        }

        /// <summary>
        /// Constructeur avec booléen
        /// </summary>
        /// <param name="abs">Présence de l'étudiant</param>
        public Note(bool abs)
        {
            if (abs == true)
            {
                this.valeur = 0;
            }
        }

        /// <summary>
        /// Constructeur de la note avec une valeur
        /// </summary>
        /// <param name="val">La valeur de la note</param>
        /// <exception cref="ArgumentException">val doit être compris entre 0 et 20</exception>
        public Note(float val)
        {
            if ((val >= 0) && (val <= 20))
            {
                this.valeur = val;
                this.absent = false;
            }
            else
            {
                throw new ArgumentException("La note doit être comprise entre 0 et 20");
            }
        }
        #endregion
    }
}
