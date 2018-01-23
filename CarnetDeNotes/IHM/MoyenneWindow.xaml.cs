using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour WindowMoyenne.xaml
    /// </summary>
    public partial class MoyenneWindow : Window
    {

        private ListeUE liste;

        /// <summary>
        /// Constructeur d'une moyenneWindow à partir d'une liste d'UE
        /// </summary>
        /// <param name="liste"></param>
        public MoyenneWindow(ListeUE liste)
        {
            InitializeComponent();
            this.liste = liste;
            this.AfficherMoyenne();
        }

        /// <summary>
        /// Permet d'afficher les moyennes de l'étudiant
        /// </summary>
        private void AfficherMoyenne()
        {
            foreach(UE ue in this.liste.ListerUE())
            {   
                foreach (Module mod in ue.ListerModulesUE())
                {
                    txtAreaMoyenne.Text += mod.Nom;
                    txtAreaMoyenne.Text += " : " + mod.CalculerMoyenneModule() + "\n";
                }
                txtAreaMoyenne.Text += ue.Nom;
                txtAreaMoyenne.Text += " : " + ue.CalculerMoyenneUE() + "\n\n";
            }
            txtAreaMoyenne.Text += "\nMoyenne générale : " + this.liste.CalculerMoyenneGenerale();
        }
    }
}
