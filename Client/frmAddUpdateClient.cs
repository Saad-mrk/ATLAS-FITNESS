using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using ATLASS_FITNESS.Person;
using ATLASS_FITNESS.Subscrbtion;
using ATLASS_FITNESS_BUISNESS;
using Guna.UI2.WinForms;

namespace ATLASS_FITNESS.Client
{
    public partial class frmAddUpdateClient : Form
    {
        // ========== PROPRIÉTÉS ==========
        private ClsClient _client;
        private int _ClientID = -1;
        private int _PersonID = -1;
        private int _UserID = 1; // TODO: Remplacer par Clsglobal.CurrentUser.UserID

        private FrmAddUpdatePerson frmPerson;
        private FrmAddUpdateSubscrition frmSubscription;

        // Mode du formulaire
        private enum Mode { AddNew, Update }
        private Mode _Mode;

        // Événement pour retourner le ClientID
        public delegate void DataBackHandler(object sender, int clientID);
        public event DataBackHandler DataBack;

        // ========== CONSTRUCTEURS ==========

        // Mode ADD NEW
        public frmAddUpdateClient()
        {
            InitializeComponent();
            _Mode = Mode.AddNew;
            _client = new ClsClient();

            // TODO: Récupérer l'utilisateur connecté
            // _UserID = Clsglobal.CurrentUser?.UserID ?? -1;

            if (_UserID == -1)
            {
                MessageBox.Show("Aucun utilisateur connecté !", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // Mode UPDATE
        public frmAddUpdateClient(int clientID)
        {
            InitializeComponent();
            _ClientID = clientID;
            _Mode = Mode.Update;

            // TODO: Récupérer l'utilisateur connecté
            // _UserID = Clsglobal.CurrentUser?.UserID ?? -1;
        }

        // ========== CHARGEMENT DU FORMULAIRE ==========

        private void frmAddUpdateClient_Load(object sender, EventArgs e)
        {
            if (_Mode == Mode.Update)
            {
                LoadClientData();
            }
            else
            {
                SetupAddNewMode();
            }

            LoadPersonForm();
        }

        // Charger les données du client existant
        private void LoadClientData()
        {
            _client = ClsClient.GetClientbyid(_ClientID);

            if (_client == null)
            {
                MessageBox.Show($"Client avec ID {_ClientID} introuvable.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _PersonID = _client.PERSONID;

            // Créer les formulaires enfants en mode UPDATE
            frmPerson = new FrmAddUpdatePerson(_PersonID);

            // Configurer l'interface pour le mode UPDATE
            SetupUpdateMode();

            // ✅ CHARGER LE FORMULAIRE D'ABONNEMENT EN MODE UPDATE
            LoadSubscriptionForm();
        }

        // Configurer l'interface pour le mode ADD NEW
        private void SetupAddNewMode()
        {
            // Créer le formulaire personne en mode ADD
            frmPerson = new FrmAddUpdatePerson();

            // Masquer les boutons de navigation initialement
            btnprevious.Visible = false;
            btnprevious.Enabled = false;
            btnsave.Visible = false;
            btnsave.Enabled = false;

            // Désactiver l'onglet abonnement
            tabPage2.Enabled = false;

            // Positionner le bouton Next
            btnPersonInfoNext.Visible = true;
            btnPersonInfoNext.Enabled = true;
        }

        // Configurer l'interface pour le mode UPDATE
        private void SetupUpdateMode()
        {
            // Activer tous les onglets
            tabPage2.Enabled = true;

            // Configurer les boutons
            btnPersonInfoNext.Visible = false;
            btnprevious.Visible = true;
            btnprevious.Enabled = true;
            btnprevious.Text = "Previous";
            btnsave.Visible = true;
            btnsave.Enabled = true;
        }

        // ========== CHARGEMENT DES FORMULAIRES ENFANTS ==========

        // Charger le formulaire Person dans Tab1
        private void LoadPersonForm()
        {
            if (frmPerson == null)
                return;

            frmPerson.TopLevel = false;
            frmPerson.FormBorderStyle = FormBorderStyle.None;
            frmPerson.Dock = DockStyle.Fill;

            tabPage1.Controls.Clear();
            tabPage1.Controls.Add(frmPerson);

            // S'abonner à l'événement DataBack
            frmPerson.DataBack += FrmPerson_DataBack;

            frmPerson.Show();

            // ✅ MASQUER LES BOUTONS SAVE ET CLOSE EN MODE ADD NEW
            if (_Mode == Mode.AddNew)
            {
                frmPerson.btnSave.Visible = false;
                frmPerson.btnClose.Visible = false;
            }
        }

        // ✅ CHARGER LE FORMULAIRE SUBSCRIPTION CORRECTEMENT
        private void LoadSubscriptionForm()
        {
            try
            {
                // ✅ VÉRIFIER QUE LE ClientID EST VALIDE
                if (_ClientID <= 0)
                {
                    MessageBox.Show($"❌ ClientID invalide : {_ClientID}", "Erreur");
                    return;
                }

                MessageBox.Show($"🔍 Chargement Subscription pour ClientID: {_ClientID}", "Debug");

                // ✅ CRÉER LE FORMULAIRE EN LUI PASSANT LE ClientID
                frmSubscription = new FrmAddUpdateSubscrition(_ClientID);

                // ✅ ASSIGNER LE ClientID AVANT DE CONFIGURER LE FORMULAIRE
                frmSubscription.ClientID = _ClientID;

                // Configuration du formulaire
                frmSubscription.TopLevel = false;
                frmSubscription.FormBorderStyle = FormBorderStyle.None;
                frmSubscription.Dock = DockStyle.Fill;

                tabPage2.Controls.Clear();
                tabPage2.Controls.Add(frmSubscription);

                // S'abonner à l'événement DataBack AVANT Show()
                frmSubscription.DataBack += FrmSubscription_DataBack;

                // ✅ MASQUER LE BOUTON SAVE AVANT Show()
                if (frmSubscription.btnsave != null)
                {
                    frmSubscription.btnsave.Visible = false;
                    frmSubscription.btnsave.Enabled = false;
                }

                // ✅ MAINTENANT ON PEUT APPELER Show()
                frmSubscription.Show();

                MessageBox.Show("✅ Formulaire Subscription chargé avec succès", "Succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur LoadSubscriptionForm:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== GESTION DES ÉVÉNEMENTS ==========

        // Recevoir le PersonID du formulaire Person
        private void FrmPerson_DataBack(object sender, int personID)
        {
            _PersonID = personID;
            MessageBox.Show($"✅ PersonID reçu : {_PersonID}", "Information");
        }

        // Recevoir le SubscriptionID du formulaire Subscription
        private void FrmSubscription_DataBack(object sender, int subscriptionID)
        {
            MessageBox.Show($"✅ Abonnement créé avec succès (ID: {subscriptionID})",
                "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Déclencher l'événement DataBack pour notifier le formulaire parent
            DataBack?.Invoke(this, _ClientID);

            this.Close();
        }

        // ========== BOUTONS DE NAVIGATION ==========

        // Bouton "Next" : Passer de l'onglet Person à Subscription
        private void btnPersonInfoNext_Click_1(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Sauvegarder la personne
                MessageBox.Show("📝 Sauvegarde des informations personnelles...", "Information");
                frmPerson.btnSave_Click(frmPerson.btnSave, EventArgs.Empty);

                // Attendre que l'événement DataBack soit déclenché
                if (_PersonID <= 0)
                {
                    MessageBox.Show("⚠️ PersonID non reçu. Vérifiez que la personne a été sauvegardée.",
                        "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"✅ PersonID validé : {_PersonID}", "Information");

                // 2️⃣ Créer le client si mode ADD NEW
                if (_Mode == Mode.AddNew)
                {
                    MessageBox.Show("🔄 Création du client...", "Information");
                    if (!CreateClient())
                    {
                        MessageBox.Show("❌ Échec de la création du client.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                   
                    if (_client.GenerateQRCodeAndSendEmail())
                    {
                        MessageBox.Show("Email envoyé!", "Succès");
                    }
                    else
                    {
                        MessageBox.Show("Email non envoyé", "Erreur");
                    }
                }

                // 3️⃣ Configurer l'interface
                btnPersonInfoNext.Visible = false;
                btnPersonInfoNext.Enabled = false;
                btnprevious.Visible = true;
                btnprevious.Enabled = true;
                btnprevious.Text = "Previous";
                btnsave.Visible = true;
                btnsave.Enabled = true;
                tabPage2.Enabled = true;

                // 4️⃣ Charger le formulaire Subscription
                MessageBox.Show("📋 Chargement du formulaire d'abonnement...", "Information");
                LoadSubscriptionForm();

                // 5️⃣ Passer à l'onglet Subscription
                guna2TabControl1.SelectedTab = tabPage2;

                MessageBox.Show("✅ Navigation réussie vers l'onglet Abonnement", "Information");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur btnPersonInfoNext_Click:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Créer le client en base de données
        private bool CreateClient()
        {
            try
            {
                _client = new ClsClient
                {
                    PERSONID = _PersonID,
                    registration = DateTime.Now,
                    createdby = _UserID,
                    last_seance = DateTime.Now,
                    Is_Active = true
                };

                MessageBox.Show(
                    $"📊 Données avant Save() :\n" +
                    $"PersonID = {_client.PERSONID}\n" +
                    $"CreatedBy = {_client.createdby}\n" +
                    $"Registration = {_client.registration}\n" +
                    $"IsActive = {_client.Is_Active}",
                    "Informations Client"
                );

                // Sauvegarder le client
                if (!_client.Save())
                {
                    MessageBox.Show("❌ Échec de la sauvegarde du client en base de données.",
                        "Erreur");

                    return false;
                }


                _ClientID = _client.ClientID;
                _Mode = Mode.Update;

                MessageBox.Show($"✅ Client sauvegardé avec ClientID: {_ClientID}",
                    "Succès");
                MessageBox.Show($"✅ Client créé avec succès ! ClientID: {_ClientID} {_client.ClientPerson.Email}",
                           "Succès");


                // 🔥 GÉNÉRER LE QR CODE
                MessageBox.Show("🔄 Génération du QR Code...", "Information");

                if (_client.GenerateAndSaveQRCode())
                {
                    MessageBox.Show($"✅ QR Code généré avec succès !\nPath: {_client.QRCode}",
                        "Succès QR Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                }
                else
                {
                    MessageBox.Show("⚠️ Attention : Le QR Code n'a pas pu être généré.",
                        "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Déclencher l'événement DataBack
                DataBack?.Invoke(this, _ClientID);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur CreateClient:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Bouton "Previous/Next" : Navigation entre onglets
        private void btnprevious_Click_1(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedTab == tabPage2)
            {
                // Retour à l'onglet Person
                guna2TabControl1.SelectedTab = tabPage1;
                btnprevious.Text = "Next";
            }
            else
            {
                // Aller à l'onglet Subscription
                if (_ClientID <= 0)
                {
                    MessageBox.Show("⚠️ Veuillez d'abord créer le client.", "Attention",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                guna2TabControl1.SelectedTab = tabPage2;
                btnprevious.Text = "Previous";
            }
        }

        // Bouton "Save" : Sauvegarder l'abonnement
        private void btnsave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (frmSubscription == null)
                {
                    MessageBox.Show("❌ Le formulaire d'abonnement n'est pas chargé.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Déclencher la sauvegarde de l'abonnement
                MessageBox.Show("📝 Sauvegarde de l'abonnement...", "Information");
                frmSubscription.btnsave_Click(frmSubscription.btnsave, EventArgs.Empty);

                // L'événement FrmSubscription_DataBack sera déclenché après la sauvegarde
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur btnsave_Click:\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== NETTOYAGE ==========

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Désabonner des événements
            if (frmPerson != null)
                frmPerson.DataBack -= FrmPerson_DataBack;

            if (frmSubscription != null)
                frmSubscription.DataBack -= FrmSubscription_DataBack;

            base.OnFormClosing(e);
        }
    }
}