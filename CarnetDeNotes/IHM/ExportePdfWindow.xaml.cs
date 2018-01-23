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
    /// Logique d'interaction pour ExportePdfWindow.xaml
    /// </summary>
    public partial class ExportePdfWindow : Window
    {
        private ListeUE liste = null;
        /// <summary>
        /// Constructeur d'une ExportePdfWindow à partir d'une listeUE
        /// Se crée par l'utilisateur depuis le menu de la mainwindow
        /// </summary>
        /// <param name="liste"></param>
        public ExportePdfWindow(ListeUE liste)
        {
            this.liste = liste;
            InitializeComponent();
        }

        /// <summary>
        /// Listener sur le bouton Exporter
        /// </summary>
        /// <param name="sender">Le bouton Exporter</param>
        /// <param name="e">Event : Click</param>
        private void Sauvegarder(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savePDF = new SaveFileDialog();
            if (savePDF.ShowDialog() == true)
            {
                // todo

                // On pourra éventuellement utiliser une bibliothèque appropriée
                // comme :
                //PDFsharp - A.NET library for processing PDF

            }
        }
    }
}
