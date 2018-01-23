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
    /// Logique d'interaction pour EditionWindowUE.xaml
    /// </summary>
    public partial class EditionWindowUE : Window
    {
        /// <summary>
        /// Création d'une EditionWindowUE à partir d'une UE
        /// </summary>
        /// <param name="ue"></param>
        public EditionWindowUE(UE ue)
        {
            //on récupère l'objet rélié via le Binding
            DataContext = ue;
            InitializeComponent();

            // permet de vider les champs
            this.tbCoeff.Text = "."; // on laisse seulement un séparateur décimal
            this.tbNom.Text = "";
        }

        /// <summary>
        /// Gestion du bouton valider
        /// </summary>
        /// <param name="sender">Le button Valider</param>
        /// <param name="e">Event : Un click sur le bouton</param>
        private void Valider(object sender, RoutedEventArgs e)
        {
            // si le nom de l'UE est vide
            if (this.tbNom.Text == "")
            {
                // on avertit l'utilisateur
                MessageBox.Show("Le nom de l'UE ne peut pas être vide");
            }

            // si le coeff est vide
            //on ne gère pas le cas des nombres négatifs car la méthode NoText() empêche la saisie de caractères autres que des chiffres
            if ((this.tbCoeff.Text == ".") || (this.tbCoeff.Text == ""))
            {
                MessageBox.Show("L'UE doit posséder un coefficient");
            }

            if ( (this.tbNom.Text != "") && (this.tbCoeff.Text != ".") )
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
