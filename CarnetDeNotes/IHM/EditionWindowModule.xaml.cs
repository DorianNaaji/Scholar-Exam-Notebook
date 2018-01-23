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
    /// Logique d'interaction pour EditionWindowModule.xaml
    /// </summary>
    public partial class EditionWindowModule : Window
    {
        public EditionWindowModule(Module m)
        {
            DataContext = m;
            InitializeComponent();
            // on vide les champs
            this.tbNom.Text = "";
            this.tbCoeff.Text = "."; // on laisse un séparateur décimal.
        }

        /// <summary>
        /// Gestion du bouton valider
        /// </summary>
        /// <param name="sender">Le button Valider</param>
        /// <param name="e">Event : Un click sur le bouton</param>
        private void Valider(object sender, RoutedEventArgs e)
        {
            // si le nom du module est vide
            if (this.tbNom.Text == "")
            {
                // on avertit l'utilisateur
                MessageBox.Show("Le nom du module ne peut pas être vide");
            }

            // si le coeff est vide
            //on ne gère pas le cas des nombres négatifs car la méthode NoText() empêche la saisie de caractères autres que des chiffres
            if ( (this.tbCoeff.Text == ".") || (this.tbCoeff.Text == "") )
            {
                MessageBox.Show("Le module doit posséder un coefficient");
            }

            if ((this.tbNom.Text != "") && (this.tbCoeff.Text != "."))
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
    }
}
