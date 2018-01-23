using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using metier;
using Sauvegarde;

/// <summary>
/// La partie IHM du code
/// </summary>
namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attributs, constructeur et sauvegarde
        private ListeUE liste = null;
        private IListeUEdata data = new ListeUEdataXml("data.xml"); //création du fichier XML contenant l'ensemble des données pour la persistance

        /// <summary>
        /// Constructeur de la mainwindow (Contient le code de chargement des données pour la fonctionnalité 5)
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.AfficheSemestres();

            // chargement des données ou création d'une nouvelle liste (fonctionnalité 5)
            try
            {
                this.liste = this.data.Charger();  
                if (this.liste == null)
                {
                    this.liste = new ListeUE();
                }
                // conditionnelle pour la fonctionnalité 6 (ajout)
                else
                {
                    this.AjouteUEserialiséesAuxSemestres();
                }
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine("Exception : " + ex);
            }
            
        }

        /// <summary>
        /// permet de sauvegarder (listener sur la fermeture de la mainwindow)
        /// </summary>
        /// <param name="sender">l'objet envoyant le messange (la mainwidow)</param>
        /// <param name="e">l'événement : la fermeture</param>
        void Sauvegarde(object sender, EventArgs e)
        {
            this.data.Sauver(this.liste, "data.xml");
        }
        #endregion

        #region Ajout des UE, Modules et Examens

        /// <summary>
        /// Méthode permettant d'ajouter une UE
        /// Listener sur le bouton "Ajouter une nouvelle UE" (click)
        /// </summary>
        /// <param name="sender">Le bouton "Ajouter une nouvelle UE"</param>
        /// <param name="e">Evenement click</param>
        private void AjouterUE(object sender, RoutedEventArgs e)
        {
            Semestre semestreSelected = this.lbSemestres.SelectedItem as Semestre;

            if (semestreSelected != null)
            {
                // on récupère le numéro du semestre sélectionné
                int numero = semestreSelected.NumeroSemestre;
                // on crée une UE pour la passer en paramètre puis récupérer ses informations grâce au binding
                UE ue = new UE("Saisir le nom de l'UE", 0.01F);
                EditionWindowUE fenetre = new EditionWindowUE(ue); // appel du constructeur de la fenêtre
                if (fenetre.ShowDialog() == true)
                {
                    // on ajoute l'UE au semestre correspondant
                    ListeSemestres.Instance.ListerSemestres().ElementAt(numero).AjouterUE(ue);
                    ue.Semestre = ListeSemestres.Instance.ListerSemestres().ElementAt(numero);
                    // on ajoute l'UE à la liste
                    this.liste.AjouterUE(ue);
                    
                    this.lbUE.Items.Clear();
                    foreach(UE ueToAdd in semestreSelected.ListerUE())
                    {
                        this.lbUE.Items.Add(ueToAdd);
                    }
                }
            }
        }

        /// <summary>
        /// permet d'ajouter un module à une UE ayant été sélectionnée
        /// listener sur le bouton d'ajout de module
        /// </summary>
        /// <param name="sender">Le bouton d'ajout de module</param>
        /// <param name="e">Evenement : click</param>
        private void AjouterModuleUE(object sender, RoutedEventArgs e)
        {
            // on récupère l'instance de l'UE sélectionnée
            UE ueSelected = lbUE.SelectedItem as UE;
            // si l'UE n'est pas nulle
            if (ueSelected != null)
            {
                // On crée un module pour le binding
                Module m = new Module("NomModule", 0.01F);
                // On crée une fenêtre d'édition à partir du module
                EditionWindowModule fen = new EditionWindowModule(m);
                // si la fenêtre est fermée
                if (fen.ShowDialog() == true)
                {
                    // On ajoute le module à l'UE qui était sélectionnée et qui a été modifié via le binding
                    ueSelected.InsererNouveauModule(m);
                    // On l'ajoute à la listBox des Modules
                    this.lbModules.Items.Add(m);
                }
            }
        }

        /// <summary>
        /// permet de créer un examen dans un module
        /// </summary>
        /// <param name="sender">Bouton ajout d'examen</param>
        /// <param name="e">Click</param>
        private void CreationExamenModule(object sender, RoutedEventArgs e)
        {
            // booléen permettant de récupérer la présence de l'étudiant (absent ou non)
            bool abs = false;
            // on récupère l'UE sélectionné
            UE ueSelected = lbUE.SelectedItem as UE;
            // on récupére le module sélectionné
            Module moduleSelected = lbModules.SelectedItem as Module;
            // si l'UE n'est pas nulle
            if (ueSelected != null)
            {
                // si le module n'est pas nul
                if (moduleSelected != null)
                {
                    // On crée une note pour le binding
                    Note n = new Note(0);
                    // on crée une nouvelle fenêtre à partir de la note
                    EditionWindowExamen fen = new EditionWindowExamen(n);
                    // si la fenêtre est fermée
                    if (fen.ShowDialog() == true)
                    {
                        // si le RadioButton rbAbsent était coché
                        if (fen.rbAbsent.IsChecked == true)
                        {
                            // l'étudiant est noté absent
                            abs = true;
                        }
                        // on crée un examen à partir de la valeur de la note récupérée via le binding
                        Examen ex = new Examen(n.Valeur);
                        // on attribue la valeur abs à l'attribut Absent de la note
                        ex.Note.Absent = abs;
                        // on ajoute l'examen à la liste des examens du module
                        moduleSelected.AjouterExamenModule(ex);
                        // on l'affiche
                        this.lbExamens.Items.Add(ex);
                    }
                }
            }
        }

        #endregion

        #region Affichage des UE, Modules et Examens


        /// <summary>
        /// Méthode permettant d'afficher les modules de l'UE sélectionnée.
        /// Listener sur la sélection des UE au sein de la listBox (click)
        /// </summary>
        /// <param name="sender">L'objet lbUE</param>
        /// <param name="e">Un événement click (changement de sélection)</param>
        private void AfficherModSelectedUE(object sender, SelectionChangedEventArgs e)
        {
            // on récupère l'instance de l'object sélectionné
            UE ueSelected = this.lbUE.SelectedItem as UE;
            // si l'objet n'est pas nul
            if (ueSelected != null)
            {
                // on vide la listbox des modules
                this.lbModules.Items.Clear();
                // on vide la listbox des examens
                this.lbExamens.Items.Clear();
                // pour chaque module de la liste des modules de l'UE
                foreach (Module m in ueSelected.ListerModulesUE())
                {
                    // on les ajoute à la listBox des modules
                    this.lbModules.Items.Add(m);
                }
            }
        }

        /// <summary>
        /// Méthode permettant d'afficher les examens de l'UE sélectionnée
        /// Listener sur la sélection des examens au sein de la listBox (click)
        /// </summary>
        /// <param name="sender">L'objet lbModules</param>
        /// <param name="e">Un événement click (changement de sélection)</param>
        private void ListerNotesModule(object sender, SelectionChangedEventArgs e)
        {
            // on récupère l'instance de l'UE sélectionnée
            UE ueSelected = this.lbUE.SelectedItem as UE;
            // on récupère l'instance du module sélectionné
            Module moduleSelected = this.lbModules.SelectedItem as Module;
            // si l'UE n'est pas nulle
            if (ueSelected != null)
            {
                // Si le module n'est pas nul
                if (moduleSelected != null)
                {
                    // on rafraîchit l'affichage de la listBox des examens
                    this.lbExamens.Items.Clear();
                    foreach (Examen ex in moduleSelected.ListerExamens())
                    {
                        this.lbExamens.Items.Add(ex);
                    }
                }
            }
        }
        #endregion

        #region Edition des UE, Modules et Examens

        /// <summary>
        /// Méthode permettant de modifier une UE (nom ou coeff)
        /// Listener sur le double click sur un élément de la listBox des UE
        /// </summary>
        /// <param name="sender">L'UE sélectionnée</param>
        /// <param name="e">Double click</param>
        private void EditerPtésUE(object sender, MouseButtonEventArgs e)
        {
            Semestre semestreSelected = this.lbSemestres.SelectedItem as Semestre;
            // on récupère l'ue sélectionnée
            UE ueSelected = this.lbUE.SelectedItem as UE;
            if (semestreSelected != null)
            {
                if (ueSelected != null)
                {
                    // nouvelle fenêtre d'édition
                    EditionWindowUE fenetre = new EditionWindowUE(ueSelected);
                    // remplissage des champs
                    fenetre.tbNom.Text = ueSelected.Nom;
                    fenetre.tbCoeff.Text = ueSelected.Coefficient.ToString().Replace(",", ".");
                    if (fenetre.ShowDialog() == true)
                    {
                        // on actualise l'affichage
                        this.lbUE.Items.Clear();
                        foreach(UE ue in semestreSelected.ListerUE())
                        {
                            this.lbUE.Items.Add(ue);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Permet d'éditer les propriétés d'un module d'une UE
        /// Listener sur le double click sur un module
        /// </summary>
        /// <param name="sender">Le module sélectionné</param>
        /// <param name="e">Evenement : double click</param>
        private void EditerPtésModule(object sender, MouseButtonEventArgs e)
        {
            // on récupère l'UE sélectionnée
            UE ueSelected = lbUE.SelectedItem as UE;
            // on récupère le module selectionné
            Module moduleSelected = lbModules.SelectedItem as Module;
            // si l'ue est bien sélectionnée lors du double click sur le module
            if (ueSelected != null)
            {
                // check
                if (moduleSelected != null)
                {
                    // on créée une nouvelle fenêtre d'édition avec pour paramètre le module sélectionné pour la gestion des infos grâce au binding
                    EditionWindowModule fenetre = new EditionWindowModule(moduleSelected);
                    // on préremplit le champ Nom de la fenêtre d'édition avec le nom du module existant
                    fenetre.tbNom.Text = moduleSelected.Nom;
                    // on préremplit le champ  coefficient de la fenêtre d'édition avec le coefficient du module existant
                    fenetre.tbCoeff.Text = moduleSelected.Coefficient.ToString().Replace(",", ".");
                    // actualisation de l'affichage lorsque la fênêtre d'édition est fermée.
                    if (fenetre.ShowDialog() == true)
                    {
                        // on vide la listbox des modules
                        this.lbModules.Items.Clear();
                        // pour chaque module de la liste des modules de l'UE sélectionnée
                        foreach (Module m in ueSelected.ListerModulesUE())
                        {
                            // on les ajoute à la listBox des modules
                            this.lbModules.Items.Add(m);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Méthode permettant d'éditer les propriétés d'un examen d'un module d'une UE
        /// </summary>
        /// <param name="sender">lbExamens</param>
        /// <param name="e">Un double click (event)</param>
        private void EditerNoteExamen(object sender, MouseButtonEventArgs e)
        {
            // on récupère l'UE sélectionnée
            UE ueSelected = lbUE.SelectedItem as UE;
            // on récupère le module sélectionné
            Module moduleSelected = lbModules.SelectedItem as Module;
            // on récupère l'examen sélectionnée
            Examen examenSelected = lbExamens.SelectedItem as Examen;
            // UE non nulle ?
            if (ueSelected != null)
            {
                // module non nul ?
                if (moduleSelected != null)
                {
                    // examen non nul ?
                    if (examenSelected != null)
                    {
                        // on crée une fenêtre à partir de la Note de l'examen
                        EditionWindowExamen fen = new EditionWindowExamen(examenSelected.Note);
                        // on pré-remplit le champ tbNote avec la note obtenue à l'examen
                        fen.tbNote.Text = examenSelected.Note.Valeur.ToString().Replace(",", ".");
                        // si l'étudiant était absent à l'examen
                        if (examenSelected.Note.Absent == true)
                        {
                            // on coche le radiobutton absent
                            fen.rbAbsent.IsChecked = true;
                        }
                        // si l'étudiant n'était pas absent à l'examen
                        if (examenSelected.Note.Absent == false)
                        {
                            // on coche le radiobutton présent
                            fen.rbPresent.IsChecked = true;
                        }
                        // si la fenêtre est fermée
                        if (fen.ShowDialog() == true)
                        {
                            // si le radiobutton absent était checké
                            if (fen.rbAbsent.IsChecked == true)
                            {
                                // on change la valeur de l'attribut absent, ce qui passe la note à 0
                                examenSelected.Note.Absent = true;
                            }
                            // si le radiobutton présent était checké
                            if (fen.rbPresent.IsChecked == true)
                            {
                                // on passe la valeur de l'attribut absent à faux
                                examenSelected.Note.Absent = false;
                            }
                            // on gère l'affichage : réactualisation
                            this.lbExamens.Items.Clear();
                            foreach (Examen ex in moduleSelected.ListerExamens())
                            {
                                this.lbExamens.Items.Add(ex);
                            }          
                        }
                    }
                }
            }
        }
        #endregion

        #region Fonctionnalité 4 : calcul de moyenne
        /// <summary>
        /// Permet de réagir à l'appui sur le bouton de calcul des moyennes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculerMoyenne(object sender, RoutedEventArgs e)
        {
            MoyenneWindow fen = new MoyenneWindow(this.liste);
            if (fen.ShowDialog() == true)
            {

            }
        }
        #endregion

        #region Fonctionnalité 6 : gestion des semestres
        /* d'autres changements mineurs ont été effectués dans MainWindow.AjouterUE() 
         * et MainWindow.MainWindow() pour permettre cette fonctionnalité 
         */

        /// <summary>
        /// Méthode basique permettant d'afficher les semestres
        /// </summary>
        private void AfficheSemestres()
        {
            ListeSemestres liste = ListeSemestres.Instance;
            foreach (Semestre s in liste.ListerSemestres()) 
            {
                lbSemestres.Items.Add(s);
            }
        }

        /// <summary>
        /// Méthode permettant d'afficher les UE d'un semestre ayant été selectionné
        /// </summary>
        /// <param name="sender">La listBox des Semestres</param>
        /// <param name="e">Un changement d'état sur un objet de la listbox (nouvel objet sélectionné)</param>
        private void AfficheUEsemestre(object sender, SelectionChangedEventArgs e)
        {
            // on supprime "l'ancien affichage"
            this.lbExamens.Items.Clear();
            this.lbModules.Items.Clear();
            this.lbUE.Items.Clear();
            // et on actualise
            Semestre semestreSelected = this.lbSemestres.SelectedItem as Semestre;
            if (semestreSelected != null)
            {
                int numeroSemestre = semestreSelected.NumeroSemestre;
                foreach (UE ue in ListeSemestres.Instance.ListerSemestres().ElementAt(numeroSemestre).ListerUE())
                {
                    this.lbUE.Items.Add(ue);
                }
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les UE ayant été sérialisées dans le semestre correspondant.
        /// </summary>
        private void AjouteUEserialiséesAuxSemestres()
        {
            foreach(UE ue in this.liste.ListerUE())
            {
                ListeSemestres.Instance.ListerSemestres().ElementAt(ue.Semestre.NumeroSemestre).AjouterUE(ue);
            }
        }
        #endregion
    }
}
