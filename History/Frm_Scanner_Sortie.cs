using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ATLASS_FITNESS_BUISNESS;
using ZXing;
using ZXing.Common;

namespace ATLASS_FITNESS
{
    public partial class Frm_Scanner_Sortie : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private BarcodeReader barcodeReader;
        private bool isScanning = false;
        private bool processingQRCode = false;
        private int frameCount = 0;
        private int scanInterval = 5;
        private int selectedSessionID = 63;

        public Frm_Scanner_Sortie()
        {
            InitializeComponent();
            InitializeBarcodeReader();
        }

        private void InitializeBarcodeReader()
        {
            barcodeReader = new BarcodeReader
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new DecodingOptions
                {
                    TryHarder = true,
                    PossibleFormats = new[] { BarcodeFormat.QR_CODE },
                    UseCode39ExtendedMode = false,
                    UseCode39RelaxedExtendedMode = false,
                    CharacterSet = "UTF-8"
                }
            };
        }

        private void Frm_Scanner_Sortie_Load(object sender, EventArgs e)
        {
            LoadCameras();
            LoadCurrentlyPresent();
        }

        private void LoadCameras()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    MessageBox.Show("❌ Aucune caméra détectée.\n\nConnectez une webcam et redémarrez l'application.",
                        "Aucune caméra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btndemarer.Enabled = false;
                    return;
                }

                btndemarer.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur:\n{ex.Message}", "Erreur");
            }
        }

        private void LoadCurrentlyPresent()
        {
            try
            {
                var dt = ClsAttendance.GetCurrentlyPresent();
                dataGridViewPresents.DataSource = dt;

                if (dataGridViewPresents.Columns["AttendanceID"] != null)
                    dataGridViewPresents.Columns["AttendanceID"].Visible = false;

                if (dataGridViewPresents.Columns["ClientID"] != null)
                    dataGridViewPresents.Columns["ClientID"].Visible = false;

                lbltotale.Text = $"Actuellement présents: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement:\n{ex.Message}", "Erreur");
            }
        }

        private void btndemarer_Click_1(object sender, EventArgs e)
        {
            StartCamera();
            btndemarer.Enabled = false;
          lblarreter.Enabled = true;
        }

        private void StartCamera()
        {
            try
            {
                if (videoDevices == null || videoDevices.Count == 0)
                {
                    MessageBox.Show("Aucune caméra disponible", "Erreur");
                    return;
                }

                if (videoSource != null && videoSource.IsRunning)
                {
                    StopCamera();
                    System.Threading.Thread.Sleep(500);
                }

                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

                if (videoSource.VideoCapabilities.Length > 0)
                {
                    VideoCapabilities bestCap = videoSource.VideoCapabilities[0];
                    foreach (var cap in videoSource.VideoCapabilities)
                    {
                        if (cap.FrameSize.Width == 640 && cap.FrameSize.Height == 480)
                        {
                            bestCap = cap;
                            break;
                        }
                    }
                    videoSource.VideoResolution = bestCap;
                }

                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();

                isScanning = true;
                processingQRCode = false;
                frameCount = 0;

                btndemarer.Enabled = false;
                lblarreter.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur démarrage:\n{ex.Message}", "Erreur");
            }
        }

        private void lblarreter_Click_1(object sender, EventArgs e)
        {
            StopCamera();
            btndemarer.Enabled = true;
            lblarreter.Enabled = false;
        }

        private void StopCamera()
        {
            try
            {
                isScanning = false;

                if (videoSource != null)
                {
                    if (videoSource.IsRunning)
                    {
                        videoSource.SignalToStop();
                        videoSource.WaitForStop();
                    }

                    videoSource.NewFrame -= VideoSource_NewFrame;
                    videoSource = null;
                }

                btndemarer.Enabled = true;
                lblarreter.Enabled = false;

                if (pictureBoxCamera.Image != null)
                {
                    pictureBoxCamera.Image.Dispose();
                    pictureBoxCamera.Image = null;
                }
            }
            catch { }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (!isScanning) return;

            Bitmap frame = null;

            try
            {
                frame = (Bitmap)eventArgs.Frame.Clone();
                frameCount++;

                UpdatePictureBox(frame);

                if (!processingQRCode && frameCount % scanInterval == 0)
                {
                    var result = barcodeReader.Decode(frame);

                    if (result != null)
                    {
                        processingQRCode = true;
                        string qrCode = result.Text;

                        System.Media.SystemSounds.Beep.Play();

                        if (InvokeRequired)
                        {
                            Invoke(new Action(() => ProcessQRCode(qrCode)));
                        }
                        else
                        {
                            ProcessQRCode(qrCode);
                        }
                    }
                }
            }
            catch { }
            finally
            {
                if (frame != null)
                {
                    frame.Dispose();
                }
            }
        }

        private void UpdatePictureBox(Bitmap frame)
        {
            if (pictureBoxCamera.InvokeRequired)
            {
                pictureBoxCamera.Invoke(new Action(() =>
                {
                    try
                    {
                        var oldImage = pictureBoxCamera.Image;
                        pictureBoxCamera.Image = (Bitmap)frame.Clone();
                        if (oldImage != null)
                        {
                            oldImage.Dispose();
                        }
                    }
                    catch { }
                }));
            }
        }

        private void ProcessQRCode(string qrCode)
        {
            try
            {
                isScanning = false;

                string message;
                bool success = ClsAttendance.RecordPresenceByQRCode(qrCode, selectedSessionID, out message, isCheckOut: true);

                if (success)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    MessageBox.Show(message, "Sortie Enregistrée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCurrentlyPresent();

                    // ✅ DÉCLENCHER L'ÉVÉNEMENT
                    AttendanceEvents.OnAttendanceRecorded();
                }
                else
                {
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show(message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ResumeScanning();
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show($"❌ Erreur:\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResumeScanning();
            }
        }

        private void ResumeScanning()
        {
            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += (s, e) =>
            {
                processingQRCode = false;
                isScanning = true;
                frameCount = 0;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCurrentlyPresent();
        }

        private void btntekecharger_fichier_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Sélectionner une image QR Code";
                    openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap bitmap = new Bitmap(openFileDialog.FileName);

                        if (picture_fichier.Image != null)
                        {
                            picture_fichier.Image.Dispose();
                        }

                        picture_fichier.Image = (Bitmap)bitmap.Clone();

                        var result = barcodeReader.Decode(bitmap);

                        bitmap.Dispose();

                        if (result != null)
                        {
                            System.Media.SystemSounds.Beep.Play();

                            DialogResult response = MessageBox.Show(
                                $"✅ QR Code: {result.Text}\n\nEnregistrer la SORTIE ?",
                                "QR Code Détecté",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (response == DialogResult.Yes)
                            {
                                ProcessQRCode(result.Text);
                            }
                        }
                        else
                        {
                            System.Media.SystemSounds.Hand.Play();
                            MessageBox.Show("❌ Aucun QR Code détecté.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur:\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopCamera();
            base.OnFormClosing(e);
        }
    }
}