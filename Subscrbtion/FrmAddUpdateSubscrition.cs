using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATLASS_FITNESS.Client;
using ATLASS_FITNESS_BUISNESS;

namespace ATLASS_FITNESS.Subscrbtion
{
    public partial class FrmAddUpdateSubscrition : Form
    {
        // ========== PROPRIÉTÉS ==========
        double price = 0;
        double pricecoach = 0;
        private ClsSubscription _Subscription;
        private int _SubscriptionID = -1;
        public int ClientID { get; set; } = -1; // ✅ Propriété publique
        private ClsPayment _payment = new ClsPayment();

        public delegate void DataBackHandler(object sender, int paymentid);
        public event DataBackHandler DataBack;

        public enum enumMode
        {
            AddNew = 0,
            Update = 1
        };
        public enumMode Mode = enumMode.AddNew;

        // ========== CONSTRUCTEURS ==========

        // Mode ADD NEW
        public FrmAddUpdateSubscrition()
        {
            InitializeComponent();
            Mode = enumMode.AddNew;
            _Subscription = new ClsSubscription(); // ✅ Initialiser pour éviter null
        }

        // Mode UPDATE (passer un SubscriptionID ou ClientID)
        public FrmAddUpdateSubscrition(int clientID)
        {
            InitializeComponent();
            this.ClientID = clientID;
            MessageBox.Show($"ClientID passé au constructeur: {this.ClientID}",
                "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ========== ÉVÉNEMENTS ==========

        private void FrmAddUpdateSubscrition_Load(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show($"🔍 Load - ClientID: {ClientID}", "Debug");

                // Initialiser les valeurs par défaut
                guna2TextBox1.Text = "0";
                guna2TextBox2.Text = (price + pricecoach).ToString();

                // ✅ VÉRIFIER SI ClientID EST VALIDE
                if (ClientID <= 0)
                {
                    MessageBox.Show("⚠️ ClientID invalide. Mode AddNew impossible.",
                        "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Initialiser un abonnement vide pour mode AddNew
                    _Subscription = new ClsSubscription();
                    Mode = enumMode.AddNew;
                    lbltitle.Text = "Add New Subscription";
                    return;
                }

                // ✅ CHERCHER SI UNE SUBSCRIPTION EXISTE DÉJÀ POUR CE CLIENT
                _Subscription = ClsSubscription.getSubscriptionByClientID(ClientID);

                if (_Subscription != null)
                {
                    // MODE UPDATE : Subscription existante trouvée
                    MessageBox.Show($"✅ Subscription trouvée pour ClientID {ClientID}",
                        "Debug");
                    Mode = enumMode.Update;
                    _SubscriptionID = _Subscription.id;
                    _loaddata();
                }
                else
                {
                    // MODE ADD NEW : Aucune subscription pour ce client
                    MessageBox.Show($"ℹ️ Aucune subscription trouvée pour ClientID {ClientID}. Mode AddNew.",
                        "Information");
                    _Subscription = new ClsSubscription();
                    Mode = enumMode.AddNew;
                    lbltitle.Text = "Add New Subscription";

                    // Définir les valeurs par défaut
                    guna2ComboBox1.SelectedIndex = 0; // Monthly par défaut
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur dans Load:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem == null)
                return;

            string selectedPlan = guna2ComboBox1.SelectedItem.ToString();

            switch (selectedPlan)
            {
                case "Monthly":
                    price = 200;
                    txtdur.Text = "1";
                    txtEnd.Text = DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy");
                    break;
                case "Quarterly":
                    price = 550;
                    txtdur.Text = "3";
                    txtEnd.Text = DateTime.Now.AddMonths(3).ToString("dd/MM/yyyy");
                    break;
                case "Semi_Annual":
                    price = 1000;
                    txtdur.Text = "6";
                    txtEnd.Text = DateTime.Now.AddMonths(6).ToString("dd/MM/yyyy");
                    break;
                case "Annual":
                    price = 1800;
                    txtdur.Text = "12";
                    txtEnd.Text = DateTime.Now.AddMonths(12).ToString("dd/MM/yyyy");
                    break;
            }

            txtPrice.Text = price.ToString();
            UpdateCoachPrice();
        }

        private void guna2CheckBox1_CheckStateChanged(object sender, EventArgs e)
        {
            UpdateCoachPrice();
        }

        // ✅ MÉTHODE POUR CALCULER LE PRIX DU COACH
        private void UpdateCoachPrice()
        {
            if (guna2CheckBox1.Checked && guna2ComboBox1.SelectedIndex >= 0)
            {
                pricecoach = (100 * (guna2ComboBox1.SelectedIndex + 1));
                guna2TextBox1.Text = pricecoach.ToString();
            }
            else
            {
                pricecoach = 0;
                guna2TextBox1.Text = "0";
            }

            guna2TextBox2.Text = (price + pricecoach).ToString();
        }

        // ========== CHARGEMENT DES DONNÉES (MODE UPDATE) ==========

        private void _loaddata()
        {
            try
            {
                lbltitle.Text = "Update Subscription";

                // Charger les données de la subscription existante
                // TODO: Mapper correctement selon votre ClsSubscription
                // guna2ComboBox1.SelectedIndex = ... (selon le type d'abonnement)

                txtEnd.Text = _Subscription.EndDate.ToString("dd/MM/yyyy");

                if (_Subscription.has_personal_coach)
                {
                    guna2CheckBox1.Checked = true;
                }
                else
                {
                    guna2CheckBox1.Checked = false;
                }

                UpdateCoachPrice();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur _loaddata:\n{ex.Message}", "Erreur");
            }
        }

        // ========== REMPLIR LES DONNÉES AVANT SAUVEGARDE ==========

        public void _filldata()
        {
            try
            {
                MessageBox.Show($"📝 _filldata - ClientID: {ClientID}", "Debug");

                // ✅ VÉRIFIER QUE LE ClientID EST VALIDE
                if (ClientID <= 0)
                {
                    MessageBox.Show("❌ ClientID invalide dans _filldata", "Erreur");
                    return;
                }

                // Remplir les données de l'abonnement
                _Subscription.ClientID = ClientID;
                _Subscription.StartDate = DateTime.Now;

                // Calculer la date de fin selon la durée sélectionnée
                int months = int.Parse(txtdur.Text);
                _Subscription.EndDate = DateTime.Now.AddMonths(months);

                _Subscription.createdby = 1; // TODO: Utiliser l'utilisateur connecté
                _Subscription.has_personal_coach = guna2CheckBox1.Checked;

                if (_Subscription.has_personal_coach)
                {
                    _Subscription.coqchesID = 1; // TODO: Sélectionner le coach
                }
                else
                {
                    _Subscription.coqchesID = -1;
                }

                MessageBox.Show(
                    $"📊 Données de l'abonnement:\n" +
                    $"ClientID: {_Subscription.ClientID}\n" +
                    $"StartDate: {_Subscription.StartDate}\n" +
                    $"EndDate: {_Subscription.EndDate}\n" +
                    $"Has Coach: {_Subscription.has_personal_coach}\n" +
                    $"CoachID: {_Subscription.coqchesID}",
                    "Debug Info"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur _filldata:\n{ex.Message}", "Erreur");
            }
        }

        // ========== BOUTON SAVE ==========

        public void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                // Valider les champs
                if (guna2ComboBox1.SelectedIndex < 0)
                {
                    MessageBox.Show("⚠️ Veuillez sélectionner un type d'abonnement.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Remplir les données
                _filldata();

                // Sauvegarder l'abonnement
                if (_Subscription.save())
                {
                    MessageBox.Show($"✅ Abonnement sauvegardé avec succès ! (ID: {_Subscription.id})",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Passer en mode Update
                    if (Mode == enumMode.AddNew)
                    {
                        Mode = enumMode.Update;
                        _SubscriptionID = _Subscription.id;
                        lbltitle.Text = "Update Subscription";
                    }

                    // Demander si paiement immédiat
                    DialogResult result = MessageBox.Show(
                        "Voulez-vous effectuer le paiement maintenant ?",
                        "Paiement",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        _loadpaymentPayment();
                    }
                    else
                    {
                        _loadpaymentnopayment();
                    }

                    // Sauvegarder le paiement
                    if (_payment.Save())
                    {
                        MessageBox.Show($"✅ Paiement sauvegardé ! (ID: {_payment.ID})",
                            "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Déclencher l'événement DataBack
                        DataBack?.Invoke(this, _payment.ID);
                    }
                    else
                    {
                        MessageBox.Show("❌ Échec de la sauvegarde du paiement.",
                            "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("❌ Échec de la sauvegarde de l'abonnement.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur btnsave_Click:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== GESTION DU PAIEMENT ==========

        private void _loadpaymentPayment()
        {
            _payment.PaymentDate = DateTime.Now;
            _payment.Amount = price + pricecoach;
            _payment.method = "Cash";
            _payment.status = "Paid";
            _payment.subscirptionId = _Subscription.id;
            _payment.createdBy = 1; // TODO: Utiliser l'utilisateur connecté
        }

        private void _loadpaymentnopayment()
        {
            _payment.PaymentDate = DateTime.Now;
            _payment.Amount = price + pricecoach;
            _payment.method = "En attente";
            _payment.status = "Unpaid";
            _payment.subscirptionId = _Subscription.id;
            _payment.createdBy = 1; // TODO: Utiliser l'utilisateur connecté
        }
    }
}