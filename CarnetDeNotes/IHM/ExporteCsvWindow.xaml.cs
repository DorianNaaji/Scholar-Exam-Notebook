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
using Microsoft.Win32;

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour ExporteCsvWindow.xaml
    /// 
    /// </summary>
    public partial class ExporteCsvWindow : Window
    {

        private List<Module> mod = new List<Module>();

        /// <summary>
        /// Constructeur d'une fenêtre avec liste de modules
        /// Se crée par l'utilisateur depuis le menu de la mainwindow
        /// </summary>
        /// <param name="modules"></param>
        public ExporteCsvWindow(List<Module> modules)
        {
            InitializeComponent();
            this.mod = modules;
        }

        /// <summary>
        /// Permet de sauvegarder
        /// </summary>
        /// <param name="sender">Le bouton Exporter</param>
        /// <param name="e">Event : Click</param>
        private void Sauvegarder(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveCSV = new SaveFileDialog();
            if (saveCSV.ShowDialog() == true)
            {
                // todo
            }

        }
    }
}
