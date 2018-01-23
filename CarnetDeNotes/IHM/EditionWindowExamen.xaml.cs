using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using metier;

/// <summary>
/// La partie IHM du code
/// </summary>
namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour EditionWindowExamen.xaml
    /// </summary>
    public partial class EditionWindowExamen : Window
    {
        /// <summary>
        /// Booléen permettant de récupérer la
        /// </summary>
        private bool absent = false;

        /// <summary>
        /// Constructeur de la window sans paramètres
        /// </summary>
        public EditionWindowExamen()
        {
            InitializeComponent();
            //vide la textbox
            this.tbNote.Text = "";
            this.tbNote.Text = "."; // on laisse un séparateur décimal
        }

        /// <summary>
        /// Constucteur de la window à partir d'une note
        /// </summary>
        /// <param name="n"></param>
        public EditionWindowExamen(Note n)
        {
            DataContext = n;
            InitializeComponent();

            //vide la textbox
            this.tbNote.Text = "";
            this.tbNote.Text = "."; // on laisse un séparateur décimal

            // si l'étudiant était absent à l'examen
            if (n.Absent)
            {
                // on check le radio button
                this.rbAbsent.IsChecked = true;
                this.absent = true;
            }
        }


        /// <summary>
        /// Gestion du bouton valider
        /// </summary>
        /// <param name="sender">Le button Valider</param>
        /// <param name="e">Event : Un click sur le bouton</param>
        private void Valider(object sender, RoutedEventArgs e)
        {
            // gestion cas 1
            if ((this.tbNote.Text == ".") && (this.absent == false))
            {
                MessageBox.Show("Si l'étudiant n'était pas absent, il doit posséder une note");
            }

            // cas 2
            if ( (this.tbNote.Text != "0")  && (this.absent == true) )
            {
                MessageBox.Show("L'étudiant ne peut pas être absent et obtenir une note autre que 0 à l'examen ");
            }

            // cas 3
            if ( (this.tbNote.Text != ".") && (this.absent == false ) )
            {
                string s = this.tbNote.Text.Replace(".", "");
                // ok
                if ((Convert.ToDouble(s) <= 20) && (Convert.ToDouble(s) >= 0))
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("La note doit être comprise entre 0 et 20.");
                }
            }

            // ok
            if ( (this.tbNote.Text == "0") && (this.absent == true ))
            {
                DialogResult = true;
            }
        }

        /// <summary>
        /// Gestion du bouton annuler
        /// </summary>
        /// <param name="sender">Le button annuler</param>
        /// <param name="e">Event : click sur le bouton</param>
        private void Annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Gestion du radioButton Present
        /// </summary>
        /// <param name="sender">le radioButton</param>
        /// <param name="e">Changement d'état du button (coché)</param>
        private void Present(object sender, RoutedEventArgs e)
        {
            this.absent = false;
        }

        /// <summary>
        /// Gestion du radioButton Absent
        /// </summary>
        /// <param name="sender">Le radioButtonr</param>
        /// <param name="e">Changement d'état du button (coché</param>
        private void Absent(object sender, RoutedEventArgs e)
        {
            this.tbNote.Text = "0";
            this.absent = true;
        }

        /// <summary>
        /// Méthode permettant d'empêcher la saisie de texte dans le coefficient
        /// </summary>
        /// <param name="sender">La textBox tbCoeff</param>
        /// <param name="e">Event : entrée au clavier dans la tbCoeff</param>
        private void NoText(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            // regex acceptant les chiffres de 1 à 9
            // devrait également accepter l'entrée d'un séparateur décimal (".")
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]");
        }

        /// <summary>
        /// property r/w
        /// </summary>
        public bool GetAbsent
        {
            get
            {
                return this.absent;
            }
            set
            {
                this.absent = value;
            }
        }
    }
}
